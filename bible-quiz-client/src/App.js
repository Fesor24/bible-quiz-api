import style from "../src/styles/App.module.css";
import { Link } from "react-router-dom";
import Button from "./components/Button";

function App() {
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

export default App;
