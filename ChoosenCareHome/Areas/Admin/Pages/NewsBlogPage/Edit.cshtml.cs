using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using System.Net.Http.Headers;

namespace ChoosenCareHome.Areas.Admin.Pages.NewsBlogPage
{
    public class EditModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnv;

        public EditModel(ChoosenCareHome.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        [BindProperty]
        public NewsBlog NewsBlog { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.NewsBlogs == null)
            {
                return NotFound();
            }

            var newsblog =  await _context.NewsBlogs.FirstOrDefaultAsync(m => m.Id == id);
            if (newsblog == null)
            {
                return NotFound();
            }
            NewsBlog = newsblog;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            int imgCount = 0;
            if (HttpContext.Request.Form.Files != null && HttpContext.Request.Form.Files.Count > 0)
            {
                var newFileName = string.Empty;
                var newFileNameThumbnail = string.Empty;
                var filePath = string.Empty;
                var filePathThumbnail = string.Empty;
                string pathdb = string.Empty;
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {

                    if (file.Length > 0)
                    {
                        filePath = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        filePathThumbnail = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        imgCount++;
                        var now = DateTime.Now;
                        string nameproduct = NewsBlog.Title.Replace("@", "").Replace(".", "").Replace(" ", "").Replace("(", "").Replace(")", "").Replace(":", "");
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/News/".Trim();

                        filePath = $"{_hostingEnv.WebRootPath}{fileDbPathName}".Trim();

                        if (!(Directory.Exists(filePath)))
                            Directory.CreateDirectory(filePath);

                        var fileName = "";
                        fileName = filePath + $"{newFileName}".Trim();

                        using (FileStream fsa = System.IO.File.Create(fileName))
                        {
                            file.CopyTo(fsa);
                            fsa.Flush();
                        }



                        NewsBlog.Image = $"{fileDbPathName}{newFileName}";


                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }

            _context.Attach(NewsBlog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsBlogExists(NewsBlog.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NewsBlogExists(int id)
        {
          return (_context.NewsBlogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
