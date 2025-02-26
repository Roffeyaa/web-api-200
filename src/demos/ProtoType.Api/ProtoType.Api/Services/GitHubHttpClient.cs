using Microsoft.AspNetCore.Authorization.Infrastructure;
using ProtoType.Api.Multi;

namespace ProtoType.Api.Services;

public class GitHubHttpClient : IGetProjectCountFromGithub
{

    // knows how to do all things related to github,
    // but we are doing consumer interfaces on this.
    private HttpClient _client;

    public GitHubHttpClient(HttpClient client)
    {
        _client = client;
    }

    public async Task<int> GetRepositoryCountFor(string organizaton)
    {

        var response = await _client.GetAsync("/some-api-method");
        return 23;
    }

    // add methods to do things with github

}
