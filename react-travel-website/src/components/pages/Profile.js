import React, { useContext, useState, useRef, useEffect } from "react";
import { UserContext } from "../../context/UserContext";
import { updateProfile, uploadAvatarFile, getUserById } from "../../api/api";
import defaultAvatar from "../assets/default-avatar.jpg";
import "../Profile.scss"; // style as you like

function Profile() {
  const { user, setUser } = useContext(UserContext);

  // Local state for form fields
  const [fullName, setFullName] = useState("");
  const [nationality, setNationality] = useState("");
  const [preferredLang, setPreferredLang] = useState("");
  const [avatarUrl, setAvatarUrl] = useState("");
  const [avatarFile, setAvatarFile] = useState(null);
  const [loading, setLoading] = useState(false);
  const [msg, setMsg] = useState("");
  const [error, setError] = useState("");
  const fileInputRef = useRef(null);

  // Fetch latest user info from backend when component mounts or userId changes
  useEffect(() => {
    if (!user?.userId) return;
    setLoading(true);
    getUserById(user.userId)
      .then(res => {
        const userData = res.data;
        setFullName(userData.fullName || "");
        setNationality(userData.nationality || "");
        setPreferredLang(userData.preferredLang || "");
        setAvatarUrl(userData.avatarUrl || "");
        // Update UserContext as well for consistency
        setUser(userData);
        setError("");
      })
      .catch(err => {
        setError("Failed to load user profile.");
      })
      .finally(() => setLoading(false));
    // eslint-disable-next-line
  }, [user?.userId]); // rerun if userId changes

  // Handle avatar file select/upload
  const handleAvatarChange = async (e) => {
    const file = e.target.files[0];
    if (!file) return;
    setLoading(true);
    try {
      const url = await uploadAvatarFile(file);
      setAvatarUrl(url);
      setAvatarFile(file);
      setMsg("Avatar uploaded.");
      setError("");
    } catch (err) {
      setError("Failed to upload avatar.");
    }
    setLoading(false);
  };

  // Handle profile update submit
  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError("");
    setMsg("");
    try {
      await updateProfile(
        user.userId,
        fullName.trim(),
        nationality.trim(),
        preferredLang.trim(),
        avatarUrl
      );
      // After update, refetch profile from backend for fresh data
      const res = await getUserById(user.userId);
      setUser(res.data);
      setFullName(res.data.fullName || "");
      setNationality(res.data.nationality || "");
      setPreferredLang(res.data.preferredLang || "");
      setAvatarUrl(res.data.avatarUrl || "");
      window.dispatchEvent(new Event("user-changed")); // update nav avatar in all tabs
      setMsg("Profile updated successfully!");
    } catch (err) {
      setError(
        err.response?.data || "Failed to update profile. Please check your info and try again."
      );
    }
    setLoading(false);
  };

  if (!user)
    return <div style={{ padding: 32 }}>You must be logged in to view this page.</div>;

  return (
    <div className="profile-container">
      <h2>My Profile</h2>
      <form className="profile-form" onSubmit={handleSubmit}>
        <div className="profile-avatar">
          <img
            src={avatarUrl || defaultAvatar}
            alt="Avatar"
            className="avatar-preview"
            style={{ width: 100, height: 100, borderRadius: "50%" }}
            onClick={() => fileInputRef.current?.click()}
          />
          <input
            type="file"
            accept="image/*"
            ref={fileInputRef}
            onChange={handleAvatarChange}
            style={{ display: "none" }}
          />
          <button
            type="button"
            className="avatar-upload-btn"
            onClick={() => fileInputRef.current?.click()}
            disabled={loading}
          >
            Change Avatar
          </button>
        </div>
        <div className="form-group">
          <label>Full Name<span style={{ color: 'red' }}>*</span></label>
          <input
            type="text"
            value={fullName}
            onChange={e => setFullName(e.target.value)}
            required
            maxLength={100}
            disabled={loading}
          />
        </div>
        <div className="form-group">
          <label>Nationality</label>
          <input
            type="text"
            value={nationality}
            onChange={e => setNationality(e.target.value)}
            maxLength={100}
            disabled={loading}
          />
        </div>
        <div className="form-group">
          <label>Preferred Language</label>
          <input
            type="text"
            value={preferredLang}
            onChange={e => setPreferredLang(e.target.value)}
            maxLength={100}
            disabled={loading}
          />
        </div>
        <button type="submit" className="profile-save-btn" disabled={loading}>
          {loading ? "Saving..." : "Save Changes"}
        </button>
        {msg && <div className="profile-success">{msg}</div>}
        {error && <div className="profile-error">{error}</div>}
      </form>
    </div>
  );
}

export default Profile;
