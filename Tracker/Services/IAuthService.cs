using Tracker.DTOs;

namespace Tracker.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<LoginResult> LoginAsync(LoginDto dto);
    }
}