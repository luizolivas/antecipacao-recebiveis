import { useEffect, useState, type FunctionComponent } from "react";
import type { Company } from "../types/Company";
import { Sector } from "../types/enums/Sector";
import { useNavigate, useParams } from "react-router-dom";
import {
  createCompany,
  getCompanyById,
  updateCompany,
} from "../services/companyService";
import { toast } from "react-toastify";
import { useEmpresa } from "../context/CompanyContext";
import Cleave from "cleave.js/react";

const CompanyCardDetails: FunctionComponent = () => {
  const { id } = useParams();
  const [company, setCompany] = useState<Company>();
  const navigate = useNavigate();
  const { setEmpresaSelecionada } = useEmpresa();
  useEffect(() => {
    if (id === "nova") {
      setCompany({
        id: 0,
        nome: "",
        cnpj: "",
        faturamentoMensal: 0,
        setor: 0,
        creditLimit: 0,
      });
      return;
    }
    const fetchCompany = async () => {
        const companyData = await getCompanyById(Number(id));
        setCompany(companyData);
    };

    fetchCompany();
  }, [id]);

  const handleSave = async () => {
    if (!company) return;

    try {
      if (id && id !== "nova") {
        await updateCompany(Number(id), company);
        toast.success("Empresa atualizada com sucesso!");
        setEmpresaSelecionada(await getCompanyById(Number(id)));
        navigate("/lista-empresas");
      } else {
        await createCompany(company);
        toast.success("Empresa criada com sucesso!");
        navigate("/lista-empresas");
      }
    } catch (error) {
      toast.error("Erro ao salvar empresa.");
    }
  };

  const handleBack = () => {
    navigate(`/lista-empresas`);
  };

  if (!company) return <p>Carregando dados da empresa...</p>;

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) => {
    const { name, value } = e.target;
    setCompany((prev) => {
      if (!prev) return prev;

      return {
        ...prev,
        [name]: ["faturamentoMensal", "setor"].includes(name)
          ? Number(value)
          : value,
      };
    });
  };

  return (
    <div className="bg-white shadow-md rounded-lg p-4">
      <h2 className="text-lg font-semibold mb-4">Dados da Empresa</h2>

      <div className="mb-2">
        <label className="block text-sm font-medium">Nome</label>
        <input
          type="text"
          name="nome"
          value={company.nome}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <div className="mb-2">
        <label className="block text-sm font-medium">CNPJ</label>
        <Cleave
          options={{
            delimiters: [".", ".", "/", "-"],
            blocks: [2, 3, 3, 4, 2],
            numericOnly: true,
          }}
          name="cnpj"
          value={company.cnpj}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <div className="mb-2">
        <label className="block text-sm font-medium">Faturamento Mensal</label>
        <Cleave
          name="faturamentoMensal"
          value={company.faturamentoMensal}
          onChange={(e) =>
            handleChange({
              ...e,
              target: {
                ...e.target,
                name: "faturamentoMensal",
                value: e.target.rawValue, 
              },
            })
          }
          options={{
            numeral: true,
            numeralThousandsGroupStyle: "thousand",
            numeralDecimalMark: ",",
            delimiter: ".",
            prefix: "R$ ",
            rawValueTrimPrefix: true,
          }}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <div className="mb-2">
        <label className="block text-sm font-medium">Setor</label>
        <select
          name="setor"
          value={company.setor}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded px-2 py-1"
        >
          {Object.entries(Sector).map(([key, label]) => (
            <option key={key} value={key}>
              {label}
            </option>
          ))}
        </select>
      </div>

      <div className="mt-4 flex gap-2">
        <button
          className="bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-400 text-white px-4 py-2 rounded transition cursor-pointer"
          onClick={handleSave}
        >
          Salvar
        </button>
        <button
          className="bg-gray-300 hover:bg-gray-400 focus:outline-none focus:ring-2 focus:ring-gray-400 text-black px-4 py-2 rounded transition cursor-pointer"
          onClick={handleBack}
        >
          Voltar
        </button>
      </div>
    </div>
  );
};

export default CompanyCardDetails;
