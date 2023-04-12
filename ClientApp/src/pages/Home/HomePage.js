import React, { useEffect } from "react";
import MyNavbar from "../../components/Navbar/Navbar";
import { Container } from "@mui/material";
import { reactLocalStorage } from "reactjs-localstorage";
import { useNavigate } from "react-router-dom";

function HomePage() {
    let navigate = useNavigate();

    useEffect(() => {
        if (!reactLocalStorage.getObject("userdata").isAuthenticated) {
            navigate('/');
        }
    })

    return (
        <div>
            <MyNavbar />
            <Container>
                Welcome

            </Container>
        </div>
    )
};

export default HomePage;