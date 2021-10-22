namespace BloggerDotNet.Models
{
    public class Account : Profile
    {
        public new string Id { get; set; }
        public new string Name { get; set; }
        public new string Email { get; set; }
        public new string Picture { get; set; }
    }
}