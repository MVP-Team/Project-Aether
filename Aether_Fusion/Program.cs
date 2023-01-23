using Aether_Console.Terminal;
using Aether_Fusion.Terminal;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Nancy;

var builder = WebApplication.CreateBuilder(args);



var app = builder.Build();


app.Use(async (context, next) =>
{
	context.Response.Headers.Add("X-Powered-By", "ASP.NET");
	context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
	context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
	await next();
});

Basis bas = new Basis();

Voice_Recorder vs = new Voice_Recorder();

app.MapGet("/joke", () => Basis.GetJoke());

app.MapGet("/search={text}", (string text) => Basis.Search(text));

app.MapGet("/translate={text}", (string text) => Basis.translator(text));

app.MapGet("/open={app}", (string app) => Basis.Application(app));

app.MapGet("/voice", () => vs.Processor());

app.MapGet("/todo", () => TodoListOpen.open());

app.Run();
