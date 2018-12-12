﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingVocabulary.Models
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }
        [Required]
         public string Name { get; set; }

        public IEnumerable<Vocabulary> Vocabulary { get; set; }
    }
}
