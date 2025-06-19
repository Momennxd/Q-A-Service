using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.RefreshTokens
{
    public class RefreshTokenDTOs
    {
        public class RefreshTokenDTO
        {
            [Required(ErrorMessage = "Token is required.")]
            public string Token { get; set; }
        }
        public class TokenResponseDto
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }
        public class LoginResponseDto : TokenResponseDto
        {
        }
    }
}
