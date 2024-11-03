using Sprint4.MLData;
using Sprint4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

    // Optional: Include XML comments if you have enabled XML comments generation
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Registrar FirebaseService e SentimentAnalysisService
builder.Services.AddScoped<IFirebaseService, FirebaseService>();
builder.Services.AddSingleton<SentimentAnalysisService>();

// Serviço do http client
builder.Services.AddHttpClient();

// Treinar o modelo de análise de sentimento e salvar
var trainer = new SentimentTrainer();
trainer.Train();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
