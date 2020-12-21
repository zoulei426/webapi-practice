<!--
 * @Description: ���� ABP vNext �� Web API �����̳�
 * @Author: zoulei
 * @Date: 2020-12-18 13:26:04
 * @LastEditors: zoulei
 * @LastEditTime: 2020-12-21 17:03:29
-->

# ���� ABP vNext �� Web API �����̳�

## Ŀ¼

<!-- TOC -->

- [���� ABP vNext �� Web API �����̳�](#����-abp-vnext-��-web-api-�����̳�)
  - [Ŀ¼](#Ŀ¼)
  - [1 ����](#1-����)
  - [2 ����](#2-����)
    - [2.1 �Ⱦ�����](#21-�Ⱦ�����)
    - [2.2 ��װ .NET 5.0](#22-��װ-net-50)
    - [2.3 ��װ Visual Studio Preview](#23-��װ-visual-studio-preview)
    - [2.4 ��װ ABP](#24-��װ-abp)
    - [2.5 ��װ Postgresql](#25-��װ-postgresql)
  - [3 ������Ŀ](#3-������Ŀ)
    - [3.1 ������](#31-������)
    - [3.2 ���������ʵ��](#32-���������ʵ��)
    - [3.3 � Entity Framework Core](#33-�-entity-framework-core)
    - [3.4 ����ʵ�������ݿ�ӳ��](#34-����ʵ�������ݿ�ӳ��)
    - [3.5 �����Ǩ��](#35-�����Ǩ��)
    - [3.6 ������](#36-������)
    - [3.7 �������ݴ������ DTO](#37-�������ݴ������-dto)
    - [3.8 ���� DTO ��ʵ���ӳ��](#38-����-dto-��ʵ���ӳ��)
    - [3.9 ����Ӧ�÷���](#39-����Ӧ�÷���)
    - [3.10 ���������](#310-���������)
    - [3.11 ����������](#311-����������)
    - [3.12 �����](#312-�����)
    - [3.13 Ǩ������](#313-Ǩ������)
    - [3.14 ����](#314-����)
  - [4 ����](#4-����)
    - [4.1 ����㵥Ԫ����](#41-����㵥Ԫ����)
    - [4.2 Ӧ�ò㵥Ԫ����](#42-Ӧ�ò㵥Ԫ����)
    - [4.3 Swagger](#43-swagger)
    - [4.4 Postman](#44-postman)
  - [5 � CI/CD](#5-�-cicd)
    - [5.1 Docker](#51-docker)
    - [5.2 Jenkins](#52-jenkins)
  - [�ο�����](#�ο�����)

<!-- /TOC -->

## 1 ����

���̳̽�����ʹ�� ABP vNext ���� Web API �Ļ���֪ʶ��������������Ĳο����ס�

�ڱ��̳��У��㽫�˽⣺
- ʹ�� ABP vNext ���� Web API ��Ŀ
- ʹ�� Entity Framework Core
- �Զ����������

˭�ʺ��Ķ����̳̣�
- ���� C# �﷨�� .NET ���
- �˽� ASP.NET Core Ӧ�ó��򿪷�

## 2 ����

### 2.1 �Ⱦ�����

- .NET Core 3.1 ����߰汾���Ƽ� .NET 5.0 SDK��
- ���С�ASP.NET �� Web �������������ص� Visual Studio 2019 16.4 ����߰汾

### 2.2 ��װ .NET 5.0

*��ʹ�� .NET Core 3.1 ����������Դ˲��衣*  
[.NET 5.0 SDK ����](https://dotnet.microsoft.com/download/dotnet/5.0)

### 2.3 ��װ Visual Studio Preview

*��ʹ�� .NET Core 3.1 ����������Դ˲��衣*  
[Visual Studio Preview ����](https://visualstudio.microsoft.com/zh-hans/vs/preview/)

### 2.4 ��װ ABP

��װ ABP CLI

```cmd
dotnet tool install -g Volo.Abp.Cli
```

�����������°汾��

```cmd
dotnet tool update -g Volo.Abp.Cli
```

### 2.5 ��װ Postgresql

## 3 ������Ŀ

ʹ��ABP CLI�� new ���������Ŀ��

```cmd
abp new YuLinTu.Practice --ui none
```


### 3.1 ������

�½�һ������ .NET Standard 2.0 ����� YuLinTu.Practice.Domain.Shared�������������Ϊ YuLinTu.Practice�����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�� YuLinTu.Practice.Domain.Shared ��Ŀ�а�װ ABP �����

```PM
Install-Package Volo.Abp.Core
```

����Ŀ��Ŀ¼���½��� PracticeDomainSharedModule �̳��� AbpModule��  
�����̳̲��漰���ػ������⻧�����ݣ���˲���Ҫ��������ã�����������鿴�ο����ס���

```c#
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(typeof(PracticeDomainSharedModule))]
    public class PracticeDomainModule : AbpModule
    {
    }
}
```

�½�һ������ .NET Core 3.1 / .NET 5.0 ����� YuLinTu.Practice.Domain�����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�� YuLinTu.Practice.Domain ��Ŀ������ YuLinTu.Practice.Domain.Shared ��Ŀ������װ ABP �����

```PM
Install-Package Volo.Abp.Ddd.Domain
```

����Ŀ��Ŀ¼���½��� PracticeDomainModule �̳��� AbpModule������� PracticeDomainSharedModule ģ�飺

```c#
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    public class PracticeDomainSharedModule : AbpModule
    {
    }
}
```

### 3.2 ���������ʵ��

- YuLinTu.Practice.Domain �д���ʵ�塢������񡢲ִ��ӿڵ�
- YuLinTu.Practice.Domain.Shared �д�������ĳ�����ö�ٵ�

�� YuLinTu.Practice.Domain ��Ŀ���½� Books Ŀ¼������ Book ʵ�壺

```c#
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace YuLinTu.Practice.Books
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
```

�� YuLinTu.Practice.Domain.Shared ��Ŀ���½� Books Ŀ¼������ BookType ö�٣�

```c#
namespace YuLinTu.Practice.Books
{
    public enum BookType
    {
        Undefined,
        Adventure,
        Biography,
        Dystopia,
        Fantastic,
        Horror,
        Science,
        ScienceFiction,
        Poetry
    }
}
```

### 3.3 � Entity Framework Core

�½�һ������ .NET Core 3.1 / .NET 5.0 ����� YuLinTu.Practice.EntityFrameworkCore�����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�� YuLinTu.Practice.EntityFrameworkCore ��Ŀ������ YuLinTu.Practice.Domain ��Ŀ������װ ABP �����

```PM
Install-Package Volo.Abp.EntityFrameworkCore
Install-Package Volo.Abp.EntityFrameworkCore.PostgreSql
Install-Package Volo.Abp.AuditLogging.EntityFrameworkCore
```

�½��ļ��� EntityFrameworkCore �������� PracticeEntityFrameworkCoreModule �̳��� AbpModule������� PracticeDomainModule��AbpEntityFrameworkCorePostgreSqlModule ģ�飺

```c#
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    [DependsOn(
        typeof(PracticeDomainModule),
        typeof(AbpEntityFrameworkCorePostgreSqlModule))]
    public class PracticeEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<PracticeDbContext>(options =>
            {
                // ���ȱʡ�ִ�
                options.AddDefaultRepositories(true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                // ʹ�� Postgresql ����Դ
                options.UseNpgsql();
            });
        }
    }
}
```

��Ŀ¼ EntityFrameworkCore �´����� PracticeDbContext��

```c#
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public class PracticeDbContext : AbpDbContext<PracticeDbContext>
    {
        public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options)
        {
        }
    }
}
```

### 3.4 ����ʵ�������ݿ�ӳ��

- YuLinTu.Practice.EntityFrameworkCore �����ʵ�������ݿ�ӳ�䡢ʵ�ֲִ��ӿڵ�

�� Book ʵ����ӵ� PracticeDbContext �У�ͬʱ������չ�� PracticeDbContextModelCreatingExtensions��

```c#
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YuLinTu.Practice.Books;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public class PracticeDbContext : AbpDbContext<PracticeDbContext>
    {
        public DbSet<Book> Books { get; set; }

        public PracticeDbContext(DbContextOptions<PracticeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePractice();
        }
    }
}
```

�� PracticeDbContextModelCreatingExtensions ������ʵ�������ݿ���ӳ�䣺

```c#
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using YuLinTu.Practice.Books;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public static class PracticeDbContextModelCreatingExtensions
    {
        public static void ConfigurePractice(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Book>(b =>
            {
                b.ToTable("Books");
                b.ConfigureByConvention();
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            });
        }
    }
}
```

�½��� PracticeMigrationsDbContextFactory������ִ�� EF Core �� Add-Migration��Update-Database �����

```c#
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public class PracticeMigrationsDbContextFactory : IDesignTimeDbContextFactory<PracticeMigrationsDbContext>
    {
        public PracticeMigrationsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PracticeMigrationsDbContext>()
                .UseNpgsql("Server=127.0.0.1;Port=5432;Database=xmxf;User Id=postgres;Password=123456;");

            return new PracticeMigrationsDbContext(builder.Options);
        }
    }
}
```

### 3.5 �����Ǩ��

*����Ŀ�� DbFirst�����Ժ�������Ǩ�Ʋ��衣*

�½�һ������ .NET Core 3.1 / .NET 5.0 ����� YuLinTu.Practice.EntityFrameworkCore.DbMigrations�����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�ڿ� YuLinTu.Practice.EntityFrameworkCore.DbMigrations ��Ŀ������ YuLinTu.Practice.EntityFrameworkCore ��Ŀ������װ EF Core �����

```PM
Install-Package Microsoft.EntityFrameworkCore.Design
```

�½��ļ��� EntityFrameworkCore �������� PracticeEntityFrameworkCoreDbMigrationsModule �̳��� AbpModule������� PracticeEntityFrameworkCoreModule ģ�飺

```c#
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    [DependsOn(typeof(PracticeEntityFrameworkCoreModule))]
    public class PracticeEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<PracticeMigrationsDbContext>();
        }
    }
}
```

��Ŀ¼ EntityFrameworkCore �´����� PracticeMigrationsDbContext��

```c#
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YuLinTu.Practice.EntityFrameworkCore
{
    public class PracticeMigrationsDbContext : AbpDbContext<PracticeMigrationsDbContext>
    {
        public PracticeMigrationsDbContext(DbContextOptions<PracticeMigrationsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigurePractice();
        }
    }
}
```

### 3.6 ������

�½�һ������ .NET Standard 2.0 ����� YuLinTu.Practice.Application.Contracts�����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�� YuLinTu.Practice.Application.Contracts ��Ŀ������ YuLinTu.Practice.Domain.Shared ��Ŀ������װ ABP �����

```PM
Install-Package Volo.Abp.Ddd.Application
```

����Ŀ��Ŀ¼���½��� PracticeApplicationContractsModule �̳��� AbpModule��

```c#
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(typeof(PracticeDomainSharedModule))]
    public class PracticeApplicationContractsModule : AbpModule
    {
    }
}
```

�½�һ������ .NET Core 3.1 / .NET 5.0 ����� YuLinTu.Practice.Application�����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�� YuLinTu.Practice.Application ��Ŀ������ YuLinTu.Practice.Application.Contracts��YuLinTu.Practice.Domain ��Ŀ������װ ABP �����

```PM
Install-Package Volo.Abp.AutoMapper
```

�½��� PracticeApplicationModule �̳��� AbpModule������� PracticeApplicationContractsModule��PracticeDomainSharedModule��AbpAutoMapperModule ģ�飺

```c#
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(
        typeof(PracticeApplicationContractsModule),
        typeof(PracticeDomainModule),
        typeof(AbpAutoMapperModule))]
    public class PracticeApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<PracticeApplicationModule>();
            });
        }
    }
}
```

### 3.7 �������ݴ������ DTO

- YuLinTu.Practice.Application.Contracts �д���DTO��Ӧ�÷���ӿ�
- YuLinTu.Practice.Application ��ʵ��Ӧ�÷���ӿ�

�� YuLinTu.Practice.Application.Contracts ��Ŀ�д��� BookDto��

```c#
using System;
using Volo.Abp.Application.Dtos;

namespace YuLinTu.Practice.Books
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}
```

���� CreateUpdateBookDto��

```c#
using System;
using System.ComponentModel.DataAnnotations;

namespace YuLinTu.Practice.Books
{
    public class CreateUpdateBookDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }
    }
}
```

### 3.8 ���� DTO ��ʵ���ӳ��

�� YuLinTu.Practice.Application ��Ŀ�д��� BookStoreApplicationAutoMapperProfile ���ж���ʵ����DTO��ӳ�䡣

```c#
using AutoMapper;
using YuLinTu.Practice.Books;

namespace YuLinTu.Practice
{
    public class PracticeApplicationAutoMapperProfile : Profile
    {
        public PracticeApplicationAutoMapperProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
        }
    }
}
```

### 3.9 ����Ӧ�÷���

�� YuLinTu.Practice.Application.Contracts ��Ŀ�ж���һ����ΪIBookAppService�Ľӿڡ�

```c#
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YuLinTu.Practice.Books
{
    public interface IBookAppService :
        ICrudAppService<
            BookDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBookDto>
    {
    }
}
```

�� YuLinTu.Practice.Application ��Ŀ�д�����Ϊ BookAppService �� IBookAppService ʵ�֡�

```c#
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace YuLinTu.Practice.Books
{
    public class BookAppService :
          CrudAppService<
              Book,
              BookDto,
              Guid,
              PagedAndSortedResultRequestDto,
              CreateUpdateBookDto>,
          IBookAppService
    {
        public BookAppService(IRepository<Book, Guid> repository)
            : base(repository)
        {
        }
    }
}
```

### 3.10 ���������

�½�һ������ .NET Core 3.1 / .NET 5.0 ����� YuLinTu.Practice.HttpApi�����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�� YuLinTu.Practice.HttpApi ��Ŀ������ YuLinTu.Practice.Application.Contracts ��Ŀ������װ ABP �����

```PM
Install-Package Volo.Abp.AspNetCore.Mvc
```

�½��� PracticeHttpApiModule �̳��� AbpModule������� PracticeApplicationContractsModule��AbpAspNetCoreMvcModuleģ�飺

```c#
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(
        typeof(PracticeApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class PracticeHttpApiModule : AbpModule
    {
    }
}
```


### 3.11 ����������

> *ABP vNext �����Զ�����Ӧ�÷������� RESTFULL ���� API*  

�� YuLinTu.Practice.HttpApi ��Ŀ�д��� Controllers �ļ��У����½������� BookController �̳��� AbpController

```c#
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using YuLinTu.Practice.Books;

namespace YuLinTu.Practice.Controllers
{
    [RemoteService]
    [Route("books")]
    public class BookController : AbpController
    {
        private readonly IBookAppService bookAppService;

        public BookController(IBookAppService bookAppService)
        {
            Check.NotNull(bookAppService, nameof(bookAppService));

            this.bookAppService = bookAppService;
        }
    }
}
```

### 3.12 �����

�½�һ������ ASP.NET Core Web ��Ӧ�ó��� YuLinTu.Practice.HttpApi.Host��ѡ���ģ�壬ȡ�� HTTPS ���ã����޸�Ĭ�������ռ�Ϊ YuLinTu.Practice��

�� YuLinTu.Practice.HttpApi.Host ��Ŀ������ YuLinTu.Practice.Application��YuLinTu.Practice.EntityFrameworkCore��YuLinTu.Practice.EntityFrameworkCore.DbMigrations��YuLinTu.Practice.HttpApi ��Ŀ������װ ABP �����

```PM
Install-Package Microsoft.Extensions.Hosting
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Serilog.AspNetCore
Install-Package Serilog.Sinks.Async
Install-Package Volo.Abp.Autofac
Install-Package Volo.Abp.AspNetCore.Serilog
Install-Package Volo.Abp.Caching
Install-Package Volo.Abp.Swashbuckle
Install-Package Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared
```

�½��� PracticeHttpApiHostModule �̳��� AbpModule��

```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using YuLinTu.Practice.EntityFrameworkCore;

namespace YuLinTu.Practice
{
    [DependsOn(
        typeof(PracticeHttpApiModule),
        typeof(PracticeApplicationModule),
        typeof(PracticeEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule)
    )]
    public class PracticeHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context, configuration);
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                    {"Practice", "Practice API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Practice API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Practice API");

                var configuration = context.GetConfiguration();
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            });

            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
```

�޸� Program.cs �ļ���

```c#
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace YuLinTu.Practice
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting YuLinTu.Practice.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                 })
                 .UseAutofac()
                 .UseSerilog();
    }
}
```

�½��� HomeController �̳��� AbpController��

```c#
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace YuLinTu.Practice.Controllers
{
    public class HomeController : AbpController
    {
        public ActionResult Index()
        {
            return Redirect("~/swagger");
        }
    }
}
```

�޸� Startup.cs �ļ���

```c#
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace YuLinTu.Practice
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<PracticeHttpApiHostModule>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.InitializeApplication();
        }
    }
}
```

�޸� appsettings.json �ļ���

```json
{
  "App": {
    "CorsOrigins": "https://*.Practice.com,http://localhost:4200,https://localhost:44307"
  },
  "ConnectionStrings": {
    "Default": "Server=127.0.0.1;Port=5432;Database=practice;User Id=postgres;Password=123456;"
  },
  "AuthServer": {
    "Authority": "https://localhost:5000"
  }
}
```

�޸� Properties Ŀ¼�µ� launchSettings.json �ļ���

```json
{
  "profiles": {
    "YuLinTu.Practice.HttpApi.Host": {
      "commandName": "Project",
      "dotnetRunMessages": "true",
      "launchBrowser": true,
      "applicationUrl": "http://localhost:5001",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  }
}
```

### 3.13 Ǩ������

- YuLinTu.Practice.EntityFrameworkCore.DbMigrations ����������Ǩ���ļ�

�� VS �˵����� -> NuGet�������� -> ������������̨����ѡ��Ĭ����ĿΪ YuLinTu.Practice.EntityFrameworkCore.DbMigrations�� Ȼ��ִ���������

```cmd
Add-Migration "Init"
```

����ִ�к��� Migrations Ŀ¼������Ǩ���ļ���

��ִ����������������ݿ⣺

```cmd
Update-Database
```

����ִ�к���Կ������ɵ����ݿ������ݱ�

### 3.14 ����

�� YuLinTu.Practice.HttpApi.Host ����Ϊ����������д���Ŀ��

���гɹ���Ĭ�Ͻ��� Swagger ���棬�����Կ����Զ����ɵ� Book �ӿڡ�


## 4 ����

### 4.1 ����㵥Ԫ����

### 4.2 Ӧ�ò㵥Ԫ����

### 4.3 Swagger

### 4.4 Postman

## 5 � CI/CD

### 5.1 Docker

### 5.2 Jenkins

## �ο�����

- [Mircrosoft Docs](https://docs.microsoft.com/zh-cn/aspnet/core/web-api/?view=aspnetcore-5.0)
- [ABP Framework Docs](https://docs.abp.io/zh-Hans/abp/latest/Getting-Started?UI=MVC&DB=EF&Tiered=No)