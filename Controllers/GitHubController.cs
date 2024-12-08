using BlipChat.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlipChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _gitHubService;

        public GitHubController(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetGitHubUserInfo()
        {
            var username = "takenet";

            var userData = await _gitHubService.GetGitHubUserInfoAsync(username);

            if (userData == null)
            {
                return NotFound("Usuário não encontrado ou sem repositórios C#.");
            }

            return Ok(userData);
        }
    }
}