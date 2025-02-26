namespace ProtoType.Api.Services;

public class HitCounter(HttpContext context)
{

    public int Count { get; private set; }

    public void Increment()
    {
        Count++;
    }
}
