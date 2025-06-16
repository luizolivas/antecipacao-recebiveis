import React from "react";
import ReactDOM from "react-dom/client";
import AppRoutes from "./routes/AppRoutes";
import "./index.css";
import { EmpresaProvider } from "./context/CompanyContext";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <EmpresaProvider>
      <AppRoutes />
    </EmpresaProvider>
  </React.StrictMode>
);
