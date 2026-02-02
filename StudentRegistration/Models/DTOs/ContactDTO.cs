namespace StudentRegistration.Models.DTOs
{
    public class ContactDTO
    {
        public int ContactId { get; set; }
        public required string Name { get; set; }
        public required string Gender { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? Zip { get; set; }
        public required string Country { get; set; }
        public string? Phone { get; set; }
    }
}
