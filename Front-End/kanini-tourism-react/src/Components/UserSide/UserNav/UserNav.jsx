import React, { useEffect, useState } from 'react';
import { Navbar, Nav } from 'react-bootstrap';
import { Link, useNavigate } from 'react-router-dom';
import { Variable } from '../../../Variables';
import axios from 'axios';
const UserNav = () => {
  const navigate=useNavigate()
  const logout=()=>{
    localStorage.clear()
    navigate('/LandingPage')
    window.location.reload()
   }
   const [doctorDetails, setDoctorDetails] = useState({});

   const fetchUser = async () => {
    try {
      const response = await axios.get(Variable.UserURL.GetAll);
      const foundDoctor = response.data.find(
        (dt) => dt.email === localStorage.getItem('email')
      );
      if (foundDoctor) {
        setDoctorDetails(foundDoctor);
        localStorage.setItem('id',foundDoctor.userId)
      }
    } catch (error) {
      console.error(error);
    }
  };
  useEffect(() => {
    
      fetchUser();
    
  });

  return (
    <Navbar collapseOnSelect expand="lg"  style={{backgroundColor:'#002E65',marginBottom:'.1rem'}}>
      <Navbar.Brand href="#" style={{marginLeft:'1rem'}}><img src="https://cdn-icons-png.flaticon.com/128/826/826070.png" alt="" height={'50px'}/></Navbar.Brand>
      <Navbar.Toggle aria-controls="responsive-navbar-nav" />
      <Navbar.Collapse id="responsive-navbar-nav">
        <Nav className="" style={{display:'flex',gap:'2rem',justifyContent:'space-evenly',alignContent:'flex-end',marginLeft:'50rem'}}>
          <Nav.Link><Link to={'/LandingPage'} style={{textDecoration:'none',color:'white'}}>Home</Link></Nav.Link>
          <Nav.Link><Link to={'/Packages'} style={{textDecoration:'none',color:'white'}}>Packages</Link></Nav.Link>
          <Nav.Link><Link to={'/Hotels'} style={{textDecoration:'none',color:'white'}}>Hotels</Link></Nav.Link>
          <Nav.Link><Link to={'/Gallery'} style={{textDecoration:'none',color:'white'}}>Gallery</Link></Nav.Link>
          <Nav.Link><Link to={'/MyBookings'} style={{textDecoration:'none',color:'white'}}>My bookings</Link></Nav.Link>
          <Nav.Link><Link onClick={logout} style={{textDecoration:'none',color:'white'}}>Logout</Link></Nav.Link>
          <Nav.Link><Link  style={{textDecoration:'none',color:'white'}}>Welcome ! {doctorDetails.fullName}</Link></Nav.Link>
        </Nav>
      </Navbar.Collapse>
    </Navbar>

  
  );
};

export default UserNav;
