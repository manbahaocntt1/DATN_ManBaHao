import axios from 'axios';

const API = axios.create({
  baseURL: 'https://localhost:7084/api', // update based on your backend
  headers: {
    'Content-Type': 'application/json'
  }
});
// Standard email/password login
export const loginUser = (email, password) =>
  API.post('/user/login', { email, password });
// Standard email/password registration
export const registerUser = (fullName, email, password, preferredLang, avatarUrl, nationality) =>
  API.post('/user/register', { fullName, email, password, preferredLang, avatarUrl, nationality });

// Social login/register
export const socialLogin = (provider, socialId, fullName, email, avatarUrl) =>
  API.post('/user/social-login', { provider, socialId, fullName, email, avatarUrl });

// Avatar upload
export const uploadAvatarFile = async (file) => {
  const formData = new FormData();
  formData.append('file', file);

  try {
    const res = await axios.post('https://localhost:7084/api/user/upload-avatar', formData, {
      headers: { 'Content-Type': 'multipart/form-data' }
    });
    return res.data.url; // Return the uploaded file URL
  } catch (err) {
    console.error("Upload failed:", err);
    throw err;
  }
};

// Update profile
export const getUserById = (userId) =>
  API.get(`/user/${userId}`);

export const updateProfile = (userId, fullName, nationality, preferredLang, avatarUrl) =>
  API.put('/user/update-profile', {
    userId,
    fullName,
    nationality,
    preferredLang,
    avatarUrl
  });

  export const searchTours = (params) => API.get('/tour/search', { params });
// Fetch all tours (supports paging, language, isActive, etc.)
export const getAllTours = (params) =>
  API.get("/tour", { params });
// Fetch one tour's details
export const getTourDetails = (id) =>
  API.get(`/tour/${id}`);

// (Optional) Personalized or stats endpoints
export const getPersonalizedTours = (userId, params) =>
  API.get(`/tour/personalized/${userId}`, { params });

export const getTourStats = (tourId) =>
  API.get(`/tour/stats/${tourId}`);


export const bookTour = (data) => 
  API.post('/tourbooking/book', data);

// Get bookings for a specific user
export const getUserBookings = (userId) =>
  API.get(`/tourbooking/user/${userId}`);

export const cancelBooking = (bookingId) =>
  
  API.post(`/tourbooking/cancel/${bookingId}`);


