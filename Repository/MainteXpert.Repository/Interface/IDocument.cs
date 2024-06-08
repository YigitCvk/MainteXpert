namespace MainteXpert.Repository.Interface
{
    public interface IDocument
    {

        public string Id { get; set; }
        public string CreatedById { get; set; }
        public string UpdatedById { get; set; }
        public string DeletedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DocumentStatus Status { get; set; }

    }
}
