namespace SampleWebAPI.DTO
{
    public class SwordCreateWithType
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public int SamuraiId { get; set; }
        public TypeCreateDTO SType { get; set; }
    }
}
