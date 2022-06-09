import '../../App.css';
import {Container,Row} from 'reactstrap';
import { Link,useNavigate } from 'react-router-dom'

function Manufacturer({manufacturer}){
    const navigate = useNavigate();


    const link = (id) =>{
        navigate('/Manufacturer/' + manufacturer.Id);
        
        
    }

    return (
        <Container id='man_con' className="bg-light border m-1" onClick={link}>
            <Row><img className='man_img' src={manufacturer.ImageURL} alt="" /></Row>
            <Row className='man_name'><p>{manufacturer.Name}</p></Row>
        </Container>
    );
}

export default Manufacturer