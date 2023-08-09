import React, { useEffect, useState } from "react";
import { Variable } from "../../../Variables";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const AdminAction = () => {
  const [agents, setAgents] = useState([]);

  const handleStatusUpdate = async (id) => {
    console.log('clicked');
    try {
      const response = await axios.put(`${Variable.AgentURL.GetAll}/Updatestatus/${id}`, {
        agentId:id,
        agentStatus: true,
      });
      window.location.reload()
      if (response.status === 200) {
        // Update the UI or take any other actions on success
      
        console.log("Agent status updated successfully.");
      }
    } catch (err) {
      console.error("Agent status update error:", err);
    }
  };
  const handleDelete = async (id) => {
    try {
      
        await axios.delete(`${Variable.AgentURL.GetAll}/${id}`);
        window.location.reload()
    } catch (error) {
      console.error('Error deleting agent:', error);
    }
  };
  var navigate=useNavigate()
  useEffect(() => {
    if (localStorage.getItem('Role') === 'Admin') {
      axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem(
        'AdminToken'
      )}`;
      axios
      .get(Variable.AgentURL.GetAll)
      .then((response) => {
        setAgents(response.data.filter(x=>x.agentStatus===false));
      })
      .catch((error) => {
        console.error("Error fetching agent details:", error);
      });
    }
    else {
      navigate('/AdminSignin')
    }
  });
  return (
    <div>
      <div className="container-fluid">
        <table id="users" className="table table-hover">
          <thead className="shadow">
            <tr>
              <th></th>
              <th>
                <i
                  className="fas fa-portrait"
                  style={{ marginRight: ".4rem" }}
                ></i>{" "}
                Image
              </th>
              <th>
                <i className="fas fa-user" style={{ marginRight: ".4rem" }}></i>{" "}
                Name
              </th>
              <th>
                <i className="fas fa-at" style={{ marginRight: ".4rem" }}></i>{" "}
                Email
              </th>
              <th>
                <i
                  className="fas fa-map-marker-alt"
                  style={{ marginRight: ".4rem" }}
                ></i>{" "}
                City
              </th>
              <th>
                <i
                  className="fas fa-check-circle"
                  style={{ marginRight: ".4rem" }}
                ></i>{" "}
                Status
              </th>
              <th>
                <i
                  className="far fa-calendar"
                  style={{ marginRight: ".4rem" }}
                ></i>{" "}
                Date of Registration
              </th>
              <th>
                <i className="fas fa-cogs" style={{ marginRight: ".4rem" }}></i>
                Action
              </th>
            </tr>
          </thead>
          <tbody>
            {agents.map((agent) => (
              <tr>
                <td>
                  <input type="checkbox" value="{{user.id}}" />
                </td>
                <td>
                  <img
                    className="rounded"
                    src={`https://localhost:7075/uploads/agent/${agent.agentImage}`}
                    style={{ width: "48px", height: "48px" }}
                    alt="User"
                  />
                </td>
                <td>{agent.agentName}</td>
                <td>{agent.agentEmail}</td>
                <td>
                  <span className="badge badge-primary"></span>
                  {agent.agentCity}
                </td>
                <td>
                  <span className="badge badge-primary"></span>
                  {agent.agentStatus === false ? "Not Verified" : "Verified"}
                </td>
                <td>
                  <span className="badge badge-success"></span>
                  {agent.agentRegistrationDate}
                </td>

                <td>
                    <button onClick={()=>handleStatusUpdate(agent.agentId)} className="btn btn-sm btn-success shadow"><i className="fas fa-check " ></i></button>
                  
                    <button className="btn btn-sm btn-danger shadow" onClick={()=>handleDelete(agent.agentId)}><i className="far fa-trash-alt "></i></button>
                  
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default AdminAction;
