using Application.Dtos.Responses;
using Application.Dtos.Request;

namespace Application.Interfaces
{
    public interface IInscriptionService
    {
        Task<InscriptionResult> Inscribe(InscriptionRequest request);
    }
}