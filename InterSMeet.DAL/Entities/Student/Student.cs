using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string BirthDate { get; set; } = null!;
        public Studies studies { get; set; } = null!;
        public Language Lang { get; set; } = null!;

        // Role FK
        [Column("role_id")]
        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
