import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { Button } from './Button';
import './Navbar.scss';
import defaultAvatar from './assets/default-avatar.jpg';

function Navbar() {
  const [click, setClick] = useState(false);
  const [button, setButton] = useState(true);
  const [user, setUser] = useState(null);
  const history = useHistory();
  const [showDropdown, setShowDropdown] = useState(false);

  

  useEffect(() => {
    // Load user from localStorage
    const loadUser = () => {
      const storedUser = localStorage.getItem("user");
      setUser(storedUser ? JSON.parse(storedUser) : null);
    };
    loadUser();

    // Listen for the user-changed event
    window.addEventListener("user-changed", loadUser);

    // Handle resizing
    const handleResize = () => setButton(window.innerWidth > 960);
    window.addEventListener('resize', handleResize);

    // Cleanup
    return () => {
      window.removeEventListener("user-changed", loadUser);
      window.removeEventListener('resize', handleResize);
    };
  }, []);
  useEffect(() => {
  setShowDropdown(false); // Always hide dropdown when user changes
}, [user]);


  const handleLogout = () => {
    localStorage.removeItem("user");
    setUser(null);
    window.dispatchEvent(new Event("user-changed")); // So other tabs/components also update
    history.push("/");
  };

  const handleClick = () => setClick(!click);
  const closeMobileMenu = () => setClick(false);

  return (
    <nav className="navbar">
      <div className="navbar-container">
        <Link to="/" className="navbar-logo" onClick={closeMobileMenu}>
          HanoiTravel <i className="fab fa-typo3"></i>
        </Link>

        <div className="menu-icon" onClick={handleClick}>
          <i className={click ? 'fas fa-times' : 'fas fa-bars'} />
        </div>

        <ul className={click ? 'nav-menu active' : 'nav-menu'}>
          <li className='nav-item'><Link to="/" className="nav-links" onClick={closeMobileMenu}>Home</Link></li>
           <li className='nav-item'>
    <Link to="/tours" className="nav-links" onClick={closeMobileMenu}>Tours</Link>
  </li>
          <li className='nav-item'><Link to="/services" className="nav-links" onClick={closeMobileMenu}>Services</Link></li>
          <li className='nav-item'><Link to="/products" className="nav-links" onClick={closeMobileMenu}>Products</Link></li>
          <li className='nav-item'><Link to="/destinations" className="nav-links" onClick={closeMobileMenu}>All Destinations</Link></li>
      
        </ul>

        {!user ? (
           button && (
    <>
      <Button buttonStyle="btn--outline" to="/login">
        LOGIN
      </Button>
      <span style={{ margin: "0 6px" }}></span>
      <Button buttonStyle="btn--outline" to="/sign-up">
        SIGN UP
      </Button>
    </>
  )
        ) : (
          <div className="user-dropdown">
            <img
              src={user.avatarUrl || defaultAvatar}
              alt="avatar"
              className="avatar-icon"
              onClick={() => setShowDropdown(!showDropdown)}
              style={{ width: 40, height: 40, borderRadius: "50%", cursor: "pointer" }}
            />
              <div className="dropdown-menu" style={{ display: showDropdown ? "block" : "none" }}>
                <div className="dropdown-item" onClick={() => { setShowDropdown(false); history.push("/profile"); }}>Profile</div>
                 <div
      className="dropdown-item"
      onClick={() => { setShowDropdown(false); history.push("/my-bookings"); }}>
      My Bookings
    </div>
                <div className="dropdown-item" onClick={handleLogout}>Logout</div>
              </div>

          </div>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
