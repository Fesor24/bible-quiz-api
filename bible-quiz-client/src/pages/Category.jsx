import React, {useEffect, useState} from "react";
import Button from "../components/Button";
import style from "../styles/Home.module.css";
import { Link } from "react-router-dom";
import { checkTokenForExpiry } from "../helper/coreFunction";

function Category() {

  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    document.title = "Category";

    const loggedIn = checkTokenForExpiry();

    setIsLoggedIn(loggedIn);

    
  }, [isLoggedIn]);

  const handleLogOut = () =>{
    setIsLoggedIn(false);
    localStorage.removeItem("token");
    localStorage.removeItem("hasAccess");
  }

  return (
    <div className={style.container}>
      <div className={style.mainWrapper}>
        <h1 className={style.mainTitle}>Sections</h1>
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

        {isLoggedIn ? (
          <>
            <Link to="/category">
              <Button name="Logout" click={handleLogOut}>
                <i class="fa-solid fa-arrow-right-from-bracket"></i>
              </Button>
            </Link>
          </>
        ) : (
          <>
            <Link to="/login">
              <Button name="Login">
                <i class="fa-solid fa-right-from-bracket"></i>
              </Button>
            </Link>
          </>
        )}

        <Link to="/" className={style.link}>
          Back to home
        </Link>
      </div>
    </div>
  );
}

export default Category;
