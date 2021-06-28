using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookListRazor.Pages.BookList
{
    public class UpsertModel : PageModel
    {
        private ApplicationDBContext applicationDBContext;

        public UpsertModel(ApplicationDBContext applicationDB)
        {
            applicationDBContext = applicationDB;
        }

        [BindProperty]
        public Book Book { get; set; }


        public async Task<IActionResult> OnGet(int? id)
        {
            Book = new Book();
            if (id==null)
            {
                //create
                return Page();
            }
            Book = await applicationDBContext.Book.FirstOrDefaultAsync(x => x.Id == id);
            if (Book == null)
            {
                //Update
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    applicationDBContext.Add(Book);
                }
                else
                {
                    applicationDBContext.Book.Update(Book);
                }

             

                await applicationDBContext.SaveChangesAsync();
                return RedirectToPage("Index");
            }

            return RedirectToPage();
        }
    }
}
