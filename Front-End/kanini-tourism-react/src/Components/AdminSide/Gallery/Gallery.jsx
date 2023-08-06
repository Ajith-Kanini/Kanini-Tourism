import React, { useEffect, useState } from "react";
import "./Gallery.css";
import axios from "axios";
import { Variable } from "../../../Variables";

const Gallery = () => {
  const [Gallery, setGallery] = useState([]);

  const [file, setFile] = useState(null);



  const handleSubmit = async (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append("imageName", 'Image Name');
    formData.append("imageUrl", file);

    try {
      console.log(formData);
      const response = await axios.post(Variable.AdminURL.Gallery, formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      console.log("Image upload response:", response.data);
      // Clear form fields
      setFile(null);
      // setTitle("");
      // setDescription("");
    } catch (error) {
      console.error("Error uploading image:", error);
    }
  };
  const handleFileChange = (event) => {
    setFile(event.target.files[0]);
  }
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
  return (
    <div>
      <div className="form-group d-flex container" >
        <input type="file" id="file" onChange={handleFileChange} className="form-control"/>
        <button  onClick={handleSubmit} className="btn btn-success"> Save</button>
        </div>
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
