namespace Hien_mau.DTOs
{
    public class ReminderDto
    {
        public int ReminderId { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public DateTime RemindAt { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentAt { get; set; }


        public string? UserFullName { get; set; }
    }
 
    
        public class ReminderCreateDto
        {
            public int UserId { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public DateTime RemindAt { get; set; }
        }
    

}
