import React, { useEffect, useState } from "react";
import { getUserBookings, cancelBooking } from "../../api/api"; // Make sure cancelBooking is defined!
import "../MyBookings.css";

function Bookings() {
  const [bookings, setBookings] = useState([]);
  const [loading, setLoading] = useState(true);
  const [cancelingId, setCancelingId] = useState(null);
  const [error, setError] = useState("");
  const [successMsg, setSuccessMsg] = useState("");

 const fetchBookings = async () => {
  setLoading(true);
  setError("");
  const userStr = localStorage.getItem("user");
  console.log("Raw user string from localStorage:", userStr);

  let user = {};
  try {
    user = JSON.parse(userStr || "{}");
  } catch (e) {
    console.error("Error parsing user from localStorage:", e);
    setError("Corrupted user session.");
    setLoading(false);
    return;
  }
  console.log("Parsed user object:", user);

  if (!user.userId) {
    setError("You must be logged in.");
    setLoading(false);
    console.log("No userId found in user object. Stopping fetch.");
    return;
  }

  try {
    console.log("Calling getUserBookings with userId:", user.userId);
    const res = await getUserBookings(user.userId);
    console.log("BOOKINGS RESPONSE", res.data);
    setBookings(res.data);
  } catch (e) {
    setError("Failed to fetch bookings.");
    console.error("Fetch bookings error:", e);
  }
  setLoading(false);
};

  useEffect(() => {
    fetchBookings();
    // eslint-disable-next-line
  }, []);

  

  const canCancel = (b) => {
    if (b.paymentStatus !== "pending") return false;
    const today = new Date();
    const tourDate = new Date(b.tourDate);
    // Check if tourDate at least 7 days from now
    return (tourDate - today) / (1000 * 60 * 60 * 24) >= 7;
  };

  const handleCancel = async (bookingId) => {
    if (!window.confirm("Are you sure you want to cancel this booking?")) return;
    setCancelingId(bookingId);
    setError("");
    setSuccessMsg("");
    try {
      await cancelBooking(bookingId);
      setSuccessMsg("Booking cancelled successfully.");
      fetchBookings(); // Refresh list
    } catch (e) {
      setError("Failed to cancel booking. Try again.");
    }
    setCancelingId(null);
  };

  if (loading) return <div className="my-bookings-page">Loading bookings...</div>;

  return (
    <div className="my-bookings-page">
      <h2 className="my-bookings-title">My Bookings</h2>
      {error && <div className="booking-error">{error}</div>}
      {successMsg && <div className="booking-success">{successMsg}</div>}
      {bookings.length === 0 ? (
        <div>No bookings found.</div>
      ) : (
        <div className="booking-list">
          {bookings.map((b) => (
            <div key={b.bookingId} className="booking-card">
              <img
                src={b.tourImage || "/images/tours/default.jpg"}
                alt={b.tourTitle}
                className="booking-img"
              />
              <div className="booking-info">
                <div className="booking-title">{b.tourTitle}</div>
                <div className="booking-meta">
                  <span className="booking-label">Date:</span>
                  <span className="booking-date">{new Date(b.tourDate).toLocaleDateString()}</span>
                  <span className="booking-label">Adults:</span>
                  {b.adults}
                  <span className="booking-label">Children:</span>
                  {b.children}
                </div>
                <div>
                  <span className="booking-price">
                    {b.totalPrice?.toLocaleString()}₫
                  </span>
                  <span className={`booking-status ${b.paymentStatus.toLowerCase()}`}>
                    {b.paymentStatus}
                  </span>
                </div>
                {b.note && <div className="booking-note"><strong>Note:</strong> {b.note}</div>}
                <div className="booking-created">
                  Booked at {new Date(b.createdAt).toLocaleString()}
                </div>
                {/* Cancel Button Logic */}
                {canCancel(b) && (
                  <button
                    className="cancel-booking-btn"
                    onClick={() => handleCancel(b.bookingId)}
                    disabled={cancelingId === b.bookingId}
                  >
                    {cancelingId === b.bookingId ? "Cancelling..." : "Cancel Booking"}
                  </button>
                )}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

export default Bookings;
