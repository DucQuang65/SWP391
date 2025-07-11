﻿namespace Hien_mau.Dto
{
    public class BloodRequestDto
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int? PatientId { get; set; }
        public string PatientName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Relationship { get; set; }
        public string FacilityName { get; set; }
        public string DoctorName { get; set; }
        public string DoctorPhone { get; set; }
        public string BloodGroup { get; set; }
        public string RhType { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public byte Status { get; set; } = 0;
        public DateTime? CreatedTime { get; set; }
    }
}
