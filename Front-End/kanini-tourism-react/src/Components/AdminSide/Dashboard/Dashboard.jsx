import React, { useEffect, useState } from 'react'
import Sidebar from '../Sidebar/Sidebar'
import Navbar from '../NavBar/AdminNav'
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import AdminAction from '../AdminAction/AdminAction';
import Gallery from '../Gallery/Gallery';
import Table from '../AdminContent/Tables'
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
const Dashboard = () => {
 
  const [sidebarstate,setsidebarstate]=useState('Dashboard')

  const handleSearchChange = (string) => {
    setsidebarstate(string);
  };
  var navigate=useNavigate()
  useEffect(() => {
    if (localStorage.getItem('Role') === 'Admin') {
      axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem(
        'AdminToken'
      )}`;
    }
    else {
      navigate('/AdminSignin')
    }
  });
  return (
    <section style={{display:'flex',gap:'.2rem',backgroundColor:'white'}}>
        <div><Sidebar hdlchange={handleSearchChange}/></div>
        <div style={{width:'100%'}}>
           <div> <Navbar/></div>
           <h5 style={{marginTop:'2rem'}}>{sidebarstate}/</h5>
           <div style={{marginTop:'2rem'}}>
            {sidebarstate==='Action' && <AdminAction/>}
            {sidebarstate==='Gallery' && <Gallery/>}
            {sidebarstate==='Dashboard' && <Table/>}
            
           </div>
            
        </div>
    </section>
  )
}

export default Dashboard
