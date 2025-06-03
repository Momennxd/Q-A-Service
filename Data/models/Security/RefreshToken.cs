using System.ComponentModel.DataAnnotations;

namespace Data.models.Security
{
    public class RefreshToken
    {
        [Key]
        public int TokenId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expiration;
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public bool IsActive => RevokedOn == null && !IsExpired;

    }
}
