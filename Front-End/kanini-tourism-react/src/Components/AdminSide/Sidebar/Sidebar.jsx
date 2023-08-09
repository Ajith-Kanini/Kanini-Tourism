import React, { useEffect } from "react";
import SBStyle from "./SideBar.module.css";

import { useNavigate } from "react-router-dom";
import axios from "axios";
const Sidebar = ({ hdlchange }) => {
  const navigate=useNavigate()
  
   const logout=()=>{
    localStorage.clear()
    navigate('/LandingPage')
    window.location.reload()
   }

   useEffect(() => {
    if (localStorage.getItem('Role') === 'Admin') {
      axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem(
        'AdminToken'
      )}`;
    }
    else {
      navigate('/LandingPage')
    }
  });
  return (
    <div>
     
      <nav style={{backgroundColor:'black',width:'18rem',position:'sticky',top:'0',left:'0',
    height:'100vh',overflowX:'hidden',padding:'1.8rem 0.85rem',color:"white",display:'flex',flexDirection:'column',transition:'width 0.5s ease-in-out'}}>
        <div className={SBStyle.sidebar_top}>
          <div className={SBStyle.logo__wrapper}>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              className={SBStyle.logo}
              width="180"
              height="180"
              viewBox="0 0 180 180"
              fill="none"
            >
              <path
                d="M82.646 37.0917C85.7401 35.3054 89.552 35.3054 92.646 37.0917L133.614 60.7445L105.286 77.3318C100.901 72.9296 94.8325 70.2051 88.128 70.2051C81.1554 70.2051 74.871 73.1519 70.4523 77.8681L41.4416 60.8811L82.646 37.0917Z"
                fill="white"
              />
              <path
                d="M64.9303 87.4484L35.9141 70.4582V117.952L64.8222 101.025C64.2287 98.9258 63.9111 96.7109 63.9111 94.4219C63.9111 91.9977 64.2673 89.6567 64.9303 87.4484Z"
                fill="white"
              />
              <path
                d="M70.1924 110.694L41.8975 127.262L82.646 150.788C85.74 152.574 89.552 152.574 92.646 150.788L133.158 127.398L105.556 111.236C101.152 115.8 94.9714 118.639 88.128 118.639C81.0175 118.639 74.6227 115.574 70.1924 110.694Z"
                fill="white"
              />
              <path
                d="M111.22 101.739L139.376 118.226C139.377 118.162 139.378 118.098 139.378 118.034V70.1831L111.101 86.7403C111.908 89.154 112.345 91.7369 112.345 94.4219C112.345 96.9723 111.951 99.4305 111.22 101.739Z"
                fill="white"
              />
            </svg>
            <h2 className={SBStyle.hide} style={{ color: "white" }}>
              Hii Admin
            </h2>
          </div>
          <div className={SBStyle.expand_btn}>
            <svg
              xmlns="http://www.w3.org/2000/svg"
              width="24"
              height="24"
              viewBox="0 0 24 24"
              stroke-width="2"
              stroke="white"
              fill="none"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
              <path d="M7 7l5 5l-5 5"></path>
              <path d="M13 7l5 5l-5 5"></path>
            </svg>
          </div>
        </div>
        <div className={SBStyle.sidebar_links}>
          <ul className={SBStyle.SidebarUL}>
            <li>
              <div
               
                title="Dashboard"
                onClick={() => hdlchange("Dashboard")}
              >
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  stroke-width="2"
                  stroke="#91959C"
                  fill="none"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                >
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
                  <path d="M4 4h6v8h-6z"></path>
                  <path d="M4 16h6v4h-6z"></path>
                  <path d="M14 12h6v8h-6z"></path>
                  <path d="M14 4h6v4h-6z"></path>
                </svg>
                <span className={`${SBStyle.link} ${SBStyle.hide}`}>
                  Dashboard
                </span>
              </div>
            </li>
            <li>
              <div
                title="Project"
                onClick={() => hdlchange("Action")}
              >
                <i class="fas fa-cog"></i>
                <span className={`${SBStyle.link} ${SBStyle.hide}`}>
                  Action
                </span>
              </div>
            </li>
            <li>
              <div
                
                title="Calendar"
                onClick={() => hdlchange("Gallery")}
              >
                <i className="fas fa-images"></i>

                <span className={`${SBStyle.link} ${SBStyle.hide}`}>
                  Gallery
                </span>
              </div>
            </li>
            <li>
              <div
                
                title="Calendar"
                onClick={() => hdlchange("Feedback")}
              >
                <i class="fas fa-comment-alt"></i>
                <span className={`${SBStyle.link} ${SBStyle.hide}`}>
                Feedback
                </span>
              </div>
            </li>
            <li>
              <div title="Messages" onClick={logout}>
              <i className="fas fa-sign-out-alt"></i>
                <span className={`${SBStyle.link} ${SBStyle.hide}`}>
                  Logout
                </span>
              </div>
            </li>
          </ul>
        </div>
        <div className={`${SBStyle.sidebar_links} ${SBStyle.sidebar_bottom}`}>
          <div className={SBStyle.divider}></div>
          <ul className={SBStyle.SidebarUL}>
            <li>
              <div title="Help">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  stroke-width="2"
                  stroke="#91959C"
                  fill="none"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                >
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
                  <path d="M3 12a9 9 0 1 0 18 0a9 9 0 0 0 -18 0"></path>
                  <path d="M12 16v.01"></path>
                  <path d="M12 13a2 2 0 0 0 .914 -3.782a1.98 1.98 0 0 0 -2.414 .483"></path>
                </svg>

                <span className={`${SBStyle.link} ${SBStyle.hide}`}>Help</span>
              </div>
            </li>
            <li>
              <div title="Settings">
                <svg
                  xmlns="http://www.w3.org/2000/svg"
                  width="24"
                  height="24"
                  viewBox="0 0 24 24"
                  stroke-width="2"
                  stroke="#91959C"
                  fill="none"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                >
                  <path stroke="none" d="M0 0h24v24H0z" fill="none"></path>
                  <path d="M10.325 4.317c.426 -1.756 2.924 -1.756 3.35 0a1.724 1.724 0 0 0 2.573 1.066c1.543 -.94 3.31 .826 2.37 2.37a1.724 1.724 0 0 0 1.065 2.572c1.756 .426 1.756 2.924 0 3.35a1.724 1.724 0 0 0 -1.066 2.573c.94 1.543 -.826 3.31 -2.37 2.37a1.724 1.724 0 0 0 -2.572 1.065c-.426 1.756 -2.924 1.756 -3.35 0a1.724 1.724 0 0 0 -2.573 -1.066c-1.543 .94 -3.31 -.826 -2.37 -2.37a1.724 1.724 0 0 0 -1.065 -2.572c-1.756 -.426 -1.756 -2.924 0 -3.35a1.724 1.724 0 0 0 1.066 -2.573c-.94 -1.543 .826 -3.31 2.37 -2.37c1 .608 2.296 .07 2.572 -1.065z"></path>
                  <path d="M9 12a3 3 0 1 0 6 0a3 3 0 0 0 -6 0"></path>
                </svg>
                <span className={`${SBStyle.link} ${SBStyle.hide}`}>
                  Settings
                </span>
              </div>
            </li>
          </ul>
          <div className={SBStyle.divider}></div>
          <div className={SBStyle.sidebar__profile}>
            <div className={SBStyle.avatar__wrapper}>
              <img
                className={SBStyle.avatar}
                // src={`https://localhost:7162/uploads/user/${data.userImage}`}
                src="https://img.freepik.com/free-psd/3d-illustration-person-with-sunglasses_23-2149436188.jpg?size=626&ext=jpg&ga=GA1.2.41053441.1676020414&semt=sph"
                alt="JoeDoePicture"
              />
              <div className={SBStyle.online__status}></div>
            </div>
            <section className={`${SBStyle.avatar__name} ${SBStyle.hide}`}>
              <div className={SBStyle.user_name} style={{ color: "white" }}>
                Ajith
              </div>
              <div className={SBStyle.email} style={{ color: "white" }}>
                Kanini.com
              </div>
            </section>
          </div>
        </div>
      </nav>
    </div>
  );
};

export default Sidebar;
