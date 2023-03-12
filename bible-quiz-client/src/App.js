import Home from "./pages/Home";
import Category from "./pages/Category";
import ThousandQuestions from "./pages/ThousandQuestions";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import RevisionQuestions from "./pages/RevisionQuestions";
import FesorQuestions from "./pages/FesorQuestions";


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
    {
      path: "/revise-questions",
      element: <RevisionQuestions />
    },
    {
      path: "/fesor-questions",
      element: <FesorQuestions/>
    }
  ]);

  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}

export default App;
