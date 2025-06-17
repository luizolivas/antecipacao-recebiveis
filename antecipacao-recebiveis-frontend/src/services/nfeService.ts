import api from './api'
import type { Nfe } from '../types/Nfe'

export const getAllNfes = async (): Promise<Nfe[]> => {
  const response = await api.get('/nfe')
  return response.data
}

export const getNfeById = async (id: number): Promise<Nfe> => {
  const response = await api.get(`/nfe/${id}`)
  return response.data
}

export const getNfesByCompanyId = async (companyId: number): Promise<Nfe[]> => {
  const response = await api.get(`/nfe/company/${companyId}`)
  return response.data
}

export const createNfe = async (nfe: Omit<Nfe, 'id'>): Promise<Nfe> => {
  const response = await api.post('/nfe', nfe)
  return response.data
}

export const updateNfe = async (id: number, nfe: Nfe): Promise<Nfe> => {
  const response = await api.put(`/nfe/${id}`, nfe)
  return response.data
}

export const deleteNfe = async (id: number): Promise<void> => {
  await api.delete(`/nfe/${id}`)
}

export const getTotalLiquido = async (companyId: number): Promise<number> => {
  const response = await api.get(`/nfe/total-liquido/${companyId}`);
  return response.data;
};
