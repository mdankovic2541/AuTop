import axios from "axios";
import React, { useEffect, useState } from 'react';
import { Col, ListGroup, ListGroupItem, Container, Row } from 'reactstrap';



export default function Transmission({Transmission}){
    const [modelVersions, setModelVersion] = useState({});
    
   
        return (
            modelVersions == null ? <p>Loading...</p>:
          <Container key={modelVersions.Id} className="bg-light mt-2 ">    
        
            <h5>Transmission</h5>
            <Row className='bg-light' >
                <Col className="text-center">Type: {Transmission?.Name}</Col>
            </Row>   
            <Row className='bg-light'>
                <Col className="text-center">
                    Gears: {Transmission?.Gears}
                </Col>
            </Row>  
               
            </Container>
        )
        
}