using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
<<<<<<< HEAD
using System.Text;
=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc;
=======
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Senparc.CO2NET;
using Senparc.CO2NET.RegisterServices;
using SSCMS.Core.Extensions;
using SSCMS.Core.Plugins.Extensions;
using SSCMS.Repositories;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Web
{
    public class Startup
    {
        private const string CorsPolicy = "CorsPolicy";
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public Startup(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var directory = new DirectoryInfo(_env.ContentRootPath);
            services.AddDataProtection().PersistKeysToFileSystem(directory);

            var entryAssembly = Assembly.GetExecutingAssembly();
            var assemblies = new List<Assembly> { entryAssembly }.Concat(entryAssembly.GetReferencedAssemblies().Select(Assembly.Load));

            var settingsManager = services.AddSettingsManager(_config, _env.ContentRootPath, _env.WebRootPath, entryAssembly);
            var pluginManager = services.AddPlugins(_config, settingsManager);

<<<<<<< HEAD
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicy,
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true)
                        .AllowCredentials()
                );
            });

            services.AddHttpContextAccessor();
            
=======
            if (settingsManager.CorsIsOrigins)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy,
                        builder => builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .WithOrigins(settingsManager.CorsOrigins)
                            .AllowCredentials()
                    );
                });
            }
            else
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(CorsPolicy,
                        builder => builder
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .SetIsOriginAllowed(x => true)
                            .AllowCredentials()
                    );
                });
            }

            services.AddHttpContextAccessor();

>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(StringUtils.GetSecurityKeyBytes(settingsManager.SecurityKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    x.Events = new JwtBearerEvents
                    {
<<<<<<< HEAD
                        OnMessageReceived = (context) => {
=======
                        OnMessageReceived = (context) =>
                        {
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                            if (!context.Request.Query.TryGetValue("access_token", out var values))
                            {
                                return Task.CompletedTask;
                            }
                            if (values.Count > 1)
                            {
                                return Task.CompletedTask;
                            }
                            var token = values.Single();
                            if (string.IsNullOrWhiteSpace(token))
                            {
                                return Task.CompletedTask;
                            }
                            context.Token = token;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 524288000;//500MB
            });

            services.AddHealthChecks();

            services.AddRazorPages()
                .AddPluginApplicationParts(pluginManager);
<<<<<<< HEAD
                // .SetCompatibilityVersion(CompatibilityVersion.Latest);
=======
            // .SetCompatibilityVersion(CompatibilityVersion.Latest);
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            services.AddCache(settingsManager.Redis.ConnectionString);

            services.AddRepositories(assemblies);
            services.AddServices();
            services.AddWxManager(_config);

            services.AddPseudoServices();
            services.AddPluginServices(pluginManager);
            services.AddTaskServices();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services
                .AddControllers()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.ContractResolver
                        = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("zh-CN")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "zh-CN", uiCulture: "zh-CN");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            if (!settingsManager.IsSafeMode)
            {
<<<<<<< HEAD
              //http://localhost:5000/api/swagger/v1/swagger.json
              //http://localhost:5000/api/swagger/
              //http://localhost:5000/api/docs/
              services.AddOpenApiDocument(config =>
              {
                  config.PostProcess = document =>
                  {
                      document.Info.Version = "v1";
                      document.Info.Title = "SS CMS REST API";
                      document.Info.Description = "SS CMS REST API 为 SS CMS 提供了一个基于HTTP的API调用，允许开发者通过发送和接收JSON对象来远程与站点进行交互。";
                      document.Info.Contact = new NSwag.OpenApiContact
                      {
                          Name = "SS CMS",
                          Email = string.Empty,
                          Url = "https://sscms.com"
                      };
                      document.Info.License = new NSwag.OpenApiLicense
                      {
                          Name = "GPL-3.0",
                          Url = "https://github.com/siteserver/cms/blob/staging/LICENSE"
                      };
                  };
              });
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IAntiforgery antiforgery, ISettingsManager settingsManager, IPluginManager pluginManager, IErrorLogRepository errorLogRepository, IOptions<SenparcSetting> senparcSetting)
=======
                //http://localhost:5000/api/swagger/v1/swagger.json
                //http://localhost:5000/api/swagger/
                //http://localhost:5000/api/docs/
                services.AddOpenApiDocument(config =>
                {
                    config.PostProcess = document =>
                    {
                        document.Info.Version = "v1";
                        document.Info.Title = "SSCMS REST API";
                        document.Info.Description = "SSCMS REST API 为 SSCMS 提供了一个基于HTTP的API调用，允许开发者通过发送和接收JSON对象来远程与站点进行交互。";
                        document.Info.Contact = new NSwag.OpenApiContact
                        {
                            Name = "SSCMS",
                            Email = string.Empty,
                            Url = "https://sscms.com"
                        };
                        document.Info.License = new NSwag.OpenApiLicense
                        {
                            Name = "GPL-3.0",
                            Url = "https://github.com/siteserver/cms/blob/staging/LICENSE"
                        };
                    };
                });
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISettingsManager settingsManager, IPluginManager pluginManager, IErrorLogRepository errorLogRepository, IOptions<SenparcSetting> senparcSetting)
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                try
                {
                    errorLogRepository.AddErrorLogAsync(exception).GetAwaiter().GetResult();
                }
                catch
                {
                    // ignored
                }

                string result;
                if (env.IsDevelopment())
                {
                    result = TranslateUtils.JsonSerialize(new
                    {
                        exception.Message,
                        exception.StackTrace,
<<<<<<< HEAD
                        AddDate = DateTime.Now
=======
                        CreatedDate = DateTime.Now
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                    });
                }
                else
                {
                    result = TranslateUtils.JsonSerialize(new
                    {
                        exception.Message,
<<<<<<< HEAD
                        AddDate = DateTime.Now
=======
                        CreatedDate = DateTime.Now
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
                    });
                }
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));

            app.UseCors(CorsPolicy);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseHttpsRedirection();

            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(options);

            //if (settingsManager.Containerized)
            //{
            //    app.Map($"/{DirectoryUtils.SiteFiles.DirectoryName}/assets", assets =>
            //    {
            //        assets.UseStaticFiles(new StaticFileOptions
            //        {
            //            FileProvider = new PhysicalFileProvider(PathUtils.Combine(settingsManager.ContentRootPath, "assets"))
            //        });
            //    });
            //}

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/404.html";
                    await next();
                }
            });
            app.UseStaticFiles();

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-CN"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

<<<<<<< HEAD
            app.UsePluginsAsync(settingsManager, pluginManager, errorLogRepository).GetAwaiter().GetResult();
=======
            app.UsePluginsAsync(settingsManager, pluginManager).GetAwaiter().GetResult();
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
            //api.UseEndpoints(endpoints => { endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}"); });

            app.UseRequestLocalization();

            RegisterService.Start(senparcSetting.Value)
                //自动扫描自定义扩展缓存（二选一）
                .UseSenparcGlobal(true)
                //ָ指定自定义扩展缓存（二选一）
                //.UseSenparcGlobal(false, () => GetExCacheStrategies(senparcSetting.Value))   
                ;

<<<<<<< HEAD
            if (!settingsManager.IsSafeMode)
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
                app.UseReDoc(settings =>
                {
                    settings.Path = "/api/docs";
                    settings.DocumentPath = "/swagger/v1/swagger.json";
                });
            }
=======
            // if (!settingsManager.IsSafeMode)
            // {
            //     app.UseOpenApi();
            //     app.UseSwaggerUi3();
            //     app.UseReDoc(settings =>
            //     {
            //         settings.Path = "/api/docs";
            //         settings.DocumentPath = "/swagger/v1/swagger.json";
            //     }); 
            // }
>>>>>>> c6f12030edc3fe4820d2654bd0ed70f892a63e93
        }
    }
}
