namespace Tweetbook.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
        public bool success { get; set; }
        public  IEnumerable<string> Errors { get; set; }
       
    }
}
