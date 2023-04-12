import React, { useState } from "react";
import { FormControl, TextField, InputLabel, Select, MenuItem } from '@mui/material'
import './Auth.styles.css'
import { Button } from "reactstrap";

function AuthPage() {

    const role = ["Employer", "JobSeeker", "Admin"]

    let [userData, setUserData] = useState({
        username: "",
        password: "",
        role: ""
    });

    let handleChange = ((event) => {
        console.log(event.target.name, event.target.value)
        setUserData({
            ...userData,
            [event.target.name]: event.target.value
        })
    })

    let handleSignUp = (async () => {
        console.log(userData);
        await fetch('/User/CreateUser', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData),
        }).then((res) => {
            let data = res.json();
            console.log(data);
        });

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
        </div>

    );
}

export default AuthPage;