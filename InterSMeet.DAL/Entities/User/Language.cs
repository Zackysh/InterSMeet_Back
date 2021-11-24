using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InterSMeet.DAL.Entities
{
    public class Language
    {
        [Key]
        [Column("language_id")]
        public int LanguageId { get; set; }
        [Column("name")]
        [MaxLength(5)]
        public string Name { get; set; } = null!;
    }
}
