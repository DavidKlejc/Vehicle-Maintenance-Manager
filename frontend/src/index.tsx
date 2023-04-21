import React from "react";
import ReactDOM from "react-dom/client";
import "./styles/index.css";
import App from "./App";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Vehicle from "./components/Vehicle";
import CreateOwner from "./components/CreateOwner";

function Root() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <App />,
    },
    {
      path: "/Owner/:ownerID",
      element: <Vehicle />,
    },
    {
      path: "/Owner/Create",
      element: <CreateOwner />,
    },
  ]);

  return (
    <React.StrictMode>
      <RouterProvider router={router} />
    </React.StrictMode>
  );
}

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(<Root />);
