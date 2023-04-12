import React, { useEffect, useState } from "react";
import MyNavbar from "../../components/Navbar/Navbar";
import { reactLocalStorage } from "reactjs-localstorage";
import { useNavigate } from "react-router-dom";
import { Button, TextField } from "@mui/material";
import './admin.styles.css'



function AdminPage() {

    let [allTextField, setAllTextField] = useState({
        "addcategory": "",
        "addsubcategory": "",
        "deletecategory": 0,
        "deletesubcategory": 0,
    });

    let handleAddCategory = async () => {
        const response = await fetch('addcategory', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ "CategoryName": allTextField.addcategory }),
        });
        const data = await response.json();
        console.log(data);
        console.log("working")
        console.log(allTextField)
    }

    let handleAddSubCategory = () => {
        console.log("working")
        console.log(allTextField)
    }

    let handleDeleteCategory = () => {
        console.log("working")
        console.log(allTextField)
    }

    let handleDeleteSubCategory = () => {
        console.log("working")
        console.log(allTextField)
    }



    const buttonData = [
        { name: "addcategory", placeholder: "Add Category", color: "primary", background: "aliceblue", handlefunction: handleAddCategory },
        { name: "addsubcategory", placeholder: "Add Sub Category", color: "primary", background: "aliceblue", name2: "categoryidforsub", placeholder2: "Category Id", },
        { name: "deletecategory", placeholder: "Delete Category", color: "error", background: "#fadcdc" },
        { name: "deletesubcategory", placeholder: "Delete Sub Category", color: "error", background: "#fadcdc" }
    ];





    let navigate = useNavigate();
    useEffect(() => {
        if (!reactLocalStorage.getObject("userdata").isAuthenticated || reactLocalStorage.getObject("userdata").role != "Admin") {
            navigate('/app');
        }
    });


    let handleChange = (e) => {
        console.log(e.target.value);

        setAllTextField({
            ...allTextField, [e.target.name]: e.target.value
        });
    }




    return (
        <div className="admin-main">
            <MyNavbar />
            <div className="admin-body">
                <div className="admin-controls">
                    <Button variant="contained" color="success">View Category</Button>
                    <Button variant="contained" color="success">View Sub Category</Button>

                    {buttonData.map((button, index) => {
                        return <div className="cron-button" key={index} style={{
                            backgroundColor: button.background
                        }}>
                            {button.name === "addsubcategory" &&
                                <TextField onChange={handleChange}
                                    fullWidth
                                    placeholder={button.placeholder2}
                                    name={button.name2} />

                            }
                            <TextField fullWidth
                                onChange={handleChange}
                                placeholder={button.placeholder}
                                name={button.name} />

                            <Button fullWidth
                                variant="contained"
                                color={button.color}
                                onClick={button.handlefunction}
                            >
                                {button.placeholder}</Button>
                        </div>
                    })}
                </div>
                <div className="response-side">
                    <h1>Response Screen</h1>
                </div>
            </div>



        </div>
    )

}

export default AdminPage;
