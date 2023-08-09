import React, { useEffect, useState } from "react";
import "./Table.css";
import axios from "axios";
import { Variable } from "../../../Variables";
const Tables = () => {
  const [user,setUser]=useState([])
  const [Hotel,setHotel]=useState([])
  const [Agent,setAgent]=useState([])
  const [Packages,setPackages]=useState([])

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

      //user
      axios
      .get(Variable.UserURL.GetAll)
      .then((response) => {
        setUser(response.data);
      })
      .catch((error) => {
        console.error("Error fetching user details:", error);
      });
      //packages
      axios
      .get(Variable.PackagesURL.GetAll)
      .then((response) => {
        setPackages(response.data);
      })
      .catch((error) => {
        console.error("Error fetching package details:", error);
      });

      //Agent
      axios
      .get(Variable.AgentURL.GetAll)
      .then((response) => {
        setAgent(response.data);
      })
      .catch((error) => {
        console.error("Error fetching agent details:", error);
      });
  }, []);

  return (
    <div>
      <div className="container">
        <div className="row mt-4">
          <div className="col-12 col-sm-6 col-md-3">
            <div className="card my-card has-background-gradient-teal">
              <div className="my-auto mx-auto">
                <span className="fa fa-users my-icon py-3 px-4"></span>
              </div>
              <div className="my-auto px-4">
                <p className="mb-1">Users</p>
                <h2>{user.length}</h2>
              </div>
            </div>
          </div>

          <div className="col-12 col-sm-6 col-md-3">
            <div className="card my-card has-background-gradient-blue">
              <div className="my-auto mx-auto">
                <span className="fa fa-user-tie my-icon py-3 px-4"></span>
              </div>
              <div className="my-auto px-4">
                <p className="mb-1">Agents</p>
                <h2>{Agent.length}</h2>
              </div>
            </div>
          </div>

          <div className="col-12 col-sm-6 col-md-3">
            <div className="card my-card has-background-gradient-green">
              <div className="my-auto mx-auto">
                <span className="fa fa-hotel my-icon py-3 px-4"></span>
              </div>
              <div className="my-auto px-4">
                <p className="mb-1">Hotels</p>
                <h2>{Hotel.length}</h2>
              </div>
            </div>
          </div>  

          <div className="col-12 col-sm-6 col-md-3">
            <div className="card my-card has-background-gradient-orange">
              <div className="my-auto mx-auto">
                <span className="fa fa-suitcase my-icon py-3 px-4"></span>
              </div>
              <div className=" my-auto px-4">
                <p className="mb-1">Packages</p>
                <h2>{Packages.length}</h2>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Tables;
