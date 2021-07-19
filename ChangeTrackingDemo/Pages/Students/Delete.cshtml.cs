using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChangeTrackingDemo.Data;
using ChangeTrackingDemo.Models;
using Microsoft.AspNetCore.Http;

namespace ChangeTrackingDemo.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly ChangeTrackingDemo.Data.ChangeTrackingDemoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteModel(ChangeTrackingDemo.Data.ChangeTrackingDemoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.StudentID == id);

            if (Student == null)
            {
                return NotFound();
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FindAsync(id);

            if (Student != null)
            {
                Record audit = new Record();
                audit.DeleteRecord(Student, _httpContextAccessor.HttpContext.User.Identity.Name, Student.StudentID);
                _context.Record.Add(audit);
                _context.Student.Remove(Student);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
