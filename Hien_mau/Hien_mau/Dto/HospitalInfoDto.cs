namespace Hien_mau.Dto
{
    public class HospitalInfoDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? WorkingHours { get; set; }

        public string? MapImageUrl { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
