using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// The default builder already reads from appsettings.json and environment variables.
// We explicitly add the path to the mounted ConfigMap file.
// optional: true makes sure the app doesn't fail if the file isn't there (e.g., during local development).
// reloadOnChange: true enables the app to automatically pick up changes if the ConfigMap is updated in Kubernetes.
builder.Configuration.AddJsonFile("/app/config/appsettings.json", optional: true, reloadOnChange: true);


// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions.Add("additionalInfo", "Additional Info example"); 
        ctx.ProblemDetails.Extensions.Add("Server", Environment.MachineName);
    };
});
// Learn more about configuring Swagger/OpenAPI at
// https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseRouting();
app.UseAuthorization();
/*app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});*/
app.MapControllers();

app.Run();
