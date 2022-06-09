import {Navbar,Nav,NavbarBrand,NavbarToggler,NavItem,NavLink,UncontrolledDropdown,DropdownToggle,DropdownItem,NavbarText,DropdownMenu,Collapse,Form,Input,Button} from 'reactstrap'
import { useState } from 'react';
import logo from './logo5.png';
import { Link } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import Login from '../Login';
import Logout from '../Logout';
import useToken from '../useToken';
import useSetId from '../useSetId';
function NavigationBar(){
  const navigate = useNavigate();

  const {id, setUserId} = useSetId();

  const { token, setToken } = useToken();

    return(
        <div>
  <Navbar id='navbar'
    color="info"
    expand="lg"
    light
    className='mb-3'
    container ='lg'
  >
    <NavbarBrand id='navbrand' href="/">
      <img id='navlogo' src={logo} height={100} width={290}/>
    </NavbarBrand>
    <NavbarToggler onClick={function noRefCheck(){}} />
    <Collapse navbar>
      <Nav
        className="me-auto"
        navbar
      >
        <NavItem>
          <NavLink href="/">
            Manufacturers
          </NavLink>
        </NavItem>
        <NavItem>
          <NavLink href="https://github.com/jelenatufekovic/Praksa2022-g01-01">
            GitHub
          </NavLink>
        </NavItem>
      </Nav>
      
      <NavbarText>
      {sessionStorage.getItem('id') == null ? (
        <Button
          onClick={() => navigate("/login")}>
          Login
        </Button>
      ) : (<><span className='me-3'>{sessionStorage.getItem('username').replace(/["]+/g, '')}</span><Logout/></>)}
        
      </NavbarText>
    </Collapse>
  </Navbar>
</div>
    );
}

export default NavigationBar;