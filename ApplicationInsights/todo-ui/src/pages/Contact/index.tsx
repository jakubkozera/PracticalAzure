import logo from "../../logo.svg";
import "./style.css";
import { Link } from "react-router-dom";

export function PageTwo() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>Contact page</p>
      </header>

      <div className="App-line"></div>
      <Link to="/" className="App-link">
        ToDo app
      </Link>
      <main className="App-main">
        <p>Contact..</p>
      </main>
    </div>
  );
}
