using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using ChoosenCareHome.Data.Model;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace ChoosenCareHome.Pages.Data
{
         public class DocumantUploadModel : PageModel
    {
        private readonly ChoosenCareHome.Data.ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnv;

        public DocumantUploadModel(ChoosenCareHome.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnv)
        {
            _context = context;
            _hostingEnv = hostingEnv;
        }
        [BindProperty]
        public Application Application { get; set; } = default!;

        [BindProperty]
        public List<Documents> DocumentsList { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }
            Application = application;

            DocumentsList = await _context.Documents.Where(x=>x.ApplicationId == application.Id).ToListAsync();
            return Page();
        }

        [BindProperty]
        public Documents Documents { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
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
                        string nameproduct = file.Name + "-" + Application.Surname.Replace("@", "-").Replace(".", "-");
                        var uniqueFileName = $"{now.Millisecond}{now.Minute}{now.Second}{now.Day}-".Trim() + nameproduct;

                        var fileExtension = Path.GetExtension(filePath);

                        newFileName = uniqueFileName + fileExtension;

                        // if you wish to save file path to db use this filepath variable + newFileName
                        var fileDbPathName = $"/Documents/".Trim();

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


                        Documents.FileUrl = $"{fileDbPathName}{newFileName}";


                        #region Save Image Propertie to Db

                        #endregion

                        if (imgCount >= 5)
                            break;
                    }
                }
            }

            Documents.ApplicationId = Application.Id;
            _context.Documents.Add(Documents);
            await _context.SaveChangesAsync();

            return RedirectToPage("./DocumantUpload", new {id = Application.Id});
        }
    }

}
