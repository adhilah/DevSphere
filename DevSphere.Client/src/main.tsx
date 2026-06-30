import { ToastContainer } from "react-toastify";
import ReactDOM from "react-dom/client";
import App from "./App";
//import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./index.css";

ReactDOM.createRoot(document.getElementById("root")!).render(
    <>
        <App />
        <ToastContainer
            position="top-right"
            autoClose={3000}
            newestOnTop
            closeOnClick
            pauseOnHover
            theme="dark"
            toastClassName="devsphere-toast"
            bodyClassName="devsphere-toast-body"
            progressClassName="devsphere-toast-progress"
        />
    </>
);