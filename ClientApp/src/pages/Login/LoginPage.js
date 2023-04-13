import React, { useEffect, useState } from "react";
import { TextField } from '@mui/material'
import './Login.styles.css'
import { Button } from "reactstrap";
import { useNavigate } from "react-router-dom";
import { reactLocalStorage } from 'reactjs-localstorage';


function LoginPage() {
    let [userData, setUserData] = useState({
        username: "",
        password: "",
    });
    let navigate = useNavigate();

    useEffect(() => {
        if (reactLocalStorage.getObject("userdata").isAuthenticated) {
            navigate('/app');
        }
    })


    let handleChange = ((event) => {
        console.log(event.target.name, event.target.value)
        setUserData({
            ...userData,
            [event.target.name]: event.target.value
        })
    })

    let handleLogin = (async () => {
        console.log(userData);


        const response = await fetch('loginuser', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(userData),
        });
        const data = await response.json();
        console.log(data);

        if (data.isAuthenticated === true) {
            reactLocalStorage.setObject('userdata', data);
            navigate('/app');
        }
        else {
            navigate('/login');
        }

    });


    return (
        <div className="auth-main">

            <div className="form-main">
                <TextField id="username" variant="outlined" placeholder="Username" name="username" onChange={handleChange} className="inputfield" />
                <TextField id="password" variant="outlined" placeholder="Password" name="password" onChange={handleChange} className="inputfield" />
                <Button className="inputfield" onClick={() => { handleLogin() }}>Login</Button>
            </div>

            <hr></hr>
            <p>New Here?</p>
            <Button className="inputfield" onClick={() => { navigate('/') }}>SignUp</Button>
        </div>

    )
}

export default LoginPage;