import { useEffect, useState, type FunctionComponent } from "react";
import type { Company } from "../types/Company";
import { Sector } from "../types/enums/Sector";
import { useParams } from "react-router-dom";
import { getCompanyById } from "../services/companyService";

const CompanyCardDetails: FunctionComponent = () => {
  const { id } = useParams();
  const [company, setCompany] = useState<Company>();

  useEffect(() => {
    const fetchCompany = async () => {
      try {
        const companyData = await getCompanyById(Number(id));
        setCompany(companyData);
      } catch (err) {
        console.error("Erro ao buscar empresa:", err);
      }
    };

    fetchCompany();
  }, [id]);

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
          value={company.nome}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <div className="mb-2">
        <label className="block text-sm font-medium">CNPJ</label>
        <input
          type="text"
          value={company.cnpj}
          onChange={handleChange}
          className="w-full border border-gray-300 rounded px-2 py-1"
        />
      </div>

      <div className="mb-2">
        <label className="block text-sm font-medium">Faturamento Mensal</label>
        <input
          type="number"
          name="faturamentoMensal"
          value={company.faturamentoMensal}
          onChange={handleChange}
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

      <button className="mt-4 bg-blue-600 text-white px-4 py-2 rounded">
        Salvar
      </button>
    </div>
  );
};

export default CompanyCardDetails;
