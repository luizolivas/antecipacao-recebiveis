import { useNavigate } from "react-router-dom";

const nfeList = [
  {
    id: 1,
    numero: "001",
    valor: 1500.75,
    dataEmissao: "2025-06-01",
    cnpjEmpresa: "12.345.678/0001-99",
  },
  {
    id: 2,
    numero: "002",
    valor: 2000.5,
    dataEmissao: "2025-06-05",
    cnpjEmpresa: "12.345.678/0001-99",
  },
  {
    id: 3,
    numero: "003",
    valor: 100.0,
    dataEmissao: "2025-06-07",
    cnpjEmpresa: "12.345.678/0001-99",
  },
];

const NfeTable = () => {

    const navigate = useNavigate();

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
          <th className="text-center px-4 py-2">CNPJ</th>
        </tr>
      </thead>
      <tbody>
        {nfeList.map((item) => (
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
            <td className="text-center px-4 py-2">{item.dataEmissao}</td>
            <td className="text-center px-4 py-2">{item.cnpjEmpresa}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default NfeTable;
