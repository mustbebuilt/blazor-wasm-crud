using System;

namespace BlazorWebAbV1.Shared
{
    public class FilmUpdateForm

    {
        public string FilmTitle { get; set; }

        public string FilmCertificate { get; set; }

        public string FilmDescription { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }

        public decimal FilmPrice { get; set; }

        public int FilmRating { get; set; }

        public DateTime FilmReleaseDate { get; set; }

    }

}
