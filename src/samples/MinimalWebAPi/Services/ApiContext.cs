using Microsoft.EntityFrameworkCore;
using MinimalWebAPi.Models;

namespace MinimalWebAPi.Services;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options, DbSet<Article> articles)
        : base(options)
    {
        Articles = articles;
    }

    public ApiContext(DbContextOptions<ApiContext> options): base(options)
    {
        
    }

    public DbSet<Article> Articles { get; set; }
}