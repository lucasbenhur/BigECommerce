﻿namespace BigECommerce.Domain.Entities
{
    public class User
    {
        protected User()
        {
        }

        public User(
            string email,
            string password,
            string role)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Role = role;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
