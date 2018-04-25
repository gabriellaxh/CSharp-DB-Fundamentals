using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.DataProcessor.Dto.Export
{
    public class EmployeeDtoImport
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(15, 80)]
        public int Age { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Position { get; set; }
    }
}
