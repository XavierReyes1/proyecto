import type { Prenda } from '../types/prenda.types'

export function PrendaCard({ id, nombre, categoria, precio, stock }: Prenda) {
    return (
        <article className="bg-slate-800 border border-slate-700 rounded-xl p-5 flex flex-col justify-between hover:border-indigo-500 transition-colors shadow-sm">
            <div>
                <div className="flex items-center justify-between mb-3">
                    <span className="bg-indigo-500/10 text-indigo-400 text-xs font-semibold px-2.5 py-1 rounded-md border border-indigo-500/20">
                        ID Catálogo #{id}
                    </span>
                    <span className="text-xs text-slate-400 font-medium">{categoria}</span>
                </div>
                
                <h3 className="text-lg font-bold text-white mb-2">
                    {nombre}
                </h3>
                <p className="text-emerald-400 font-bold mb-1">
                    ${precio.toFixed(2)}
                </p>
                <p className="text-sm text-slate-400 mb-5">
                    Stock disponible: {stock}
                </p>
            </div>

            <button 
                disabled 
                className="w-full bg-slate-900/80 text-slate-400 text-sm font-medium py-2.5 px-4 rounded-lg cursor-not-allowed border border-slate-700"
            >
                🔒 Comprar (Requiere Sesión)
            </button>
        </article>
    );
}