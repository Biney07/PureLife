﻿using System.ComponentModel.DataAnnotations;

namespace Pure_Life.ViewModels
{
    public class AdminUserViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
