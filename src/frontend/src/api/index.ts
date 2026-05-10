import axios from 'axios'

const api = axios.create({
  baseURL: '/api',
  headers: { 'Content-Type': 'application/json' }
})

export interface Product {
  id: number
  name: string
  description: string
  species: string
  grade: string
  dimensions: string
  pricePerCubicMeter: number
  pricePerPiece: number
  unit: string
  categoryId: number
  categoryName: string
  inStock: boolean
  imageUrl: string
  isFeatured: boolean
}

export interface Category {
  id: number
  name: string
  slug: string
  icon: string
  productCount: number
}

export interface OrderItem {
  productId: number
  quantity: number
}

export interface CreateOrderRequest {
  customerName: string
  phone: string
  email: string
  address: string
  comment: string
  deliveryRequired: boolean
  items: OrderItem[]
}

export interface OrderResponse {
  id: number
  customerName: string
  phone: string
  totalAmount: number
  createdAt: string
  status: string
}

export interface ProductsQuery {
  categoryId?: number
  search?: string
  sortBy?: string
  inStock?: boolean
}

export const productsApi = {
  getAll: (params?: ProductsQuery) => api.get<Product[]>('/products', { params }).then(r => r.data),
  getFeatured: () => api.get<Product[]>('/products/featured').then(r => r.data),
  getById: (id: number) => api.get<Product>(`/products/${id}`).then(r => r.data),
  getCategories: () => api.get<Category[]>('/products/categories').then(r => r.data),
}

export const ordersApi = {
  create: (data: CreateOrderRequest) => api.post<OrderResponse>('/orders', data).then(r => r.data),
}