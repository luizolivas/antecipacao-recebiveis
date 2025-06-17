import { useEffect, useState, type FunctionComponent } from "react";
import type { Nfe } from "../types/Nfe";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { createNfe, getNfeById, updateNfe } from "../services/nfeService";
import { useEmpresa } from "../context/CompanyContext";
import Cleave from "cleave.js/react";

const NfeCardDetails: FunctionComponent = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { empresaSelecionada } = useEmpresa();
  const [nfe, setNfe] = useState<Nfe>();

  useEffect(() => {
    if (id === "nova") {
      setNfe({
        id: 0,
        numero: "",
        valor: 0,
        dataVencimento: "",
        companyId: 0,
      });
      return;
    }

    const fetchNfe = async () => {
      try {
        const data = await getNfeById(Number(id));

        const dataFormatada = data.dataVencimento.split("T")[0];

        setNfe({
          ...data,
          dataVencimento: dataFormatada,
        });
      } catch (err) {
        console.error("Erro ao buscar nota fiscal:", err);
      }
    };

    fetchNfe();
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNfe((prev) =>
      prev
        ? {
            ...prev,
            [name]:
              name === "valor"
                ? value === ""
                  ? ""
                  : parseFloat(value)
                : value,
          }
        : prev
    );
  };

  const handleSave = async () => {
    if (!nfe) return;
    if (!empresaSelecionada) {
      toast.error("Selecione uma empresa antes de criar a NF-e.");
      return;
    }

    if (
      !nfe.numero.trim() ||
      nfe.valor === 0 || 
      !nfe.dataVencimento.trim()
    ) {
      toast.error("Preencha todos os campos antes de salvar.");
      return;
    }
    try {
      if (id && id !== "nova") {
        await updateNfe(Number(id), nfe);
        toast.success("NF-e atualizada com sucesso!");
      } else {
        await createNfe({
          ...nfe,
          companyId: empresaSelecionada.id,
        });
        toast.success("NF-e criada com sucesso!");
      }

      navigate("/lista-notas-fiscais");
    } catch (error) {
      console.error("Erro ao salvar NF-e:", error);
      toast.error("Erro ao salvar nota fiscal.");
    }
  };

  if (!nfe) return <p>Carregando dados da NF-e...</p>;

  return (
    <div className="bg-white shadow-md rounded-lg p-4">
      <h2 className="text-lg font-semibold mb-4">Nota Fiscal</h2>

      <div className="mb-2">
        <label className="block text-sm font-medium">Número da Nota</label>
        <input
          type="text"
          name="numero"
          value={nfe.numero}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <div className="mb-2">
        <label className="block text-sm font-medium">Valor</label>
        <Cleave
          name="valor"
          value={nfe.valor}
          options={{
            numeral: true,
            numeralThousandsGroupStyle: "thousand",
            numeralDecimalMark: ",",
            delimiter: ".",
            prefix: "R$ ",
            rawValueTrimPrefix: true,
          }}
          onChange={(e) => {
            handleChange({
              ...e,
              target: {
                ...e.target,
                name: "valor",
                value: e.target.rawValue,
              },
            });
          }}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <div className="mb-2">
        <label className="block text-sm font-medium">Data de Vencimento</label>
        <input
          type="date"
          name="dataVencimento"
          value={nfe.dataVencimento}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <button
        className="mt-4 bg-blue-600 text-white px-4 py-2 rounded hover:to-blue-800 cursor-pointer"
        onClick={handleSave}
      >
        {id == "nova" ? "Salvar" : "Atualizar"}
      </button>
    </div>
  );
};

export default NfeCardDetails;
