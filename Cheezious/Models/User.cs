﻿namespace Cheezious.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime JoinedOn { get; set; }
        public string Address { get; set; }
        public string AccessToken { get; set; }
        public virtual Roles Role { get; set; }
        public int RoleId { get; set; }

        public static implicit operator User(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
