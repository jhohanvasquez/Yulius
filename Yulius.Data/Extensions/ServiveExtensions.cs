using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using Yulius.Data.Data.Auth;
using Yulius.Data.Data.Todo;

namespace Yulius.Data.Extensions
{
    public static class ServiveExtensions
    {
        public static void ConfigDatabase(this IServiceCollection services, IConfiguration configuration,string secretKey)
        {            // Connection Database SqlServar           
            services.AddTransient<IAuthRepo>(s => new AuthRepoMSSQL(configuration.GetConnectionString("DefaultConnection_sql_server"), secretKey));
            services.AddTransient<IPostRepo>(s => new PostRepoMSSQL(configuration.GetConnectionString("DefaultConnection_sql_server"), secretKey));

        }

        public static void ConfigCROS(this IServiceCollection services)
        {
            services.AddCors(cors =>
                cors.AddPolicy("AllowAll", builder =>
                     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
        }      

    }
}