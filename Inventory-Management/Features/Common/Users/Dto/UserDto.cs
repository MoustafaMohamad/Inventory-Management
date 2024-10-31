using Inventory_Management.Entities;

namespace Inventory_Management.Features.Common.Users.Dto
{
    public class UserDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? OtpCode { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public int RoleID { get; set; }
    }
}
