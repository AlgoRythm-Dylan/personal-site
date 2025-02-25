namespace PublicFacing.Data
{
    public class BlogEntry
    {
        public string Series { get; set; } = BlogSeries.UNKNOWN;
        public string? SubSeries { get; set; } = null;
        public string Title = "(untitled)";
        public string? Description = null;
        public string Link = "/";
        public int? Order = null;
    }
}
