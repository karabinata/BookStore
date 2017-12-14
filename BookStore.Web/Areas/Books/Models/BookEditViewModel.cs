namespace BookStore.Web.Areas.Books.Models
{
    public class BookEditViewModel : BookDetailsViewModel
    {
        public string PublisherName { get; set; }

        public string ISBN { get; set; }

        public bool IsNew { get; set; }

        public int PublicationYear { get; set; }

        public string Subtitle { get; set; }

        public string SeriesAndLibraries { get; set; }

        public string TranslatorName { get; set; }

        public string PaintorName { get; set; }

        public byte[] FirstPicture { get; set; }

        public byte[] SecondPicture { get; set; }

        public byte[] ThirdPicture { get; set; }

        public string KeyWords { get; set; }

        public string Format { get; set; }

        public double Heigth { get; set; }

        public double Width { get; set; }

        public double Тhickness { get; set; }

        public int Weigth { get; set; }

        public string Information { get; set; }

        public string NotesForTraider { get; set; }
    }
}
