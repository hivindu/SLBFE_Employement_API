using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using SLBFE_Employement_API.Data;
using SLBFE_Employement_API.Data.Interfaces;
using SLBFE_Employement_API.Repository;
using SLBFE_Employement_API.Repository.Interface;
using SLBFE_Employement_API.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<SLBFDatabaseSettings>(builder.Configuration.GetSection(nameof(SLBFDatabaseSettings)));
builder.Services.AddSingleton<ISLBFDatabaseSettings>(sp => sp.GetRequiredService<IOptions<SLBFDatabaseSettings>>().Value);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc(options =>
{
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    options.FormatterMappings.SetMediaTypeMappingForFormat
            ("xml", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat
        ("config", MediaTypeHeaderValue.Parse("application/xml"));
    options.FormatterMappings.SetMediaTypeMappingForFormat
        ("js", MediaTypeHeaderValue.Parse("application/json"));
}).AddXmlSerializerFormatters();

builder.Services.AddMvc(options =>
{
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SLBFE API", Version = "v1" });
});
builder.Services.AddTransient<ISLBFContext, SLBFContext>();
builder.Services.AddScoped<ICitizenRepositories, CitizensRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVacanciesRepository, VacanciesRepository>();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
