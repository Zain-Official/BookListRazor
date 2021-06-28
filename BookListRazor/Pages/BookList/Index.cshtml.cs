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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDBContext applicationDBContext;

        public IndexModel(ApplicationDBContext applicationDB)
        {
            applicationDBContext = applicationDB;
        }

        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await applicationDBContext.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await applicationDBContext.Book.FindAsync(id);
            if (book ==null)
            {
                return NotFound();
            }
            applicationDBContext.Remove(book);
            await applicationDBContext.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
