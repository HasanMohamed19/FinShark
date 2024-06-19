import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import SearchPage from "../Pages/SearchPage/SearchPage";
import CompanyPage from "../Pages/CompanyPage/CompanyPage";
import ComapnyProfile from "../Components/CompanyProfile/ComapnyProfile";
import IncomeStatement from "../Components/IncomeStatement/IncomeStatement";

export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            {path: "", element: <HomePage />},
            {path: "search", element: <SearchPage />},
            {path: "company/:ticker",
                element: <CompanyPage />,
                children: [
                    {path: "company-profile", element: <ComapnyProfile />},
                    {path: "income-statement", element: <IncomeStatement />},
                ]
            },
        ]
    }
]);