using League.Models;
using League.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        //Create a series of variables for the filter and search form

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        //Create SelectLists for the filter dropdowns

        [BindProperty(SupportsGet = true)]
        public SelectList Teams { get; set; }

        [BindProperty(SupportsGet = true)]
        public SelectList Positions { get; set; }

        //Create variables for the filter dropdowns

        [BindProperty(SupportsGet = true)]
        public string SelectedTeam { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedPosition { get; set; }

        //Create a variable for the sort dropdown

        [BindProperty(SupportsGet = true)]
        public string SortField { get; set; } = "Name";

        //Read the favorite team cookie into this member variable

        public string FavoriteTeam { get; set; }

        public void OnGet()
        {
            //Create a base query that retrieves all players

            var players = from p in _context.Players
                          select p;

            //Modify the query if the user is searching

            if (!string.IsNullOrEmpty(SearchString))
            {
                players = players.Where(p => p.Name.Contains(SearchString));
            }

            //Modify the query if the user is filtering

            if (!string.IsNullOrEmpty(SelectedTeam))
            {
                players = players.Where(p => p.TeamId == SelectedTeam);
            }

            if (!string.IsNullOrEmpty(SelectedPosition))
            {
                players = players.Where(p => p.Position == SelectedPosition);
            }

            //Modify the query if the user is sorting

            if (SortField == "Name")
            {
                players = players.OrderBy(p => p.Name);
            }
            else if (SortField == "Team")
            {
                players = players.OrderBy(p => p.Team);
            }
            else if (SortField == "Position")
            {
                players = players.OrderBy(p => p.Position);
            }
            else if (SortField == "Number")
            {
                players = players.OrderBy(p => p.Number);
            }

            //Execute the query and store the results in the Players property

            Players = players.ToList();

            //Create the select lists for the filter dropdowns

            Teams = new SelectList(_context.Teams.Distinct().OrderBy(t => t), SelectedTeam);
            Positions = new SelectList(_context.Players.Distinct().OrderBy(p => p.Position), SelectedPosition);

            //Read the favorite team cookie into this member variable

            FavoriteTeam = Request.Cookies["FavoriteTeam"];
        }

    }   
}