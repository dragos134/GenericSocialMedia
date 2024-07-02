
namespace GenericSocialMedia.Application.Features.MessageFeatures.GetConversation
{
    public class GetConversationResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string SenderUsername { get; set; } = string.Empty;
        public string SenderFullname { get; set; } = string.Empty;
        public string ReceiverUsername { get; set; } = string.Empty;
        public string ReceiverFullname { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
    }
}
