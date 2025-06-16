import { useEffect, useState } from "react";
import type { Company } from "../types/Company";
import CompanyCard from "../components/CompaniesCards";
import { getCompanies } from "../services/companyService";

const ListCompanies = () => {
  const [companies, setCompanies] = useState<Company[]>([]);
  const [loading, setLoading] = useState(true);

   useEffect(() => {
    const fetchCompanies = async () => {
      try {
        const data = await getCompanies()
        setCompanies(data)
      } catch (error) {
        console.error('Erro ao buscar empresas:', error)
      } finally {
        setLoading(false)
      }
    }

    fetchCompanies()
  }, [])

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
