import './App.css';
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import Dashboard from './Components/AdminSide/Dashboard/Dashboard';
import LandingPage from './Components/UserSide/LandingPage/LandingPage';
import Payment from './Components/UserSide/BookingPage/PaymentPage/Payment';
import Gallery from './Components/AdminSide/Gallery/Gallery';
import HotelDetails from './Components/UserSide/HotelDetails/HotelDetails';
import UserRegister from './Components/UserSide/UserRegister/UserRegister';
import UserLogin from './Components/UserSide/UserLogin/UserLogin';
import { ToastContainer } from 'react-bootstrap';
import 'react-toastify/dist/ReactToastify.css';
import AgentRegister from './Components/AgentSide/AgentRegister/AgentRegister';
import AgentLogin from './Components/AgentSide/AgentRegister/AgentLogin';
import NavigationBar from './Components/UserSide/LandingPage/LandingNavbar/LandingNavbar'
import UserNav from './Components/UserSide/UserNav/UserNav';
import AdminLogin from './Components/AdminSide/AdminLogin/AdminLogin';
import BasicModal from './Components/UserSide/BookingPage/BookingPage';
import TourPackages from './Components/UserSide/TourPackages/TourPackages';
// import MyBookings from './Components/UserSide/MyBookings/MyBookings';
import Invoice from './Components/UserSide/Invoice/InvoiceGenerator';
import AgentNavbar from './Components/AgentSide/AgentNavbar/AgentNavbar';
import MyBookingsTable from './Components/UserSide/MyBookings/MyBookingsTable';


function App() {
  return (
    <BrowserRouter>
      <ToastContainer />
      
      {localStorage.getItem('Role') === 'Admin' && <Dashboard/>}
      {localStorage.getItem('Role') === 'User' && <UserNav/>}
      {localStorage.getItem('Role') === 'Agent' && <AgentNavbar/>}
      {!localStorage.getItem('Role') && <NavigationBar/>}

      <Routes>
        <Route path='Dashboard' Component={Dashboard} />
        <Route path='LandingPage' Component={LandingPage} />
        <Route path='Payment' Component={Payment} />
        <Route path='Gallery' Component={Gallery} />
        <Route path='Hotels' Component={HotelDetails} />
        <Route path='UserRegister' Component={UserRegister} />
        <Route path='UserSignin' Component={UserLogin} />
        <Route path='AgentRegister' Component={AgentRegister} />
        <Route path='AgentSignin' Component={AgentLogin} />
        <Route path='AdminSignin' Component={AdminLogin } />
        <Route path='Booking' Component={BasicModal } />
        <Route path='Invoice' Component={Invoice } />
        <Route path='Packages' Component={TourPackages } />
        <Route path='MyBookings' Component={MyBookingsTable } />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
