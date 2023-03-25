import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";
import Button from "../../components/Button";
import toastr from "toastr";
import styles from "../../styles/Home.module.css";
import authStyles from "../../styles/Auth.module.css";
import { useRegisterUser } from "../../api/ApiClient";

function Register() {

    const navigate = useNavigate();

    const [email, setEmail] = useState();

    const [emailError, setEmailError] = useState();

    const [disableButton, setDisableButton] = useState(true)

    const [firstName, setFirstName] = useState();

    const [firstNameError, setFirstNameError] = useState();

    const [lastName, setLastName] = useState();

    const [lastNameError, setLastNameError] = useState();

    const [password, setPassword] = useState();

    const [passwordError, setPasswordError] = useState();

    const registerUser = useRegisterUser();

    const handleValidation = (e) =>{
        const {name, value} = e.target;

        console.log(name, value)

        switch (name) {
          case "firstName":
            if (value === undefined || value === null || value.length === 0) {
              setFirstNameError("First name is required");
              setDisableButton(true);
            } else {
              setFirstNameError("");
              setFirstName(value);
              setDisableButton(false);
            }
            break;
          case "lastName":
            if (value === undefined || value === null || value.length === 0) {
              setLastNameError("Last name is required");
              setDisableButton(true);
            } else {
              setLastNameError("");
              setLastName(value);
              setDisableButton(false);
            }
            break;

          case "email":
            if (value === undefined || value === null || value.length === 0) {
              setEmailError("Email is required");
              setDisableButton(true);
            } else {
              setEmailError("");
              setEmail(value);
              setDisableButton(false);
            }
            break;

          case "password":
            if (value === undefined || value === null || value.length === 0) {
              setPasswordError("Password is required");
              setDisableButton(true);
            } 
            else if(value.length < 6){
                setPasswordError("Password must be at least 6 characters");
                setDisableButton(true);
            }
            
            else {
              setPasswordError("");
              setPassword(value);
              setDisableButton(false);
            }
            break;
          default:
        }
    }

     const handleSubmitForm = async (e) => {
       e.preventDefault();

       if (firstName === undefined || lastName === undefined || email === undefined || password === undefined){
            toastr.error("Fill the missing fields");
            return;
       }

       else{
         const formData = {
           firstName: firstName,
           lastName: lastName,
           email: email,
           password: password,
         };

         console.log(formData);

         await registerUser(formData)
           .then((response) => {
             if (response.data.successful) {
               console.log(response.data.result);

               localStorage.setItem("token", response.data.result.token);
               if (response.data.result.permission === 1) {
                 console.log("1 was");
                 localStorage.setItem("hasAccess", true);
                 navigate("category");
                 console.log("left 1");
               }
               else{
                localStorage.setItem("hasAccess", false);
               }
             } else {
               console.log(response.data.errorMessage);
               toastr.error(response.data.errorMessage);
             }
           })
           .catch((error) => {
             console.log(error);
           });
       }
     
     };




  return (
    <div className={styles.container}>
      <div className={styles.mainWrapper}>
        <h2 className={styles.mainTitle}>Register</h2>
        <form
          className={authStyles.formBox}
          onSubmit={(e) => handleSubmitForm(e)}
        >
          <div className={authStyles.formBox}>
            <input
              type="text"
              name="firstName"
              placeholder="Enter your first name"
              onChange={(e) => handleValidation(e)}
            />
            <span className={authStyles.validateText}>{firstNameError}</span>
            <input
              type="text"
              name="lastName"
              placeholder="Enter your last name"
              onChange={(e) => handleValidation(e)}
            />
            <span className={authStyles.validateText}>{lastNameError}</span>
            <input
              type="text"
              placeholder="Enter your email"
              name="email"
              onChange={(e) => handleValidation(e)}
            />
            <span className={authStyles.validateText}>{emailError}</span>
            <input
              type="password"
              placeholder="Enter your password"
              name="password"
              onChange={(e) => handleValidation(e)}
            />
            <span className={authStyles.validateText}>{passwordError}</span>
            <div className={authStyles.btnGroup}>
              <Button
                type="submit"
                name="Proceed"
                disabled={disableButton}
                color={disableButton ? "brown" : "bisque"}
                backgroundColor={disableButton ? "bisque" : "rgb(29, 26, 26)"}
              />
              &nbsp;&nbsp;
              <Link to="/">
                <Button name="Back" />
              </Link>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}

export default Register;
