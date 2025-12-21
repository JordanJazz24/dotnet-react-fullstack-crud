import { useEffect, useState } from 'react'
import axios from 'axios'
import { useNavigate, useParams } from 'react-router-dom'

const PolizaFormPage = () => {
    const navigate = useNavigate()
    const { id } = useParams() // Captura el ID si estamos editando
    const esEdicion = !!id

    // 1. ESTADOS PARA LOS CATÁLOGOS (Listas Desplegables)
    const [tipos, setTipos] = useState([])
    const [coberturas, setCoberturas] = useState([])
    const [estados, setEstados] = useState([])

    // 2. ESTADO DEL FORMULARIO
    const [formData, setFormData] = useState({
        cedulaAsegurado: '',
        idTipoPoliza: '',
        idCobertura: '',
        idEstadoPoliza: '',
        montoAsegurado: '',
        prima: '',
        fechaEmision: '',
        fechaVencimiento: ''
    })

    // 3. CARGAR DATOS INICIALES
    useEffect(() => {
        cargarCatalogos()
        if (esEdicion) {
            cargarDatosPoliza(id)
        }
    }, [id])

    const cargarCatalogos = async () => {
        try {
            // Llamamos a los endpoints que acabamos de crear
            const [resTipos, resCob, resEst] = await Promise.all([
                axios.get('https://localhost:7145/api/Catalogos/Tipos'),
                axios.get('https://localhost:7145/api/Catalogos/Coberturas'),
                axios.get('https://localhost:7145/api/Catalogos/Estados')
            ])
            setTipos(resTipos.data)
            setCoberturas(resCob.data)
            setEstados(resEst.data)
        } catch (error) {
            alert('Error cargando listas desplegables')
        }
    }

    const cargarDatosPoliza = async (idPoliza) => {
        try {
            const response = await axios.get(`https://localhost:7145/api/Polizas/${idPoliza}`)
            const data = response.data

            // Ajustamos las fechas para que el input type="date" las entienda (YYYY-MM-DD)
            const formatearFecha = (fecha) => fecha ? fecha.split('T')[0] : ''

            setFormData({
                cedulaAsegurado: data.cedulaAsegurado,
                idTipoPoliza: data.idTipoPoliza,
                idCobertura: data.idCobertura,
                idEstadoPoliza: data.idEstadoPoliza,
                montoAsegurado: data.montoAsegurado,
                prima: data.prima,
                fechaEmision: formatearFecha(data.fechaEmision),
                fechaVencimiento: formatearFecha(data.fechaVencimiento)
            })
        } catch (error) {
            alert('Error cargando la póliza')
        }
    }

    // 4. MANEJAR CAMBIOS EN INPUTS
    const handleChange = (e) => {
        const { name, value } = e.target
        setFormData({ ...formData, [name]: value })
    }

    // 5. ENVIAR FORMULARIO (Crear o Editar)
    const handleSubmit = async (e) => {
        e.preventDefault()
        try {
            if (esEdicion) {
                await axios.put(`https://localhost:7145/api/Polizas/${id}`, formData)
                alert('Póliza actualizada correctamente')
            } else {
                await axios.post('https://localhost:7145/api/Polizas', formData)
                alert('Póliza creada correctamente')
            }
            navigate('/polizas')
        } catch (error) {
            console.error(error)
            const mensaje = error.response?.data || 'Error al guardar la póliza. Verifique los datos.'
            alert(typeof mensaje === 'string' ? mensaje : 'Error al guardar. Verifique que la cédula del cliente exista.')
        }
    }

    return (
        <div className="container mt-4">
            <div className="card shadow">
                <div className="card-header bg-primary text-white">
                    <h3 className="mb-0">{esEdicion ? 'Editar Póliza' : 'Nueva Póliza'}</h3>
                </div>
                <div className="card-body">
                    <form onSubmit={handleSubmit}>
                        <div className="row">

                            {/* Cédula */}
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Cédula Asegurado (Debe existir)</label>
                                <input
                                    type="text" name="cedulaAsegurado" className="form-control"
                                    value={formData.cedulaAsegurado} onChange={handleChange} required
                                />
                            </div>

                            {/* Monto */}
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Monto Asegurado</label>
                                <input
                                    type="number" name="montoAsegurado" className="form-control"
                                    value={formData.montoAsegurado} onChange={handleChange} required
                                />
                            </div>

                            {/* Listas Desplegables */}
                            <div className="col-md-4 mb-3">
                                <label className="form-label">Tipo Póliza</label>
                                <select
                                    name="idTipoPoliza" className="form-select"
                                    value={formData.idTipoPoliza} onChange={handleChange} required
                                >
                                    <option value="">Seleccione...</option>
                                    {tipos.map(t => <option key={t.idTipoPoliza} value={t.idTipoPoliza}>{t.nombre}</option>)}
                                </select>
                            </div>

                            <div className="col-md-4 mb-3">
                                <label className="form-label">Cobertura</label>
                                <select
                                    name="idCobertura" className="form-select"
                                    value={formData.idCobertura} onChange={handleChange} required
                                >
                                    <option value="">Seleccione...</option>
                                    {coberturas.map(c => <option key={c.idCobertura} value={c.idCobertura}>{c.nombre}</option>)}
                                </select>
                            </div>

                            <div className="col-md-4 mb-3">
                                <label className="form-label">Estado</label>
                                <select
                                    name="idEstadoPoliza" className="form-select"
                                    value={formData.idEstadoPoliza} onChange={handleChange} required
                                >
                                    <option value="">Seleccione...</option>
                                    {estados.map(e => <option key={e.idEstadoPoliza} value={e.idEstadoPoliza}>{e.nombre}</option>)}
                                </select>
                            </div>

                            {/* Fechas */}
                            <div className="col-md-6 mb-3">
                                <label className="form-label">Fecha Emisión</label>
                                <input
                                    type="date" name="fechaEmision" className="form-control"
                                    value={formData.fechaEmision} onChange={handleChange} required
                                />
                            </div>

                            <div className="col-md-6 mb-3">
                                <label className="form-label">Fecha Vencimiento</label>
                                <input
                                    type="date" name="fechaVencimiento" className="form-control"
                                    value={formData.fechaVencimiento} onChange={handleChange} required
                                />
                            </div>

                            <div className="col-md-12 mb-3">
                                <label className="form-label">Prima</label>
                                <input
                                    type="number" name="prima" className="form-control"
                                    value={formData.prima} onChange={handleChange} required
                                />
                            </div>

                        </div>

                        <div className="d-flex justify-content-end gap-2">
                            <button type="button" className="btn btn-secondary" onClick={() => navigate('/polizas')}>
                                Cancelar
                            </button>
                            <button type="submit" className="btn btn-success">
                                Guardar Póliza
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    )
}

export default PolizaFormPage