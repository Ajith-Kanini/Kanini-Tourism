import React, { useState } from 'react';
import { Navbar, Nav, NavDropdown } from 'react-bootstrap';
import { Link } from 'react-router-dom';
const NavigationBar = () => {
  const [isSignedIn, setIsSignedIn] = useState(false);

  const handleSignIn = () => {
    setIsSignedIn(!isSignedIn);
  };

  return (
    <Navbar collapseOnSelect expand="lg"  style={{backgroundColor:'#002E65',marginBottom:'.1rem'}}>
      <Navbar.Brand href="#" style={{marginLeft:'1rem'}}><img src="https://cdn-icons-png.flaticon.com/128/826/826070.png" alt="" height={'50px'}/></Navbar.Brand>
      <Navbar.Text><h4 className='text-white' style={{marginLeft:'2rem',fontWeight:'1000'}}>Travel Travo</h4></Navbar.Text>
      <Navbar.Toggle aria-controls="responsive-navbar-nav" />
      <Navbar.Collapse id="responsive-navbar-nav">
        <Nav className="" style={{display:'flex',gap:'2rem',justifyContent:'space-evenly',alignContent:'flex-end',marginLeft:'50rem'}}>
          <Nav.Link><Link to={'/LandingPage'} style={{textDecoration:'none',color:'white'}}>Home</Link></Nav.Link>
          <Nav.Link><Link to={'/Packages'} style={{textDecoration:'none',color:'white'}}>Packages</Link></Nav.Link>
          <Nav.Link><Link to={'/Hotels'} style={{textDecoration:'none',color:'white'}}>Hotels</Link></Nav.Link>
          <Nav.Link><Link to={'/Gallery'} style={{textDecoration:'none',color:'white'}}>Gallery</Link></Nav.Link>
        </Nav>
        <Nav>
          <NavDropdown title="Sign In" id="collasible-nav-dropdown" onClick={handleSignIn}>
            {isSignedIn ? (
              <>
                <NavDropdown.Item ><Link to={'/AgentSignin'} style={{textDecoration:'none',color:'black'}}>Agent Signin</Link></NavDropdown.Item>
                <NavDropdown.Item ><Link to={'/UserSignin'} style={{textDecoration:'none',color:'black'}}>User Signin</Link></NavDropdown.Item>
                <NavDropdown.Item ><Link to={'/AdminSignin'} style={{textDecoration:'none',color:'black'}}>Admin Signin</Link></NavDropdown.Item>
              </>
            ) : (
              <NavDropdown.Item style={{textDecoration:'none',color:'white'}} className='text-white'>Sign In</NavDropdown.Item>
            )}
          </NavDropdown>
        </Nav>
      </Navbar.Collapse>
    </Navbar>

  
  );
};

export default NavigationBar;
