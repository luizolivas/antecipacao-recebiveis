import NfeCard from '../components/NfeCard'
import { Link } from 'react-router-dom'



const NfeScreen = () => {

  return (
    <div className="max-w-2xl mx-auto bg-white shadow rounded-lg p-6">
      <h1 className="text-2xl font-bold mb-4">Cadastre sua Nota Fiscal</h1>
        <NfeCard></NfeCard>
        <Link to={'/lista-notas-fiscais'}>Voltar</Link>
    </div>
  )
}

export default NfeScreen
