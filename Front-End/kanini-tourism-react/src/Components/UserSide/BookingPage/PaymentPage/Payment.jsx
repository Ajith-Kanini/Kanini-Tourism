import React from 'react';

const Payment = ({ onSuccess }) => {
  const [amount, setAmount] = React.useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    if (amount === '') {
      alert('Please enter the amount');
    } else {
      var options = {
        key: 'rzp_test_ptZiEyaQBXWK5X',
        key_secret: '5L4mup2uHzEmhrBoDEQEYY31',
        amount: amount * 100,
        currency: 'INR',
        name: 'TRAVELTRAVO',
        description: 'Payment Testing',
        prefill: {
          name: 'Ajith',
          email: 'traveltravo.info@gmail.com',
          contact: '9360604742',
        },
        notes: {
          address: 'Razorpay Corporate office',
        },
        theme: {
          color: '#3399cc',
        },
      };
      var pay = new window.Razorpay(options);
      pay.open();

      // After payment is successful, handle the success event
      pay.on('payment.success', (response) => {
        // Call the onSuccess callback from the BookingForm component
        onSuccess();
      });
    }
  };

  return (
    <div>
      <div className="App">
        <br />
        <input
          type="text"
          placeholder="Enter Amount"
          className="formInput"
          value={amount}
          onChange={(e) => setAmount(e.target.value)}
        />
        <br />
        <br />
        <button type="submit" className="formButton" onClick={handleSubmit}>
          Pay Now
        </button>
      </div>
    </div>
  );
};

export default Payment;