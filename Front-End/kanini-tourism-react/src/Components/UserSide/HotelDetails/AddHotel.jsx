import * as React from "react";
import Backdrop from "@mui/material/Backdrop";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import Fade from "@mui/material/Fade";
import axios from "axios";
import { Variable } from "../../../Variables";
import { Button, TextField } from "@mui/material";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 500,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

export default function AddHotel() {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [formData, setFormData] = React.useState({
    name: "",
    email: "",
    description: "",
    price: "",
    duration: "",
    imageFile: null, // To store the selected image file
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleImageChange = (event) => {
    const file = event.target.files[0];
    setFormData((prevData) => ({ ...prevData, imageFile: file }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const formDataToSend = new FormData();
      formDataToSend.append("hotelName", formData.name);
      formDataToSend.append("hotelAddress", formData.email);
      formDataToSend.append("hotelCity", formData.description);
      formDataToSend.append("hotelCountry", formData.price);
      formDataToSend.append("starRating", formData.duration);
      formDataToSend.append("imageFile", formData.imageFile);
      console.log(formDataToSend);
      await axios.post(Variable.HotelURL.GetAll, formDataToSend, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });

      alert("New Hotel Added Successfully");
      window.location.reload();
    } catch (error) {
      console.error("Error:", error);
    }
  };

  React.useEffect(() => {}, []);

  return (
    <div>
      <button id="popup" className="btn btn-success" onClick={handleOpen}>
        Add
      </button>
      <Modal
        aria-labelledby="transition-modal-title"
        aria-describedby="transition-modal-description"
        open={open}
        onClose={handleClose}
        closeAfterTransition
        slots={{ backdrop: Backdrop }}
        slotProps={{
          backdrop: {
            timeout: 500,
          },
        }}
      >
        <Fade in={open}>
          <Box component="form" onSubmit={handleSubmit} sx={style}>
            <h3>Add New Hotel</h3>
            <div style={{ display: "flex" }}>
              <TextField
                label="Hotel Name"
                variant="outlined"
                name="name"
                onChange={handleChange}
                required
                margin="normal"
              />

              <TextField
                label="Address"
                variant="outlined"
                name="email"
                onChange={handleChange}
                required
                margin="normal"
                sx={{ marginLeft: "auto" }}
              />
            </div>
            <div style={{ display: "flex" }}>
              <TextField
                label="Country"
                variant="outlined"
                name="price"
                onChange={handleChange}
                required
                margin="normal"
              />

              <TextField
                label="starRating"
                variant="outlined"
                name="duration"
                type="number"
                onChange={handleChange}
                required
                margin="normal"
                sx={{ marginLeft: "auto" }}
              />
            </div>
            <TextField
              label="Hotel Image"
              variant="outlined"
              name="imageFile"
              type="file"
              onChange={handleImageChange}
              required
              fullWidth
              margin="normal"
            />
            <TextField
              label="City"
              variant="outlined"
              name="description"
              
              rows={4}
              value={formData.description}
              onChange={handleChange}
              required
              fullWidth
              margin="normal"
            />

            <Button type="submit" variant="contained" color="primary">
              Submit
            </Button>
          </Box>
        </Fade>
      </Modal>
    </div>
  );
}
