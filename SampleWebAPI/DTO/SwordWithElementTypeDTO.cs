using SampleWebAPI.Domain;
namespace SampleWebAPI.DTO
{
    public class SwordWithElementTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }

        public List<ElementReadDTO> Elements { get; set; }
        public SType SType { get; set; }
    }
}

