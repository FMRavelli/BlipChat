using System.Text.Json.Serialization;

public class Repository
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("owner")]
    public Owner Owner { get; set; }
}

public class Owner
{
    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }
}
