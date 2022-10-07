using System.Diagnostics;
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
            .UseInMemoryDatabase(databaseName: "api1")
            .Options;

        var apiContext = new ApiContext((DbContextOptions<ApiContext>)options);

        // When
        await (new ArticleService(apiContext)).CreateArticle(articleRequest);

        // Then
        Assert.True(await apiContext.Articles.FirstOrDefaultAsync() != null);
    }

}