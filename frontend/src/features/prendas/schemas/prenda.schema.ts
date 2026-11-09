// src/features/prendas/schemas/prenda.schema.ts
import { z } from 'zod'

export const crearPrendaSchema = z.object({
    nombre: z
        .string()
        .trim()
        .min(1, 'El nombre de la prenda no puede estar vacío.')
        .min(3, 'El nombre debe tener al menos 3 caracteres.')
        .max(100, 'El nombre no puede superar los 100 caracteres.'),
        
    precio: z
        .coerce.number({ invalid_type_error: "El precio debe ser un número" })
        .min(0.01, 'El precio debe ser mayor a 0.'),
        
    stock: z
        .coerce.number({ invalid_type_error: "El stock debe ser un número" })
        .min(0, 'El stock no puede ser negativo.'),
})

export type CrearPrendaSchemaType = z.infer<typeof crearPrendaSchema>