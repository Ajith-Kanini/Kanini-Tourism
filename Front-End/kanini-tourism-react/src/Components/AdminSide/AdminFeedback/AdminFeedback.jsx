import axios from "axios";
import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { Variable } from "../../../Variables";

const AdminFeedback = () => {
    const [Feedback, setFeedback] = useState([]);
  useEffect(() => {
    //Feedback
    axios
      .get(Variable.FeedBackURL.GetAll)
      .then((response) => {
        setFeedback(response.data);
      })
      .catch((error) => {
        console.error("Error fetching Feedback details:", error);
      });
  }, []);
  return (
    <div className="container mt-5">
      <h2>Feedback Management</h2>
      <table className="table table-hover">
        <thead >
          <tr >
            <th scope="col">S.No</th>
            <th scope="col">Name</th>
            <th scope="col">Email</th>
            <th scope="col">Message</th>
          </tr>
        </thead>
        <tbody>
          {
            Feedback.map((fd,index)=>(
                <tr>
            <th scope="row" key={fd.feedbackId}>{index+1}</th>
            <td>{fd.userName}</td>
            <td>{fd.userEmail}</td>
            <td>{fd.feedbackDescription}</td>
          </tr>
            ))
          }
        </tbody>
      </table>
    </div>
  );
};

export default AdminFeedback;
