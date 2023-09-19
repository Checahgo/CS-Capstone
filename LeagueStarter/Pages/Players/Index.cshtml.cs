using League.Models;
using League.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace League.Pages.Players
{
    public class IndexModel : PageModel
    {
        //Inject db context for use
        private readonly LeagueContext _context;

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        //Create a list of players
        public List<Player> Players { get; set; }

        //Linq Query: sort players by position
        public List<Player> GetPlayersPosition(string Position)
        {
            return Players.Where(p => p.Position.Equals(Position)).OrderBy(p => p.Position).ToList();
        }
        //Linq Query: sort players by team
        public List<Player> GetPlayersTeam(string TeamId)
        {
            return Players.Where(p => p.TeamId.Equals(TeamId)).OrderBy(p => p.TeamId).ToList();
        }
        //Linq Query: sort players by name
        public List<Player> GetPlayersName(string Name)
        {
            return Players.Where(p => p.Name.Equals(Name)).OrderBy(p => p.Name).ToList();
        }
    }
}