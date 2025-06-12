import {BrowserRouter as Router, Switch, Route} from 'react-router-dom';

import './App.scss';
import Navbar from './components/Navbar';
import Footer from './components/Footer';
import Home from './components/pages/Home';
import Products from './components/pages/Products';
import ProductDetail from './components/pages/ProductDetail';
import Services from './components/pages/Services';
import Destinations from './components/pages/Destinations';
import SignUp from './components/pages/SignUp';
import Login from './components/pages/Login';
import Profile from './components/pages/Profile';
import Tour from './components/pages/Tour'; // <<--- Add this
import TourDetails from './components/pages/TourDetails'; // <<--- Add this
import Bookings from './components/pages/Bookings';

function App() {
  return (
    <div className="App">
      <Router>
        <Navbar/>
        <Switch>
          <Route path="/" exact component={Home}/>
          <Route path="/services" exact component={Services}/>
          <Route path="/products" exact component={Products}/>
          <Route path="/destinations" exact component={Destinations}/>
          <Route path="/sign-up" exact component={SignUp}/>
          <Route path="/login" exact component={Login}/>
          <Route path="/products/:id" component={ProductDetail}/>
          <Route path="/profile" component={Profile} />
          <Route path="/tours" exact component={Tour} /> {/* <-- Add this route */}
          <Route path="/tours/:tourId" component={TourDetails} />
          <Route path="/my-bookings" component={Bookings} />

        </Switch>
        <Footer/>
      </Router>
    </div>
  );
}

export default App;
