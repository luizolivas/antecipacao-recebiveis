import { createContext, useState, type ReactNode, useContext } from "react";
import type { Company } from "../types/Company";


interface EmpresaContextType {
  empresaSelecionada: Company | null;
  setEmpresaSelecionada: (empresa: Company | null) => void;
}

const EmpresaContext = createContext<EmpresaContextType | undefined>(undefined);

export const EmpresaProvider = ({ children }: { children: ReactNode }) => {
  const [empresaSelecionada, setEmpresaSelecionada] = useState<Company | null>(null);

  return (
    <EmpresaContext.Provider value={{ empresaSelecionada, setEmpresaSelecionada }}>
      {children}
    </EmpresaContext.Provider>
  );
};

export const useEmpresa = () => {
  const context = useContext(EmpresaContext);
  if (!context) {
    throw new Error("useEmpresa deve ser usado dentro de EmpresaProvider");
  }
  return context;
};
