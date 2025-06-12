import React, { useState } from "react";
import { Link, useHistory } from "react-router-dom";
import {
  Container, Row, Col, Card, CardText, CardBody,
  CardFooter, CardTitle, Form, FormGroup, Label, Input
} from "reactstrap";
import "../../App.scss";
import { registerUser, socialLogin, uploadAvatarFile } from "../../api/api";
import { useGoogleLogin } from '@react-oauth/google';
import axios from "axios";

function SignUp() {
  const [fullName, setFullName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [preferredLang, setPreferredLang] = useState("en");
  const [avatarFile, setAvatarFile] = useState(null);
  const [nationality, setNationality] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");

  const history = useHistory();

  const handleAvatarChange = (e) => {
    setAvatarFile(e.target.files[0]);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!fullName || !email || !password || !confirmPassword) {
      setError("Please fill in all required fields.");
      return;
    }

    if (password !== confirmPassword) {
      setError("Passwords do not match.");
      return;
    }

    let uploadedAvatarUrl = "";
    if (avatarFile) {
      uploadedAvatarUrl = await uploadAvatarFile(avatarFile);
      if (!uploadedAvatarUrl) {
        setError("Avatar upload failed.");
        return;
      }
    }

    try {
      await registerUser(fullName, email, password, preferredLang, uploadedAvatarUrl, nationality || null);
      setSuccess("Registration successful.");
      setError("");
      setTimeout(() => history.push("/login"), 2000);
    } catch (err) {
      setError(err.response?.data || "Registration failed.");
      setSuccess("");
    }
  };

  // ✅ Modern Google Login
  const googleLogin = useGoogleLogin({
    onSuccess: async (tokenResponse) => {
      try {
        const userInfo = await axios.get("https://www.googleapis.com/oauth2/v3/userinfo", {
          headers: { Authorization: `Bearer ${tokenResponse.access_token}` }
        });

        const profile = userInfo.data;

        await socialLogin("google", profile.sub, profile.name, profile.email, profile.picture);
        localStorage.setItem("user", JSON.stringify({
          fullName: profile.name,
          email: profile.email,
          avatarUrl: profile.picture
        }));
        setSuccess("Logged in via Google.");
        setError("");
        history.push("/");
      } catch (err) {
        console.error("Google user info fetch failed:", err);
        setError("Google login failed.");
      }
    },
    onError: () => setError("Google login failed.")
  });

  return (
    <Container>
      <Row>
        <Col sm={{ size: 8, offset: 2 }}>
          <div className="signup-layout">
            <Card>
              <CardBody>
                <CardTitle className="text-center">Create New Account</CardTitle>
                <CardText className="px-5 py-2">
                  {error && <div className="text-danger mb-3">{error}</div>}
                  {success && <div className="text-success mb-3">{success}</div>}
                  <Form onSubmit={handleSubmit}>
                    <FormGroup row>
                      <Label for="namefield" md={2}>Name</Label>
                      <Col md={10}>
                        <Input
                          type="text"
                          id="namefield"
                          value={fullName}
                          onChange={(e) => setFullName(e.target.value)}
                          required
                        />
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Label for="emailfield" md={2}>Email</Label>
                      <Col md={10}>
                        <Input
                          type="email"
                          id="emailfield"
                          value={email}
                          onChange={(e) => setEmail(e.target.value)}
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
                          required
                        />
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Label for="confirmpassword" md={2}>Confirm</Label>
                      <Col md={10}>
                        <Input
                          type="password"
                          id="confirmpassword"
                          value={confirmPassword}
                          onChange={(e) => setConfirmPassword(e.target.value)}
                          required
                        />
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Label for="langfield" md={2}>Language</Label>
                      <Col md={10}>
                        <Input
                          type="select"
                          id="langfield"
                          value={preferredLang}
                          onChange={(e) => setPreferredLang(e.target.value)}
                        >
                          <option value="en">English</option>
                          <option value="vi">Tiếng Việt</option>
                        </Input>
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Label for="nationalityfield" md={2}>Nationality</Label>
                      <Col md={10}>
                        <Input
                          type="text"
                          id="nationalityfield"
                          value={nationality}
                          onChange={(e) => setNationality(e.target.value)}
                          placeholder="e.g. Vietnam, USA"
                        />
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Label for="avatarUpload" md={2}>Avatar</Label>
                      <Col md={10}>
                        <Input
                          type="file"
                          id="avatarUpload"
                          accept="image/*"
                          onChange={handleAvatarChange}
                        />
                      </Col>
                    </FormGroup>
                    <FormGroup row>
                      <Col md={{ size: 10, offset: 2 }}>
                        <Input type="submit" value="Sign Up" className="btn btn-submit" />
                      </Col>
                    </FormGroup>
                  </Form>

                  <div className="text-center mt-3">
                    <button onClick={() => googleLogin()} className="btn btn-outline-dark">
                      <img src="https://developers.google.com/identity/images/g-logo.png" alt="google" style={{ width: 20, marginRight: 8 }} />
                      Sign up with Google
                    </button>
                  </div>
                </CardText>
              </CardBody>
              <CardFooter className="text-center">
                Already have an account? <Link to="/login" className="btn btn-link">Log In</Link>
              </CardFooter>
            </Card>
          </div>
        </Col>
      </Row>
    </Container>
  );
}

export default SignUp;
