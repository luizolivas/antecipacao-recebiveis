import type { Sector } from "./enums/Sector"

export interface Enterprise {
  nome: string
  cnpj: string
  faturamentoMensal: number
  setor: Sector
}