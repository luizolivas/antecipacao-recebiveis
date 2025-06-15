import { useState, type FunctionComponent } from "react";
import type { Company } from "../types/Company";
import { Sector } from "../types/enums/Sector";

const CompanyCardDetails: FunctionComponent = () =>{

    const [company, setCompany] = useState<Company>({
        id: 1,
        nome: '',
        cnpj: '',
        faturamentoMensal: 0,
        setor: 0
    })


    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target
        setCompany((prev) => ({
            ...prev,
            [name]: name === 'valor' ? Number(value) : value
        }))
    }

    
     return ( 
        <div className="bg-white shadow-md rounded-lg p-4">
            <h2 className="text-lg font-semibold mb-4">Dados da Empresa</h2>

            <div className="mb-2">
                <label className="block text-sm font-medium">Nome</label>
                <input
                type="text"
                value={company.nome}
                onChange={handleChange}
                className="w-full border border-gray-300 rounded px-2 py-1"         
                />        
            </div>

            <div className="mb-2">
                <label className="block text-sm font-medium">CNPJ</label>
                <input
                type="text"
                value={company.cnpj}
                onChange={handleChange}
                className="w-full border border-gray-300 rounded px-2 py-1"         
                />
            </div>

            <div className="mb-2">
                <label className="block text-sm font-medium">Faturamento Mensal</label>
                <input
                    type="number"
                    name="dataVencimento"             
                    value={company.faturamentoMensal}
                    onChange={handleChange}
                    className="w-full border border-gray-300 rounded px-2 py-1"
                />
            </div>

            <div className="mb-2">
                <label className="block text-sm font-medium">Setor</label>
                <select
                name="setor"
                value={company.setor}
                onChange={handleChange}
                className="w-full border border-gray-300 rounded px-2 py-1"
                >
                {Object.entries(Sector).map(([key,label])=>
                    <option key={key} value={key}>
                        {label}
                    </option>
                )}
                </select>
            </div>

            <button className="mt-4 bg-blue-600 text-white px-4 py-2 rounded">
                Salvar
            </button>
        </div>
    )
}

export default CompanyCardDetails