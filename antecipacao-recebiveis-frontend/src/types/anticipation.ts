export interface AnticipationFormData {
  amount: number
  installments: number
  mdr: number
}

export interface AnticipationResult {
  [days: number]: number
}
