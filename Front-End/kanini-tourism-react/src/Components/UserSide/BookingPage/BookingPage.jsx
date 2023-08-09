import * as React from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import BookingForm from './BookingForm';
import { useNavigate } from 'react-router-dom';

const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 700,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 2,
};

export default function BasicModal() {
  const [open, setOpen] = React.useState(false);
  const navigate=useNavigate()
  const handleOpen = () => {
   
     if(localStorage.getItem('Role')==='User' )
    {
      setOpen(true)
    }     
    else{
      navigate('/usersignin')
    }
   
  };
  const handleClose = () => setOpen(false);

  return (
    <div>
      <button onClick={handleOpen} className='btn btn-danger'>Book Now</button>
      <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"

      >
        <Box sx={style}>
          <BookingForm/>
        </Box>
      </Modal>
    </div>
  );
}