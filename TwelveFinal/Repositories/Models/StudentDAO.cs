using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class StudentDAO
    {
        public StudentDAO()
        {
            Forms = new HashSet<FormDAO>();
            Users = new HashSet<UserDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public Guid? EthnicId { get; set; }
        public Guid? TownId { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Identify { get; set; }
        public Guid? HighSchoolId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double? Maths { get; set; }
        public double? Literature { get; set; }
        public double? Languages { get; set; }
        public double? Physics { get; set; }
        public double? Chemistry { get; set; }
        public double? Biology { get; set; }
        public double? History { get; set; }
        public double? Geography { get; set; }
        public double? CivicEducation { get; set; }
        public int Status { get; set; }
        public byte[] Image { get; set; }

        public virtual EthnicDAO Ethnic { get; set; }
        public virtual HighSchoolDAO HighSchool { get; set; }
        public virtual TownDAO Town { get; set; }
        public virtual ICollection<FormDAO> Forms { get; set; }
        public virtual ICollection<UserDAO> Users { get; set; }
    }
}
