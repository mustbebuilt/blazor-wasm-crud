using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorWebAbV1.Shared
{
    public class Film

    {

        public int FilmID { get; set; }

        public string FilmTitle { get; set; }

        public string FilmCertificate { get; set; }

        public string FilmDescription { get; set; }

        public string FilmImage { get; set; }

        public decimal FilmPrice { get; set; }

        public int FilmRating { get; set; }

        public DateTime FilmReleaseDate { get; set; }

    }

}
