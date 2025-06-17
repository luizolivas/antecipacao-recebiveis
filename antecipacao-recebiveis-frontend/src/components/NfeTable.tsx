import { useNavigate } from "react-router-dom";
import type { Nfe } from "../types/Nfe";
import { useEffect, useState } from "react";
import { getNfesByCompanyId } from "../services/nfeService";
import { useEmpresa } from "../context/CompanyContext";
import { formatToBrazilian } from "../utils/date";
import { getCartItems } from "../services/cartItemService";

const NfeTable = () => {
  const [nfe, setNfe] = useState<Nfe[]>([]);
  const [cartNfeIds, setCartNfeIds] = useState<number[]>([]);

  const { empresaSelecionada } = useEmpresa();

  useEffect(() => {
    const fetchData = async () => {
      if (!empresaSelecionada?.id) return;

      try {
        const [nfeData, cartItems] = await Promise.all([
          getNfesByCompanyId(Number(empresaSelecionada.id)),
          getCartItems(Number(empresaSelecionada.id)),
        ]);

        const nfesComDataFormatada = nfeData.map((nfe) => ({
          ...nfe,
          dataVencimento: nfe.dataVencimento?.split("T")[0] ?? "",
        }));

        setNfe(nfesComDataFormatada);
        setCartNfeIds(cartItems.map((item) => item.nfeId));
      } catch (err) {
        console.error("Erro ao buscar dados:", err);
      }
    };

    fetchData();
  }, [empresaSelecionada?.id]);

  const navigate = useNavigate();

  if (!empresaSelecionada) {
    return (
      <p className="text-center text-gray-500 mt-10">
        Nenhuma empresa selecionada. Vá para Empresas e selecione uma.
      </p>
    );
  }

  const handleEdit = (id: number) => {
    navigate(`/notas-fiscais/${id}`);
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
                disabled={cartNfeIds.includes(item.id)}
                className={`font-medium ${
                  cartNfeIds.includes(item.id)
                    ? "text-gray-400 cursor-not-allowed"
                    : "text-blue-600 hover:underline cursor-pointer"
                }`}
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
            <td className="text-center px-4 py-2">
              {formatToBrazilian(item.dataVencimento)}
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default NfeTable;
