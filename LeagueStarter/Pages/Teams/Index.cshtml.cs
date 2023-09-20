using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using League.Data;
using League.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace League.Pages.Teams
{
    public class IndexModel : PageModel
    {
        //inject database context
        private readonly LeagueContext _context;
        

        public IndexModel(LeagueContext context)
        {
            _context = context;
        }

        //Load Conferences, Divisions and Teams for use
        public List<Conference>  Conferences { get; set; }
        public List<Division> Divisions { get; set; }
        public List<Team> Teams { get; set; }

        //Favorite team selection storage
        [BindProperty(SupportsGet = true)]
        public string FavoriteTeam { get; set; }
        public SelectList AllTeams { get; set; }


        public async Task OnGetAsync()
        {
            //Load from db
            Conferences = await _context.Conferences.ToListAsync();
            Divisions = await _context.Divisions.ToListAsync();
            Teams = await _context.Teams.ToListAsync();
            
            //Query for dropdown list
            IQueryable<string> teamQuery = from t in _context.Teams
                                           orderby t.TeamId
                                           select t.TeamId;

            AllTeams = new SelectList(await teamQuery.ToListAsync());
            
            //Favorite Cookie init
            if(FavoriteTeam != null)
            {
                HttpContext.Session.SetString("_Favorite", FavoriteTeam);
            } else 
            {
                FavoriteTeam = HttpContext.Session.GetString("_Favorite");
            }
        }

        //Get all divisions in conferences
        public List<Division> GetConferenceDivisions(string ConferenceId)
        {
            return Divisions.Where(d => d.ConferenceId.Equals(ConferenceId)).OrderBy(d => d.Name).ToList();
        }

        //Get all teams in divisions
        public List<Team> GetDivisionTeams(string DivisionId)
        {
            return Teams.Where(t => t.DivisionId.Equals(DivisionId)).OrderByDescending(t => t.Win).ToList();
        }
    }

    /*(public class RealTimeData
    {
        public string Data { get; set; }
        RealTimeData realTimeData = JsonConverter.DeserializeObject<RealTimeData>(Data);

    }*/
}