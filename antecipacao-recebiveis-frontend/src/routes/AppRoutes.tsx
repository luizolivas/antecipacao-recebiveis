import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Home from '../pages/Home'
import NfeScreen from '../pages/NfeScreen'
import SideBar from '../components/SideBar'
import RegisterEntreprise from '../pages/RegisterEntreprise'
import ListNfe from '../pages/ListNfe'


const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<SideBar />}>
          <Route index element={<Home />} />
          <Route path="/lista-notas-fiscais" element={<ListNfe />} />
          <Route path="/notas-fiscais/nova" element={<NfeScreen />} />
          <Route path="/notas-fiscais/:id/editar" element={<NfeScreen />} />
          <Route path="/empresa" element={<RegisterEntreprise />} />
        </Route>

      </Routes>
    </BrowserRouter>
  )
}

export default AppRoutes
