using System.Text.Json.Serialization;

namespace GenericSocialMedia.Domain.ServicesModels.Cometchat
{
    public class CometchatPagination
    {
        public int Total { get; set; }
        public int Count { get; set; }
        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }
        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}
