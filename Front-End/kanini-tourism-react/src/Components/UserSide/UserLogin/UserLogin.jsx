import axios from "axios";
import React, { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Variable } from "../../../Variables";
import { toast } from "react-toastify";
const UserLogin = () => {
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
  const handleRegister = async (event) => {
    event.preventDefault();
       validateForm();
       try {
        const response = await axios.post(Variable.UserURL.Login, {
          email: formData.email,
          password: formData.Password
        });
      
        localStorage.setItem('UserToken',response.data)
        localStorage.setItem('Role','User')
        localStorage.setItem('email',formData.email)
        toast.success('Successfully logged in')
        navigate('/landingpage')
        window.location.reload()
        
      
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

  return (
    <div>
      <span className="background"></span>
      <span className="centering">
        <div className="my-form">
          <span className="signup-welcome-row">
            <img
              className="signup-welcome"
              src="https://cdn-icons-png.flaticon.com/128/10614/10614531.png"
              alt=""
            />
            <h4>Sign In!</h4>
          </span>
          <span className="socials-row">
            <a href="link" title="Use Google">
              <img
                src="https://cdn-icons-png.flaticon.com/128/300/300221.png"
                alt="Google"
              />
              Use Google
            </a>
            <a href="link" title="Use Facebook">
              <img
                src="https://cdn-icons-png.flaticon.com/128/5968/5968764.png"
                alt="Facebook"
              />
              Use Facebook
            </a>
          </span>
          <span className="divider">
            <span className="divider-line"></span>
            OR
            <span className="divider-line"></span>
          </span>

          <div className="my-form__content">
            <div className="text-field">
              <label for="email">Email:</label>
              <input
                aria-label="Email"
                type="email"
                id="email"
                name="email"
                placeholder="Your Email"
                autocomplete="off"
                onChange={handleInputChange}
                required
              />
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="icon icon-tabler icon-tabler-mail"
                width="44"
                height="44"
                viewBox="0 0 24 24"
                stroke-width="1.5"
                stroke="#A7A2CB"
                fill="none"
                stroke-linecap="round"
                stroke-linejoin="round"
              >
                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                <path d="M3 7a2 2 0 0 1 2 -2h14a2 2 0 0 1 2 2v10a2 2 0 0 1 -2 2h-14a2 2 0 0 1 -2 -2v-10z" />
                <path d="M3 7l9 6l9 -6" />
              </svg>
            </div>
            {formErrors.email && <div className="error">{formErrors.email}</div>}
            <div className="text-field">
              <label for="password">Password:</label>
              <input
                aria-label="Password"
                type="Password"
                id="Password"
                name="Password"
                placeholder="Your Password"
                autocomplete="off"
                onChange={handleInputChange}
                required
              />
              <svg
                xmlns="http://www.w3.org/2000/svg"
                className="icon icon-tabler icon-tabler-password"
                width="44"
                height="44"
                viewBox="0 0 24 24"
                stroke-width="1.5"
                stroke="#A7A2CB"
                fill="none"
                stroke-linecap="round"
                stroke-linejoin="round"
              >
                <path stroke="none" d="M0 0h24v24H0z" fill="none" />
                <rect x="3" y="6" width="18" height="12" rx="2" />
                <circle cx="7" cy="12" r="1" />
                <circle cx="17" cy="12" r="1" />
              </svg>
            </div>
            {formErrors.Password && <div className="error">{formErrors.Password}</div>}
            <div className="error">{loginError}</div>
            {/* <!--? more fields --> */}
            <input type="submit" className="my-form__button" value="Sign-In" onClick={handleRegister}/>
          </div>
          <div className="my-form__actions">
            <span>
              By Signig in you agree to our
              <a href="link" title="Reset Password">
                Terms
              </a>
              and{" "}
              <a href="link" title="Reset Password">
                Privacy
              </a>
            </span>
            <div className="my-form__signin">
              <a title="Sign In" href="link">
                <Link to={"/UserRegister"}>Sign-up</Link>
              </a>
            </div>
          </div>
        </div>
      </span>
    </div>
  );
};

export default UserLogin;






