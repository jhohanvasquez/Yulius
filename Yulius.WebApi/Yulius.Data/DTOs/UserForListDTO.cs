using System;
using System.Collections.Generic;
using System.Text;

namespace Yulius.Data.DTOs
{
    public class UserForListDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string Role { get; set; }

    }

}
