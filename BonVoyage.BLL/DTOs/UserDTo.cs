namespace BonVoyage.BLL.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; }
       
    }
}
