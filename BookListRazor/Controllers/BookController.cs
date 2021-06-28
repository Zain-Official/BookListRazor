using BookListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _Db;

        public BookController(ApplicationDBContext applicationDB)
        {
            _Db = applicationDB;
        }

        [HttpGet]

        
          public async Task<IActionResult> GetAll()
          {
              return Json(new { data =await _Db.Book.ToListAsync() });
          }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
       
            var bookDB = await _Db.Book.FirstOrDefaultAsync(x => x.Id == id);
            if (bookDB == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _Db.Book.Remove(bookDB);
            await _Db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successfully" });
        }

    }
}
