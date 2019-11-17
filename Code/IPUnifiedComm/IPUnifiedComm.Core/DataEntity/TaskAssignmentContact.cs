using System;
namespace IPUnifiedComm.Core.DataEntity
{
    public class TaskAssignmentContact
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ContactUserdesignationLevel { get; set; }
        public string ContactUserId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsTaskAssigned { get; set; }
    }
}
