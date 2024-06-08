namespace MainteXpert.Common.Models.Pagination
{
    public class PaginationDocument<TDocument>
    {
        public int Count { get; set; }
        public IEnumerable<TDocument> Document { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public PaginationDocument(IEnumerable<TDocument> Document, int Count, int TotalPages, int TotalRecords)
        {
            this.Count = Count;
            this.Document = Document;
            this.TotalPages = TotalPages;
            this.TotalRecords = TotalRecords;
        }
    }
}
