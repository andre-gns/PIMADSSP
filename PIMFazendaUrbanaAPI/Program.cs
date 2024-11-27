using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PIMFazendaUrbanaAPI.Mapping;
using PIMFazendaUrbanaLib;
using PIMFazendaUrbanaAPI.Services;
using PIMFazendaUrbanaRadzen.Controllers;
using System.Text;

namespace PIMFazendaUrbanaAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(MappingProfile)); // Adiciona AutoMapper definido em MappingProfile.cs

            builder.Services.AddHttpClient<CepController>();

            // Adiciona a connection string do appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddScoped<IClienteService>(provider => new ClienteService(connectionString));
            builder.Services.AddScoped<IFornecedorService>(provider => new FornecedorService(connectionString));
            builder.Services.AddScoped<IFuncionarioService>(provider => new FuncionarioService(connectionString));
            builder.Services.AddScoped<IRecomendacaoService>(provider => new RecomendacaoService(connectionString));
            builder.Services.AddScoped<ICultivoService>(provider => new CultivoService(connectionString));
            builder.Services.AddScoped<IProducaoService>(provider => new ProducaoService(connectionString));
            builder.Services.AddScoped<IInsumoService>(provider => new InsumoService(connectionString));
            builder.Services.AddScoped<ICompraService>(provider => new CompraService(connectionString));
            builder.Services.AddScoped<IVendaService>(provider => new VendaService(connectionString));
            builder.Services.AddScoped<IEstoqueProdutoService>(provider => new EstoqueProdutoService(connectionString));
            builder.Services.AddScoped<IExportacaoService>(provider => new ExportacaoService());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configuração do JWT
            var jwtKey = builder.Configuration["Jwt:Key"];
            var issuer = builder.Configuration["Jwt:Issuer"];
            var audience = builder.Configuration["Jwt:Audience"];
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });


            // Configuração do CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.WithOrigins("https://localhost:5001") // URL do front-end com HTTPS
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowAllOrigins"); // Habilita o CORS

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
