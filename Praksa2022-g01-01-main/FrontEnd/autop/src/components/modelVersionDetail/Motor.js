import axios from 'axios';
import React, { useEffect, useState } from 'react';
import { Row, Button, CardHeader, Container, ListGroup, ListGroupItem } from 'reactstrap';


export default function Motor({motor}) {
   
        return (
          <Container className="bg-light border mt-2">   
             <h5>Motor</h5>
                <Row className='bg-light mt-2 '>Type: {motor?.Name}</Row>   
                <Row className='bg-light'>Year: {motor?.Year}</Row>  
                <Row className='bg-light'>Type: {motor?.Type}</Row>  
                <Row className='bg-light'>MaxHP: {motor?.MaxHP}</Row>                     
            
          </Container>
        )
        
}