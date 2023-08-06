import React from "react";
import './UserNavbar.css'
import {Link } from "react-router-dom";

const UserNavbar = () => {
  return (
    <div>
       <header>
        <div className="container">

          <input type="checkbox" name="check" id="check"/>

          <div className="logo-container">
            <h3 className="logo">Travel<span>Travo</span></h3>
          </div>

          <div className="nav-btn">
            <div className="nav-links">
              <ul>
                <li className="nav-link" style={{ "--i": ".6s" }}>
                <Link to={'/LandingPage'}>Home</Link>
                </li>
                <li className="nav-link" style={{ "--i": ".85s" }}>
                  <Link to={'/Packages'}>Packages</Link>
                </li>
                <li className="nav-link" style={{ "--i": "1.1s" }}>
                  <a href="link">Hotels</a>
                  
                </li>
                <li className="nav-link" style={{ "--i": "1.35s" }}>
                  <a href="link">Feedback</a>
                </li>
                <li className="nav-link" style={{ "--i": "1.40s" }}>
                <div className="log-sign" style={{ "--i": "1.8s" }}>
                <a href="link" className="btn transparent">Log in</a>
                <a href="link" className="btn solid">Sign up</a>
              </div>

          
                </li>
              </ul>
            </div>
            </div>

            

          <div className="hamburger-menu-container">
            <div className="hamburger-menu">
              <div></div>
            </div>
          </div>

        </div>
      </header>

      
    </div>
  );
};

export default UserNavbar;
