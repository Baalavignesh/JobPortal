import React, { useEffect, useState, CSSProperties } from "react";
import MyNavbar from "../../components/Navbar/Navbar";
import { Container, useScrollTrigger, Button } from "@mui/material";
import { reactLocalStorage } from "reactjs-localstorage";
import { useNavigate } from "react-router-dom";
import HashLoader from "react-spinners/HashLoader";
import './Home.styles.css'

function HomePage() {
    let navigate = useNavigate();
    let [allJobs, setAllJobs] = useState([]);
    let [loading, setLoading] = useState(true);

    let handleGetJobs = async () => {
        const response = await fetch('getalljob');
        console.log(response)
        const data = await response.json();
        setAllJobs(data);

        setTimeout(() => {
            setLoading(false);
        }, 1000);
    }


    useEffect(() => {
        if (!reactLocalStorage.getObject("userdata").isAuthenticated) {
            navigate('/');
        }
        handleGetJobs();
    }, [])


    useEffect(() => {
        console.log(allJobs)
    }, [allJobs])


    return (
        <div className="homepage-main">
            <MyNavbar />
            <Container style={{height:"80%"}}>
                {!loading ? (
                    <div className="homepage-heading">
                        <h1 style={{textAlign:"center", margin:"3rem"}}>{allJobs.length} Jobs Available</h1>
                        {allJobs.map((job, index) => {
                            return <div key={index} className="jobcard">
                                <div>
                                    <h4>Job ID - {job.jobId}</h4>
                                    <h4>{job.jobName}</h4>
                                    <div>{job.jobDescribtion}</div>
                                </div>
                                <div className="job-button">
                                    <Button variant="outlined" color="primary">View Details</Button>
                                    <Button variant="contained" color="primary">Apply Now</Button>

                                    
                                </div>

                            </div>

                        })
                        }


                    </div>) : (
                    <div style={{ display: "flex", justifyContent: "center", alignItems: "center", flexDirection: "column", height: "100%" }}>
                        <h1 style={{margin:"2rem"}}>Fetching Data</h1>
                        <HashLoader
                            color="#faaaff"
                            loading={loading}
                            size={150}
                            aria-label="Loading Spinner"
                            data-testid="loader"
                        />
                    </div>
                )

                }

            </Container>
        </div>
    )
};

export default HomePage;