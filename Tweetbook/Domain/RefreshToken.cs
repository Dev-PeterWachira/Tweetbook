using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tweetbook.Domain
{
    public class RefreshToken
    {
        public string Token { get; set; } = Guid.NewGuid().ToString();
        public string JwtId { get; set; }
        public DateTime Expires { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime CreationDate { get; set; }

        public bool Used { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        public bool used { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;

        public bool Invalidated { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
