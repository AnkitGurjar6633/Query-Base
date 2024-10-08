﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBase.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }
    }
}
