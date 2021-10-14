namespace PlatformService.Dtos
{
    public class PlatformReadDto
    {
        // Representation data for anybody reading from us.

        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Cost { get; set; }
    }
}