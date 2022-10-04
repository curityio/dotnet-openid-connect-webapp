using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

[Authorize]
public class ProtectedModel : PageModel
{
    public string Username { get; set; }
    public string AccessToken { get; set; }

    public async Task OnGet()
    {
        ClaimsPrincipal user = this.User;
        this.Username = user.FindFirstValue("sub");

        // Get the access token required for upstream requests
        this.AccessToken = await this.HttpContext.GetTokenAsync("access_token");
    }
}
