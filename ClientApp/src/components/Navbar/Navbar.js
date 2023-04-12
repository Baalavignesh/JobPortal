import React, { useEffect, useState } from "react";
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { reactLocalStorage } from "reactjs-localstorage";


function MyNavbar() {

    let [userRole, setUserRole] = useState();
    useEffect(() => {
        setUserRole(reactLocalStorage.getObject("userdata").role);
    })

    return (
        <div className="navbar-main">
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
                <NavbarBrand tag={Link} to="/">JobPortal</NavbarBrand>
                <ul className="navbar-nav flex-grow">
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
                </ul>
            </Navbar>
        </div>
    );

}

export default MyNavbar;