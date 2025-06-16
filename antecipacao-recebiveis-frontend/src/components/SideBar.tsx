import { useEffect, useState } from "react";
import { Link, Outlet } from "react-router-dom";
import { FaHome, FaBuilding, FaFileInvoice } from "react-icons/fa";
import CompanySelector from "./CompanySelector";
import type { Company } from "../types/Company";
import { getCompanies } from "../services/companyService";

const SideBar = () => {
  const [isOpen, setIsOpen] = useState(true);
  const [companies, setCompanies] = useState<Company[]>([]);

  useEffect(() => {
    async function fetchEmpresas() {
       try {
         const data = await getCompanies()
         setCompanies(data)
       } catch (error) {
         console.error('Erro ao buscar empresas:', error)
       }
    }
    fetchEmpresas();
  }, []);

  return (
    <div className="flex min-h-screen">
      <aside
        className={`bg-gray-800 text-white w-64 fixed md:static top-0 left-0 p-4 transform transition-transform duration-300 ${
          isOpen ? "translate-x-0" : "-translate-x-full"
        }`}
      >
        <h2 className="text-xl font-bold mb-6">Menu</h2>
        <CompanySelector empresas={companies} />
        <button onClick={() => setIsOpen(false)} className="md:hidden mb-4">
          Fechar ×
        </button>
        <nav>
          <Link
            to={"/"}
            className="flex items-center gap-2 hover:bg-gray-700 rounded px-3 py-2"
          >
            <FaHome />
            Inicio
          </Link>
          <Link
            to={"/lista-notas-fiscais"}
            className="flex items-center gap-2 hover:bg-gray-700 rounded px-3 py-2"
          >
            <FaBuilding />
            Notas Fiscais
          </Link>
          <Link
            to={"/lista-empresas"}
            className="flex items-center gap-2 hover:bg-gray-700 rounded px-3 py-2"
          >
            <FaFileInvoice />
            Empresa
          </Link>
        </nav>
      </aside>
      <main className="flex-1">
        <button onClick={() => setIsOpen(true)} className="md:hidden mb-4">
          Abrir Menu ☰
        </button>
        <Outlet></Outlet>
      </main>
    </div>
  );
};

export default SideBar;
