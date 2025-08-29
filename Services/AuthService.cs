using Microsoft.EntityFrameworkCore;
using RestaurantBookingAPI.Data;
using RestaurantBookingAPI.DTOs;
using RestaurantBookingAPI.Models.Entities;
using RestaurantBookingAPI.Services.IServices;

namespace RestaurantBookingAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly RestaurantDBContext _context;
        private readonly ITokenService _tokenService;

        public AuthService(RestaurantDBContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginAdminDTO loginDto)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(loginDto.Username))
                throw new ArgumentException("Username is required");

            if (string.IsNullOrWhiteSpace(loginDto.Password))
                throw new ArgumentException("Password is required");

            // Find admin by username - Direct DbContext access
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Username == loginDto.Username);

            if (admin == null)
            {
                throw new InvalidOperationException("Invalid username or password");
            }

            // Verify password
            bool passwordMatches = BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash);
            if (!passwordMatches)
            {
                throw new InvalidOperationException("Invalid username or password");
            }

            // Generate and return JWT token
            var token = _tokenService.GenerateToken(admin);
            return new AuthResponseDTO
            {
                Token = token,
                Username = admin.Username,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<bool> RegisterAsync(AdminRegisterDTO registerDto)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(registerDto.Username))
                throw new ArgumentException("Username is required");

            if (string.IsNullOrWhiteSpace(registerDto.Password))
                throw new ArgumentException("Password is required");

            if (string.IsNullOrWhiteSpace(registerDto.Email))
                throw new ArgumentException("Email is required");

            // Check if admin already exists - Direct DbContext access
            bool adminExists = await _context.Admins
                .AnyAsync(a => a.Username == registerDto.Username || a.Email == registerDto.Email);

            if (adminExists)
            {
                throw new ArgumentException("Username or Email already exists");
            }

            // Hash password
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Create and save admin - Direct DbContext access
            var admin = new Admin
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash
            };

            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
