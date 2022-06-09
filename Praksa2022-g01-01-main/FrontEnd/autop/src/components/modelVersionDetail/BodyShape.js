import axios from "axios";
import React, { useEffect, useState } from 'react';
import { Row, ListGroupItem, ListGroup, Container, List } from 'reactstrap';



export default function BodyShape({bodyShape}){
    const [modelVersions, setModelVersion] = useState({});
   
        return (
            modelVersions == null ? <p>Loading...</p>:
          <Container key={modelVersions.Id} className="bg-light mt-2 border">    
                <h5>Body Shape</h5>
                <Row className='bg-light mt-2 '>Name: { bodyShape?.Name}</Row>   
                                 
          </Container>
        )
        
}