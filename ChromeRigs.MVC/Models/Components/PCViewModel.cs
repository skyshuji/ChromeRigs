﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ChromeRigs.MVC.Models.Components
{
    public class PCViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }


    }
}
