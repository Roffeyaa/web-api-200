using ProtoType.Api.Multi;
using ProtoType.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.AddHttpClient(); // create one for everyone, but lose a lot of optimzation, etc.

builder.Services.AddHttpClient<GitHubHttpClient>(client =>
{
    client.BaseAddress = new Uri("https://github.com"); // this would come from config, probably.
});

builder.Services.AddScoped<IGetProjectCountFromGithub>(sp =>
{
    return sp.GetRequiredService<GitHubHttpClient>();
});
//builder.Services.AddHttpClient("github", client =>
//{
//    client.BaseAddress = new Uri("https://github.com");
//});

builder.Services.AddHttpClient("amazon", client =>
{
    client.BaseAddress = new Uri("https://amazon.com");
});


//builder.Services.AddSingleton<MessageOfTheDay>();


// configurable but lazy

builder.Services.AddScoped<HitCounter>(); // This is lazy creation

//var messageServicetoUse = new MessageOfTheDay("this was created eagerly");
//builder.Services.AddSingleton(() => messageServicetoUse); 
builder.Services.AddSingleton<MessageOfTheDay>(sp =>
{
    using var scope = sp.CreateScope();
    var gh = scope.ServiceProvider.GetRequiredService<GitHubHttpClient>();
    var hc = sp.GetRequiredService<HitCounter>();
    return new MessageOfTheDay(hc);
});

var app = builder.Build(); // Life before this line and life after this.
// from here on, the services collection cannot be modified.
// all about what to expose and how to handle incoming requests
// "Middleware, Controllers, Minimal APIs, etc."
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// HTTP default port is 80
// HTTPS default port is 443

app.UseHttpsRedirection(); // if they get here using HTTP, redirect them to the HTTPs version.

app.UseAuthorization();

app.MapControllers(); // Create the route table.

app.MapGet("/cinnamon-roll", (HitCounter hitCounter, HttpContext context) =>
{
   
    hitCounter.Increment();
    return $"Those are good too ({hitCounter.Count})";
});

app.Run(); // this is a blocking call that will "spin" forever..
