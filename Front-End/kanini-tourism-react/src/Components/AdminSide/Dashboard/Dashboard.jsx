import React from 'react'
import Sidebar from '../Sidebar/Sidebar'
import Navbar from '../NavBar/AdminNav'
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import AdminAction from '../AdminAction/AdminAction';
import Gallery from '../Gallery/Gallery';

const Dashboard = () => {
  return (
    <section style={{display:'flex',gap:'.2rem'}}>
        <div><Sidebar/></div>
        <div style={{width:'100%'}}>
           <div> <Navbar/></div>
           <div style={{marginTop:'5rem'}}>
            <AdminAction/>
            <Gallery/>
           </div>
            
        </div>
    </section>
  )
}

export default Dashboard
