﻿namespace DotNet8ConsoleApp.CustomAdoDotNetService;

public class Program
{
    public static async Task Main(string[] args)
    {
        DotNetEnv.Env.Load(".env");

        await Read();
        //await Create("Sample Title", "Sample Author", "Sample Content");
    }

    public static async Task Read()
    {
        try
        {
            AdoDotNetService adoDotNetService = new();
            string query = Query.GetAllBlogsQuery;
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
            string query = Query.CreateBlogQuery;
            List<SqlParameter> parameters =
                new()
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
