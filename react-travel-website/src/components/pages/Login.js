import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { loginUser } from "../../api/api";
import {
  Container, Row, Col, Card, CardText, CardBody,
  CardFooter, CardTitle, Form, FormGroup, Label, Input
} from "reactstrap";

function Login() {
  const [password, setPassword] = useState("");
  const [emailId, setEmailId] = useState("");
  const [error, setError] = useState("");
  const history = useHistory();

  const handleLogin = async (e) => {
    e.preventDefault();
    if (!emailId || !password) {
      setError("Please enter both email and password.");
      return;
    }

    try {
      const res = await loginUser(emailId, password);
      localStorage.setItem("user", JSON.stringify(res.data));
      // Dispatch custom event to update Navbar immediately
      window.dispatchEvent(new Event("user-changed"));
      setError("");
      history.push("/");
    } catch (err) {
      setError(err.response?.data || "Login failed. Try again.");
    }
  };

  return (
    <Container>
      <Row>
        <Col sm={{ size: 8, offset: 2 }}>
          <div className="login-layout">
            <Card>
              <CardBody>
                <CardTitle className="text-center">Access Your Account</CardTitle>
                <CardText className="px-5 py-2">
                  {error && <div className="text-danger mb-3">{error}</div>}
                  <Form onSubmit={handleLogin}>
                    <FormGroup row>
                      <Label for="emailfield" md={2}>Email</Label>
                      <Col md={10}>
                        <Input
                          type="email"
                          id="emailfield"
                          value={emailId}
                          onChange={(e) => setEmailId(e.target.value)}
                          placeholder="Input Your Email"
                          required
                        />
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Label for="passwordfield" md={2}>Password</Label>
                      <Col md={10}>
                        <Input
                          type="password"
                          id="passwordfield"
                          value={password}
                          onChange={(e) => setPassword(e.target.value)}
                          placeholder="Input Your Password"
                          required
                        />
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Col md={{ size: 10, offset: 2 }}>
                        <Input type="submit" value="Login" className="btn btn-submit" />
                      </Col>
                    </FormGroup>
                  </Form>
                </CardText>
              </CardBody>
              <CardFooter className="text-center">
                Don't have an account? <a href="/sign-up" className="btn btn-link">Sign Up</a>
              </CardFooter>
            </Card>
          </div>
        </Col>
      </Row>
    </Container>
  );
}

export default Login;
