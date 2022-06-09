import { Container,Row,Form,Input, Label, Button,Col} from "reactstrap";
import { useState,useEffect } from "react";
import axios from "axios";
function Filter({onFilterSubmit}){
    const [transmissions,setTransmissions] = useState([])
    const [bodyShapes,setBodyShapes]= useState([])
    const initialFormData = Object.freeze({
        transmission: null,
        bodyShape: null,
        powerFrom: 0,
        powerTo: 0,
        yearFrom: 0,
        yearTo: 0 

      });
    const [formData, updateFormData] = useState(initialFormData);
    

    useEffect(() => {
        const Get = async () => {
            await fetchTransmission();
            await fetchBodyShapes();
            }
        Get()
    },[])

    const fetchTransmission = async () => {
        axios.get('https://localhost:44343/api/Transmission').then((response) => {
            setTransmissions(response.data)
        })
    }

    const fetchBodyShapes = async () => {
        axios.get('https://localhost:44343/api/BodyShape').then((response) => {
            setBodyShapes(response.data)
        })
    }
    

  const handleChange = (e) => {
    updateFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault()
    console.log(formData);
    onFilterSubmit(formData)
    // ... submit to API or something
  };
  const handleClear = () => {
      updateFormData(initialFormData)
      onFilterSubmit(formData)
  }


    const year = (new Date()).getFullYear();
    const years = Array.from(new Array(30),( val, index) => year - index);
    return(
        <Container className="bg-light border">
                <Row className="my-3">
                    <Label for="transmissions">Transmission</Label>
                    <Input type="select" id="transmissions" name="transmission" onChange={handleChange}>
                    <option value={null}></option>
                        {transmissions.length < 1 ? (null) : (
                            transmissions.map((transmission) => (
                                <option value={transmission.Id}>{transmission.Gears}-gear {transmission.Name}</option>
                            ))
                        )}
                    </Input>
                </Row>
                <Row className="my-3">
                <Label for="body_shapes">Body Shape</Label>
                    <Input type="select" id="body_shapes" name="bodyShape" onChange={handleChange}>
                        <option value={null}></option>
                    {bodyShapes.length < 1 ? (null) : (
                            bodyShapes.map((bodyShape) => (
                                <option value={bodyShape.Id}>{bodyShape.Name}</option>
                            ))
                        )}
                    </Input> 
                </Row>
                <Row className="my-3">
                    <Label for='power'>Engine power (hp)</Label>
                    <Row className="m-auto">
                        <Col md='5'>
                            <Input type="number" id="power" name="powerFrom" onChange={handleChange}/>
                        </Col>
                        <Col md='2'>
                        <p>to</p>
                        </Col>
                        <Col md='5'>
                            <Input type="number" id="power" name="powerTo" onChange={handleChange}/>
                        </Col>
                    </Row>
                </Row>
                <Row className="my-3">
                    <Label for='year'>Year</Label>
                    <Row className="m-auto">
                        <Col md='5'>
                            <Input type="select" id="year" name="yearFrom" onChange={handleChange}>
                               {years.map((year, index) => {
                                    return <option key={index} value={year}>{year}</option>
                                })}
                            </Input>
                        </Col>
                        <Col md='2'>
                        <p>to</p>
                        </Col>
                        <Col md='5'>
                        <Input type="select" id="year" name='yearTo' onChange={handleChange}>
                               {years.map((year, index) => {
                                    return <option key={index} value={year}>{year}</option>
                                })}
                            </Input>
                        </Col>
                    </Row>
                </Row>
                <Row className="my-4">
                    <Button type="button" color="info" onClick={handleSubmit}>Apply Filter</Button>
                </Row>
                <Row className="my-4">
                <Button type="button" color="secondary" onClick={handleClear}>Clear Filter</Button>
                </Row>
        </Container>
    );
}
export default Filter