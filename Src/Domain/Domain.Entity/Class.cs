namespace Domain.Entity
{
    public class Class
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Max_Users { get; set; }
        public List<Schedule> Schedules { get; set; } = new();

        public Class() { }

        //pk del admin que creo la clase

        //public Guid adminID { get; set; }

    }
}
