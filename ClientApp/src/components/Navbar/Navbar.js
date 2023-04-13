import React, { useEffect, useState } from "react";
import { Collapse, Nav, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';
import { reactLocalStorage } from "reactjs-localstorage";
import { Button } from "@mui/material";


function MyNavbar() {

    let [userRole, setUserRole] = useState();
    let navigate = useNavigate();

    useEffect(() => {
        setUserRole(reactLocalStorage.getObject("userdata").role);
    })

    return (
        <div className="navbar-main">
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
                <NavbarBrand tag={Link} to="/">JobPortal</NavbarBrand>
                <Collapse isOpen={true} navbar>
                <Nav className="me-auto" navbar>

                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/app">Home</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/searchjobs">Search Jobs</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} className="text-dark" to="/postjob">Post Job</NavLink>
                    </NavItem>

                    {userRole == "Admin" &&
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/adminpage">Admin Page</NavLink>
                        </NavItem>
                    }

                    </Nav>
                        <Button color="error" variant="contained" onClick={() => {
                            reactLocalStorage.clear();
                            navigate('/');
                        }}>Logout</Button>
                </Collapse>


            </Navbar>
        </div>
    );

}

export default MyNavbar;