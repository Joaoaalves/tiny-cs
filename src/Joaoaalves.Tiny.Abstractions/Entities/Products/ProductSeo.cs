namespace Joaoaalves.Tiny.Abstractions.Entities.Products;

/// <summary>
/// SEO metadata associated with a product for e-commerce catalogue pages.
/// </summary>
public sealed class ProductSeo
{
    /// <summary>Title displayed in Google search results. Max 120 characters.</summary>
    public string? Title { get; init; }

    /// <summary>Comma-separated keywords used for search engine indexing. Max 255 characters.</summary>
    public string? Keywords { get; init; }

    /// <summary>URL of a promotional video for the product. Max 100 characters.</summary>
    public string? VideoUrl { get; init; }

    /// <summary>Short description displayed below the title in search results. Max 255 characters.</summary>
    public string? Description { get; init; }

    /// <summary>Human-readable URL slug used to identify the product in the e-commerce storefront.</summary>
    public string? Slug { get; init; }
}
