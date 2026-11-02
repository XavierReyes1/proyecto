import type { Prenda } from '../types/prenda.types'

// Función asíncrona que retorna una Promesa con el arreglo de prendas
export const obtenerPrendasAsync = (): Promise<Prenda[]> => {
    return new Promise((resolve) => {
        setTimeout(() => {
            const datosCatalogo: Prenda[] = [
                { id: 1, nombre: 'Camisa Oxford', categoria: 'Camisa', precio: 25.99, stock: 10 },
                { id: 2, nombre: 'Jeans Slim Fit', categoria: 'Pantalón', precio: 39.50, stock: 15 },
                { id: 3, nombre: 'Vestido Floral', categoria: 'Vestido', precio: 45.00, stock: 5 },
                { id: 4, nombre: 'Chaqueta de Cuero', categoria: 'Abrigo', precio: 89.99, stock: 8 },
                { id: 5, nombre: 'Zapatillas Deportivas', categoria: 'Calzado', precio: 59.99, stock: 12 },
                { id: 6, nombre: 'Bufanda de Lana', categoria: 'Accesorio', precio: 15.50, stock: 20 },
            ]
            resolve(datosCatalogo)
        }, 1000)
    })
}