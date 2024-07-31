namespace BonVoyage.BLL.DTOs
{
    public class CustomerPreferenceDTO
    {
        public int CustomerPreferenceId { get; set; }
        public int UserId { get; set; }
        public string? PreferencesDetails { get; set; }
    }
}
