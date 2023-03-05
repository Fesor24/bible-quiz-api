import React from 'react';
import { Link } from 'react-router-dom';
import Button from '../components/Button';
import style from '../styles/Home.module.css';
import { useEffect } from 'react';

function Home() {

  useEffect(() => {
    document.title = "Home"
  }, [])

  return (
    <div className={style.container}>
      <h1>gtcc quiz preparation</h1>

      <Link to="/category">
        <Button name="Get started">
          <i class="fa fa-play-circle" aria-hidden="true"></i>
        </Button>
      </Link>
    </div>
  );
}

export default Home;