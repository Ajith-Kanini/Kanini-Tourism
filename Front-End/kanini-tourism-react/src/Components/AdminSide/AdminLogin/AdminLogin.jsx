import React, { useState } from "react";
import "./AdminLogin.css";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import { Variable } from "../../../Variables";
import { toast } from "react-toastify";
const AdminLogin = () => {
   const [formErrors, setFormErrors] = useState({});
   const [loginError, setLoginError] = useState('');
   const navigate=useNavigate();
   var [formData, setFormData] = useState({
     Name: '',
     email: '',
     Password: '',
     cpassword: ''
   });
  
   
   const reset = {
     Name: '',
     email: '',
     Password: ''
   }
   const validateForm = () => {
     let errors = {};
     if (!formData.email) {
       errors.email = 'Email is required';
     } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
       errors.email = 'Invalid email address';
     }
 
     if (!formData.Password) {
       errors.Password = 'Password is required';
     }
 
     if (!formData.Name) {
       errors.Name = 'Username is required';
     }
     if (formData.Password !== formData.cpassword) {
       errors.cpassword = 'Passwords do not match';
     }
     setFormErrors(errors);
 
     return Object.keys(errors).length === 0;
   };

   const handleInputChange = (e) => {
      const { name, value } = e.target;
  
      // Perform validation checks for the specific input
      let error = '';
      if (name === 'email') {
        if (!value) {
          error = 'Email is required';
        } else if (!/\S+@\S+\.\S+/.test(value)) {
          error = 'Invalid email address';
        }
      } else if (name === 'Password') {
        if (!value) {
          error = 'Password is required';
        } else if (
          !/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}/.test(value)
        ) {
          error =
            'Password must contain at least 8 characters, one uppercase letter, one lowercase letter, one number, and one special character';
        }
      } else if (name === 'Name') {
        if ( !value) {
          error = 'Username is required';
        } else if ( value.length < 5) {
          error = 'Username must be at least 5 characters long';
        } else if ( !/^[a-zA-Z]+$/.test(value)) {
          error = 'Username can only contain alphabetic characters';
        }
      } else if (name === 'cpassword') {
        if (value !== formData.Password) {
          error = 'Passwords do not match';
        }
      }
  
      // Set the formErrors state with the error for the specific input
      setFormErrors((prevErrors) => ({
        ...prevErrors,
        [name]: error,
      }));
  
      setFormData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    };
    const handleRegister = async (event) => {
      event.preventDefault();
         validateForm();
         try {
          const response = await axios.post(Variable.AdminURL.Login, {
            adminEmailId: formData.email,
            adminPassword: formData.Password
          });
        
          localStorage.setItem('AdminToken',response.data)
          localStorage.setItem('Role','Admin')
          toast.success('Successfully logged in')
          window.location.reload()
          navigate('/Dashboard')
          
          setFormData(reset)
        } catch (error) {
         
          if (error.response) {
            setLoginError('Invalid credentials')
          } else if (error.request) {
            setLoginError('Invalid credentials')
          
          } else {
            setLoginError('Invalid credentials')
           
          }
        }
        
    };
  
  return (
    <div className="AdminLogin">
      <div className="c1">
        <form className="signin">
          <h1 className="signup1">SIGN IN</h1>
          <br />
          <input
            name="email"
            type="text"
            placeholder="Username*"
            className="username"
            onChange={handleInputChange}
          />
          {formErrors.email && <div className="error">{formErrors.email}</div>}

          <input
            name="Password"
            type="password"
            placeholder="Password*"
            className="username"
            onChange={handleInputChange}
          />
          {formErrors.Password && <div className="error">{formErrors.Password}</div>}
          <div className="error">{loginError}</div>
          <button className="btn btn-danger mt-3" onClick={handleRegister} style={{width:'60%',marginLeft:'20%'}}>Sign in</button>

          <br />
         
          <br />
          <a href="link">
            <p className="signup2">Forget Password?</p>
          </a>
        </form>
      </div>
    </div>
  );
};

export default AdminLogin;
