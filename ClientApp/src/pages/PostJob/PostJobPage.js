import React, { useEffect, useState } from "react";
import MyNavbar from "../../components/Navbar/Navbar";
import { Container, MenuItem, TextField } from "@mui/material";
import { reactLocalStorage } from "reactjs-localstorage";
import { Button } from "reactstrap";
import './postjob.styles.css'
import { useNavigate } from "react-router-dom";

function PostJobPage() {

    let [allSubCategory, setAllSubCategory] = useState();
    let [userId, setUserId] = useState();
    let [loading, setLoading] = useState(true);


    let navigate = useNavigate();

    let [jobInformation, setJobInformation] = useState({
        "JobName": "",
        "JobDescribtion": "",
        "SubCategoryId": 0,
        "EmployerId": 0,
        "isInserted": true
    });



    let GetSubCategory = async () => {
        const response = await fetch('getallsubcategory', {
            method: "GET",
            headers: {
                'Content-Type': 'application/json'
            },
        });
        const data = await response.json();

        const trimmedArray = data.map(({ subCategoryId, subCategoryName, categoryId }) => ({ subCategoryId, subCategoryName, categoryId }));
        setAllSubCategory(trimmedArray);
        console.log(trimmedArray)
        setLoading(false);
    }



    useEffect(() => {
        GetSubCategory();
        let data = reactLocalStorage.getObject('userdata');
        console.log(data)
        setUserId(data["userId"]);
    }, [])


    useEffect(() => {
        setJobInformation({
            ...jobInformation,
            ["EmployerId"]: userId
        });
    }, [userId])


    let handleInput = (e) => {

        if (e.target.name == "SubCategoryId") {
            const [subCategoryName, subCategoryId] = e.target.value.split(":");
            console.log(subCategoryId)

            setJobInformation({
                ...jobInformation,
                [e.target.name]: subCategoryId
            });
        }
        else {
            setJobInformation({
                ...jobInformation,
                [e.target.name]: e.target.value
            });
        }

    };

    let onSubmit = async () => {
        console.log(jobInformation);
        const response = await fetch('addjob', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(jobInformation),
        });
        const data = await response.json();
        navigate('/app');
    }


    return <div className="postjob-main">
        <MyNavbar />
        {
            !loading && <Container>
                <h1>Post Job Page</h1>
                <div className="job-fields">
                    <TextField fullWidth label="Job Name" id="fullWidth" name="JobName" onChange={handleInput} />
                    <TextField fullWidth label="Job Description" id="fullWidth" name="JobDescribtion" onChange={handleInput} />

                    <TextField
                        fullWidth
                        id="select-sub-category"
                        select
                        label="Sub Category"
                        name="SubCategoryId"
                        onChange={handleInput}
                    >
                        {allSubCategory.map((val, index) => (
                            <MenuItem key={val['subCategoryId']} value={`${val["subCategoryName"]}:${val["subCategoryId"]}`}>
                                {val['subCategoryName']}
                            </MenuItem>
                        ))}
                    </TextField>
                    <Button type="contained" color="primary" onClick={onSubmit}>Add Job</Button>

                </div>


            </Container>
        }

    </div>
}

export default PostJobPage;