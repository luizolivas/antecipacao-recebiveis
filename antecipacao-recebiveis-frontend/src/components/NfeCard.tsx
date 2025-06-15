import { useState, type FunctionComponent } from "react";
import type { Nfe } from "../types/Nfe";

const NfeCard: FunctionComponent = () =>{

    const [nfe,setNfe] = useState<Nfe>({
        numero: '',
        valor: 0,
        dataVencimento: '',
    })


    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target
        setNfe((prev) => ({
            ...prev,
            [name]: name === 'valor' ? Number(value) : value
        }))
    }

    
     return (
        <div className="bg-white shadow-md rounded-lg p-4">
            <h2 className="text-lg font-semibold mb-4">Nota Fiscal</h2>

            <div className="mb-2">
                <label className="block text-sm font-medium">Número da Nota</label>
                <input
                type="text"
                value={nfe.numero}
                onChange={handleChange}
                className="w-full border border-gray-300 rounded px-2 py-1"         
                />        
            </div>

            <div className="mb-2">
                <label className="block text-sm font-medium">Valor</label>
                <input
                type="text"
                value={nfe.valor}
                onChange={handleChange}
                className="w-full border border-gray-300 rounded px-2 py-1"         
                />
            </div>

            <div className="mb-2">
                <label className="block text-sm font-medium">Data de Vencimento</label>
                <input
                    type="date"
                    name="dataVencimento"             
                    value={nfe.dataVencimento}
                    onChange={handleChange}
                    className="w-full border border-gray-300 rounded px-2 py-1"
                />
            </div>

            <button className="mt-4 bg-blue-600 text-white px-4 py-2 rounded">
                Salvar
            </button>
        </div>
    )
}

export default NfeCard