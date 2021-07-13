using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChangeTrackingDemo.Data;
using ChangeTrackingDemo.Models;

namespace ChangeTrackingDemo.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ChangeTrackingDemo.Data.ChangeTrackingDemoContext _context;

        public CreateModel(ChangeTrackingDemo.Data.ChangeTrackingDemoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        /// <summary>
        /// Post function which creates a record with the primary key
        /// Has to call SaveChanges twice to create primary key for new Student referenced in NewRecord call
        /// </summary>
        /// <returns>@Students/Index</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Student.Add(Student);
            await _context.SaveChangesAsync();
            Record audit = new Record();
            audit.NewRecord(User.Identity.Name, Student, Student.StudentID);
            _context.Record.Add(audit);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
