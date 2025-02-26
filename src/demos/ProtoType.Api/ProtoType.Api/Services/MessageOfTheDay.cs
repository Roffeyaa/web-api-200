namespace ProtoType.Api.Services;

public class MessageOfTheDay(HitCounter counter) 
{



    public string GetMessageOfTheDay()
    {
        return $"Things look good! Counter is a t {counter.Count}";
    }


}
