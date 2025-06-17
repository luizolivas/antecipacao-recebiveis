import React from "react";
import ReactDOM from "react-dom/client";
import AppRoutes from "./routes/AppRoutes";
import "./index.css";
import { EmpresaProvider } from "./context/CompanyContext";
import { ToastContainer } from "react-toastify";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <EmpresaProvider>
      <ToastContainer position="top-right" autoClose={3000} />
      <AppRoutes />
    </EmpresaProvider>
  </React.StrictMode>
);
