import React, { useEffect, useState } from "react";
import { useParams, useHistory } from "react-router-dom";
import { getTourDetails, bookTour } from "../../api/api"; // make sure this endpoint returns full details!
import "../TourDetails.css";

function TourDetails() {
  const { tourId } = useParams();
  const [tour, setTour] = useState(null);
  const [loading, setLoading] = useState(true);
  const [showBooking, setShowBooking] = useState(false);
  const [bookingInfo, setBookingInfo] = useState({
    adults: 1,
    children: 0,
    date: "",
    note: "",
    paymentMethod: "credit", // default
  });
  const [bookingError, setBookingError] = useState("");
  const [bookingSuccess, setBookingSuccess] = useState(false);

  const history = useHistory();

  // Ensure numbers for calculation
  const adultsCount = Number(bookingInfo.adults) || 0;
  const childrenCount = Number(bookingInfo.children) || 0;

  // Calculate total price live
  const totalPrice =
    tour && tour.priceAdult && tour.priceChild
      ? adultsCount * tour.priceAdult + childrenCount * tour.priceChild
      : 0;

  // Show booking modal only if user is logged in
  const handleBookNow = () => {
    const user = localStorage.getItem("user");
    if (!user) {
      history.push("/login");
    } else {
      setShowBooking(true);
    }
  };

  // Fetch tour details
  useEffect(() => {
    const fetchTour = async () => {
      setLoading(true);
      try {
        const res = await getTourDetails(tourId);
        setTour(res.data);
      } catch {
        setTour(null);
      }
      setLoading(false);
    };
    fetchTour();
  }, [tourId]);

  // Handle booking form submit
  const handleBookingSubmit = async (e) => {
    e.preventDefault();
    setBookingError("");
    setBookingSuccess(false);

    // Validate fields
    if (!bookingInfo.date) {
      setBookingError("Please select a departure date.");
      return;
    }

    // Get userId from localStorage
    const user = JSON.parse(localStorage.getItem("user") || "{}");
    if (!user.userId) {
      setBookingError("You need to login to book.");
      return;
    }

    try {
  await bookTour({
    userId: user.userId,
    tourId: tour.tourId,
    tourDate: bookingInfo.date,
    adults: Number(bookingInfo.adults),
    children: Number(bookingInfo.children),
    note: bookingInfo.note,
    paymentMethod: bookingInfo.paymentMethod,
  });
  setBookingSuccess(true);
  setTimeout(() => {
    setShowBooking(false);
    setBookingSuccess(false);
    setBookingInfo({
      adults: 1,
      children: 0,
      date: "",
      note: "",
      paymentMethod: "credit"
    });
    history.push("/my-bookings"); // Redirect after booking
  }, 1500);
} catch (e) {
  setBookingError(e.response?.data?.message || "Booking failed. Please try again.");
}
    setShowBooking(false);
  };

  if (loading) return <div className="tour-details-loading">Loading...</div>;
  if (!tour) return <div className="tour-details-empty">Tour not found.</div>;

  return (
    <div className="tour-details-page">
      {/* Image Gallery */}
      <div className="tour-gallery">
        {tour.images && tour.images.length > 0 ? (
          <div className="tour-gallery-carousel">
            {tour.images.map((img, i) => (
              <img key={img.imageId} src={img.imageUrl} alt={img.caption} className={i === 0 ? "active" : ""} />
            ))}
          </div>
        ) : (
          <img src="/images/tours/hanoi1_3.jpg" alt={tour.title} />
        )}
      </div>

      {/* Tour Summary */}
      <div className="tour-header">
        <h1>{tour.title}</h1>
        <div className="tour-summary-meta">
          <span className="tour-location">{tour.location}</span>
          <span className="tour-type">{tour.type}</span>
          <span className="tour-duration">{tour.durationDays} day(s)</span>
          <span className="tour-price"> {tour.priceAdult.toLocaleString()}₫ / adult</span>
        </div>
        <button className="book-now-btn" onClick={handleBookNow}>
          Book Now
        </button>
      </div>

      {/* Description */}
      <div className="tour-description">
        <h2>About this tour</h2>
        <p>{tour.description}</p>
      </div>

      {/* Itinerary */}
      {tour.places && tour.places.length > 0 && (
        <div className="tour-itinerary">
          <h2>Itinerary Highlights</h2>
          <ul>
            {tour.places.map(place => (
              <li key={place.placeId}>
                <img src={place.imageUrl} alt={place.name} />
                <div>
                  <strong>{place.name}</strong>
                  <p>{place.description?.slice(0, 80)}...</p>
                </div>
              </li>
            ))}
          </ul>
        </div>
      )}

      {/* Availability */}
      <div className="tour-availability">
        <h2>Upcoming Dates & Availability</h2>
        <table>
          <thead>
            <tr><th>Date</th><th>Total Slots</th><th>Available</th></tr>
          </thead>
          <tbody>
            {tour.availabilities && tour.availabilities.map(a => (
              <tr key={a.tourDate}>
                <td>{new Date(a.tourDate).toLocaleDateString()}</td>
                <td>{a.totalSlots}</td>
                <td>{a.availableSlots}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {/* Reviews */}
      <div className="tour-reviews">
        <h2>Reviews</h2>
        {tour.reviews.length === 0
          ? <div>No reviews yet. Be the first to review!</div>
          : tour.reviews.map((r, i) => (
            <div key={i} className="review">
              <img src={r.user?.avatarUrl || "/images/avatars/default.jpg"} alt={r.user?.fullName || "User"} />
              <div>
                <span className="review-user">{r.user?.fullName || "Anonymous"}</span>
                <span className="review-rating">{'★'.repeat(r.rating)}</span>
                <p>{r.comment}</p>
                <small>{new Date(r.createdAt).toLocaleDateString()}</small>
              </div>
            </div>
          ))}
      </div>

      {/* Booking Modal */}
      {showBooking && (
        <div className="booking-modal">
          <h2>Book this Tour</h2>
          <form onSubmit={handleBookingSubmit}>
            <label>
              Adults:
              <input
                type="number"
                min={1}
                name="adults"
                value={bookingInfo.adults}
                onChange={e => setBookingInfo(info => ({
                  ...info,
                  adults: e.target.value
                }))}
                required
              />
            </label>
            <label>
              Children:
              <input
                type="number"
                min={0}
                name="children"
                value={bookingInfo.children}
                onChange={e => setBookingInfo(info => ({
                  ...info,
                  children: e.target.value
                }))}
              />
            </label>
            <label>
              Departure Date:
              <select
                name="date"
                value={bookingInfo.date}
                onChange={e => setBookingInfo(info => ({
                  ...info,
                  date: e.target.value
                }))}
                required
              >
                <option value="">--Select--</option>
                {tour.availabilities.map(a => (
                  <option key={a.availabilityId} value={a.tourDate}>
                    {new Date(a.tourDate).toLocaleDateString()} ({a.availableSlots} slots left)
                  </option>
                ))}
              </select>
            </label>
            <label>
              Special Notes:
              <textarea
                name="note"
                value={bookingInfo.note}
                onChange={e => setBookingInfo(info => ({
                  ...info,
                  note: e.target.value
                }))}
              />
            </label>
            <label>
              Payment Method:
              <select
                name="paymentMethod"
                value={bookingInfo.paymentMethod}
                onChange={e => setBookingInfo(info => ({
                  ...info,
                  paymentMethod: e.target.value
                }))}
                required
              >
                <option value="credit">Credit Card</option>
                <option value="momo">Momo</option>
                <option value="paypal">PayPal</option>
                {/* Add more as needed */}
              </select>
            </label>
            {/* Live Total Price */}
            <div className="total-price">
              <strong>Total Price:</strong>{" "}
              <span style={{ color: "#1976d2", fontWeight: 700, fontSize: "1.15em" }}>
                {totalPrice.toLocaleString()}₫
              </span>
            </div>
            <button type="submit" className="submit-booking-btn">Confirm Booking</button>
            <button type="button" className="cancel-booking-btn" onClick={() => setShowBooking(false)}>
              Cancel
            </button>
            {bookingError && <div className="booking-error">{bookingError}</div>}
            {bookingSuccess && <div className="booking-success">Booking successful!</div>}
          </form>
        </div>
      )}
    </div>
  );
}

export default TourDetails;
