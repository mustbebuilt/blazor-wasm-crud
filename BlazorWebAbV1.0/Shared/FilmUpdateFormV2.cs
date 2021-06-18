using System;

namespace BlazorWebAbV1.Shared
{
    public class FilmUpdateFormV2

    {
        public string FilmTitle { get; set; }

        public string FilmCertificate { get; set; }

        public string FilmDescription { get; set; }

        public bool Uploaded { get; set; }

        public string FileName { get; set; }

        public string StoredFileName { get; set; }

        public int ErrorCode { get; set; }

        public decimal FilmPrice { get; set; }

        public int FilmRating { get; set; }

        public DateTime FilmReleaseDate { get; set; }

    }

}
