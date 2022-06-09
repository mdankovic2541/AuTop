import React, {useState} from 'react';
import axios from 'axios';
import ReactStars from 'react-rating-stars-component';
import { Container,Form,FormGroup,Label,Input, Button,Row} from 'reactstrap';

export default function ReviewInput({modelVersionId}){
    
    const [comment, setComment] = useState("");
    const [rating, setRating] = useState(0);   
    
    const AddReview = () => {
        const newReviewData = {Comment : comment,
                             Rating : rating,
                             UserId : sessionStorage.getItem('id').replace(/["]+/g, ''),
                             ModelVersionId: modelVersionId};
        
        axios.post('https://localhost:44343/reviews', newReviewData).then((response) => {
          setComment('');
          setRating(0);
          console.log(response.data);
          console.log(newReviewData);
          window.location.reload()  
    })};


    const ratingChanged = (newRating) => {
        setRating(newRating)
      };

    return(
        <Container className='Registration bg-light border mt-3'>                   
            <FormGroup>
                <Row>
                   <Label>Comment</Label>
                <Input
                    id="exampleText"
                    name="text"
                    type="textarea"
                    value={comment} onChange={(e) => setComment(e.target.value)}
                    /> 
                </Row>
                <Row className='justify-content-center'>
                   <Label>Rating</Label>
                       <ReactStars classNames={"mx-auto mb-2"}
                    count={5} 
                    size={45}
                    value={rating} 
                    onChange={ratingChanged}
                    />  
                      
                
                </Row>
                <Row>
                  <Button onClick={AddReview}>Submit</Button>  
                </Row>
                
            </FormGroup>
        </Container>
    )
}


