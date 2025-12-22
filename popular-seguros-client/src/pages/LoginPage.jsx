import { useState } from 'react'
import axios from 'axios'
import { useNavigate } from 'react-router-dom'

const LoginPage = () => {
    const [usuario, setUsuario] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const navigate = useNavigate()

    const handleLogin = async (e) => {
        e.preventDefault()
        setError('')

        try {
            const url = 'https://localhost:7145/api/Auth/login'
            const response = await axios.post(url, { usuario, password })

            if (response.data.success) {
                localStorage.setItem('user', JSON.stringify(response.data.data))
                navigate('/polizas')
            }

        } catch (err) {
            console.error(err)
            if (err.response && err.response.status === 401) {
                setError('Usuario o contraseña incorrectos.')
            } else {
                setError('Error de conexión. Verifica que la API esté corriendo.')
            }
        }
    }

    return (
        <div className="d-flex justify-content-center align-items-center vh-100 bg-light">
            <div className="card shadow p-4" style={{ width: '24rem' }}>
                <div className="card-body">
                    <h2 className="text-primary text-center fw-bold mb-4">Popular Seguros</h2>
                    <form onSubmit={handleLogin}>
                        <div className="mb-3">
                            <label className="form-label">Usuario</label>
                            <input type="text" className="form-control" value={usuario} onChange={(e) => setUsuario(e.target.value)} required />
                        </div>
                        <div className="mb-3">
                            <label className="form-label">Contraseña</label>
                            <input type="password" className="form-control" value={password} onChange={(e) => setPassword(e.target.value)} required />
                        </div>
                        {error && <div className="alert alert-danger text-center p-2">{error}</div>}
                        <div className="d-grid gap-2 mt-4">
                            <button type="submit" className="btn btn-primary btn-lg">Ingresar</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}

export default LoginPage