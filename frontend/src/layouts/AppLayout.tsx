import type { ReactNode } from 'react'

interface AppLayoutProps {
  children: ReactNode; // Representa la vista que se inyectará en el área central
}

export function AppLayout({ children }: AppLayoutProps) {
  return (
    <div className="min-h-screen bg-slate-900 text-slate-100 flex flex-col md:flex-row">
      
      {/* 1. COLUMNA IZQUIERDA: MENÚ CORPORATIVO (SIDEBAR) */}
      <aside className="hidden md:flex md:w-64 md:flex-col bg-slate-950 border-r border-slate-800 justify-between p-5 shrink-0">
        <div>
          <div className="flex items-center gap-3 mb-8 px-2">
            <div className="w-8 h-8 rounded-lg bg-indigo-600 flex items-center justify-center font-bold text-white shadow">
              YC
            </div>
            <span className="text-xl font-bold tracking-tight text-white">YourCloset</span>
          </div>

          <nav className="space-y-2">
            <a href="#" className="flex items-center gap-3 px-3 py-2.5 bg-indigo-600/15 text-indigo-400 rounded-lg font-medium text-sm border border-indigo-500/20">
              <span>👕</span> Catálogo de Prendas
            </a>
            <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-500 rounded-lg font-medium text-sm cursor-not-allowed">
              <span>🛒</span> Mis Compras (Unidad 3)
            </a>
            <a href="#" className="flex items-center gap-3 px-3 py-2.5 text-slate-500 rounded-lg font-medium text-sm cursor-not-allowed">
              <span>👥</span> Clientes (Unidad 3)
            </a>
          </nav>
        </div>

        <div className="p-3.5 bg-slate-900 rounded-xl border border-slate-800">
          <p className="text-xs text-slate-400 mb-1">Estado de Seguridad</p>
          <p className="text-sm font-semibold text-amber-400">👤 Modo Invitado</p>
        </div>
      </aside>

      {/* 2. CABECERA MÓVIL */}
      <header className="md:hidden bg-slate-950 border-b border-slate-800 p-4 flex items-center justify-between">
        <div className="flex items-center gap-2.5">
          <div className="w-7 h-7 rounded-md bg-indigo-600 flex items-center justify-center font-bold text-sm text-white">
            YC
          </div>
          <span className="font-bold text-lg text-white">YourCloset</span>
        </div>
        <span className="text-xs bg-amber-500/10 text-amber-400 px-2.5 py-1 rounded-md border border-amber-500/20 font-medium">
          👤 Invitado
        </span>
      </header>

      {/* 3. ÁREA CENTRAL DE CONTENIDO (Aquí se inyecta cualquier módulo hijo) */}
      <main className="flex-1 p-6 md:p-10 overflow-y-auto">
        {children}
      </main>
    </div>
  )
}