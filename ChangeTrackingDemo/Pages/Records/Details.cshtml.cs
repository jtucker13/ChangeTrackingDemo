﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChangeTrackingDemo.Data;
using ChangeTrackingDemo.Models;

namespace ChangeTrackingDemo.Pages.Records
{
    public class DetailsModel : PageModel
    {
        private readonly ChangeTrackingDemo.Data.ChangeTrackingDemoContext _context;

        public DetailsModel(ChangeTrackingDemo.Data.ChangeTrackingDemoContext context)
        {
            _context = context;
        }

        public Record Record { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Record = await _context.Record.FirstOrDefaultAsync(m => m.RecordID == id);

            if (Record == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
