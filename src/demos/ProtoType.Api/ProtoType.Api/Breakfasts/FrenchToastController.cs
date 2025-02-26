using Microsoft.AspNetCore.Mvc;
using ProtoType.Api.Services;

namespace ProtoType.Api.Breakfasts;

public class FrenchToastController(HitCounter hitCounter, MessageOfTheDay message) : ControllerBase
{

   

    [HttpGet("/french-toast")]
  
    public ActionResult Get()
    {
       
        
        hitCounter.Increment();
       
        return Ok($"{message.GetMessageOfTheDay()} has been called {hitCounter.Count} times");
    }
}
