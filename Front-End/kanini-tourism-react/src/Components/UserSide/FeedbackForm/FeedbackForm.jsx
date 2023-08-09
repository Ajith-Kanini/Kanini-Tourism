import * as React from "react";
import Backdrop from "@mui/material/Backdrop";
import Box from "@mui/material/Box";
import Modal from "@mui/material/Modal";
import Fade from "@mui/material/Fade";
import { TextField, Button } from "@mui/material";
import "./FeedbackForm.css";
import axios from "axios";
import { Variable } from "../../../Variables";
const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

export default function TransitionsModal() {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);
  const [user, setuser] = React.useState({});
  const [formData, setFormData] = React.useState({
    name: "",
    email: "",
    description: "",
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleSubmit = async(event) => {
    event.preventDefault();
    try {
        await axios.post(Variable.FeedBackURL.GetAll, {
            userName:formData.name,
            userEmail:formData.email,
            feedbackDescription:formData.description
        });
        alert('Thank you for your feedback')
        window.location.reload()
      } catch (error) {
        console.error("Error:", error);
      }
  };
  React.useEffect(() => {
    //user
    axios
      .get(Variable.UserURL.GetAll + "/"+localStorage.getItem('id'))
      .then((response) => {
        setuser(response.data);
      })
      .catch((error) => {
        console.error("Error fetching user details:", error);
      });
       
  }, []);
  return (
    <div>
      <button id="popup" className="feedback-button" onClick={handleOpen}>
        Feedback
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
        <Box component="form" onSubmit={handleSubmit} sx={style} >
            <h3>Feedback Form</h3>
      <TextField
        label="Name"
        variant="outlined"
        name="name"
        value={user.userName}
        onChange={handleChange}
        required
        fullWidth
        margin="normal"
      />

      <TextField
        label="Email"
        variant="outlined"
        name="email"
        type="email"
        value={user.userEmail}
        onChange={handleChange}
        required
        fullWidth
        margin="normal"
      />

      <TextField
        label="Description"
        variant="outlined"
        name="description"
        multiline
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
