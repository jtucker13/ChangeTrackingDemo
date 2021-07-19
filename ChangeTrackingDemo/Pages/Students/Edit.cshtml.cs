using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChangeTrackingDemo.Data;
using ChangeTrackingDemo.Models;
using Microsoft.AspNetCore.Http;

namespace ChangeTrackingDemo.Pages.Students
{
    public class EditModel : CustomPageModel
    {
        private readonly ChangeTrackingDemo.Data.ChangeTrackingDemoContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(ChangeTrackingDemo.Data.ChangeTrackingDemoContext context, IHttpContextAccessor httpContextAccessor)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            Student StudentToUpdate = await _context.Student.FindAsync(id);
            Student OldStudent = new Student();
            if(StudentToUpdate == null)
            { 
                return NotFound();
            }
            DeepCopy(StudentToUpdate, OldStudent);
            if(await TryUpdateModelAsync<Student>(StudentToUpdate, "Student"))
            {
                await _context.SaveChangesAsync();
                Record audit = new Record();
                audit.CompareRecords(OldStudent, StudentToUpdate, _httpContextAccessor.HttpContext.User.Identity.Name, id);
                if(audit.anyChanges)
                {
                    _context.Record.Add(audit);
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
            }
            return Page();
        }

    }
}
