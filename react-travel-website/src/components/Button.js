import React from "react";
import "./Button.scss";
import { Link } from "react-router-dom";

const STYLES = ["btn--primary", "btn--outline"];
const SIZES = ["btn--medium", "btn--large"];

// Add a "to" prop for navigation, or fallback to onClick
export const Button = ({
  children,
  type,
  onClick,
  buttonStyle,
  buttonSize,
  to,          // New: navigation path
  ...rest      // Any other props
}) => {
  const checkButtonStyle = STYLES.includes(buttonStyle) ? buttonStyle : STYLES[0];
  const checkButtonSize = SIZES.includes(buttonSize) ? buttonSize : SIZES[0];

  if (to) {
    // If a "to" prop is provided, use Link for navigation
    return (
      <Link to={to} className="btn-mobile">
        <button
          className={`btn ${checkButtonStyle} ${checkButtonSize}`}
          type={type}
          {...rest}
        >
          {children}
        </button>
      </Link>
    );
  }
  // Otherwise, render a regular button (with onClick)
  return (
    <button
      className={`btn ${checkButtonStyle} ${checkButtonSize}`}
      type={type}
      onClick={onClick}
      {...rest}
    >
      {children}
    </button>
  );
};
