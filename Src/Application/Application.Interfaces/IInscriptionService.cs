using Application.Dtos.Responses;
using Application.Dtos.Requests;

namespace Application.Interfaces
{
    public interface IInscriptionService
    {
        Task<InscriptionResponse?> Inscribe(InscriptionRequest request);
    }
}