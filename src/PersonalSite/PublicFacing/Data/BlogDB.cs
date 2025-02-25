namespace PublicFacing.Data
{
    public static class BlogDB
    {
        public static List<BlogEntry> Blogs = new()
        {
            new()
            {
                Series = BlogSeries.PROGRAMMING,
                SubSeries = BlogSubSeries.IDIOMATIC_ZIG,
                Order = 1,
                Title = "Idiomatic Zig #1 - Modules",
                Description = "Exploring how to create Zig modules the Zig way",
                Link = "/Blog/Zig/Modules"
            }
        };
    }
}
