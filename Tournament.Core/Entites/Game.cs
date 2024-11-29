using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Core.Entites
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }

        // Foreign Key from TournamentDetails class
        public int TournamentDetailsId { get; set; }

    }
}
