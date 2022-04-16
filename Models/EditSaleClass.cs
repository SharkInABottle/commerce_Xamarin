namespace CommerceApi.Models
{
    public class EditSaleClass
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Region { get; set; }
        public int? Price { get; set; }
        public string? Categorie { get; set; }
        public string? UserClassId { get; set; }
    }
}
