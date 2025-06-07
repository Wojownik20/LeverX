
namespace MusicStore.Shared.Models.JWT_Authentication
{
    public record LoginDto
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
