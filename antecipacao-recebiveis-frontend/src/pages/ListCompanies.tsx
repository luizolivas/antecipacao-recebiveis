import { useEffect, useState } from "react";
import type { Company } from "../types/Company";
import CompanyCard from "../components/CompaniesCards";

const ListCompanies = () => {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const data: Company[] = [
      {
        id: 1,
        nome: "Empresa A",
        cnpj: "00.000.000/0001-00",
        faturamentoMensal: 1000,
        setor: 0,
      },
      {
        id: 2,
        nome: "Empresa B",
        cnpj: "11.111.111/0001-11",
        faturamentoMensal: 2000,
        setor: 1,
      },
      {
        id: 3,
        nome: "Empresa C",
        cnpj: "11.111.222/0001-11",
        faturamentoMensal: 3000,
        setor: 1,
      },

      {
        id: 4,
        nome: "Empresa D",
        cnpj: "11.111.333/0001-11",
        faturamentoMensal: 4000,
        setor: 0,
      },
    ];
    setCompanies(data);
    setLoading(false);
  }, []);

  return (
    <div className="max-w-4xl mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Empresas Cadastradas</h1>

      {loading ? (
        <p className="text-gray-600">Carregando empresas...</p>
      ) : companies.length === 0 ? (
        <p className="text-gray-600">Nenhuma empresa cadastrada.</p>
      ) : (
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
          {companies.map((company) => (
            <CompanyCard key={company.id} company={company} />
          ))}
        </div>
      )}
    </div>
  );
};

export default ListCompanies;
