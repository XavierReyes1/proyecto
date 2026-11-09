// src/features/prendas/api/prendasApi.ts
import axios from 'axios'
import type { Prenda, CrearPrendaDto, FluentValidationError } from '../types/prenda.types'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL

if (!API_BASE_URL) {
    console.error('🚨 ERROR CRÍTICO: No se detectó VITE_API_BASE_URL en frontend/.env')
}

export const apiClient = axios.create({
    baseURL: API_BASE_URL,
    headers: { 'Content-Type': 'application/json' },
})

// 1. Endpoint GET
export const obtenerPrendasAsync = async (): Promise<Prenda[]> => {
    const respuesta = await apiClient.get<Prenda[]>('/prendas')
    if (!Array.isArray(respuesta.data)) {
        throw new Error('La respuesta recibida no es un arreglo JSON válido.')
    }
    return respuesta.data
}

// 2. Endpoint POST
export const crearPrendaAsync = async (nuevaPrenda: CrearPrendaDto): Promise<Prenda> => {
    const respuesta = await apiClient.post<Prenda>('/prendas', nuevaPrenda)
    return respuesta.data
}

// 3. Extractor de Errores (idéntico al de la clase)
export const extraerMensajeErrorBackend = (error: unknown): string => {
    if (axios.isAxiosError(error)) {
        if (!error.response) {
            return 'Sin respuesta del servidor (.NET apagado o bloqueo de seguridad CORS).'
        }

        const status = error.response.status
        const data = error.response.data

        if (Array.isArray(data)) {
            const erroresFluent = (data as FluentValidationError[])
                .map((item) => item?.errorMessage)
                .filter((msg): msg is string => typeof msg === 'string' && msg.trim().length > 0)

            if (erroresFluent.length > 0) {
                return `[HTTP ${status}] ${erroresFluent.join(' | ')}`
            }
        }

        if (typeof data === 'string' && data.trim().length > 0) {
            return `[HTTP ${status}] ${data}`
        }

        if (data && typeof data === 'object') {
            const obj = data as Record<string, unknown>
            if ('errors' in obj && obj.errors && typeof obj.errors === 'object') {
                const listaErrores = Object.values(obj.errors as Record<string, string[]>).flat()
                if (listaErrores.length > 0) return `[HTTP ${status}] ${listaErrores.join(' | ')}`
            }
            const mensajeExplicito = obj.error || obj.mensaje || obj.message || obj.detail || obj.title
            if (typeof mensajeExplicito === 'string' && mensajeExplicito.trim().length > 0) {
                return `[HTTP ${status}] ${mensajeExplicito}`
            }
        }

        return `[HTTP ${status}] El servidor .NET rechazó la operación.`
    }
    return error instanceof Error ? error.message : 'Error inesperado en la aplicación.'
}

export const eliminarPrendaAsync = async (id: number): Promise<void> => {
    await apiClient.delete(`/prendas/${id}`)
}