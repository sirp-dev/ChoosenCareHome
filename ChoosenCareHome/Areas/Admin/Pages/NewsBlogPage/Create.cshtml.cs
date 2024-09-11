using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChoosenCareHome.Data;
using ChoosenCareHome.Data.Model;
using System.Net.Http.Headers;

namespace ChoosenCareHome.Areas.Admin.Pages.NewsBlogPage
{
    public class CreateModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnv;

        public CreateModel(ChoosenCareHome.Data.ApplicationDbContext context, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public NewsBlog NewsBlog { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            _context.NewsBlogs.Add(NewsBlog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
