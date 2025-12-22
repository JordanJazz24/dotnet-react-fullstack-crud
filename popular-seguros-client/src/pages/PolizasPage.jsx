import { useEffect, useState } from 'react'
import axios from 'axios'
import { useNavigate } from 'react-router-dom'

const PolizasPage = () => {
    const [polizas, setPolizas] = useState([])
    const navigate = useNavigate()
    const user = JSON.parse(localStorage.getItem('user'))

    useEffect(() => {
        if (!user) navigate('/')
        fetchPolizas()
    }, [])

    const fetchPolizas = async () => {
        try {
            const response = await axios.get('https://localhost:7145/api/Polizas')
            setPolizas(response.data)
        } catch (error) {
            console.error("Error cargando polizas:", error)
        }
    }

    const handleLogout = () => {
        localStorage.removeItem('user')
        navigate('/')
    }

    // --- LÓGICA DE ELIMINAR (NUEVA) ---
    const handleDelete = async (id) => {
        // 1. Confirmación de usuario
        if (!window.confirm(`¿Está seguro que desea eliminar la póliza #${id}?`)) {
            return
        }

        try {
            // 2. Llamada a la API (Endpoint DELETE)
            await axios.delete(`https://localhost:7145/api/Polizas/${id}`)

            // 3. Actualizar la tabla visualmente (filtras la que borraste)
            setPolizas(polizas.filter(p => p.numeroPoliza !== id))
            alert('Póliza eliminada correctamente.')
        } catch (error) {
            console.error(error)
            alert('Error al intentar eliminar.')
        }
    }

    // Función auxiliar para formatear dinero (Colones/Dólares)
    const formatMoney = (amount) => {
        return new Intl.NumberFormat('es-CR', { style: 'currency', currency: 'CRC' }).format(amount)
    }

    return (
        <div className="container-fluid p-0">
            <nav className="navbar navbar-dark bg-primary px-4">
                <span className="navbar-brand mb-0 h1">Sistema de P&oacute;lizas</span>
                <div className="d-flex align-items-center gap-3">
                    <span className="text-white">Hola, {user?.nombre}</span>
                    <button onClick={handleLogout} className="btn btn-sm btn-light text-primary fw-bold">Salir</button>
                </div>
            </nav>

            <div className="container mt-4">
                <div className="d-flex justify-content-between align-items-center mb-3">
                    <h2>Gesti&oacute;n de P&oacute;lizas</h2>
                    <button className="btn btn-success" onClick={() => navigate('/polizas/nueva')}>
                        + Nueva P&oacute;liza
                    </button>
                </div>

                {/* 3. TABLA DE DATOS COMPACTA */}
                <div className="card shadow-sm">
                    {/* Agregamos 'small' aquí para reducir un poco la fuente de toda la tabla */}
                    <div className="card-body p-0 small">
                        <div className="table-responsive">
                            {/* Agregamos 'table-sm' para reducir el padding de las celdas */}
                            <table className="table table-sm table-striped table-hover mb-0 align-middle">
                                <thead className="table-dark text-nowrap"> {/* text-nowrap evita que los títulos se rompan */}
                                    <tr>
                                        <th>#</th>
                                        <th>Cliente</th>
                                        <th>Aseguradora</th>
                                        <th>Tipo</th>
                                        <th>Cobertura</th>
                                        <th>Monto</th>
                                        <th>Prima</th>
                                        <th>Emisi&oacute;n</th>
                                        <th>Vencimiento</th>
                                        <th>Inclusi&oacute;n</th>
                                        <th>Estado</th>
                                        <th className="text-end">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {polizas.map((poliza) => (
                                        <tr key={poliza.numeroPoliza}>
                                            <td>{poliza.numeroPoliza}</td>
                                            {/* Usamos text-truncate y un max-width para nombres muy largos */}
                                            <td style={{ maxWidth: '150px' }}>
                                                <div className="fw-bold text-truncate" title={poliza.cedulaAsegurado}>{poliza.cedulaAsegurado}</div>
                                                <div className="small text-muted text-truncate" title={poliza.nombreCliente}>{poliza.nombreCliente}</div>
                                            </td>
                                            <td className="text-truncate" style={{ maxWidth: '100px' }} title={poliza.aseguradora}>{poliza.aseguradora}</td>
                                            <td>{poliza.nombreTipoPoliza}</td>
                                            <td className="text-truncate" style={{ maxWidth: '120px' }} title={poliza.nombreCobertura}>{poliza.nombreCobertura}</td>
                                            {/* text-nowrap en montos para que no se partan en dos líneas */}
                                            <td className="text-nowrap">{formatMoney(poliza.montoAsegurado)}</td>
                                            <td className="text-nowrap">{formatMoney(poliza.prima)}</td>

                                            <td>{new Date(poliza.fechaEmision).toLocaleDateString()}</td>
                                            <td>{new Date(poliza.fechaVencimiento).toLocaleDateString()}</td>
                                            <td className="text-muted">{new Date(poliza.fechaInclusion).toLocaleDateString()}</td>

                                            <td>
                                                <span className={`badge ${poliza.nombreEstado === 'Activa' ? 'bg-success' : 'bg-secondary'}`}>
                                                    {poliza.nombreEstado}
                                                </span>
                                            </td>
                                            {/* La columna de acciones ahora tiene ancho fijo mínimo para que no se aplaste */}
                                            <td className="text-end text-nowrap" style={{ minWidth: '140px' }}>
                                                <button
                                                    className="btn btn-sm btn-outline-primary me-1" // Reduje margen de me-2 a me-1
                                                    onClick={() => navigate(`/polizas/editar/${poliza.numeroPoliza}`)}
                                                >
                                                    Editar
                                                </button>
                                                <button
                                                    className="btn btn-sm btn-outline-danger"
                                                    onClick={() => handleDelete(poliza.numeroPoliza)}
                                                >
                                                    Eliminar
                                                </button>
                                            </td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                        {polizas.length === 0 && <p className="text-center p-5 text-muted">No hay p&oacute;lizas registradas.</p>}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default PolizasPage