namespace TravelBackend.Models.DTOs
{
    public class AdminUpdateUserDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Nationality { get; set; }
        public string PreferredLang { get; set; }
        public string AvatarUrl { get; set; }
    }
}
