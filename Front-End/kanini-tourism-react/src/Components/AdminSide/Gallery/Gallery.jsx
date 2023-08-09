import React, { useEffect, useState } from "react";
import "./Gallery.css";
import axios from "axios";
import { Variable } from "../../../Variables";

const Gallery = () => {
  const [Gallery, setGallery] = useState([]);
  const [selectedImage, setSelectedImage] = useState(null);
 const [place,setplace]=useState();
  const handleImageChange = (event) => {
    setSelectedImage(event.target.files[0]);
  };

  
  useEffect(() => {
    axios
      .get(Variable.AdminURL.Gallery)
      .then((response) => {
        setGallery(response.data);
      })
      .catch((error) => {
        console.error("Error fetching agent details:", error);
      });
  
  
  });
  const handleFormSubmit = async (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append('imageFile', selectedImage);
    formData.append('imageName', place);

    try {
      console.log(formData);
      const response = await fetch(Variable.AdminURL.Gallery, {
        method: 'POST',
        body: formData,
      });

      if (response.ok) {
       
        console.log('Image uploaded successfully');
      } else {

        console.error('Image upload failed');
      }
    } catch (error) {
      // Handle network errors
      console.error('Error occurred while uploading image:', error);
    }
  };
  return (
    <div>
      {localStorage.getItem('Role')==='Admin' && <div>
      <form onSubmit={handleFormSubmit} className="d-flex">
        <input type="text" placeholder="Place Name" name={place} onChange={(e)=>setplace(e.target.value)} className="form-control"/>
        <input type="file" onChange={handleImageChange} className=" form-control" />
        <button type="submit" className="btn btn-success">Upload Image</button>
      </form>
    </div>}
      <div id="gallery" className="container-fluid mt-5">
        
        {
          Gallery.map(img=>(
            <img
          src={`https://localhost:7075/uploads/gallery/${img.imageUrl}`}
          alt=" 1"
          className="img-responsive"
        />
          ))
        }
        
      
      </div>

      <div id="myModal" className="modal fade" role="dialog">
        <div className="modal-dialog">
          <div className="modal-content">
            <div className="modal-body">{/* Modal content here */}</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Gallery;
