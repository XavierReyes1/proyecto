// src/features/prendas/components/CatalogoPrendasView.tsx
import { useState, useEffect } from 'react'
import type { Prenda } from '../types/prenda.types'
import { obtenerPrendasAsync, extraerMensajeErrorBackend, eliminarPrendaAsync } from '../api/prendasApi'
import { PrendaCard } from './PrendaCard'
import { CrearPrendaForm } from './CrearPrendaForm'

export function CatalogoPrendasView() {
  const [prendas, setPrendas] = useState<Prenda[]>([])
  const [cargando, setCargando] = useState<boolean>(true)
  const [errorRed, setErrorRed] = useState<string | null>(null)
  const [busqueda, setBusqueda] = useState<string>('')
  const [mostrarFormulario, setMostrarFormulario] = useState<boolean>(false)

  useEffect(() => {
    obtenerPrendasAsync()
      .then((datosDesdePostgres) => {
        setPrendas(datosDesdePostgres)
        setErrorRed(null)
      })
      .catch((error) => {
        setErrorRed(extraerMensajeErrorBackend(error))
      })
      .finally(() => setCargando(false))
  }, [])

  const manejarPrendaCreada = (nuevaPrendaDesdeBD: Prenda) => {
    setPrendas((prendasActuales) => [...prendasActuales, nuevaPrendaDesdeBD])
    setMostrarFormulario(false)
  }

  // --- NUEVO: Manejo Asíncrono del DELETE ---
  const manejarEliminarPrenda = async (id: number) => {
    if (!window.confirm('¿Estás seguro de que deseas eliminar esta prenda?')) return;

    try {
      setErrorRed(null)
      await eliminarPrendaAsync(id)
      
      // Filtramos el arreglo para quitar la prenda eliminada (Inmutabilidad)
      setPrendas((prendasActuales) => prendasActuales.filter((p) => p.id !== id))
      
    } catch (error) {
      // Atrapamos el error. Si la prenda tiene ventas asociadas, PostgreSQL lanzará un error de FK
      const mensaje = extraerMensajeErrorBackend(error)
      setErrorRed(`Error al eliminar: ${mensaje}`)
    }
  }

  const prendasFiltradas = prendas.filter((prenda) =>
    prenda.nombre.toLowerCase().includes(busqueda.toLowerCase())
  )

  return (
    <div>
      <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 pb-6 mb-6 border-b border-slate-800">
        <div>
          <h1 className="text-2xl md:text-3xl font-bold text-white">Catálogo de Prendas</h1>
        </div>

        {!mostrarFormulario && (
          <button onClick={() => setMostrarFormulario(true)} className="bg-indigo-600 hover:bg-indigo-500 text-white font-medium text-sm px-4 py-2.5 rounded-lg shadow-sm cursor-pointer">
            + Nueva Prenda
          </button>
        )}
      </div>

      {mostrarFormulario && (
        <CrearPrendaForm 
          onPrendaCreada={manejarPrendaCreada}
          onCancelar={() => setMostrarFormulario(false)}
        />
      )}

      {errorRed && (
        <div className="mb-4 bg-red-950/60 border border-red-500/40 rounded-lg p-3 text-sm text-red-300">
          🚨 {errorRed}
        </div>
      )}

      <div className="mb-6 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <input type="text" placeholder="🔍 Filtrar prendas..." value={busqueda} onChange={(e) => setBusqueda(e.target.value)} className="w-full sm:max-w-md bg-slate-950 border border-slate-800 rounded-lg px-4 py-2.5 text-sm text-white" />
      </div>

      {cargando && <p className="text-indigo-400 animate-pulse">⏳ Consultando endpoint GET...</p>}

      {!cargando && (
        <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
          {prendasFiltradas.map((prenda) => (
            <PrendaCard 
              key={prenda.id} 
              id={prenda.id} 
              nombre={prenda.nombre} 
              categoria={prenda.categoria} 
              precio={prenda.precio} 
              stock={prenda.stock}
              onEliminar={manejarEliminarPrenda} // Pasamos la función a la tarjeta
            />
          ))}
        </section>
      )}
    </div>
  )
}