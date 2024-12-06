using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTO
{
    public class TournamentCreateDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
    }
}
