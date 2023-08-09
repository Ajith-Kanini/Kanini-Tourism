import axios from "axios";
import React, { useEffect, useState } from "react";
import { Variable } from "../../../Variables";
import AddHotel from "./AddHotel";
import Rating from 'react-rating-stars-component';

const HotelDetails = () => {
  const [Hotel, setHotel] = useState([]);
  useEffect(() => {
    //Hotel
    axios
      .get(Variable.HotelURL.GetAll)
      .then((response) => {
        setHotel(response.data);
      })
      .catch((error) => {
        console.error("Error fetching hotel details:", error);
      });
  }, []);
  return (
    <div>
      <div className="row " style={{ margin: "20px 50px" }}>
        <div style={{ display: "flex" }}>
          <h3>Popular hotels and Restaurents</h3>
         {localStorage.getItem('Role')==='Agent' &&  <h3 style={{ marginLeft: "auto" }}>
          <AddHotel/></h3>}
        
        </div>
        
        {Hotel.map((hotel) => (
          <div className="col-sm-3 mt-3" style={{height:'30rem'}}>
            <div className="card card-block p-2">
              <img
                className="card-img-top"
                data-src="holder.js/100px180/"
                alt="100%x180"
                src={`https://localhost:7006/uploads/hotels/${hotel.hotelImage}`}
                data-holder-rendered="true"
                style={{ height: "180px", width: "100%", display: "block" }}
              />
              <div className="card-block">
                <h4 className="card-title">{hotel.hotelName}</h4>
                <p className="card-text">
                <b>Land Mark : {hotel.hotelAddress}</b> <br/>
                  <b>City : {hotel.hotelCity}</b>
                </p>
                <Rating count={5} value={hotel.starRating} size={24} activeColor='#ffd700' inactiveColor='#e4e4e4' classNames="rating"
                style={{marginLeft:'5rem'}}/>
                <a href="#link" className="btn btn-primary">
                  Book Now
                </a>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default HotelDetails;
