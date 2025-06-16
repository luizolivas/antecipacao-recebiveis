import api from './api'
import type { Company } from '../types/Company'

export const getCompanies = async (): Promise<Company[]> => {
     console.log('consultado')

  const response = await api.get('/Company')
  return response.data
}

export const getCompanyById = async (id: number): Promise<Company> => {
    console.log('consultado')
  const response = await api.get(`/Company/${id}`)
  return response.data
}

export const createCompany = async (company: Company): Promise<Company> => {
  const response = await api.post('/Company', company)
  return response.data
}