namespace Application.Dtos.Responses
{
    public class ClientInscriptionsResponse
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientEmail { get; set; } = string.Empty;
        public List<MyInscriptionResponse> Inscriptions { get; set; } = new();
    }
}