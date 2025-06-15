import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Home from '../pages/Home'
import NfeScreen from '../pages/NfeDetails'
import SideBar from '../components/SideBar'
import ListNfe from '../pages/ListNfe'
import ListCompanies from '../pages/ListCompanies'
import CompanyDetails from '../pages/CompanyDetails'


const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<SideBar />}>
          <Route index element={<Home />} />
          <Route path="/lista-notas-fiscais" element={<ListNfe />} />
          <Route path="/notas-fiscais/nova" element={<NfeScreen />} />
          <Route path="/notas-fiscais/:id/editar" element={<NfeScreen />} />
          <Route path="/lista-empresas" element={<ListCompanies />} />
          <Route path="/empresa-detalhes/:id/editar" element={<CompanyDetails />} />
        </Route>

      </Routes>
    </BrowserRouter>
  )
}

export default AppRoutes
