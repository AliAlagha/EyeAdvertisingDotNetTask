namespace EyeAdvertisingDotNetTask.Data.DbEntities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            IsDeleted = false;
            CreatedAt = DateTime.Now;
        }

        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedById { get; set; }
        public string? UpdatedById { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}