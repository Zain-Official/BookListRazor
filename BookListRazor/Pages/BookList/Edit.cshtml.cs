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
    public class EditModel : PageModel
    {
        private ApplicationDBContext applicationDBContext;

        public EditModel(ApplicationDBContext applicationDB)
        {
            applicationDBContext = applicationDB;
        }

        [BindProperty]
        public Book Book { get; set; }


        public async Task OnGet(int id)
        {
            Book = await applicationDBContext.Book.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFromDB = await applicationDBContext.Book.FindAsync(Book.Id);
                                 
                BookFromDB.Name = Book.Name;
                BookFromDB.Author = Book.Author;
                BookFromDB.ISBN = Book.ISBN;

                await applicationDBContext.SaveChangesAsync();         
            }
            return RedirectToPage("Index");
        }
    }
}
