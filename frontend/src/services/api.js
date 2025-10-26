import axios from 'axios'

const API_BASE_URL = 'http://localhost:5000/api'

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

export default api
