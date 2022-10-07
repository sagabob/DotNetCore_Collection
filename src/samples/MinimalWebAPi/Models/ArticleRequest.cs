namespace MinimalWebAPi.Models;

public record ArticleRequest(string? Title, string? Content, DateTime? PublishedAt);