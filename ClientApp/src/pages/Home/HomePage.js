import React, { useEffect, useState } from "react";
import MyNavbar from "../../components/Navbar/Navbar";
import { Container, useScrollTrigger } from "@mui/material";
import { reactLocalStorage } from "reactjs-localstorage";
import { useNavigate } from "react-router-dom";

function HomePage() {
    let navigate = useNavigate();
    let [allJobs, setAllJobs] = useState();

    let handleGetJobs = async () => {
        const response = await fetch('getalljob');
        console.log(response)
        const data = await response.json();
        console.log(data);
        setAllJobs(allJobs);
    }

    useEffect(() => {
        console.log(allJobs)
    }, [allJobs])



    useEffect(() => {
        if (!reactLocalStorage.getObject("userdata").isAuthenticated) {
            navigate('/');
        }

        handleGetJobs();


    })





    return (
        <div>
            <MyNavbar />
            <Container>
                <h1>View Jobs Here</h1>


            </Container>
        </div>
    )
};

export default HomePage;