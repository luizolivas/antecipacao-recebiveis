import NfeCard from '../components/NfeCardDetails'



const NfeScreen = () => {

  return (
    <div className="max-w-2xl mx-auto bg-white shadow rounded-lg p-6">
      <h1 className="text-2xl font-bold mb-4">Cadastre sua Nota Fiscal</h1>
        <NfeCard></NfeCard>
    </div>
  )
}

export default NfeScreen
