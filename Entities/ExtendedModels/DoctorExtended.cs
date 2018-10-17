﻿using System;
using System.Dynamic;
using Entities.Models;

namespace Entities.ExtendedModels
{
    public class DoctorExtended
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string BoardNumber { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public DoctorExtended()
        {
        }

        public DoctorExtended(Doctor doctor)
        {
            Id = doctor.Id;
            Name = doctor.Name;
            BoardNumber = doctor.BoardNumber;
            Telephone = doctor.Telephone;
        }
    }
}