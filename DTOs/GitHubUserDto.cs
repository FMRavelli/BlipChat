namespace BlipChat.DTOs
{
    public class GitHubUserDto
    {
        public string AvatarUrl { get; set; }
        public List<GitHubRepoDto> Repositories { get; set; }
    }

    public class GitHubRepoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
