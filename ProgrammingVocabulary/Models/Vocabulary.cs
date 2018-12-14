using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingVocabulary.Models
{
    public class Vocabulary
    {
        [Key]
        public int VocabularyId { get; set; }
       
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