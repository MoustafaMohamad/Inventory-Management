using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly Random _random = new Random();

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //[Authorize]
        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] User dto)
        //{
        //    var emailExists = await _mediator.Send(new EmailExistsQuery(dto.Email));
        //    if (emailExists!=null)
        //        return BadRequest("Email already registered.");

        //    var result = await _mediator.Send(new RegisterUserCommand(dto));
        //    if (!result)
        //        return BadRequest("Failed to register user.");

        //    var otp=_random.Next(100000, 999999).ToString();
        //    var confirmationUrl = Url.Action("ConfirmRegistration", "Auth",
        //        new { email = dto.Email, otp = otp }, Request.Scheme);
        //   await _mediator.Send(new SendEmailCommand(dto.Email, "ConfirmRegistration", $"Please confirm your registration using this OTP: {otp} or click on the link: {confirmationUrl}"));

           


        //    return Ok("Registration successful, please check your email for OTP.");
        //}
        //[Authorize]

        //[HttpPost("confirm-registration")]
        //public async Task<IActionResult> ConfirmRegistration([FromBody] OtpVerification dto)
        //{
        //    var isOtpValid = await _mediator.Send(new Validate_Otp_Orchestrator(dto.Email, dto.Otp));
        //    if (!isOtpValid)
        //        return BadRequest("Invalid OTP.");

        //    var result = await _mediator.Send(new ConfirmUserOrchestrator(dto.Email ,dto.PasswordHash));
        //    if (!result)
        //        return BadRequest("Failed to confirm registration.");

        //    return Ok("Registration confirmed successfully.");
        //}


    }
}
