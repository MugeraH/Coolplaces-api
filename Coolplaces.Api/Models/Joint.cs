namespace Coolplaces.Api.Models
{
    public class Joint
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime LastModified { get; set; }
    }
}
