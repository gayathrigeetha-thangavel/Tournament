using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.DTO
{
    public class GameDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public int TournamentDetailsId {  get; set; }
    }
}
