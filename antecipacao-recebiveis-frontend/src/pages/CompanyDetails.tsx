import { Link } from "react-router-dom";
import CompanyCardDetails from "../components/CompanyCardDetails";

const CompanyDetails = () => {
  return (
    <div className="max-w-2xl mx-auto bg-white shadow rounded-lg p-6 flex flex-col gap-6">
      <CompanyCardDetails />

      <Link
        to="/lista-empresas"
        className="self-start bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition cursor-pointer"
      >
        Voltar
      </Link>
    </div>
  );
};

export default CompanyDetails;
