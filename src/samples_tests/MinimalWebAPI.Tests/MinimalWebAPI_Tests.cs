using Microsoft.EntityFrameworkCore;
using MinimalWebAPi.Models;
using MinimalWebAPi.Services;
using Xunit;

namespace MinimalWebAPI.Tests;

public class MinimalWebAPI_Tests
{
    [Fact]
    public async Task GivenArticleRequest_WhenArticleIsCreated_ThenArticleIsCreatedSuccessfully()
    {
        // Given
        var articleRequest = new ArticleRequest("Title", "Content", null);

        var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase("api1")
            .Options;

        var apiContext = new ApiContext(options);

        // When
        await new ArticleService(apiContext).CreateArticle(articleRequest);

        // Then

        Assert.True(await apiContext.Articles.FirstOrDefaultAsync() != null);
    }

    [Fact]
    public async Task GivenExistingArticle_WhenArticleIsUpdated_ThenArticleIsUpdatedSuccessfully()
    {
        // Given
        var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase("api2")
            .Options;

        var apiContext = new ApiContext(options);
        apiContext.Articles.Add(new Article { Title = "Title", Content = "Content" });
        await apiContext.SaveChangesAsync();

        var articleRequest = new ArticleRequest("Title2", "Content", null);

        // When
        await new ArticleService(apiContext).UpdateArticle(1, articleRequest);

        // Then
        var updatedArticle = await apiContext.Articles.FirstOrDefaultAsync();

        Assert.Equal("Title2", updatedArticle!.Title);
    }

    [Fact]
    public async Task GivenExistingArticle_WhenArticleIsDeleted_ThenArticleIsDeletedSuccessfully()
    {
        // Given
        var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase("api3")
            .Options;

        var apiContext = new ApiContext(options);
        apiContext.Articles.Add(new Article { Title = "Title", Content = "Content" });
        await apiContext.SaveChangesAsync();

        // When
        await new ArticleService(apiContext).DeleteArticle(1);

        // Then
        var deletedArticle = await apiContext.Articles.FirstOrDefaultAsync();

        Assert.Null(deletedArticle);
    }
    
    public async Task GivenNonExistingArticle_WhenArticleIsDeleted_ThenArticleIsDeletedSuccessfully()
    {
        // Given
        var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase("api3")
            .Options;

        var apiContext = new ApiContext(options);
        apiContext.Articles.Add(new Article { Title = "Title", Content = "Content" });
        await apiContext.SaveChangesAsync();

        // When
        await new ArticleService(apiContext).DeleteArticle(1);

        // Then
        var deletedArticle = await apiContext.Articles.FirstOrDefaultAsync();

        Assert.NotNull(deletedArticle);
    }
    [Fact]
    public async Task GivenExistingArticle_WhenArticleIsGet_ThenArticleIsGetSuccessfully()
    {
        // Given
        var options = new DbContextOptionsBuilder<ApiContext>()
            .UseInMemoryDatabase(databaseName: "api4")
            .Options;

        var apiContext = new ApiContext(options);
        apiContext.Articles.Add(new Article { Title = "Title", Content = "Content" });
        await apiContext.SaveChangesAsync();

        // When
        await (new ArticleService(apiContext)).GetArticles();

        // Then
        var existingArticle = await apiContext.Articles.FirstOrDefaultAsync();

        Assert.NotNull(existingArticle);
    }
}