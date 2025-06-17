import { Link } from "react-router-dom";
import NfeTable from "../components/NfeTable";
import { useEmpresa } from "../context/CompanyContext";

const ListNfe = () => {
  const { empresaSelecionada } = useEmpresa();

  const isEmpresaSelecionada = !!empresaSelecionada;

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Notas Fiscais</h1>
      <div className="flex justify-end mb-4">
        {isEmpresaSelecionada ? (
          <Link
            to="/notas-fiscais/nova"
            className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700"
          >
            + NFE
          </Link>
        ) : (
          <button
            disabled
            className="bg-gray-400 text-white px-4 py-2 rounded cursor-not-allowed"
            title="Selecione uma empresa para cadastrar uma nota fiscal"
          >
            + NFE
          </button>
        )}
      </div>
      <NfeTable />
    </div>
  );
};

export default ListNfe;
