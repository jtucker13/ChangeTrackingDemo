using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChangeTrackingDemo.Data;
using ChangeTrackingDemo.Models;

namespace ChangeTrackingDemo.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ChangeTrackingDemo.Data.ChangeTrackingDemoContext _context;

        public IndexModel(ChangeTrackingDemo.Data.ChangeTrackingDemoContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Student.ToListAsync();
        }
    }
}
