using SampleWebAPI.Domain;
namespace SampleWebAPI.DTO
{
    public class SamuraiWithSwordsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SwordWithElementTypeDTO> Swords { get; set; }
    }
}
