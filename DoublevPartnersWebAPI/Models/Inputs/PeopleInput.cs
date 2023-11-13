namespace DoublevPartnersWebAPI.Models.Inputs
{
    public class PeopleInput
    {
        public string? Names { get; set; }

        public string? LastNames { get; set; }

        public int? IdentificationNumber { get; set; }

        public string? Email { get; set; }

        public string? IdentificationType { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
