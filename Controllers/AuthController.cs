using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Services.IServices;

namespace RestauantBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginAdminDTO loginDto)
        {
            
            var message = await _authService.LoginAsync(loginDto);
            return Ok(message);
        }

        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register(AdminRegisterDTO registerDto)
        {
            
            var message = await _authService.RegisterAsync(registerDto);
            return Ok(message);
        }
    }
}
