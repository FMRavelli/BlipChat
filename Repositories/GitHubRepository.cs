using System.Text.Json;
using BlipChat.DTOs;

namespace BlipChat.Repositories
{
    public interface IGitHubRepository
    {
        Task<GitHubUserDto> GetGitHubRepositoriesAsync(string username, int itemsPerPage, int page);
    }

    public class GitHubRepository : IGitHubRepository
    {
        private readonly HttpClient _httpClient;

        public GitHubRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");
        }

        public async Task<GitHubUserDto> GetGitHubRepositoriesAsync(string username, int itemsPerPage, int page)
        {
            var accumulatedRepos = new List<GitHubRepoDto>();
            var avatarUrl = string.Empty;
            var currentPage = page;

            while (accumulatedRepos.Count < 5)
            {
                var url = $"https://api.github.com/users/{username}/repos?sort=created&direction=asc&per_page={itemsPerPage}&page={currentPage}";

                // Fazer requisição
                var response = await _httpClient.GetStringAsync(url);
                if (string.IsNullOrEmpty(response))
                {
                    break;
                }

                var repos = JsonSerializer.Deserialize<List<Repository>>(response);

                if (repos == null || repos.Count == 0)
                {
                    break;
                }

                // Filtrar apenas repositórios em C#
                var filteredRepos = repos
                    .Where(w => w.Language != null && w.Language.Equals("C#", StringComparison.OrdinalIgnoreCase))
                    .Select(s => new GitHubRepoDto
                    {
                        Title = s.Name,
                        Description = s.Description
                    })
                    .Take(5)
                    .ToList();

                avatarUrl = repos.Select(s => s.Owner.AvatarUrl).FirstOrDefault();

                accumulatedRepos.AddRange(filteredRepos);

                // Verificar se já tem 5 repositorios
                if (accumulatedRepos.Count >= 5)
                {
                    break;
                }

                // Tentar novamente até atingir 5 ou terminar a lista
                currentPage++;
            }

            if (accumulatedRepos.Count < 5)
                return null;

            var retorno = new GitHubUserDto() 
            {
                AvatarUrl = avatarUrl,
                Repositories = accumulatedRepos
            };

            return retorno;
        }

    }
}

