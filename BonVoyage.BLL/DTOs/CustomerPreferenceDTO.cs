namespace BonVoyage.BLL.DTOs
{
    public class CustomerPreferenceDTO
    {
        public int PreferenceId { get; set; }
        public int UserId { get; set; }
        public string? PreferencesDetails { get; set; }
    }
}
