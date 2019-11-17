using System;

namespace IPUnifiedComm.Core.DataEntity
{
    public class ChatMessage
    {
        public string Key { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string PhotoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string TimeStamp { get; set; }
        public string TimeAgo { get; set; }

        public ChatMessage()
        {
            TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
