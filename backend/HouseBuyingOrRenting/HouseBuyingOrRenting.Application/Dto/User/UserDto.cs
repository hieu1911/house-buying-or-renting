﻿using HouseBuyingOrRenting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuyingOrRenting.Application
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string? UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Guid? DistrictId { get; set; }

        public string? Address { get; set; }

        public int Role { get; set; }
    }
}
