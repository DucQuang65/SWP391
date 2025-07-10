namespace Hien_mau.Dto
{
    public class InformationDto
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string IDCardType { get; set; }     
        public string IDCard { get; set; }         
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }  
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }           
        public string District { get; set; }       
        public string Ward { get; set; }           
        public string Address { get; set; }
        public double? Distance { get; set; }
        public string BloodGroup { get; set; }
        public string RhType { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public byte? Status { get; set; }
        public int RoleID { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
