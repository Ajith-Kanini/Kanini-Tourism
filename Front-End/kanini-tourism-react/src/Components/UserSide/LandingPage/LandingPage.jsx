import React from "react";
import LPStyle from "./LandingPAge.module.css";
import News from "../Packages/Packages";
import Agents from "../../AgentSide/AgentsProfiles/Agents";
import { Link } from "react-router-dom";
import BasicModal from "../BookingPage/BookingPage";
import { Button } from "react-bootstrap";
import TransitionsModal from "../FeedbackForm/FeedbackForm";
const LandingPage = () => {
  return (
    <section>
      <div>

        <section className={LPStyle.section}>
          <section className={LPStyle.headline}>
          <Link > {localStorage.getItem('Role')==='Agent' ?  <Link to={'/packages'}><Button style={{backgroundColor:'red'}}>Manage Package & Offers</Button></Link>: <BasicModal/> }</Link>
          </section>
          <section className="mt-3">
          <TransitionsModal/>
            <h3>Popular Destinations</h3>
          <News/>
          </section>
          <section className="mt-3">
            <h3>Our Agents</h3>
          <Agents/>
          </section>

        </section>
        <div class={LPStyle.home}>
          <div class={LPStyle.home_popular}>
            <div class={LPStyle.slider}>
              <ul>
                <li>
                  <img
                    className={`${LPStyle.item} ${LPStyle.small1}`}
                    src="https://img.freepik.com/free-photo/close-up-doctor-with-stethoscope_23-2149191355.jpg?w=1060&t=st=1688505511~exp=1688506111~hmac=8262a286f5a0698b18ca4565301caf23e9ad09cd90c683455c4b012b5c3773d4"
                    alt="imgage"
                  />
                </li>
                <li>
                  <img
                    className={`${LPStyle.item} ${LPStyle.big1}`}
                    src="https://img.freepik.com/free-photo/travel-concept-with-worldwide-landmarks_23-2149153263.jpg?size=626&ext=jpg&ga=GA1.2.41053441.1676020414&semt=sph"
                    alt="imgage"
                  />
                </li>

                <li>
                  <img
                    className={`${LPStyle.item} ${LPStyle.focus}`}
                    src="https://img.freepik.com/free-vector/vacation-holidays-background-with-realistic-globe-suitcase-photo-camera_1284-10476.jpg?1&w=740&t=st=1690798647~exp=1690799247~hmac=f0ddf58e95519bf46bc133ba8b62d481c3dfe4eda4e8fcdd44a8af1acc9e0da5"
                    alt="imgage"
                  />
                </li>

                <li>
                  <img
                    className={`${LPStyle.item} ${LPStyle.big2}`}
                    src="https://img.freepik.com/free-photo/travel-concept-with-worldwide-landmarks_23-2149153263.jpg?size=626&ext=jpg&ga=GA1.2.41053441.1676020414&semt=sph"
                    alt="imgage"
                  />
                </li>

                <li>
                  <img
                    className={`${LPStyle.item} ${LPStyle.small2}`}
                    src="https://img.freepik.com/free-photo/smiling-young-female-doctor-holding-clipboard-hospital_231208-13041.jpg?w=1060&t=st=1688505617~exp=1688506217~hmac=9f0f633b5ce08dfabe669f0b1363ac4bf7012157ed287f35a1350bc586037d03"
                    alt="imgage"
                  />
                </li>
              </ul>
            </div>
          </div>
          <div class={LPStyle.home_header}>
            <h1>Experience</h1>
            <h1 className={LPStyle.hhead}>World-Class</h1>
            <h1>Travel</h1>
          </div>
        </div>
        
        <footer class={LPStyle.footer}>
          <div class={LPStyle.waves}>
            <div class={LPStyle.wave} id={LPStyle.wave1}></div>
            <div class={LPStyle.wave} id={LPStyle.wave2}></div>
            <div class={LPStyle.wave} id={LPStyle.wave3}></div>
            <div class={LPStyle.wave} id={LPStyle.wave4}></div>
          </div>
          <ul class="menu">
            <li class={LPStyle.menu__item}>
              <a class={LPStyle.menu__link} href="home">
                Home
              </a>
            </li>
            <li class={LPStyle.menu__item}>
              <a class={LPStyle.menu__link} href="About">
                About
              </a>
            </li>
            <li class={LPStyle.menu__item}>
              <a class={LPStyle.menu__link} href="Services">
                Services
              </a>
            </li>
            <li class={LPStyle.menu__item}>
              <a class={LPStyle.menu__link} href="Team">
                Team
              </a>
            </li>
            <li class={LPStyle.menu__item}>
              <a class={LPStyle.menu__link} href="Contact">
                Contact
              </a>
            </li>
          </ul>
          <p>&copy;2023 UNIQUE HEALTHCARE | All Rights Reserved</p>
        </footer>
      </div>
    </section>
  );
};

export default LandingPage;
