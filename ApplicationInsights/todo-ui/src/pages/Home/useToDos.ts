import axios from "axios";
import { useCallback, useEffect, useState } from "react";

export interface ToDo {
  id: string;
  description: string;
  isFinished: boolean;
}

const useToDos = () => {
  const apiBaseUrl =
    window.location.hostname === "localhost"
      ? "https://localhost:7206"
      : "https://sample1-api.azurewebsites.net";

  const [todos, setTodos] = useState<ToDo[]>([]);

  const fetchTodos = useCallback(async () => {
    const { data: todos } = await axios.get<ToDo[]>(`${apiBaseUrl}/api/todos`);
    setTodos(todos);
  }, [setTodos]);

  const createTodo = async (description: string) => {
    const { data: todos } = await axios.post<ToDo[]>(
      `${apiBaseUrl}/api/todos`,
      { description }
    );
    setTodos(todos);
  };

  useEffect(() => {
    fetchTodos();
  }, [fetchTodos]);

  return {
    createTodo,
    todos,
  };
};

export default useToDos;
