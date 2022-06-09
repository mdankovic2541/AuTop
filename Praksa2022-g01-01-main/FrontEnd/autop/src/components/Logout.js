import React, {useState} from 'react';
import { Container,Form,FormGroup,Label,Input, Button,Row} from 'reactstrap';

export default function Logout(){
    const LogoutUser = () => {
        sessionStorage.removeItem("id")
        sessionStorage.removeItem("token")
        sessionStorage.removeItem("username")
        window.location.reload();
        alert("You are logged out")

    }

    return(
        <Button onClick={LogoutUser}>
            Logout
        </Button>
    )
}