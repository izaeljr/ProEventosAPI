using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Application.DTOs
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Function { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}