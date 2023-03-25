import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import Button from "../../components/Button";
import toastr from "toastr";
import styles from "../../styles/Home.module.css";
import authStyles from "../../styles/Auth.module.css";
import { useLoginUser } from "../../api/ApiClient";
import produce from "immer";

function Login() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    email: "",
    password: "",
  });

  const [formErrors, setFormErrors] = useState({
    email: "",
    password: "",
  });

  const [disableButton, setDisableButton] = useState(true);

  const loginUser = useLoginUser();

  const handleValidation = (e) => {
    const { name, value } = e.target;

    setFormData(
      produce((draft) => {
        draft[name] = value;
      })
    );

    switch (name) {
      case "email":
        setFormErrors(
          produce((draft) => {
            if (value === undefined || value === null || value.length === 0) {
              draft.email = "Email is required";
              setDisableButton(true);
            } else {
              draft.email = "";
              setDisableButton(false);
            }
          })
        );
        break;
      case "password":
        setFormErrors(
          produce((draft) => {
            if (value === undefined || value === null || value.length === 0) {
              draft.password = "Password is required";
              setDisableButton(true);
            } else {
              draft.password = "";
              setDisableButton(false);
            }
          })
        );
        break;
      default:
        break;
    }
  };

  const handleSubmitForm = async (e) => {
    e.preventDefault();

    if (formErrors.email || formErrors.password) {
      toastr.error("Fill the missing fields");
      return;
    }

      await loginUser(formData)
        .then((response) => {
          if (response.data.successful) {
            localStorage.removeItem("token");
            localStorage.setItem("token", response.data.result.token);

            if (response.data.result.permission === 1) {
              localStorage.setItem("hasAccess", true);
              navigate("category");
            }
            else{
              localStorage.setItem('hasAccess', false);
            }

            navigate("/category");
          } else {
            toastr.error(response.data.errorMessage);
          }
        })
        .catch((error) => {
          console.log(error)
          toastr.error("Unauthorized");
        })

      
   
  };

  return (
    <div className={styles.container}>
      <div className={styles.mainWrapper}>
        <h2 className={styles.mainTitle}>Login</h2>
        <form
          className={authStyles.formBox}
          onSubmit={(e) => handleSubmitForm(e)}
        >
          <div className={authStyles.formBox}>
            <input
              type="text"
              placeholder="Enter your email"
              name="email"
              onChange={(e) => handleValidation(e)}
            />
            <span className={authStyles.validateText}>{formErrors.email}</span>
            <input
              type="password"
              placeholder="Enter your password"
              name="password"
              onChange={(e) => handleValidation(e)}
            />
            <span className={authStyles.validateText}>
              {formErrors.password}
            </span>
            <div className={authStyles.btnGroup}>
              <Button
                type="submit"
                name="Proceed"
                disabled={disableButton}
                color={disableButton ? "rgb(29, 26, 26)" : "bisque"}
                backgroundColor={
                  disableButton ? "bisque" : "rgb(29, 26, 26)"
                }
              />
              &nbsp;&nbsp;
              <Link to="/">
                <Button name="Back" />
              </Link>
            </div>
          </div>
        </form>
        <p className={authStyles.validateText}>
          Don't have an account?
          <Link to="/register" className={authStyles.validateText}>
            {" "}
            Click to register
          </Link>
        </p>
      </div>
    </div>
  );
}

export default Login;