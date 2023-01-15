﻿using System;
using System.Collections.Generic;
using Lab6.Models.EntityMetadata;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Models.DataAccess
{
    public partial class Employee
    {
        public Employee()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Role> Roles { get; set; }
    }
}
