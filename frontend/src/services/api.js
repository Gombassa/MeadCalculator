import axios from 'axios'

// Use relative path for API - will be proxied by nginx in production
// In development, nginx proxy forwards /api/ to backend
const API_BASE_URL = '/api'

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})

export const ingredientService = {
  getAll: () => api.get('/ingredients'),
  getById: (id) => api.get(`/ingredients/${id}`),
  getByType: (type) => api.get(`/ingredients/by-type/${type}`),
}

export const calculatorService = {
  calculate: (request) => api.post('/calculator/calculate', request),
}

export const nutrientService = {
  calculateYAN: (request) => api.post('/nutrient/calculate-yan', request),
  generateSNA: (request) => api.post('/nutrient/generate-sna', request),
  getAdditives: () => api.get('/nutrient/additives'),
}

export default api
