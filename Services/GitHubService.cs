using BlipChat.DTOs;
using BlipChat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlipChat.Services 
{
    public interface IGitHubService
    {
        Task<GitHubUserDto> GetGitHubUserInfoAsync(string username);
    }

    public class GitHubService : IGitHubService
    {
        private readonly IGitHubRepository _gitHubRepository;

        public GitHubService(IGitHubRepository gitHubRepository)
        {
            _gitHubRepository = gitHubRepository;
        }

        public async Task<GitHubUserDto> GetGitHubUserInfoAsync(string username)
        {
            try
            {
                // Obter os 5 repositórios mais antigos de C#
                var retorno = await _gitHubRepository.GetGitHubRepositoriesAsync(username, itemsPerPage: 90, page: 1);

                if (retorno == null || !retorno.Repositories.Any() || string.IsNullOrEmpty(retorno.AvatarUrl))
                {
                    return null;
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Falha ao buscar informações do GitHub.", ex);
            }
        }

    }
}
