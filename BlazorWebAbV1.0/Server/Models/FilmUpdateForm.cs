using System;
using Microsoft.AspNetCore.Http;


namespace BlazorWebAbV1.Server.Models
{
    public class FilmUpdateForm

    {
        public string FilmTitle { get; set; }

        public string FilmCertificate { get; set; }

        public string FilmDescription { get; set; }

        //public string FilmImage { get; set; }
        public IFormFile FilmImage { get; set; }

        public decimal FilmPrice { get; set; }

        public int FilmRating { get; set; }

        public DateTime FilmReleaseDate { get; set; }

    }

}
