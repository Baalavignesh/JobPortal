import React, { useEffect } from "react";
import MyNavbar from "../../components/Navbar/Navbar";
import { reactLocalStorage } from "reactjs-localstorage";
import { useNavigate } from "react-router-dom";
import { Button, TextField } from "@mui/material";
import './admin.styles.css'



function AdminPage() {

    let navigate = useNavigate();
    useEffect(() => {
        if (!reactLocalStorage.getObject("userdata").isAuthenticated || reactLocalStorage.getObject("userdata").role != "Admin") {
            navigate('/app');
        }
    });

    return (
        <div className="admin-main">
            <MyNavbar />

            <div className="admin-controls">
                <Button variant="contained" color="success">View Category</Button>
                <Button variant="contained" color="success">View Sub Category</Button>
                <Button variant="contained" color="primary">Add Category</Button>



                <Button variant="contained" color="primary">Add Sub Category</Button>
                <Button variant="contained" color="error">Delete Category</Button>
                <Button variant="contained" color="error"> Delete Sub Category</Button>
            </div>
        </div>
    )

}

export default AdminPage;
