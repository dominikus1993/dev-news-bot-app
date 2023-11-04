using DevNewsBot.App.Core.Model;
using DevNewsBot.App.Core.Services;
using DevNewsBot.App.ViewModel;
using Htmx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevNewsBot.App.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IArticlesProvider _articlesProvider;

    public long PageSize { get; private set; } = 1;
    public List<Article> Articles { get; private set; }
    
    [BindProperty(SupportsGet = true)] 
    public int Cursor { get; set; } = 1;

    public long ArticlesPage { get; private set; } 
    public IndexModel(ILogger<IndexModel> logger, IArticlesProvider articlesProvider)
    {
        _logger = logger;
        _articlesProvider = articlesProvider;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var isHtmx = Request.IsHtmx();
        var page = GetPage(Cursor, PageSize);
        ArticlesPage = page;
        _logger.LogInformation("GetAsync {IsHtmx} {Page} {PageSize} {Cursor}", isHtmx, ArticlesPage, PageSize, Cursor);
        var articles = await _articlesProvider.GetArticles(new ArticlesQuery(ArticlesPage, PageSize));
        
        if (isHtmx)
        {
            return Partial("Shared/_Articles", new ArticlesViewModel() { PageSize = PageSize, Page = ArticlesPage, Articles = articles.Articles.ToList(), Cursor = Cursor });
        }
        Articles = new List<Article>(articles.Articles);
        return Page();
    }

    private static long GetPage(int cursor, long pageSize)
    {
        var result = (long)Math.Ceiling(cursor / (double)pageSize);
        return result;
    }
    
}
