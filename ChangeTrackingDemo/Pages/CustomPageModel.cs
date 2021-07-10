using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChangeTrackingDemo.Pages
{
    public class  CustomPageModel:PageModel
    {
        public void DeepCopy(object original, object copy)
        {
            foreach (var property in original.GetType().GetProperties())
            {
                var value = property.GetValue(original, null);
                property.SetValue(copy, value);
            }
        }
    }
}
