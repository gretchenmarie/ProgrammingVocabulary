using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingVocabulary.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required]
        public string Word { get; set; }

        [Required]
        public string Definition { get; set; }




        [Required]
        public string Link { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public ICollection<UserVocabulary> UserVocabulary { get; set; }
    }
}
