using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingVocabulary.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }

        [Required]
        public string User1Id { get; set; }

        [Required]
        public ApplicationUser Friend1 { get; set; }

        [Required]
        public string User2Id { get; set; }

        [Required]
        public ApplicationUser Friend2 { get; set; }


    }
}
