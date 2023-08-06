import React, { useEffect, useState } from "react";
import PkgStyles from './Packages.module.css';
import Slider from 'react-slick';
import axios from "axios";
import { Variable } from "../../../Variables";
const cards = [
  {"image": "https://s3-us-west-2.amazonaws.com/s.cdpn.io/331810/sample87.jpg", "title": "Burgundy ", "subtitle": "Advertising"},
  {"image": "https://s3-us-west-2.amazonaws.com/s.cdpn.io/331810/sample119.jpg", "title": "Nigel Nigel", "subtitle": "Sound & Vision"},
  {"image": "https://s3-us-west-2.amazonaws.com/s.cdpn.io/331810/sample120.jpg", "title": "Caspian ", "subtitle": "Accounting"},
  {"image": "https://s3-us-west-2.amazonaws.com/s.cdpn.io/331810/sample120.jpg", "title": "Caspian ", "subtitle": "Accounting"},
  {"image": "https://s3-us-west-2.amazonaws.com/s.cdpn.io/331810/sample120.jpg", "title": "Caspian ", "subtitle": "Accounting"}
];
const Article = ({ data }) => {
  const { imageUrl, imageName} = data;

  return (
    <figure className={PkgStyles.snip1584}>
      <img src={`https://localhost:7075/uploads/gallery/${imageUrl}`}alt="" />
      <figcaption>
        <h3>{imageName}</h3>
      </figcaption>
    </figure>
  );
};

const News = () => {
  const [Gallery, setGallery] = useState([]);
  useEffect(() => {
    axios
      .get(Variable.AdminURL.Gallery)
      .then((response) => {
        setGallery(response.data);
      })
      .catch((error) => {
        console.error("Error fetching agent details:", error);
      });
  }, []);
  const settings = {
    dots: true,
    infinite: true,
    slidesToShow: 3,
    slidesToScroll: 1,
  };

  if (cards.length === 0) {
    return <p>Please add some cards</p>;
  }
  

  return (
    <div className={PkgStyles.news}>
      <Slider {...settings}>
        {Gallery.map((item, index) => (
          <div key={index}>
            <Article data={item} />
          </div>
        ))}
      </Slider>
    </div>
  );
};

export default News;
