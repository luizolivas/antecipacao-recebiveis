import React from "react";
import { useEmpresa } from "../context/CompanyContext";
import type { Company } from "../types/Company";

interface Props {
  empresas: Company[];
}

const CompanySelector: React.FC<Props> = ({ empresas }) => {
  const { empresaSelecionada, setEmpresaSelecionada } = useEmpresa();

const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
  const empresaId = Number(e.target.value);
  const empresa = empresas.find((empresa) => empresa.id === empresaId) || null;
  setEmpresaSelecionada(empresa);
};

  return (
    <div>
      <label htmlFor="company-select">Selecione a empresa:</label>
      <select id="company-select" value={empresaSelecionada?.id || ""} onChange={handleChange}>
        <option value="">-- Escolha --</option>
        {empresas.map((empresa) => (
          <option key={empresa.id} value={empresa.id}>
            {empresa.nome}
          </option>
        ))}
      </select>
    </div>
  );
};

export default CompanySelector;
