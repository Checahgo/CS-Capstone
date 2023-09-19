using System.Data.Common;
using System.Threading.Tasks;
using League.Data;
using League.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;



    public class TeamModel : PageModel
    {
        //Inject db context
        private readonly LeagueContext _context;

        public TeamModel(LeagueContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            //Conference = await _context.Conferences.FindAsync(ConferenceId);
        }

    }