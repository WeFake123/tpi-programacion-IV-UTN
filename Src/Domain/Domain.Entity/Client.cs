using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entity
{
    public class Client : User
    {
        public Guid? Id_Plan { get; set; }

        [ForeignKey("Id_Plan")]
        public Plan? Plan { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }

    }
}
