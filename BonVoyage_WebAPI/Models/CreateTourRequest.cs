namespace BonVoyage_WebAPI.Models
{
    public class CreateTourRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string? Country { get; set; }
        public string? Route { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // для загрузки фотографии, позволяет принимать и обрабатывать файл, который загружается с формы на клиенте
        public IFormFile? Photo { get; set; }
    }

}
