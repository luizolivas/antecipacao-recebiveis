import api from './api'
import type { CartItem } from '../types/CartItem'

export const getCartItems = async (companyId: number): Promise<CartItem[]> => {
  const response = await api.get(`/Cart?companyId=${companyId}`)
  return response.data
}

export const addCartItem = async (item: CartItem): Promise<CartItem> => {
  const response = await api.post('/Cart', item)
  return response.data
}

export const removeCartItem = async (id: number): Promise<void> => {
  await api.delete(`/Cart/${id}`)
}

export const getTotalValorBruto = async (companyId: number): Promise<number> => {
  const response = await api.get(`/Cart/total-valor-bruto/${companyId}`);
  return response.data;
};

export const getDetailedCalculation = async (companyId: number): Promise<any> => {
  const response = await api.get(`/Cart/calculo-detalhado/${companyId}`);
  return response.data;
};

