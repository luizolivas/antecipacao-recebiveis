import type { Sector } from "./enums/Sector"

export interface Company {
  id: number
  nome: string
  cnpj: string
  faturamentoMensal: number
  setor: Sector
  creditLimit: number
}