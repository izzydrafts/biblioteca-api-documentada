using BibliotecaApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Repositório em memória (Singleton)
builder.Services.AddSingleton<BibliotecaRepository>();

builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Version = "v1",
        Title = "API da Biblioteca",
        Description = "API REST para gerenciamento da biblioteca",
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Equipe",
            Email = "dev@teste.com"
        }
    });

    // Inclui comentários XML no Swagger
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API da Biblioteca v1");

   
    options.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

app.Run();