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
            // Defina o User-Agent para evitar o erro 403
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; MyApp/1.0)");
        }

        public async Task<GitHubUserDto> GetGitHubRepositoriesAsync(string username, int itemsPerPage, int page)
        {
            var accumulatedRepos = new List<GitHubRepoDto>();
            var avatarUrl = string.Empty;
            var currentPage = page;

            while (accumulatedRepos.Count < 5)
            {
                // Construir URL com paginação
                var url = $"https://api.github.com/users/{username}/repos?sort=created&direction=asc&per_page={itemsPerPage}&page={currentPage}";

                // Fazer requisição
                var response = await _httpClient.GetStringAsync(url);
                if (string.IsNullOrEmpty(response))
                {
                    break; // Se resposta vazia, parar o loop
                }

                // Desserializar os repositórios
                var repos = JsonSerializer.Deserialize<List<Repository>>(response);

                if (repos == null || repos.Count == 0)
                {
                    break; // Parar se não houver mais repositórios
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

                // Verificar se já temos 5 repositórios
                if (accumulatedRepos.Count >= 5)
                {
                    break;
                }

                // Avançar para a próxima página
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

