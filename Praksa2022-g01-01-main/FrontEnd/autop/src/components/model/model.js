import { Link,useNavigate } from 'react-router-dom';
import {Container} from 'reactstrap'
import '../../App.css'
import {useEffect} from 'react'

function Model(model){
const navigate = useNavigate();


const link = (id) =>{
    navigate('/Model/' + model.model.Id);
    
    
}

    return(
        <Container className="list_container bg-light border pb-3 my-2" id='list_con' onClick={link}>
            <p>{model.model.Name}</p>
            <img className='mod_img' src={model.model.ImageURL}></img>
        </Container>
    )
}

export default Model;