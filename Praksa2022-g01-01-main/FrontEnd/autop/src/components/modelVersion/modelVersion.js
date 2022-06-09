import {useParams,useNavigate} from 'react-router-dom'
import{useState,useEffect} from 'react'
import axios from 'axios';
import{Container,Row,Col} from 'reactstrap'
import Model from '../model/model.js'
import '../../App.css'


function ModelVersion(modelVersion){

    {console.log(modelVersion)}
    const navigate = useNavigate();


    const link = (id) =>{
        navigate('/ModelVersion/' + modelVersion.modelVersion.Id);
    }
    return(
        <Container className='bg-light border my-2' id='list_con' onClick={link}>
            <Row>
                <Col>
                <img className='mod_img' src={modelVersion.modelVersion.Model.ImageURL}/>
                </Col>
                <Col>
                <p>{modelVersion.modelVersion.Model.Manufacturer.Name} {modelVersion.modelVersion.Model.Name} {modelVersion.modelVersion.Name}</p>
                </Col>
            </Row>
        </Container>
    )
}

export default ModelVersion