import { Component } from 'react';
import {
  Form,
  FormFeedback,
  FormGroup,
  Label,
  Input,
  Button,
} from 'reactstrap';
import axios from 'axios';

class Registration extends Component {
  constructor(props) {
    super(props);
    this.state = {
      username: '',
      email: '',
      password: '',
      validate: {
        emailState: '',
      },
    };
    this.handleChange = this.handleChange.bind(this);
  }

  handleChange = (event) => {
    const { target } = event;
    const value = target.type === 'checkbox' ? target.checked : target.value;
    const { name } = target;

    this.setState({
      [name]: value,
    });
  };

  validateEmail(e) {
    const emailRex =
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    const { validate } = this.state;

    if (emailRex.test(e.target.value)) {
      validate.emailState = 'has-success';
    } else {
      validate.emailState = 'has-danger';
    }

    this.setState({ validate });
  }

  submitForm(e) {
    e.preventDefault();

    const newUserData = {username: this.state.username,
                         email : this.state.email,
                         password : this.state.password};
        axios.post('https://localhost:44343/users', newUserData).then((response) => {          
          console.log(newUserData);
        });
      
  }

  render() {
    const { username, email, password } = this.state;

    return (
      <div className="Registration">
        <h2>Register</h2>
        <Form className="form" onSubmit={(e) => this.submitForm(e)}>
          <FormGroup>
          <Label>Username</Label>
            <Input
              type="username"
              name="username"
              id="exampleUsername"
              placeholder="example"
              value={username}
              onChange={(e) => this.handleChange(e)}
            />
          </FormGroup>
          <FormGroup>            
            <Label>Email</Label>
            <Input
              type="email"
              name="email"
              id="exampleEmail"
              placeholder="example@example.com"
              valid={this.state.validate.emailState === "has-success"}
              invalid={this.state.validate.emailState === "has-danger"}
              value={email}
              onChange={(e) => {
                this.validateEmail(e);
                this.handleChange(e);
              }}
            />
            <FormFeedback>
              Please input a correct email.
            </FormFeedback>
            <FormFeedback valid>
              Valid email
            </FormFeedback>
          </FormGroup>
          <FormGroup>
            <Label for="examplePassword">Password</Label>
            <Input
              type="password"
              name="password"
              id="examplePassword"
              placeholder="********"
              value={password}
              onChange={(e) => this.handleChange(e)}
            />
          </FormGroup>
          <Button>Register</Button>
        </Form>
      </div>
    );
  }
}

export default Registration;