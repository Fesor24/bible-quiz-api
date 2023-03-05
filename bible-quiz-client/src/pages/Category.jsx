import React, {useEffect} from "react";
import Button from "../components/Button";
import style from "../styles/Home.module.css";
import { Link } from "react-router-dom";

function Category() {

  useEffect(() => {
    document.title = "Category";
  }, [])

  return (
    <div className={style.container}>
      <h1>Select a category</h1>
      <Link to="/thousand-questions">
        <Button name="1000 Questions">
          <i class="fa fa-file-text" aria-hidden="true"></i>
        </Button>
      </Link>

      <Link to="/fesor-questions">
        <Button name="Fesor's Question">
          <i class="fa fa-user" aria-hidden="true"></i>
        </Button>
      </Link>

      <Link to="/revise-questions">
        <Button name="Revise Questions">
          <i class="fa-brands fa-think-peaks"></i>
        </Button>
      </Link>

      <Link to="/" className={style.link}>
        Back to home
      </Link>
    </div>
  );
}

export default Category;
