import { FaIdCard, FaMoneyBillWave, FaIndustry } from "react-icons/fa";
import { Sector } from "../types/enums/Sector";
import type { Company } from "../types/Company";
import { useNavigate } from "react-router-dom";
import { useEmpresa } from "../context/CompanyContext";

interface Props {
  company: Company;
}

const CompaniesCards = ({ company }: Props) => {
  const navigate = useNavigate();
  const { empresaSelecionada, setEmpresaSelecionada } = useEmpresa();

  const handleEdit = () => {
    navigate(`/empresa-detalhes/${company.id}/editar`);
  };

  const handleSelect = () => {
    setEmpresaSelecionada(company);
  };

  const isSelecionada = empresaSelecionada?.id === company.id;

  return (
    <div className={`bg-white p-6 rounded-xl shadow max-w-full border-2 ${isSelecionada ? "border-green-500" : "border-transparent"}`}>
      <div className="flex justify-between items-center mb-4">
        <h2 className="text-xl font-bold text-gray-800">{company.nome}</h2>
        {isSelecionada && (
          <span className="bg-green-500 text-white text-xs font-semibold px-2 py-1 rounded">
            Selecionada
          </span>
        )}
      </div>

      <div className="space-y-3 text-base text-gray-700">
        <div className="flex items-center gap-2">
          <FaIdCard className="text-blue-600" />
          <span className="font-semibold">CNPJ:</span> {company.cnpj}
        </div>

        <div className="flex items-center gap-2">
          <FaMoneyBillWave className="text-blue-600" />
          <span className="font-semibold">Faturamento:</span> R${" "}
          {company.faturamentoMensal.toLocaleString("pt-BR")}
        </div>

        <div className="flex items-center gap-2">
          <FaIndustry className="text-blue-600" />
          <span className="font-semibold">Setor:</span> {Sector[company.setor]}
        </div>

        <div className="mt-4 flex gap-2">
          <button
            onClick={handleEdit}
            className="bg-blue-500 text-white px-3 py-1 rounded cursor-pointer"
          >
            Editar
          </button>
          <button
            onClick={handleSelect}
            className="bg-green-500 text-white px-3 py-1 rounded cursor-pointer"
          >
            Utilizar
          </button>
        </div>
      </div>
    </div>
  );
};

export default CompaniesCards;
