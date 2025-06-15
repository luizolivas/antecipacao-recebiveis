import EnterpriseCard from '../components/EntrepriseCard'
import { Link } from 'react-router-dom'



function RegisterEntreprise() {

  return (
    <div className="max-w-2xl mx-auto bg-white shadow rounded-lg p-6">
      <h1 className="text-2xl font-bold mb-4">Cadastre sua Empresa</h1>
        <EnterpriseCard></EnterpriseCard>
        <Link to={'/'}>Voltar</Link>
    </div>
  )
}

export default RegisterEntreprise
