import { useState, useEffect } from 'react'
import type { Prenda } from '../types/prenda.types'
import { obtenerPrendasAsync } from '../api/prendasApi'
import { PrendaCard } from './PrendaCard'

export function CatalogoPrendasView() {
  // --- ZONA 3: CEREBRO DEL MÓDULO ---
  const [prendas, setPrendas] = useState<Prenda[]>([])
  const [cargando, setCargando] = useState<boolean>(true)
  const [busqueda, setBusqueda] = useState<string>('')

  useEffect(() => {
    // Consumimos nuestra capa de servicio (prendasApi.ts)
    obtenerPrendasAsync().then((datos) => {
      setPrendas(datos)
      setCargando(false)
    })
  }, [])

  const prendasFiltradas = prendas.filter((prenda) =>
    prenda.nombre.toLowerCase().includes(busqueda.toLowerCase())
  )

  // --- ZONA 4: RENDERIZADO EXCLUSIVO DEL CATÁLOGO ---
  return (
    <div>
      {/* Barra Superior de Título y Acción Principal */}
      <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 pb-6 mb-6 border-b border-slate-800">
        <div>
          <h1 className="text-2xl md:text-3xl font-bold text-white">Catálogo de Prendas</h1>
          <p className="text-slate-400 text-sm mt-1">
            Módulo desacoplado bajo arquitectura <code className="text-indigo-400 font-mono">Vertical Slicing</code>.
          </p>
        </div>

        <button className="bg-indigo-600 hover:bg-indigo-500 text-white font-medium text-sm px-4 py-2.5 rounded-lg transition-colors shadow-sm self-start sm:self-auto cursor-pointer">
          + Nueva Prenda
        </button>
      </div>

      {/* Buscador Reactivo */}
      <div className="mb-6 flex flex-col sm:flex-row sm:items-center justify-between gap-4">
        <input
          type="text"
          placeholder="🔍 Filtrar prendas en el catálogo..."
          value={busqueda}
          onChange={(e) => setBusqueda(e.target.value)}
          className="w-full sm:max-w-md bg-slate-950 border border-slate-800 rounded-lg px-4 py-2.5 text-sm text-white placeholder-slate-500 focus:outline-none focus:border-indigo-500 transition-colors"
        />
        <span className="text-xs text-slate-400">
          Mostrando <strong className="text-white">{prendasFiltradas.length}</strong> de {prendas.length} registros
        </span>
      </div>

      {/* Renderizado Condicional y Rejilla */}
      {cargando ? (
        <div className="bg-slate-800/50 border border-slate-800 rounded-xl p-12 text-center">
          <p className="text-indigo-400 font-semibold animate-pulse text-lg">
            ⏳ Consultando capa de servicios (prendasApi.ts)...
          </p>
        </div>
      ) : (
        <section className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
          {prendasFiltradas.map((prenda) => (
            <PrendaCard 
              key={prenda.id} 
              id={prenda.id} 
              nombre={prenda.nombre} 
              categoria={prenda.categoria}
              precio={prenda.precio}
              stock={prenda.stock}
            />
          ))}
        </section>
      )}
    </div>
  )
}