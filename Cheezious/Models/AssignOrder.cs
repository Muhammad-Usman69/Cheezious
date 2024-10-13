namespace Cheezious.Models
{
    public class AssignOrder
    {

        public int Id { get; set; }

        public DateTime DeliverdOn { get; set; }

        public string? Image { get; set; }

        public virtual User Rider { get; set; }

        public int RiderId { get; set; }
      

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }


     

    }
}
