import { getCompanyById } from "../services/companyService";
import type { Company } from "../types/Company";

export const verifyCompany = async (empresa: Company | null): Promise<boolean> => {
  if (!empresa) return false;

  try {
    await getCompanyById(empresa.id);
    return true;
  } catch (error) {
    return false;
  }
};
