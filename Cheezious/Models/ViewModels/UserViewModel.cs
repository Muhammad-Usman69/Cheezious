using System.Data;

namespace Cheezious.Models.ViewModels
{

    public class UserViewModel
    {
        public User Users { get; set; }


        public List<Order> Orders { get; set; }
        public List<AssignOrder> AssignOrders { get; set; }

        public UserViewModel()
        {
            Orders = new List<Order>();
            AssignOrders = new List<AssignOrder>();
        }
    }
}
