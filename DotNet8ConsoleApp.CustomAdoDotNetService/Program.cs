using System.Data.SqlClient;

namespace DotNet8ConsoleApp.CustomAdoDotNetService;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Read();
        //await Create("Sample Title", "Sample Author", "Sample Content");
    }

    public static async Task Read()
    {
        try
        {
            AdoDotNetService adoDotNetService = new();
            string query = @"SELECT BlogId, BlogTitle, BlogAuthor, BlogContent FROM Tbl_Blog
ORDER BY BlogId DESC";
            List<BlogModel> lst = await adoDotNetService.QueryAsync<BlogModel>(query);

            foreach (var item in lst)
            {
                Console.WriteLine($"Blog Id: {item.BlogId}");
                Console.WriteLine($"Blog Title: {item.BlogTitle}");
                Console.WriteLine($"Blog Author: {item.BlogAuthor}");
                Console.WriteLine($"Blog Content: {item.BlogContent}");
            }
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }

    public static async Task Create(string blogTitle, string blogAuthor, string blogContent)
    {
        try
        {
            AdoDotNetService adoDotNetService = new();
            string query = @"INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent)
VALUES(@BlogTitle, @BlogAuthor, @BlogContent)";
            List<SqlParameter> parameters = new()
        {
            new("@BlogTitle", blogTitle),
            new("@BlogAuthor", blogAuthor),
            new("@BlogContent", blogContent)
        };
            int result = await adoDotNetService.ExecuteAsync(query, parameters.ToArray());

            Console.WriteLine(result > 0 ? "Saving Successful." : "Saving Fail.");
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}

public class BlogModel
{
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }
}

public class CustomException : Exception
{
    public CustomException(string? message) : base(message)
    {
    }
}