﻿using System.ComponentModel.DataAnnotations;

namespace Pure_Life.Models.Analiza
{
    public class Lloji
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Emri { get; set; }

        [StringLength(500)]
        public string VleratReferente { get; set; }

        // Navigation property
        public ICollection<AnalizaLloji> AnalizaLlojet { get; set; }
    }
}
