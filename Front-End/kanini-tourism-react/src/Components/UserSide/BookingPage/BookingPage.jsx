
import React, { useState } from 'react';
import Drawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './BookingPage.css';
import Payment from './PaymentPage/Payment';
import classNames from 'classnames';

const BookingPage = ({ onClose }) => {
  const [state, setState] = useState({
    right: true,
  });

  const toggleDrawer = (anchor, open) => (event) => {
    if (event && event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }

    setState({ ...state, [anchor]: open });
  };

  const list = (anchor) => (
    <Box
      sx={{
        width: anchor === 'left' ? 'auto' : 650,
      }}
      role="presentation"
      onClick={toggleDrawer(anchor, false)}
      onKeyDown={toggleDrawer(anchor, true)}
    >
      {/* ... (content of the drawer list) ... */}
    </Box>
  );

  const [formData, setFormData] = useState({
    name: '',
    email: '',
    tour: '',
    hotels: '',
    numberOfPeople: 1,
    bookingDate: null,
    paymentAmount: 0,
  });

  const [formErrors, setFormErrors] = useState({
    name: '',
    email: '',
    tour: '',
    hotels: '',
    numberOfPeople: '',
    bookingDate: '',
    paymentAmount: '',
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleDateChange = (date) => {
    setFormData((prevData) => ({ ...prevData, bookingDate: date }));
  };

  const validateForm = () => {
    const errors = {};
    if (!formData.name.trim()) {
      errors.name = 'Name is required';
    }
    if (!formData.email.trim()) {
      errors.email = 'Email is required';
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      errors.email = 'Invalid email format';
    }
    if (!formData.tour.trim()) {
      errors.tour = 'Tour selection is required';
    }
    if (!formData.hotels.trim()) {
      errors.hotels = 'Hotels field is required';
    }
    if (formData.numberOfPeople <= 0) {
      errors.numberOfPeople = 'Number of People must be greater than 0';
    }
    if (formData.paymentAmount <= 0) {
      errors.paymentAmount = 'Payment Amount must be greater than 0';
    }
    if (!formData.bookingDate) {
      errors.bookingDate = 'Booking Date is required';
    }

    setFormErrors(errors);
    return Object.keys(errors).length === 0;
  };

  // Function to handle form submission and payment
  const handleBookAndPay = (e) => {
    e.preventDefault();
    const isValid = validateForm();
    if (isValid) {
      // Display the Payment component to initiate the payment process
      setPaymentOpen(true);
    } else {
      toast.error('Please check the form and try again.', {
        position: 'top-center',
      });
    }
  };

  // Function to get the current date in yyyy-MM-dd format
  const getCurrentDate = () => {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, '0');
    const day = String(currentDate.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  };

  // State to control the Payment component
  const [paymentOpen, setPaymentOpen] = useState(false);

  // Function to handle successful payment
  const handlePaymentSuccess = () => {
    toast.success('Booking Successful!', {
      position: 'top-center',
    });
    // Reset the form after submission (optional)
    setFormData({
      name: '',
      email: '',
      tour: '',
      hotels: '',
      numberOfPeople: 1,
      bookingDate: null,
      paymentAmount: 0,
    });
    setFormErrors({
      name: '',
      email: '',
      tour: '',
      hotels: '',
      numberOfPeople: '',
      bookingDate: '',
      paymentAmount: '',
    });
    setPaymentOpen(false);
  };

  return (
    <div className="bookingFormContainer">
      {['right'].map((anchor) => (
        <React.Fragment key={anchor}>
          <Drawer
            anchor={anchor}
            open={state[anchor]}
            onClose={onClose}
            onOpen={toggleDrawer(anchor, true)}
            sx={{
              '& .MuiBackdrop-root.MuiModal-backdrop': {
                backgroundColor: '#143b6f48',
              },
            }}
          >
            {list(anchor)}
            <div className="bookingFormWrapper">
              <form className="bookingForm" onSubmit={handleBookAndPay}>
                <div className="formHeadline">
                  <div>
                    <h1 className="createHead">Book Your Trip</h1>
                  </div>
                  <div className="crossleft">
                    <svg
                      xmlns="http://www.w3.org/2000/svg"
                      className="closeImg"
                      width="32"
                      height="32"
                      viewBox="0 0 32 32"
                      fill="none"
                      onClick={onClose}
                    >
                      {/* ... (rest of the SVG path) ... */}
                    </svg>
                  </div>
                </div>
                <div className={classNames('formControl', { formError: formErrors.name })}>
                  <label className="formLabel" htmlFor="name">
                    Name:
                  </label>
                  <input
                    type="text"
                    id="name"
                    name="name"
                    value={formData.name}
                    onChange={handleChange}
                    required
                    className="formInput"
                  />
                  {formErrors.name && <span className="formError">{formErrors.name}</span>}
                </div>
                <div className={classNames('formControl', { formError: formErrors.email })}>
                  <label className="formLabel" htmlFor="email">
                    Email:
                  </label>
                  <input
                    type="email"
                    id="email"
                    name="email"
                    value={formData.email}
                    onChange={handleChange}
                    required
                    className="formInput"
                  />
                  {formErrors.email && <span className="formError">{formErrors.email}</span>}
                </div>
                <div className={classNames('formControl', { formError: formErrors.tour })}>
                  <label className="formLabel" htmlFor="tour">
                    Select Tour:
                  </label>
                  <select
                    id="tour"
                    name="tour"
                    value={formData.tour}
                    onChange={handleChange}
                    required
                    className="formSelect"
                  >
                    <option value="">-- Select a Tour --</option>
                    <option value="Tour A">Tour A</option>
                    <option value="Tour B">Tour B</option>
                    <option value="Tour C">Tour C</option>
                  </select>
                  {formErrors.tour && <span className="formError">{formErrors.tour}</span>}
                </div>
                <div className={classNames('formControl', { formError: formErrors.hotels })}>
                  <label className="formLabel" htmlFor="hotels">
                    Hotels:
                  </label>
                  <select
                    type="text"
                    id="hotels"
                    name="hotels"
                    value={formData.hotels}
                    onChange={handleChange}
                    required
                    className="formSelect"
                  >
                    <option value="">-- Select a Hotel --</option>
                    <option value="Tour A">Tour A</option>
                    <option value="Tour B">Tour B</option>
                    <option value="Tour C">Tour C</option>
                  </select>
                  {formErrors.hotels && <span className="formError">{formErrors.hotels}</span>}
                </div>
                <div className="splitInput">
                  <div className={classNames('formControl', { formError: formErrors.numberOfPeople })}>
                    <label className="formLabel" htmlFor="numberOfPeople">
                      Number of People:
                    </label>
                    <input
                      type="number"
                      id="numberOfPeople"
                      name="numberOfPeople"
                      value={formData.numberOfPeople}
                      min="1"
                      max="10"
                      onChange={handleChange}
                      required
                      className="formInput"
                    />
                    {formErrors.numberOfPeople && (
                      <span className="formError">{formErrors.numberOfPeople}</span>
                    )}
                  </div>
                  <div className={classNames('formControl', { formError: formErrors.bookingDate })}>
                    <label className="formLabel" htmlFor="bookingDate">
                      Booking Date:
                    </label>
                    <DatePicker
                      selected={formData.bookingDate}
                      onChange={handleDateChange}
                      dateFormat="yyyy-MM-dd"
                      minDate={getCurrentDate()} // Set minDate to the current date
                      required
                      className="formDateInput"
                    />
                    {formErrors.bookingDate && (
                      <span className="formError">{formErrors.bookingDate}</span>
                    )}
                  </div>
                </div>
                <div className={classNames('formControl', { formError: formErrors.paymentAmount })}>
                  <label className="formLabel" htmlFor="paymentAmount">
                    Payment Amount:
                  </label>
                  <input
                    type="text"
                    id="paymentAmount"
                    name="paymentAmount"
                    value={formData.paymentAmount}
                    onChange={handleChange}
                    required
                    className="formInput"
                  />
                  {formErrors.paymentAmount && (
                    <span className="formError">{formErrors.paymentAmount}</span>
                  )}
                </div>
                <div className="formActions">
                  <button type="submit" className="formButton">
                    Proceed
                  </button>
                </div>
              </form>
              {paymentOpen && <Payment onClose={() => setPaymentOpen(false)} onSuccess={handlePaymentSuccess} />}
             
            </div>
          </Drawer>
        </React.Fragment>
      ))}
    </div>
  );
};

export default BookingPage;