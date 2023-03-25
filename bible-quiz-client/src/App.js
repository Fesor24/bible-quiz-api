import Home from "./pages/Home";
import Category from "./pages/Category";
import ThousandQuestions from "./pages/ThousandQuestions";
import {
  createBrowserRouter,
  RouterProvider,
  Router,
  Routes,
  Route,
  BrowserRouter,
} from "react-router-dom";
import RevisionQuestions from "./pages/RevisionQuestions";
import FesorQuestions from "./pages/FesorQuestions";
import Login from "./pages/Auth/Login";
import Register from "./pages/Auth/Register";
import RequireAuth from "./components/Auth/requireAuth";

function App() {
  // const ThousandQuestionsWithAuth = RequireAuth(ThousandQuestions);

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="category" element={<Category />} />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route path="thousand-questions" element={<ThousandQuestions />} />
      </Routes>
    </BrowserRouter>
  );

  // const router = createBrowserRouter([
  //   {
  //     path: "/",
  //     element: <Home />,
  //   },
  //   {
  //     path: "/category",
  //     element: <Category />,
  //   },
  //   {
  //     path: "/thousand-questions",
  //     element: <ThousandQuestions />,
  //   },
  //   {
  //     path: "/revise-questions",
  //     element: <RevisionQuestions />
  //   },
  //   {
  //     path: "/fesor-questions",
  //     element: <FesorQuestions/>
  //   },
  //   {
  //     path: "/login",
  //     element: <Login />
  //   }
  // ]);

  // return (
  //   <>
  //     <RouterProvider router={router} />
  //   </>
  // );
}

export default App;
