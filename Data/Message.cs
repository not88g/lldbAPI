namespace lldbAPI.Data
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Sender { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
