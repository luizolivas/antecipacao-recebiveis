import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Home from '../pages/Home'
import NfeScreen from '../pages/NewNfe'
import SideBar from '../components/SideBar'
import RegisterEntreprise from '../pages/RegisterEntreprise'


const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<SideBar />}>
          <Route index element={<Home />} />
          <Route path="/notas-fiscais" element={<NfeScreen />} />
          <Route path="/empresa" element={<RegisterEntreprise />} />
        </Route>

      </Routes>
    </BrowserRouter>
  )
}

export default AppRoutes
