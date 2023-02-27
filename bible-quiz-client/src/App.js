import Home from "./pages/Home";
import Category from "./pages/Category";
import ThousandQuestions from "./pages/ThousandQuestions";
import { createBrowserRouter, RouterProvider } from "react-router-dom";


function App() {

  const router = createBrowserRouter([
    {
      path: "/",
      element: <Home />,
    },
    {
      path: "/category",
      element: <Category />,
    },
    {
      path: "/thousand-questions",
      element: <ThousandQuestions />,
    },
  ]);

  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}

export default App;
