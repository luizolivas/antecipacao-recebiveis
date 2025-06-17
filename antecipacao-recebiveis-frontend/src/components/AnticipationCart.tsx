import { useEffect, useState } from "react";
import type { Nfe } from "../types/Nfe";
import { getNfesByCompanyId, getTotalLiquido } from "../services/nfeService";
import {
  getCartItems,
  addCartItem,
  removeCartItem,
} from "../services/cartItemService";

interface Props {
  companyId: number;
}

const AnticipationCart = ({ companyId }: Props) => {
  const [notas, setNotas] = useState<Nfe[]>([]);
  const [carrinho, setCarrinho] = useState<Nfe[]>([]);
  const [totalLiquido, setTotalLiquido] = useState(0);

  useEffect(() => {
    loadNotas();
    loadCarrinho();
  }, []);

  const loadNotas = async () => {
    const data = await getNfesByCompanyId(companyId);
    setNotas(data);
  };

  const loadCarrinho = async () => {
    const items = await getCartItems(companyId);
    const nfeIds = items.map((item) => item.nfeId);
    const todasNotas = await getNfesByCompanyId(companyId);
    const selecionadas = todasNotas.filter((n) => nfeIds.includes(n.id));
    setCarrinho(selecionadas);
    calcularTotalLiquido(companyId);
  };

  const calcularTotalLiquido = async (companyId: number) => {
    const total = await getTotalLiquido(companyId);
    setTotalLiquido(total);
  };

  const handleAdicionar = async (nfe: Nfe) => {
    await addCartItem({ id: 0, companyId: companyId, nfeId: nfe.id });
    await loadCarrinho();
  };

  const handleRemover = async (nfeId: number) => {
    const item = await getCartItems(companyId);
    const encontrado = item.find((i) => i.nfeId === nfeId);
    if (encontrado) {
      await removeCartItem(encontrado.id);
      await loadCarrinho();
    }
  };

  return (
    <div className="grid md:grid-cols-2 gap-6 p-4">
      <div>
        <h2 className="text-xl font-bold mb-2">Notas Fiscais da Empresa</h2>
        <ul className="space-y-2">
          {notas.map((nfe) => (
            <li
              key={nfe.id}
              className="border rounded p-2 flex justify-between items-center"
            >
              <div>
                <p className="font-medium">Nota {nfe.numero}</p>
                <p>Valor: R$ {nfe.valor.toFixed(2)}</p>
                <p>
                  Vencimento:{" "}
                  {new Date(nfe.dataVencimento).toLocaleDateString()}
                </p>
              </div>
              <button
                onClick={() => handleAdicionar(nfe)}
                disabled={carrinho.some((item) => item.id === nfe.id)}
                className={`px-3 py-1 rounded text-white 
    ${
      carrinho.some((item) => item.id === nfe.id)
        ? "bg-gray-400 cursor-not-allowed"
        : "bg-green-600 hover:bg-green-800 cursor-pointer"
    }`}
              >
                {carrinho.some((item) => item.id === nfe.id)
                  ? "Adicionada"
                  : "Adicionar"}
              </button>
            </li>
          ))}
        </ul>
      </div>

      <div>
        <h2 className="text-xl font-bold mb-2">Carrinho de Antecipação</h2>
        {carrinho.length === 0 ? (
          <p className="text-gray-600">Nenhuma nota adicionada.</p>
        ) : (
          <ul className="space-y-2">
            {carrinho.map((nfe) => (
              <li
                key={nfe.id}
                className="border rounded p-2 flex justify-between items-center"
              >
                <div>
                  <p className="font-medium">Nota {nfe.numero}</p>
                  <p>Valor Bruto: R$ {nfe.valor.toFixed(2)}</p>
                  <p>
                    Vencimento:{" "}
                    {new Date(nfe.dataVencimento).toLocaleDateString()}
                  </p>
                </div>
                <button
                  onClick={() => handleRemover(nfe.id)}
                  className="bg-red-600 text-white px-3 py-1 rounded hover:bg-red-800 cursor-pointer"
                >
                  Remover
                </button>
              </li>
            ))}
          </ul>
        )}

        <div className="mt-4 text-right">
          <p className="text-lg font-bold">
            Valor Líquido Total: R$ {totalLiquido.toFixed(2)}
          </p>
        </div>
      </div>
    </div>
  );
};

export default AnticipationCart;
