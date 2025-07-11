    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    namespace Hien_mau.Models;

    public partial class Appointments
    {
        [Key]
        public int AppointmentId { get; set; }
        public int UserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? TimeSlot { get; set; }
        public byte Status { get; set; } = 0; // 0: chờ duyệt, 1: từ chối, 2: chấp nhận
        public string? Notes { get; set; }
        public string? BloodPressure { get; set; } 
        public int? HeartRate { get; set; }
        public double? Hemoglobin { get; set; }
        public double? Temperature { get; set; }
        public int? DoctorId { get; set; }
        public bool Cancel { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ForeignKey(nameof(UserId))]
    public Users? User { get; set; }

    [ForeignKey(nameof(DoctorId))]
    public Users? Doctor { get; set; }
}

  

