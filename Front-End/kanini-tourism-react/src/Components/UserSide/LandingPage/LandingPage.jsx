import React, { useEffect } from 'react';
import './LandingPage.css';
import News from '../Packages/Packages';
import { Link } from 'react-router-dom';
import UserNavbar from '../UserNav/UserNavbar'
import UserFooter from '../User Footer/UserFooter'
class SliderClip {
  constructor(el) {
    this.el = el;
    this.slides = Array.from(this.el.querySelectorAll('li'));
    this.nav = Array.from(this.el.querySelectorAll('aside a'));
    this.totalSlides = this.slides.length;
    this.current = 0;
    this.autoPlay = true; // true or false
    this.timeTrans = 4000; // transition time in milliseconds
    this.indexElements = Array.from({ length: this.totalSlides }, (_, i) => i);
    this.setCurret();
    this.initEvents();
  }

  setCurret() {
    this.slides[this.current].classList.add('current');
    this.nav[this.current].classList.add('current_dot');
  }

  initEvents() {
    const self = this;

    this.nav.forEach((dot) => {
      dot.addEventListener('click', (ele) => {
        ele.preventDefault();
        this.changeSlide(this.nav.indexOf(dot));
      });
    });

    this.el.addEventListener('mouseenter', () => (self.autoPlay = false));
    this.el.addEventListener('mouseleave', () => (self.autoPlay = true));

    setInterval(function () {
      if (self.autoPlay) {
        self.current = self.current < self.totalSlides - 1 ? self.current + 1 : 0;
        self.changeSlide(self.current);
      }
    }, this.timeTrans);
  }

  changeSlide(index) {
    this.nav.forEach((allDot) => allDot.classList.remove('current_dot'));
    this.slides.forEach((allSlides) => allSlides.classList.remove('prev', 'current'));

    const getAllPrev = (value) => value < index;
    const prevElements = this.indexElements.filter(getAllPrev);

    prevElements.forEach((indexPrevEle) => this.slides[indexPrevEle].classList.add('prev'));

    this.slides[index].classList.add('current');
    this.nav[index].classList.add('current_dot');
  }
}

const LandingPage = () => {
  useEffect(() => {
    const slider = new SliderClip(document.querySelector('.slider'));
    console.log(slider);
    return () => {
      // Clean up the slider when the component is unmounted
    };
  }, []);

  return (
    <section>
      <UserNavbar/>
      <div className='Landing'>
      <section className="slider">
        <ul>
          <li>
            <article className="center-y padding_2x">
              <h3 className="big title">
                <em>W</em>anderlust <em>A</em>dventures
              </h3>
              <p>“Travel, because money returns. Time doesn’t.” </p>
              <Link to={'/Booking'} className="btn btn_3">Book Now</Link>
            </article>
          </li>
          <li>
            <article className="center-y padding_2x">
              <h3 className="big title">
                <em>Adventure</em> Awaits.
              </h3>
              <p>“Happiness is… a well-deserved vacation.”</p>
              <Link to={'/Booking'} className="btn btn_3">Book Now</Link>
            </article>
          </li>
          <li>
            <article className="center-y padding_2x">
              <h3 className="big title">
                <em>Travel </em> Beyond Boundaries
              </h3>
              <p>“Live your life by a compass, not a clock.”</p>
          
                <Link to={'/Booking'} className="btn btn_3">Book Now</Link>
            
            </article>
          </li>
          <aside>
            <a href="link"> </a>
            <a href="link"> </a>
            <a href="link"> </a>
          </aside>
        </ul>
        
      </section>
      <News/>
     
      
      
    </div>
    <UserFooter/>
    </section>
  );
};

export default LandingPage;
