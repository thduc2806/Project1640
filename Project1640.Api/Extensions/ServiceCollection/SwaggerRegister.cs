//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.OpenApi.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Project1640.Api.Extensions.ServiceCollection
//{
//    public static class SwaggerRegister
//    {
//        public static void AddSwagger(this IServiceCollection services)
//        {
//            services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Swagger eShop Solution", Version = "v1" });

//                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                {
//                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
//                      Enter 'Bearer' [space] and then your token in the text input below.
//                      \r\n\r\nExample: 'Bearer 12345abcdef'",
//                    Name = "Authorization",
//                    In = ParameterLocation.Header,
//                    Type = SecuritySchemeType.ApiKey,
//                    Scheme = "Bearer"
//                });

//                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
//                  {
//                    {
//                      new OpenApiSecurityScheme
//                      {
//                        Reference = new OpenApiReference
//                          {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                          },
//                          Scheme = "oauth2",
//                          Name = "Bearer",
//                          In = ParameterLocation.Header,
//                        },
//                        new List<string>()
//                      }
//                    });
//            });

//            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
//            string signingKey = Configuration.GetValue<string>("Tokens:Key");
//            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

//            services.AddAuthentication(opt =>
//            {
//                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.RequireHttpsMetadata = false;
//                options.SaveToken = true;
//                options.TokenValidationParameters = new TokenValidationParameters()
//                {
//                    ValidateIssuer = true,
//                    ValidIssuer = issuer,
//                    ValidateAudience = true,
//                    ValidAudience = issuer,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    ClockSkew = System.TimeSpan.Zero,
//                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
//                };
//            });
//        }
//    }
//}
