using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlazorWebAbV1.Server.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace BlazorWebAbV1.Server.Controllers
{
    public class FilmsController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilmsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
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
        public IActionResult ApiAddFilm([FromBody] Film model)
        {

            if (ModelState.IsValid)
            {
                ////string uniqueFileName = UploadedFile(model);
                //var tuple = UploadedFile(model);
                //string uniqueFileName = tuple.Item1;
                //string fileExt = tuple.Item2;
                //long fileSize = tuple.Item3;
                //string[] permittedExtensions = { ".gif", ".jpg", ".jpeg", ".png" };

                //// 5 MB
                //if (fileSize > 5000000)
                //{
                //    ViewBag.msg = "Image Too Big: " + fileSize.ToString();

                //    return View();
                //}
                //if (!permittedExtensions.Contains(fileExt))
                //{
                //    ViewBag.msg = "Wrong File type " + fileExt;
                //    return View();
                //}

                Film newFilm = new Film
                {
                    FilmTitle = model.FilmTitle,
                    FilmCertificate = model.FilmCertificate,
                    FilmDescription = model.FilmDescription,
                    FilmImage = model.FilmImage,  // uniqueFileName, // 
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

        private Tuple<string, string, long> UploadedFile(FilmUpdateForm model)
        {
            string uniqueFileName = null;
            string fileExtension = null;
            long fileSize = 0;

            if (model.FilmImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                //uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FilmImage.FileName;
                fileExtension = Path.GetExtension(model.FilmImage.FileName);
                fileExtension = fileExtension.ToLowerInvariant();
                uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                //uniqueFileName = "test";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FilmImage.CopyTo(fileStream);
                    fileSize = fileStream.Length;
                }
            }
            return new Tuple<string, string, long>(uniqueFileName, fileExtension, fileSize);
        }


    }
}
