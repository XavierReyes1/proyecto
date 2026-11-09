// src/features/prendas/types/prenda.types.ts

// 1. Contrato de Salida (Lo que viene de PostgreSQL)
export interface Prenda {
    id: number;
    nombre: string;
    categoria: string;
    precio: number;
    stock: number;
}

// 2. Contrato de Entrada (Lo que enviamos por POST)
// Basado en el CreatePrendaRequestDto que hicimos en la Clase 8
export interface CrearPrendaDto {
    nombre: string;
    categoria: string;
    talla: string;
    color: string;
    precio: number;
    stock: number;
}

// 3. Contrato de Error de FluentValidation (.NET ValidationFailure)
export interface FluentValidationError {
    propertyName: string;
    errorMessage: string;
    attemptedValue?: unknown;
    errorCode?: string;
}