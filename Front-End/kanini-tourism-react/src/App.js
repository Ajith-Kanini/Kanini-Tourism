import './App.css';
import { BrowserRouter, Route, Routes} from 'react-router-dom'
import Dashboard from './Components/AdminSide/Dashboard/Dashboard';
// import UserNavbar from './Components/UserSide/UserNav/UserNavbar';
import LandingPage from './Components/UserSide/LandingPage/LandingPage';
import News from './Components/UserSide/Packages/Packages';
import BookingPage from './Components/UserSide/BookingPage/BookingPage';
// import UserFooter from './Components/UserSide/User Footer/UserFooter';
import Payment from './Components/UserSide/BookingPage/PaymentPage/Payment';
import Agents from './Components/AgentSide/AgentsProfiles/Agents';



function App() {
  return (
    <BrowserRouter>
    
        <Routes>
          <Route path='Dashboard' Component={Dashboard}/>
          <Route path='BookingPage' Component={BookingPage}/>
          <Route path='Packages' Component={News}/>
          <Route path='LandingPage' Component={LandingPage}/>
          <Route path='' Component={LandingPage}/>
          <Route path='Payment' Component={Payment}/>
          <Route path='Booking' Component={BookingPage}/>
          <Route path='agents' Component={Agents}/>
        </Routes>
    </BrowserRouter>
  );
}

export default App;
