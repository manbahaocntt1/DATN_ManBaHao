import React, { useEffect, useState } from "react";
import { getAllTours, searchTours } from "../../api/api";
import "../Tour.css";

function Tour() {
  // Paging & search state
  const [tours, setTours] = useState([]);
  const [page, setPage] = useState(1);
  const [pageSize] = useState(9);
  const [isActive] = useState(true);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(true);
  const [filters, setFilters] = useState({
    keyword: "",
    location: "",
    type: "",
    minPrice: "",
    maxPrice: "",
    durationDays: ""
  });

  // Fetch tours when page or filters change
  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        let res;
        const anyFilter = Object.values(filters).some((v) => v);
        if (anyFilter) {
          res = await searchTours({ ...filters, page, pageSize });
        } else {
          res = await getAllTours({ isActive, page, pageSize });
        }
        setTours(res.data.tours);
        setTotalPages(res.data.totalPages);
      } catch (err) {
        setTours([]);
        setTotalPages(1);
      }
      setLoading(false);
    };
    fetchData();
    // eslint-disable-next-line
  }, [page, filters]);

  // Handle filter change
  const handleInputChange = (e) => {
    setFilters((f) => ({
      ...f,
      [e.target.name]: e.target.value
    }));
    setPage(1); // Reset to first page on filter change
  };

  // Clear filters
  const clearFilters = () => {
    setFilters({
      keyword: "",
      location: "",
      type: "",
      minPrice: "",
      maxPrice: "",
      durationDays: ""
    });
    setPage(1);
  };

  return (
    <div className="tour-page">
      <h2 className="tour-title">Available Tours</h2>
      {/* Filter Form */}
      <form
        className="tour-filter-form"
        onSubmit={(e) => { e.preventDefault(); setPage(1); }}>
        <input
          type="text"
          name="keyword"
          value={filters.keyword}
          placeholder="Keyword (e.g. food, history...)"
          onChange={handleInputChange}
        />
        <input
          type="text"
          name="location"
          value={filters.location}
          placeholder="Destination (e.g. Hà Nội)"
          onChange={handleInputChange}
        />
        <select
          name="type"
          value={filters.type}
          onChange={handleInputChange}
        >
          <option value="">All Types</option>
          <option value="Văn hóa">Culture</option>
          <option value="Ẩm thực">Food</option>
          <option value="Khám phá">Adventure</option>
          <option value="Thể thao">Sports</option>
          {/* Add more as needed */}
        </select>
        <input
          type="number"
          name="minPrice"
          value={filters.minPrice}
          placeholder="Min Price"
          onChange={handleInputChange}
          min={0}
        />
        <input
          type="number"
          name="maxPrice"
          value={filters.maxPrice}
          placeholder="Max Price"
          onChange={handleInputChange}
          min={0}
        />
        <input
          type="number"
          name="durationDays"
          value={filters.durationDays}
          placeholder="Duration (days)"
          onChange={handleInputChange}
          min={1}
        />
        <button type="submit" className="tour-search-btn">Search</button>
        <button type="button" className="tour-clear-btn" onClick={clearFilters}>Clear</button>
      </form>

      {/* Tours List */}
      <div className="tour-list">
        {loading ? (
          <div className="tour-loading">Loading tours...</div>
        ) : tours.length === 0 ? (
          <div className="tour-empty">No tours found.</div>
        ) : (
          tours.map((tour) => (
            <div className="tour-card" key={tour.tourId}>
              <div className="tour-image">
                <img
                  src={tour.images && tour.images.length > 0 ? tour.images[0].imageUrl : "/images/tours/hanoi1_3.jpg"}
                  alt={tour.title}
                  loading="lazy"
                />
              </div>
              <div className="tour-info">
                <h3>{tour.title}</h3>
                <div className="tour-meta">
                  <span className="tour-location"><i className="fas fa-map-marker-alt"></i> {tour.location}</span>
                  <span className="tour-type">{tour.type}</span>
                </div>
                <div className="tour-desc">{tour.description?.slice(0, 90)}...</div>
                <div className="tour-pricing">
                  <span>Adult: <b>{tour.priceAdult?.toLocaleString()}₫</b></span>
                  <span>Child: <b>{tour.priceChild?.toLocaleString()}₫</b></span>
                </div>
                <div className="tour-duration"><i className="far fa-clock"></i> {tour.durationDays} day(s)</div>
                <a href={`/tours/${tour.tourId}`} className="tour-details-link">View Details</a>
              </div>
            </div>
          ))
        )}
      </div>

      {/* Pagination */}
      <div className="tour-pagination">
        <button
          disabled={page <= 1}
          onClick={() => setPage((p) => Math.max(1, p - 1))}
        >Previous</button>
        <span>Page {page} / {totalPages}</span>
        <button
          disabled={page >= totalPages}
          onClick={() => setPage((p) => Math.min(totalPages, p + 1))}
        >Next</button>
      </div>
    </div>
  );
}

export default Tour;
