import { useState } from "react";
import { Link, Outlet } from "react-router-dom";
import { FaHome, FaBuilding, FaFileInvoice } from "react-icons/fa";

const SideBar = () => {
  const [isOpen, setIsOpen] = useState(false);

  return (
    <div className="flex min-h-screen">
      {isOpen && (
        <div
          onClick={() => setIsOpen(false)}
          className="fixed inset-0 bg-black bg-opacity-50 z-10 md:hidden"
        ></div>
      )}

      <aside
        className={`bg-gray-800 text-white w-64 fixed top-0 left-0 h-full p-4 transform transition-transform duration-300 z-20
          ${isOpen ? "translate-x-0" : "-translate-x-full"} md:translate-x-0 md:static md:h-auto
        `}
      >
        <h2 className="text-xl font-bold mb-6">Menu</h2>
        <button
          onClick={() => setIsOpen(false)}
          className="md:hidden mb-4 text-white"
          aria-label="Fechar menu"
        >
          Fechar ×
        </button>
        <nav>
          <Link
            to={"/"}
            className="flex items-center gap-2 hover:bg-gray-700 rounded px-3 py-2"
            onClick={() => setIsOpen(false)}
          >
            <FaHome />
            Inicio
          </Link>
          <Link
            to={"/lista-notas-fiscais"}
            className="flex items-center gap-2 hover:bg-gray-700 rounded px-3 py-2"
            onClick={() => setIsOpen(false)}
          >
            <FaBuilding />
            Notas Fiscais
          </Link>
          <Link
            to={"/lista-empresas"}
            className="flex items-center gap-2 hover:bg-gray-700 rounded px-3 py-2"
            onClick={() => setIsOpen(false)}
          >
            <FaFileInvoice />
            Empresa
          </Link>
        </nav>
      </aside>

      <main className="flex-1 md:ml-64 p-4">
        <button
          onClick={() => setIsOpen(true)}
          className="md:hidden mb-4 bg-gray-800 text-white px-3 py-2 rounded"
          aria-label="Abrir menu"
        >
          ☰ Abrir Menu
        </button>
        <Outlet />
      </main>
    </div>
  );
};

export default SideBar;
