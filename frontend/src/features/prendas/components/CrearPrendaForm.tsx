// src/features/prendas/components/CrearPrendaForm.tsx
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { zodResolver } from '@hookform/resolvers/zod'
import type { Prenda } from '../types/prenda.types'
import { crearPrendaAsync, extraerMensajeErrorBackend } from '../api/prendasApi'
import { crearPrendaSchema, type CrearPrendaSchemaType } from '../schemas/prenda.schema'

interface CrearPrendaFormProps {
    onPrendaCreada: (nuevaPrenda: Prenda) => void;
    onCancelar: () => void;
}

export function CrearPrendaForm({ onPrendaCreada, onCancelar }: CrearPrendaFormProps) {
    const [errorServidor, setErrorServidor] = useState<string | null>(null)

    // Conexión con Zod (Validación en tiempo real)
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isValid, isSubmitting },
    } = useForm<CrearPrendaSchemaType>({
        resolver: zodResolver(crearPrendaSchema),
        mode: 'onChange', // Evalúa cada tecla
        defaultValues: { nombre: '', precio: 0, stock: 0 },
    })

    const procesarEnvioLimpio = async (datosValidados: CrearPrendaSchemaType) => {
        try {
            setErrorServidor(null)

            // Agregamos los campos que Zod no valida pero .NET sí requiere
            const prendaCreadaEnBd = await crearPrendaAsync({
                ...datosValidados,
                categoria: 'General',
                talla: 'Única',
                color: 'N/A'
            })

            reset()
            onPrendaCreada(prendaCreadaEnBd)
        } catch (error) {
            setErrorServidor(extraerMensajeErrorBackend(error))
        }
    }

    return (
        <form onSubmit={handleSubmit(procesarEnvioLimpio)} className="bg-slate-800/90 border border-indigo-500/40 rounded-xl p-5 mb-8 shadow-lg">
            <div className="flex items-center justify-between mb-4">
                <h2 className="text-base font-bold text-white">✨ Registrar Nueva Prenda</h2>
                <span className="text-xs font-mono text-emerald-400 bg-emerald-950/60 px-2.5 py-1 rounded border border-emerald-500/30">
                    🛡️ Esquema Zod + FluentValidation
                </span>
            </div>

            <div className="flex flex-col sm:flex-row gap-3">
                <div className="flex-1">
                    <input type="text" placeholder="Nombre..." disabled={isSubmitting} {...register('nombre', { onChange: () => { if (errorServidor) setErrorServidor(null) } })} className={`w-full bg-slate-950 border rounded-lg px-4 py-2.5 text-sm text-white ${errors.nombre ? 'border-amber-500' : 'border-slate-700'}`} />
                    {errors.nombre && <p className="text-amber-400 text-xs mt-1">⚡ {errors.nombre.message}</p>}
                </div>

                <div className="w-32">
                    <input type="number" step="0.01" placeholder="Precio..." disabled={isSubmitting} {...register('precio')} className={`w-full bg-slate-950 border rounded-lg px-4 py-2.5 text-sm text-white ${errors.precio ? 'border-amber-500' : 'border-slate-700'}`} />
                    {errors.precio && <p className="text-amber-400 text-xs mt-1">⚡ {errors.precio.message}</p>}
                </div>

                <div className="w-32">
                    <input type="number" placeholder="Stock..." disabled={isSubmitting} {...register('stock')} className={`w-full bg-slate-950 border rounded-lg px-4 py-2.5 text-sm text-white ${errors.stock ? 'border-amber-500' : 'border-slate-700'}`} />
                    {errors.stock && <p className="text-amber-400 text-xs mt-1">⚡ {errors.stock.message}</p>}
                </div>

                <div className="flex items-start gap-2">
                    <button type="submit" disabled={!isValid || isSubmitting} className="bg-indigo-600 hover:bg-indigo-500 disabled:bg-slate-800 disabled:text-slate-500 text-white font-medium text-sm px-5 py-2.5 rounded-lg cursor-pointer transition-colors">
                        {isSubmitting ? '⏳ Guardando...' : '💾 Guardar'}
                    </button>
                    <button type="button" onClick={onCancelar} disabled={isSubmitting} className="bg-slate-700 hover:bg-slate-600 text-white font-medium text-sm px-4 py-2.5 rounded-lg cursor-pointer transition-colors">
                        Cancelar
                    </button>
                </div>
            </div>

            {errorServidor && (
                <div className="mt-4 bg-red-950/60 border border-red-500/40 rounded-lg p-3 text-xs text-red-300 flex items-center gap-2">
                    <span className="text-base">🛡️</span>
                    <span><strong>Respuesta del Backend (.NET):</strong> {errorServidor}</span>
                </div>
            )}
        </form>
    )
}