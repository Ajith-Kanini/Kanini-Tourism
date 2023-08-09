import React, { useEffect, useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./BookingForm.css";
import Payment from "./PaymentPage/Payment";
import classNames from "classnames";
import axios from "axios";
import { Variable } from "../../../Variables";
import { jsPDF } from 'jspdf';
import 'jspdf-autotable';
const BookingForm = ({ onClose }) => {
  const [Hotel, setHotel] = useState([]);
  const [Packages, setPackages] = useState([]);
  const [amount, setamount] = useState([]);
  const [user, setuser] = useState({});
  const [offers, setOffers] = useState([]);
  const [formData, setFormData] = useState({
    name: "",
    email: "",
    tour: "",
    hotels: "",
    numberOfPeople: 1,
    bookingDate: null,
    offers:'',
    paymentAmount: 0,
  });
  

  const generatePaymentPDF = () => {

    const doc = new jsPDF();
    const currentDate = new Date();
    
    const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    
    const logo = new Image();
    logo.src = 'https://cdn-icons-png.flaticon.com/128/826/826070.png'; 
  
    logo.onload = () => {
      const logoWidth = 15; 
      const logoHeight = (logoWidth * logo.height) / logo.width; 
  
      doc.addImage(logo, 'PNG', 15, 8, logoWidth, logoHeight); 
      doc.text('Travel Travo', 55, 20); 
      doc.text('Tour Booking Invoice', 15, 40);
      const tableData = [
        ['Name', formData.name],
        ['Email ID', formData.email],
        ['Number of People', formData.numberOfPeople],
        ['Package Name',  'Kerala'],
        ['Hotel Name', 'Grand Hyatt Kochi Bolgatty , Kochi'],
        ['Booking Date', months[currentDate.getMonth()] + ' ' + currentDate.getDate() + ', ' + currentDate.getFullYear() + ' (' + days[currentDate.getDay()] + ')'],
        ['Total', 'INR ' + formData.paymentAmount],
      ];
  
      doc.autoTable({
        startY: 60, // Adjust the vertical position
        head: [['Description', 'Amount']],
        body: tableData,
      });
  
      doc.save('payment_receipt.pdf');
    };
  };
  
  
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

    //packages
    axios
      .get(Variable.PackagesURL.GetAll)
      .then((response) => {
        setPackages(response.data);
      })
      .catch((error) => {
        console.error("Error fetching package details:", error);
      });
    //user
    axios
      .get(Variable.UserURL.GetAll + "/"+localStorage.getItem('id'))
      .then((response) => {
        setuser(response.data);
      })
      .catch((error) => {
        console.error("Error fetching user details:", error);
      });
       //offers
    axios
    .get(Variable.OfferURL.GetAll)
    .then((response) => {
      setOffers(response.data);
    })
    .catch((error) => {
      console.error("Error fetching offer details:", error);
    });
  }, []);

  const [formErrors, setFormErrors] = useState({
    name: "",
    email: "",
    tour: "",
    hotels: "",
    numberOfPeople: "",
    bookingDate: "",
    paymentAmount: "",
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
      errors.name = "Name is required";
    }
    if (!formData.email.trim()) {
      errors.email = "Email is required";
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      errors.email = "Invalid email format";
    }
    if (!formData.tour.trim()) {
      errors.tour = "Tour selection is required";
    }
    if (!formData.hotels.trim()) {
      errors.hotels = "Hotels field is required";
    }
    if (formData.numberOfPeople <= 0) {
      errors.numberOfPeople = "Number of People must be greater than 0";
    }
    if (formData.paymentAmount <= 0) {
      errors.paymentAmount = "Payment Amount must be greater than 0";
    }
    if (!formData.bookingDate) {
      errors.bookingDate = "Booking Date is required";
    }

    setFormErrors(errors);
    return Object.keys(errors).length === 0;
  };

  // Function to handle form submission and payment
  const handleProceed = async (e) => {
    e.preventDefault(); 
    // validateForm();
      try {
        
        const response = await axios.post(Variable.UserURL.Booking, {
            bookingDate:new Date(),
            numberOfPersons:formData.numberOfPeople,
            bookingStatus:true,
            totalPrice:formData.paymentAmount,
            offerId:formData.offers,
            packageId:formData.tour,
            user:{
                userId:localStorage.getItem('id')
            }
        });
        console.log('Response:', response.data);
        alert('Booked successfully')
      } catch (error) {
        console.error('Error:', error);
      }
    
   
    
    
  };
  const handleBookAndPay = (e) => {
    e.preventDefault();
    const isValid = validateForm();
    if (isValid) {
      // Display the Payment component to initiate the payment process
      setPaymentOpen(true);
    } else {
      toast.error("Please check the form and try again.", {
        position: "top-center",
      });
    }
  };

  // Function to get the current date in yyyy-MM-dd format
  const getCurrentDate = () => {
    const currentDate = new Date();
    console.log(amount);
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, "0");
    const day = String(currentDate.getDate()).padStart(2, "0");
    return `${year}-${month}-${day}`;
  };

  // State to control the Payment component
  const [paymentOpen, setPaymentOpen] = useState(false);

  // Function to handle successful payment
  const handlePaymentSuccess = () => {
    toast.success("Booking Successful!", {
      position: "top-center",
    });
    // Reset the form after submission (optional)
    setFormData({
      name: "",
      email: "",
      tour: "",
      hotels: "",
      offers:'',
      numberOfPeople: 1,
      bookingDate: null,
      paymentAmount: 0,
    });
    setFormErrors({
      name: "",
      email: "",
      tour: "",
      hotels: "",
      offers:'',
      numberOfPeople: "",
      bookingDate: "",
      paymentAmount: "",
      
    });
    setPaymentOpen(false);
  };

  return (
    <div className="bookingFormContainer">
      {["right"].map((anchor) => (
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
            <div
              className={classNames("formControl", {
                formError: formErrors.name,
              })}
            >
              <label className="formLabel" htmlFor="name">
                Name:
              </label>
              <input
                type="text"
                id="name"
                name="name"
                value={user.fullName}
                onChange={handleChange}
                required
                className="formInput"
              />
              {formErrors.name && (
                <span className="formError">{formErrors.name}</span>
              )}
            </div>
            <div
              className={classNames("formControl", {
                formError: formErrors.email,
              })}
            >
              <label className="formLabel" htmlFor="email">
                Email:
              </label>
              <input
                type="email"
                id="email"
                name="email"
                value={user.email}
                onChange={handleChange}
                required
                className="formInput"
              />
              {formErrors.email && (
                <span className="formError">{formErrors.email}</span>
              )}
            </div>
            <div className="splitInput">
              <div
                className={classNames("formControl", {
                  formError: formErrors.tour,
                })}
              >
                <label className="formLabel" htmlFor="tour">
                  Select Tour:
                </label>
                <select
                  id="tour"
                  name="tour"
                  value={formData.tour}
                  onChange={(e) => {
                    setamount(e.target.value);
                    handleChange(e);
                  }}
                  required
                  className="formDateInput"
                >
                  <option value="">-- Select a Tour --</option>
                  {Packages.map((pkg) => (
                    <option key={pkg.packageId} value={pkg.packageId} >
                      {pkg.packageName}, {pkg.price}
                    </option>
                  ))}
                </select>
                {formErrors.tour && (
                  <span className="formError">{formErrors.tour}</span>
                )}
              </div>
              <div
                className={classNames("formControl", {
                  formError: formErrors.hotels,
                })}
              >
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
                  className="formDateInput"
                >
                  <option value="">-- Select a Hotel --</option>
                  {Hotel.map((hotel) => (
                    <option value={hotel.hotelId}>
                      {hotel.hotelName}, {hotel.hotelCity}
                    </option>
                  ))}
                </select>
                {formErrors.hotels && (
                  <span className="formError">{formErrors.hotels}</span>
                )}
              </div>
            </div>
            <div className="splitInput">
              <div
                className={classNames("formControl", {
                  formError: formErrors.numberOfPeople,
                })}
              >
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
              <div
                className={classNames("formControl", {
                  formError: formErrors.bookingDate,
                })}
              >
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
            <div className="splitInput">
              <div
                className={classNames("formControl", {
                  formError: formErrors.paymentAmount,
                })}
              >
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
                  className="formDateInput"
                />
                {formErrors.paymentAmount && (
                  <span className="formError">{formErrors.paymentAmount}</span>
                )}
              </div>
              <div
                className={classNames("formControl", {
                  formError: formErrors.hotels,
                })}
              >
                <label className="formLabel" htmlFor="hotels">
                  Offers:
                </label>
                <select
                  type="text"
                  id="offers"
                  name="offers"
                  value={formData.offers}
                  onChange={handleChange}
                  required
                  className="formDateInput"
                >
                  <option value="">-- Available Offers --</option>
                  {offers.map((hotel) => (
                    <option value={hotel.tourOfferId}>
                      {hotel.offerName}
                    </option>
                  ))}
                </select>
              </div>
            </div>
            <div className="formActions">
              <button
                type="submit"
                className="formButton"
                onClick={()=>setPaymentOpen(true)}
              >
                Proceed
              </button>
            </div>
          </form>
          {paymentOpen && (
            <Payment
              onClose={() => setPaymentOpen(false)}
              onSuccess={handlePaymentSuccess}
            
              amountpsd={formData.paymentAmount} 
            />
          )}
          <ToastContainer autoClose={3000} pauseOnHover />
               
          <button onClick={(e)=>{generatePaymentPDF(e);handleProceed(e)}} className="  formButton" style={{width:'50%', marginTop:'.3rem',marginLeft:'5rem'}}>Book Now</button>
        </div>
      ))}
    </div>
  );
};

export default BookingForm;

