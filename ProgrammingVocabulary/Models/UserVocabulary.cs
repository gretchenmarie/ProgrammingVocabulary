using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingVocabulary.Models
{
    public class UserVocabulary
    {
        [Key]
        public int UserVocabularyId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int VocabularyId { get; set; }


    }
}
