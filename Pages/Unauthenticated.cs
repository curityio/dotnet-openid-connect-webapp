using Microsoft.AspNetCore.Mvc.RazorPages;

public class UnauthenticatedModel : PageModel
{
    public string Message { get; set; }

    public void OnGet()
    {
        Message = "View Protected Data";
    }
}