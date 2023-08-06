import React from "react";
import "./UserFooter.css";

const UserFooter = () => {
  return (
    <div>
      <footer>
        <div class="row">
          <div class="column">
            <h4>About Us</h4>

            <p className="aboutPara">
              Discover a world of endless possibilities with us, as we curate
              immersive experiences that celebrate the rich tapestry of our
              heritage and the boundless wonders of nature.
            </p>
          </div>

          <div class="column">
            <h4>Quick Links</h4>

            <ul>
              <li>
                <a href="link">
                  <i class="fa fa-angle-right"></i> Subscription
                </a>
              </li>

              <li>
                <a href="link">
                  <i class="fa fa-angle-right"></i> Contact us
                </a>
              </li>

              <li>
                <a href="link">
                  <i class="fa fa-angle-right"></i> Bug report
                </a>
              </li>
            </ul>
          </div>

          <div class="column">
            <h4>Connect with Us</h4>

            <ul class="social-icons">
              <li>
                <a href="link">
                  <i class="fa-brands fa-facebook-f"></i>
                </a>
              </li>

              <li>
                <a href="link">
                  <i class="fa-brands fa-instagram"></i>
                </a>
              </li>

              <li>
                <a href="link">
                  <i class="fa-brands fa-twitter"></i>
                </a>
              </li>

              <li>
                <a href="link">
                  <i class="fa-brands fa-github"></i>
                </a>
              </li>
            </ul>
          </div>
        </div>

        <p class="copyright">© 2023 All Rights Reserved</p>
      </footer>
    </div>
  );
};

export default UserFooter;
