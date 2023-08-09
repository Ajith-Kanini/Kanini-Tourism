// import React, { useEffect, useState } from 'react'
// import {  Table } from 'react-bootstrap'
// import MyBookings from './MyBookings'
// import axios from 'axios'
// import { Variable } from '../../../Variables'
// const MyBookingsTable = () => {

//   const [Bookings,setBookings]=useState([])
//   const [Packages,setPackages]=useState({})

//   async function handlePackages(id){
//    await  axios
//   .get(Variable.PackagesURL.GetAll+'/'+id)
//   .then((response) => {
//     setPackages(response.data);
//     return Packages.packageName
//   })
//   .catch((error) => {
//     console.error("Error fetching package details:", error);
//   });
//   }
//   useEffect(() => {
    
      
      
//       //bookings
//       axios
//       .get(Variable.BookingURL.GetAll)
//       .then((response) => {
//         setBookings(response.data);

//       })
//       .catch((error) => {
//         console.error("Error fetching package details:", error);
//       });

//   }, []);

//   return (
//     <div style={{margin:'5rem 6rem'}}>
//        <Table striped bordered hover>
//       <thead>
//         <tr>
//           <th>S.No</th>
//           <th>Package Name</th>
//           <th>Details</th>
//         </tr>
//       </thead>
//       <tbody>
//         {
//             Bookings.map(bkg=>(
//                 <tr>
//           <td>{bkg.bookingId}</td>
  
//         </tr>
//             ))
//         }
//       </tbody>
//     </Table>
//     </div>
//   )
// }

// export default MyBookingsTable

import React from 'react'

const MyBookingsTable = () => {
  return (
    <div>
      
    </div>
  )
}

export default MyBookingsTable
