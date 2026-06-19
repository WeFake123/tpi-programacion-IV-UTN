using Domain.Entity;
namespace Domain.Interface
{
    public interface IPlanRepository
    {
        List<Plan> GetAll();
    }
}
