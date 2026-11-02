// src/App.tsx
import { AppLayout } from './layouts/AppLayout'
import { CatalogoPrendasView } from './features/prendas/components/CatalogoPrendasView'

function App() {
  return (
    <AppLayout>
      <CatalogoPrendasView />
    </AppLayout>
  )
}

export default App