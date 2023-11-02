using Htmx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevNewsBot.App.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private static int _count = 0;
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var isHtmx = Request.IsHtmx();
        Interlocked.Increment(ref _count);
        return isHtmx
            ? Content($"<span>Hello, World! {_count}</span>", "text/html")
            : Page();
    }
    
    
}
