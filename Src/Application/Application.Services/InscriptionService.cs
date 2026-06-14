using Application.Dtos.Request;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Mapper;
using Domain.Entity;
using Domain.Interface;

namespace Application.Services
{
    public class InscriptionService : IInscriptionService
    {
        private readonly IInscriptionRepository _inscriptionRepo;
        private readonly IClassRepository _classRepo;
        private readonly IUserRepository _userRepo;
        private readonly IPlanRepository _planRepo;

        public InscriptionService(
            IInscriptionRepository inscriptionRepo,
            IClassRepository classRepo,
            IUserRepository userRepo,
            IPlanRepository planRepo)
        {
            _inscriptionRepo = inscriptionRepo;
            _classRepo = classRepo;
            _userRepo = userRepo;
            _planRepo = planRepo;
        }

        public async Task<InscriptionResult> Inscribe(InscriptionRequest request)
        {
            // 1. Validar que el usuario existe y es un Client
            var user = await _userRepo.GetById(request.UserId);
            if (user == null || user is not Client client)
                return new InscriptionResult { Success = false, ErrorMessage = "El usuario no existe o no es un cliente." };

            // 2. Validar que la clase existe
            var gymClass = await _classRepo.GetById(request.ClassId);
            if (gymClass == null)
                return new InscriptionResult { Success = false, ErrorMessage = "La clase no existe." };

            // 3. Validar que el usuario no esté ya inscripto
            var existing = await _inscriptionRepo.GetByUserAndClass(request.UserId, request.ClassId);
            if (existing != null && existing.IsActive)
                return new InscriptionResult { Success = false, ErrorMessage = "El cliente ya está inscripto en esta clase." };

            // 4. Validar cupos disponibles en la clase
            var inscriptions = await _inscriptionRepo.GetByClassId(request.ClassId);
            var activeCount = inscriptions.Count(i => i.IsActive);
            if (activeCount >= gymClass.Max_Users)
                return new InscriptionResult { Success = false, ErrorMessage = "La clase no tiene cupos disponibles." };

            // 5. Validar que el cliente tiene plan
            if (client.Id_Plan == null)
                return new InscriptionResult { Success = false, ErrorMessage = "El cliente no tiene un plan activo." };

            // 6. Validar límite del plan
            var plan = await _planRepo.GetById(client.Id_Plan.Value);
            if (plan == null)
                return new InscriptionResult { Success = false, ErrorMessage = "El plan del cliente no existe." };

            if (!plan.IsUnlimited)
            {
                var clientInscriptions = await _inscriptionRepo.GetByUserId(request.UserId);
                var clientActiveCount = clientInscriptions.Count(i => i.IsActive);
                if (clientActiveCount >= plan.Max_Class)
                    return new InscriptionResult { Success = false, ErrorMessage = $"El cliente alcanzó el límite de clases de su plan ({plan.Max_Class})." };
            }

            // 7. Crear la inscripción
            var inscription = new Inscription
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                ClassId = request.ClassId,
                InscriptionDate = DateTime.UtcNow,
                IsActive = true
            };

            await _inscriptionRepo.Add(inscription);
            await _inscriptionRepo.Save();

            return new InscriptionResult
            {
                Success = true,
                Data = inscription.ToInscriptionResponse()
            };
        }
    }
}