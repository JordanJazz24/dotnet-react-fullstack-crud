import React from 'react'
import ReactDOM from 'react-dom/client'
// 1. ESTA LÍNEA ES CRÍTICA PARA QUE SE VEA BIEN:
import 'bootstrap/dist/css/bootstrap.min.css';

import App from './App.jsx'

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <App />
    </React.StrictMode>,
)