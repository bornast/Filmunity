using Application.Interfaces;

namespace Application.Dtos.Common
{
    public class TokenDto : IObjectToValidate
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
