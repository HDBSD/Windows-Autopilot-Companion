namespace Windows_Autopilot_Companion.Models
{
    public class User
    {
        public string? DisplayName { get; set; }
        public string? GivenName { get; set; }
        public string? Surname { get; set; }
        public string? Mail { get; set; }
        public string? UserPrincipalName { get; set; }
        public string? TelephoneNumber { get; set; }
        public ImageSource ProfileImage { get; set; }
    }
}