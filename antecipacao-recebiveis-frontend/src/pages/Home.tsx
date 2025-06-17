import AnticipationCart from "../components/AnticipationCart"
import { useEmpresa } from "../context/CompanyContext"


const Home = () => {
  const { empresaSelecionada } = useEmpresa()

  if (!empresaSelecionada) {
    return <p className="text-center text-gray-500 mt-10">Nenhuma empresa selecionada. Vá para Empresas e selecione uma.</p>
  }

  return (
    <div className="p-6">
      <h1 className="text-2xl font-bold mb-4">Antecipação de Recebíveis</h1>
      <p className="text-lg text-gray-700 mb-6">
        Empresa: {empresaSelecionada.nome} (CNPJ: {empresaSelecionada.cnpj})
      </p>
      <AnticipationCart companyId={empresaSelecionada.id} />
    </div>
  )
}

export default Home
