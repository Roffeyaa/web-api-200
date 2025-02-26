using Microsoft.AspNetCore.Mvc;
using ProtoType.Api.Services;

namespace ProtoType.Api.Multi;

public class MultiController(IGetProjectCountFromGithub github) : ControllerBase
{
    [HttpGet("/multi/hypertheory")]
    public async Task<ActionResult> DoSomeStuff()
    {
        var count = await github.GetRepositoryCountFor("hypertheorytraining");

        return Ok(new { repos = count });
    }
}
public interface IGetProjectCountFromGithub
{
    Task<int> GetRepositoryCountFor(string organizaton);
}