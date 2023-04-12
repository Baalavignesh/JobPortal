import React, { useState } from "react";
import { FormControl, TextField, InputLabel, Select, MenuItem } from '@mui/material'
import './Auth.styles.css'
import { Button } from "reactstrap";
import { useNavigate } from "react-router-dom";
import {reactLocalStorage} from 'reactjs-localstorage';
 


function SignupPage() {

    const role = ["Employer", "JobSeeker", "Admin"]

    let [userData, setUserData] = useState({
        username: "",
        password: "",
        role: ""
    });

    let navigate = useNavigate();

    let handleChange = ((event) => {
        console.log(event.target.name, event.target.value)
        setUserData({
            ...userData,
            [event.target.name]: event.target.value
        })
    })

    let handleSignUp = (async () => {
        console.log(userData);


        const response = await fetch('createuser', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData),
        });
        const data = await response.json();
        console.log(data);

        if (data.isAuthenticated == true) {
            reactLocalStorage.setObject('userdata', data);
            navigate('/app');
        }
        else {
            navigate('/');
        }

    });

    return (
        <div className="auth-main">

            <div className="form-main">

                <TextField id="username" variant="outlined" placeholder="Username" name="username" onChange={handleChange} className="inputfield" />
                <TextField id="password" variant="outlined" placeholder="Password" name="password" onChange={handleChange} className="inputfield" />
                <FormControl className="inputfield" >

                    <InputLabel id="demo-simple-select-label">Role</InputLabel>
                    <Select
                        labelId="demo-simple-select-label"
                        id="demo-simple-select"
                        value={userData[role]}
                        label="role"
                        name="role"
                        onChange={handleChange}
                    >
                        {
                            role.map((val, index) => {
                                return <MenuItem value={val} key={index}>{val}</MenuItem>

                            })
                        }
                    </Select>

                </FormControl>
                <Button className="inputfield" onClick={() => { handleSignUp() }}>Sign Up</Button>

            </div>

            <hr></hr>
            <p>Already a user?</p>
            <Button className="inputfield" onClick={() => { navigate('/login') }}>Login</Button>
        </div>

    );
}

export default SignupPage;