namespace TravelBackend.Models.DTOs
{ 
public class SearchPlaceDto
{
    public string? Keyword { get; set; }
    public string? Category { get; set; }
        public string? LangCode { get; set; }
}
}