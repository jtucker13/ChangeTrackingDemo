using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChangeTrackingDemo.Data;
using ChangeTrackingDemo.Models;
using Microsoft.AspNetCore.Http;

namespace ChangeTrackingDemo.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ChangeTrackingDemo.Data.ChangeTrackingDemoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(ChangeTrackingDemo.Data.ChangeTrackingDemoContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
            audit.NewRecord(_httpContextAccessor.HttpContext.User.Identity.Name, Student, Student.StudentID);
            _context.Record.Add(audit);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
