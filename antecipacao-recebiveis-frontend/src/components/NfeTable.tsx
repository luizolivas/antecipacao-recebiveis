import { useNavigate } from "react-router-dom";
import type { Nfe } from "../types/Nfe";
import { useEffect, useState } from "react";
import { getNfesByCompanyId } from "../services/nfeService";
import { useEmpresa } from "../context/CompanyContext";

const NfeTable = () => {
  const [nfe, setNfe] = useState<Nfe[]>([]);
  const { empresaSelecionada } = useEmpresa();

  useEffect(() => {
    const fetchCompany = async () => {
      if (!empresaSelecionada?.id) return <p>Carregando empresa...</p>; 

      try {
        const nfeData = await getNfesByCompanyId(Number(empresaSelecionada.id));
        setNfe(nfeData);
      } catch (err) {
        console.error("Erro ao buscar empresa:", err);
      }
    };

    fetchCompany();
  }, [empresaSelecionada?.id]);

  const navigate = useNavigate();


  if (!empresaSelecionada) {
    return <p className="text-center text-gray-500 mt-10">Nenhuma empresa selecionada. Vá para Empresas e selecione uma.</p>
  }

  const handleEdit = (id: number) => {
    navigate(`/notas-fiscais/${id}/editar`);
  };
  return (
    <table className="min-w-full bg-white rounded shadow border-t border-gray-200 text-sm">
      <thead>
        <tr>
          <th className="text-center px-4 py-2">Número</th>
          <th className="text-center px-4 py-2">Valor</th>
          <th className="text-center px-4 py-2">Data Emissão</th>
        </tr>
      </thead>
      <tbody>
        {nfe.map((item) => (
          <tr key={item.id}>
            <td className="text-center px-4 py-2">
              <button
                onClick={() => handleEdit(item.id)}
                className="text-blue-600 font-medium hover:underline cursor-pointer"
              >
                {item.numero}
              </button>
            </td>
            <td className="text-center px-4 py-2">
              {item.valor.toLocaleString("pt-BR", {
                style: "currency",
                currency: "BRL",
              })}
            </td>
            <td className="text-center px-4 py-2">{item.dataVencimento}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default NfeTable;
