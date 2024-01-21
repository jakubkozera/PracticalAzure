import logo from "../../logo.svg";
import "./style.css";
import { Link } from "react-router-dom";
import useToDos from "./useToDos";
import { useState } from "react";

export function HomePage() {
  const { createTodo, todos } = useToDos();

  const [newTodo, setNewTodo] = useState("");

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>ToDo app</p>
      </header>

      <div className="App-line"></div>
      <Link to="/contact" className="App-link">
        Contact
      </Link>
      <main className="App-main">
        <p>Todos:</p>
        {todos.map((todo, i) => (
          <li className="todo">{todo.description}</li>
        ))}

        <p>Add new todo:</p>
        <div className="create-todo">
          <input value={newTodo} onChange={(e) => setNewTodo(e.target.value)} />
          <button onClick={() => createTodo(newTodo)}>Create</button>
        </div>
      </main>
    </div>
  );
}
