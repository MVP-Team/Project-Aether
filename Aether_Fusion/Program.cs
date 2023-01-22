using Aether_Console.Terminal;
using Nancy;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Basis bas = new Basis();

Voice_Recorder vs = new Voice_Recorder();

app.MapGet("/joke", () => Basis.GetJoke());

app.MapGet("/search={text}", (string text) => Basis.Search(text));

app.MapGet("/translate={text}", (string text) => Basis.translator(text));

app.MapGet("/open={app}", (string app) => Basis.Application(app));

app.MapGet("/voice", () => vs.Processor());

app.Run();
