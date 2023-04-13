import React, { useEffect, useState } from "react";
import MyNavbar from "../../components/Navbar/Navbar";
import { reactLocalStorage } from "reactjs-localstorage";
import { useNavigate } from "react-router-dom";
import { Button, TextField } from "@mui/material";
import './admin.styles.css'

import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import { Row } from "reactstrap";


function AdminPage() {

    let [allTextField, setAllTextField] = useState({
        "addcategory": "",
        "addsubcategory": "",
        "deletecategory": 0,
        "deletesubcategory": 0,
        "categoryidforsub": 0,
    });
    let [serverResponse, setServerResponse] = useState("Server Response Displayed Here");
    let [tableToggle, setTableToogle] = useState(false);
    let [tableData, setTableData] = useState([]);

    // Add a Category
    let handleAddCategory = async () => {
        const response = await fetch('addcategory', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ "CategoryName": allTextField.addcategory }),
        });
        const data = await response.json();
        if (data.isInserted) {
            setServerResponse("New Category Successfully Added");
        }
        else {
            setServerResponse("Category Insert Failed");
        }
        setTableData(false);
    }

    // Add a Sub Category
    let handleAddSubCategory = async () => {
        const response = await fetch('addsubcategory', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ "SubCategoryName": allTextField.addsubcategory, "CategoryId": allTextField.categoryidforsub }),
        });
        const data = await response.json();
        if (data.isInserted) {
            setServerResponse("New Sub Category Successfully Added");
        }
        else {
            setServerResponse("Sub Category Insert Failed");
        }
        setTableData(false);

    }

    // Delete Category
    let handleDeleteCategory = async () => {
        const response = await fetch('deletecategory', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ "CategoryId": parseInt(allTextField.deletecategory) }),
        });
        const data = await response.json();
        if (data.isDeleted) {
            setServerResponse("Category Successfully Deleted");
        }
        else {
            setServerResponse("Category Delete Failed, " + data.title);
        }
        setTableData(false);
    }

    // Delete Sub Category
    let handleDeleteSubCategory = async () => {

        const response = await fetch('deletesubcategory', {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ "SubCategoryId": parseInt(allTextField.deletesubcategory) }),
        });
        const data = await response.json();
        if (data.isDeleted) {
            setServerResponse("Sub Category Successfully Deleted");
        }
        else {
            setServerResponse("Sub Category Delete Failed");
        }
        setTableData(false);
    }

    const buttonData = [
        { name: "addcategory", placeholder: "Add Category", color: "primary", background: "aliceblue", handlefunction: handleAddCategory },
        { name: "addsubcategory", placeholder: "Add Sub Category", color: "primary", background: "aliceblue", handlefunction: handleAddSubCategory, name2: "categoryidforsub", placeholder2: "Category Id", },
        { name: "deletecategory", placeholder: "Delete Category", color: "error", background: "aliceblue", handlefunction: handleDeleteCategory },
        { name: "deletesubcategory", placeholder: "Delete Sub Category", color: "error", background: "aliceblue", handlefunction: handleAddSubCategory, handlefunction: handleDeleteSubCategory }
    ];


    useEffect(() => {
        setTableToogle(true);
        if (tableData.length > 0) {

            Object.keys(tableData[0]).forEach((val) => {
                console.log(val)
            })
        }

    }, [tableData])


    useEffect(() => {
        console.log(tableToggle);
    }, [tableToggle])


    let handleViewCategory = async () => {
        const response = await fetch('getallcategory');
        const data = await response.json();

        const trimmedArray = data.map(({ categoryId, categoryName }) => ({ categoryId, categoryName }));
        setTableData(trimmedArray);

    }

    let handleViewSubCategory = async () => {
        const response = await fetch('getallsubcategory', {
            method: "GET",
            headers: {
                'Content-Type': 'application/json'
            },
        });
        const data = await response.json();

        const trimmedArray = data.map(({ subCategoryId, subCategoryName, categoryId }) => ({ subCategoryId, subCategoryName, categoryId }));
        setTableData(trimmedArray);

    }



    let navigate = useNavigate();
    useEffect(() => {
        if (!reactLocalStorage.getObject("userdata").isAuthenticated || reactLocalStorage.getObject("userdata").role != "Admin") {
            navigate('/app');
        }
    });


    let handleChange = (e) => {
        setAllTextField({
            ...allTextField, [e.target.name]: e.target.value
        });
    }

    return (
        <div className="admin-main">
            <MyNavbar />
            <div className="admin-body">
                <div className="admin-controls">

                    <Button variant="contained" color="success" style={{ margin: "1rem" }} onClick={handleViewCategory}>View Category</Button>
                    <Button variant="contained" color="success" style={{ margin: "1rem" }} onClick={handleViewSubCategory}>View Sub Category</Button>

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
                                value={allTextField[button.name]}
                                name={button.name} />

                            <Button
                                variant="contained"
                                color={button.color}
                                onClick={button.handlefunction}
                                style={{margin:"1rem"}}
                            >
                                {button.placeholder}</Button>
                        </div>
                    })}
                </div>
                <div className="response-side">

                    {tableToggle && tableData.length > 0 ?

                        <div className="table-style">
                            <TableContainer component={Paper}>
                                <Table sx={{ minWidth: 650 }} aria-label="simple table">
                                    <TableHead>
                                        <TableRow>
                                            {Object.keys(tableData[0]).map((val, index) => {
                                                return <TableCell key={index}><b>{val}</b></TableCell>
                                            })}
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        {tableData.map((row, index) => (

                                            <TableRow
                                                key={index}
                                                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                            >
                                                {Object.keys(row).map((val, index) => {
                                                    return <TableCell key={index}>{row[val]}</TableCell>
                                                })}
                                            </TableRow>
                                        ))}
                                    </TableBody>
                                </Table>
                            </TableContainer>
                        </div>
                        : <h1>{serverResponse}</h1>

                    }

                </div>
            </div>



        </div>
    )

}

export default AdminPage;
