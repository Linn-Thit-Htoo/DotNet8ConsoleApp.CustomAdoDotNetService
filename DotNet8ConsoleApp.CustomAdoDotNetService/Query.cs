namespace DotNet8ConsoleApp.CustomAdoDotNetService;

public class Query
{
    public static string GetAllBlogsQuery { get; } =
        @"SELECT BlogId, BlogTitle, BlogAuthor, BlogContent FROM Tbl_Blog
ORDER BY BlogId DESC";
    public static string CreateBlogQuery { get; } =
        @"INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent)
VALUES(@BlogTitle, @BlogAuthor, @BlogContent)";
}
