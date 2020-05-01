﻿using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class UserDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }
        public Guid? StudentId { get; set; }
        public string Email { get; set; }

        public virtual StudentDAO Student { get; set; }
    }
}
