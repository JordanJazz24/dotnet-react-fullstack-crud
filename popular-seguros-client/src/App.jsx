import { BrowserRouter, Routes, Route } from 'react-router-dom'
import LoginPage from './pages/LoginPage'
import PolizasPage from './pages/PolizasPage'
import PolizaFormPage from './pages/PolizaFormPage'

function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<LoginPage />} />
                <Route path="/polizas" element={<PolizasPage />} />
                <Route path="/polizas/nueva" element={<PolizaFormPage />} />
                <Route path="/polizas/editar/:id" element={<PolizaFormPage />} />
            </Routes>
        </BrowserRouter>
    )
}
export default App