using Contact.BLL.Mapping;
using Contact.BLL.Services;
using Contact.BLL.Services.Interfaces;
using Contact.DAL.Context;
using Contact.DAL.Repositories;
using Contact.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Contact.UI.ServiceCollection
{
    public static class ServiceCollection
    {
        public static void AddServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddEndpointsApiExplorer();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();

            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            //services.AddSwaggerGen(opt =>
            //{
            //    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            //    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        In = ParameterLocation.Header,
            //        Description = "Please enter token",
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.Http,
            //        BearerFormat = "JWT",
            //        Scheme = "bearer"
            //    });

            //    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type=ReferenceType.SecurityScheme,
            //                    Id="Bearer"
            //                }
            //            },
            //            new string[]{}
            //        }
            //    });
            //});
        }
    }
}
