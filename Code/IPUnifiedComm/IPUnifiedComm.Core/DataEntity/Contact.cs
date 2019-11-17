using System;
namespace IPUnifiedComm.Core.DataEntity
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public string Description { get => $"{Designation}, {Location}"; }
        public string PhotoUrl { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
    }
}
