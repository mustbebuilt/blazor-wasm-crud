using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BlazorWebAbV1.Shared;
using BlazorWebAbV1.Server.Models;

namespace BlazorWebAbV1.Server.Controllers
{
    public class FilmsController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IWebHostEnvironment env;

        public FilmsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IWebHostEnvironment env)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
            this.env = env;
        }

        public IActionResult AllMovies()
        {
            List<Film> model = _context.Films.ToList();
            return View(model);
        }

        [Produces("application/json")]
        public IActionResult ApiData()
        {
            List<Film> model = _context.Films.ToList();
            return Json(model, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        }

        [Produces("application/json")]
        public IActionResult ApiFilmDetails(int id)
        {
            //List<Movie> model = _context.Movies.Find(FilmID);
            Film model = _context.Films.Find(id);
            return Json(model, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        }

        [HttpPost]
        public IActionResult ApiAddFilm([FromBody] FilmUpdateForm model)
        {
    
            if (ModelState.IsValid)
            {
                var tuple = UploadedFile(model);
                string uniqueFileName = tuple.Item1;
                string fileExt = tuple.Item2;
                long fileSize = tuple.Item3;
                string[] permittedExtensions = { ".gif", ".jpg", ".jpeg", ".png" };

                // 5 MB
                // 512000
                if (fileSize > 512000)
                {
                    return Ok("Bad Image Size");
                }
                if (!permittedExtensions.Contains(fileExt))
                {
                    return Ok("Bad Image type of  " + fileExt);
                }

                Film newFilm = new Film
                {
                    FilmTitle = model.FilmTitle,
                    FilmCertificate = model.FilmCertificate,
                    FilmDescription = model.FilmDescription,
                    FilmImage = uniqueFileName,
                    FilmPrice = model.FilmPrice,
                    FilmRating = model.FilmRating,
                    FilmReleaseDate = model.FilmReleaseDate,
                };
                _context.Add(newFilm);
                _context.SaveChanges();
                return Ok("Good");
            }

            return Ok("Bad");

        }

        [HttpPut]
        public IActionResult ApiEditFilm([FromBody] Film model)
        {

            if (ModelState.IsValid)
            {

                Film editFilm = new Film
                {
                    FilmTitle = model.FilmTitle,
                    FilmCertificate = model.FilmCertificate,
                    FilmDescription = model.FilmDescription,
                    FilmImage = model.FilmImage,
                    FilmPrice = model.FilmPrice,
                    FilmRating = model.FilmRating,
                    FilmReleaseDate = model.FilmReleaseDate,
                };
                _context.Films.Update(editFilm);
                _context.SaveChanges();
                return Ok("Good");
            }

            return Ok("Bad");

        }


        [HttpDelete]
        public IActionResult ApiDeleteFilm(int Id)
        {
            Film model = _context.Films.Find(Id);
            _context.Films.Remove(model);
            _context.SaveChanges();
            return Ok("Done");
        }

        private Tuple<string, string, long> UploadedFile(FilmUpdateForm uploadedFile)
        {
            string uniqueFileName = null;
            string fileExtension = null;
            long fileSize = 0;

            if (uploadedFile.FileContent != null)
            {
                fileExtension = Path.GetExtension(uploadedFile.FileName);
                fileExtension = fileExtension.ToLowerInvariant();
                uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                var path = $"{env.WebRootPath}\\{uniqueFileName}";
                var fs = System.IO.File.Create(path);
                fs.Write(uploadedFile.FileContent, 0,
        uploadedFile.FileContent.Length);
                fs.Close();
                string targetPath = $"{env.WebRootPath}//images";
                string destFile = System.IO.Path.Combine(targetPath, uniqueFileName);
                System.IO.File.Copy(path, destFile, true);

            }
            return new Tuple<string, string, long>(uniqueFileName, fileExtension, fileSize);
        }




    }
}
