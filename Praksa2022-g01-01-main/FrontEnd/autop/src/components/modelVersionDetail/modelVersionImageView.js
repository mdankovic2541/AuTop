import { Container,Row,Col } from "reactstrap";
import ReactStars from 'react-rating-stars-component';
function ModelVersionImageView({manufac_name,model_name,model_version_name,imageURL}){
    return(
        <Container className="bg-light border mt-3">
            <Row className="my-3">
                <Col md='8'>
                    <h3>{manufac_name} {model_name} {model_version_name}</h3>
                </Col>
                <Col md='4'>
                    <ReactStars
                    edit={false}
                    count={5}
                    size={24}
                    value={3}
                    />
                </Col>
            </Row>
            <Row>
                <img src={imageURL}/>
            </Row>
        </Container>
    );

}
export default ModelVersionImageView