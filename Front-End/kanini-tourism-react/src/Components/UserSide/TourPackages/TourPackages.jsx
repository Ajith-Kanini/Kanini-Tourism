import axios from "axios";
import React from "react";
import { Variable } from "../../../Variables";
import TPstyles from "./TourPackages.module.css";
import AddPackage from "../../AgentSide/AddPackage/AddPackage";
import TourOffers from "./TourOffers";

const TourPackages = () => {
  const [TourPackages, setTourPackages] = React.useState([]);

  React.useEffect(() => {
    axios
      .get(Variable.PackagesURL.GetAll)
      .then((response) => {
        setTourPackages(response.data);
      })
      .catch((error) => {
        console.error("Error fetching package details:", error);
      });
  }, []);
  return (
    <div style={{margin:'2% 6%'}}>
      <div style={{display:'flex'}}>
      <h3>Popular Packages</h3>
      {localStorage.getItem('Role')==='Agent' && 
      <h3 style={{marginLeft:'auto'}}><AddPackage/></h3>}
      </div>
    <div className="row">
     
      {TourPackages.map((pkg) => (
        <div className="col-2">
          <div id={TPstyles.card_2} className={`${TPstyles.card} rounded`}>
            <div className={TPstyles.card__overlay}></div>
            <div className={TPstyles.card__image}>
              <img
                src={`https://localhost:7075/uploads/packages/${pkg.packageImage}`}
                alt=""
              />
            </div>
            <div className={TPstyles.card__heading}>
              <span className={TPstyles.small}>{pkg.description}</span>
              <h2>{pkg.packageName}</h2>
            </div>
          </div>
        </div>
      ))}
    </div>
    <TourOffers/>
    </div>
  );
};

export default TourPackages;
