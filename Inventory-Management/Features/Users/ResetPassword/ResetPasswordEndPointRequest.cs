using Inventory_Management.Entities;

namespace Inventory_Management.Features.Users.ResetPassword
{
    public class ResetPasswordEndPointRequest
    {
        
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Otp{ get; set; }
    }
}
