import React, { useEffect, useState } from "react";
import Agstyle from "./Agents.module.css";
import axios from "axios";
import { Variable } from "../../../Variables";
const Agents = () => {
  const [agents, setAgents] = useState([]);

  useEffect(() => {
    axios
      .get(Variable.AgentURL.GetAll)
      .then((response) => {
        setAgents(response.data);
      })
      .catch((error) => {
        console.error("Error fetching agent details:", error);
      });
  }, []);

  return (
    <div style={{display:'flex'}}>
      {agents.map((agent) => (
        <div className={`card ${Agstyle.card} ${Agstyle.card2}`}>
          <div className={Agstyle.card_image}>
            <img
              src={`https://localhost:7075/uploads/agent/${agent.agentImage}`}
              style={{ height: "395px", width: "300px" }}
              alt=""
            />
          </div>
          <div className={Agstyle.card2_content}>
            <p>{agent.agentName}</p>
             <h5>Tourist  Agent</h5>
            <br />
            <b>{agent.agentCity}</b>
            <br />
            <div className={Agstyle.card2_icons}>
              <a href="fghj">
                <i className="fab fa-facebook-f"></i>
              </a>
              <a href="fghj">
                <i className="fab fa-instagram"></i>
              </a>
              <a href="fghj">
                <i className="fab fa-twitter"></i>
              </a>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};

export default Agents;
