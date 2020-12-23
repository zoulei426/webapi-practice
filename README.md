<!--
 * @Description: 基于 ABP vNext 的 Web API 开发教程
 * @Author: zoulei
 * @Date: 2020-12-18 13:26:04
 * @LastEditors: zoulei
 * @LastEditTime: 2020-12-23 10:02:57
-->

# 基于 ABP vNext 的 Web API 开发教程

## 目录

<!-- TOC -->

- [基于 ABP vNext 的 Web API 开发教程](#基于-abp-vnext-的-web-api-开发教程)
  - [目录](#目录)
  - [1 概述](#1-概述)
  - [2 环境](#2-环境)
    - [2.1 先决条件](#21-先决条件)
    - [2.2 安装 .NET 5.0](#22-安装-net-50)
    - [2.3 安装 Visual Studio Preview](#23-安装-visual-studio-preview)
  - [3 创建项目](#3-创建项目)
    - [3.1 搭建领域层](#31-搭建领域层)
    - [3.2 创建领域层实体](#32-创建领域层实体)
    - [3.3 搭建 Entity Framework Core](#33-搭建-entity-framework-core)
    - [3.4 创建实体与数据库映射](#34-创建实体与数据库映射)
    - [3.5 搭建数据迁移](#35-搭建数据迁移)
    - [3.6 搭建服务层](#36-搭建服务层)
    - [3.7 创建数据传输对象 DTO](#37-创建数据传输对象-dto)
    - [3.8 创建 DTO 与实体的映射](#38-创建-dto-与实体的映射)
    - [3.9 创建应用服务](#39-创建应用服务)
    - [3.10 搭建控制器层](#310-搭建控制器层)
    - [3.11 创建控制器](#311-创建控制器)
    - [3.12 搭建宿主](#312-搭建宿主)
    - [3.13 迁移数据](#313-迁移数据)
    - [3.14 运行](#314-运行)
  - [4 测试](#4-测试)
    - [4.1 单元测试](#41-单元测试)
    - [4.2 搭建 EntityFrameworkCore 测试](#42-搭建-entityframeworkcore-测试)
    - [4.3 搭建领域层测试](#43-搭建领域层测试)
    - [4.4 搭建应用层测试](#44-搭建应用层测试)
    - [4.5 Postman](#45-postman)
  - [5 搭建 CI/CD](#5-搭建-cicd)
    - [5.1 Docker](#51-docker)
    - [5.2 Jenkins](#52-jenkins)
  - [参考文献](#参考文献)

<!-- /TOC -->

## 1 概述

本教程仅介绍使用 ABP vNext 构建 Web API 的基础知识，更多内容请查阅参考文献。

在本教程中，你将了解：
- 使用 ABP vNext 创建 Web API 项目
- 使用 Entity Framework Core
- 自动部署与测试

谁适合阅读本教程：
- 掌握 C# 语法与 .NET 框架
- 了解 ASP.NET Core 应用程序开发

## 2 环境

### 2.1 先决条件

- .NET 5.0 SDK 或更高版本
- 具有“ASP.NET 和 Web 开发”工作负载的 Visual Studio 2019 16.8 或更高版本
- Postgresql 9.2 或更高版本

### 2.2 安装 .NET 5.0

[.NET 5.0 SDK 下载](https://dotnet.microsoft.com/download/dotnet/5.0)

### 2.3 安装 Visual Studio Preview

[Visual Studio Preview 下载](https://visualstudio.microsoft.com/zh-hans/vs/preview/)

## 3 创建项目

### 3.1 搭建领域层

新建一个基于 .NET Standard 2.0 的类库 YuLinTu.Practice.Domain.Shared，解决方案名称为 YuLinTu.Practice，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.Domain.Shared 项目中安装 ABP 组件：

```PM
Install-Package Volo.Abp.Core
```

在项目根目录下新建类 PracticeDomainSharedModule 继承于 AbpModule：  
（本教程不涉及本地化、多租户等内容，因此不需要更多的配置，更多内容请查看参考文献。）

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

新建一个基于 .NET 5.0 的类库 YuLinTu.Practice.Domain，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.Domain 项目中引用 YuLinTu.Practice.Domain.Shared 项目，并安装 ABP 组件：

```PM
Install-Package Volo.Abp.Ddd.Domain
```

在项目根目录下新建类 PracticeDomainModule 继承于 AbpModule，并添加 PracticeDomainSharedModule 模块：

```c#
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    public class PracticeDomainSharedModule : AbpModule
    {
    }
}
```

### 3.2 创建领域层实体

- YuLinTu.Practice.Domain 中创建实体、领域服务、仓储接口等
- YuLinTu.Practice.Domain.Shared 中创建共享的常量、枚举等

在 YuLinTu.Practice.Domain 项目中新建 Books 目录并创建 Book 实体：

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

- ABP 为实体提供了两个基本的基类: AggregateRoot 和 Entity。AggregateRoot 即领域驱动设计中的聚合根<sup id="a1">[1](#f1)</sup>。
- Audited 前缀在 AggregateRoot / Entity 类的基础上添加了一些审计属性(CreationTime, CreatorId, LastModificationTime 等)。
- Guid 是 Book 实体的主键类型。**不要为你的实体使用 Guid.NewGuid() 创建 ID，当需要手动设置实体的 ID 时，请使用 IGuidGenerator.Create()。**

在 YuLinTu.Practice.Domain.Shared 项目中新建 Books 目录并创建 BookType 枚举：

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

### 3.3 搭建 Entity Framework Core

新建一个基于 .NET 5.0 的类库 YuLinTu.Practice.EntityFrameworkCore，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.EntityFrameworkCore 项目中引用 YuLinTu.Practice.Domain 项目，并安装 ABP 组件：

```PM
Install-Package Volo.Abp.EntityFrameworkCore
Install-Package Volo.Abp.EntityFrameworkCore.PostgreSql
Install-Package Volo.Abp.AuditLogging.EntityFrameworkCore
```

新建文件夹 EntityFrameworkCore 并创建类 PracticeEntityFrameworkCoreModule 继承于 AbpModule，并添加 PracticeDomainModule, AbpEntityFrameworkCorePostgreSqlModule 模块：

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
                // 添加缺省仓储
                options.AddDefaultRepositories(true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                // 使用 Postgresql 数据源
                options.UseNpgsql();
            });
        }
    }
}
```

在目录 EntityFrameworkCore 下创建类 PracticeDbContext：

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

### 3.4 创建实体与数据库映射

- YuLinTu.Practice.EntityFrameworkCore 中完成实体与数据库映射、实现仓储接口等

将 Book 实体添加到 PracticeDbContext 中，同时创建扩展类 PracticeDbContextModelCreatingExtensions：

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

在 PracticeDbContextModelCreatingExtensions 中配置实体与数据库表的映射：

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

- ConfigureByConvention() 方法自动映射继承的属性。

新建类 PracticeMigrationsDbContextFactory，用于执行 EF Core 的 Add-Migration, Update-Database 等命令：

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
                .UseNpgsql("Server=127.0.0.1;Port=5432;Database=practice;User Id=postgres;Password=123456;");

            return new PracticeMigrationsDbContext(builder.Options);
        }
    }
}
```

### 3.5 搭建数据迁移

> *若项目是 DbFirst，可以忽略数据迁移步骤。*

新建一个基于 .NET 5.0 的类库 YuLinTu.Practice.EntityFrameworkCore.DbMigrations，并修改默认命名空间为 YuLinTu.Practice。

在空 YuLinTu.Practice.EntityFrameworkCore.DbMigrations 项目中引用 YuLinTu.Practice.EntityFrameworkCore 项目，并安装 EF Core 组件：

```PM
Install-Package Microsoft.EntityFrameworkCore.Design
```

新建文件夹 EntityFrameworkCore 并创建类 PracticeEntityFrameworkCoreDbMigrationsModule 继承于 AbpModule，并添加 PracticeEntityFrameworkCoreModule 模块：

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

在目录 EntityFrameworkCore 下创建类 PracticeMigrationsDbContext：

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

### 3.6 搭建服务层

新建一个基于 .NET Standard 2.0 的类库 YuLinTu.Practice.Application.Contracts，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.Application.Contracts 项目中引用 YuLinTu.Practice.Domain.Shared 项目，并安装 ABP 组件：

```PM
Install-Package Volo.Abp.Ddd.Application
```

在项目根目录下新建类 PracticeApplicationContractsModule 继承于 AbpModule：

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

新建一个基于 .NET 5.0 的类库 YuLinTu.Practice.Application，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.Application 项目中引用 YuLinTu.Practice.Application.Contracts, YuLinTu.Practice.Domain 项目，并安装 ABP 组件：

```PM
Install-Package Volo.Abp.AutoMapper
```

新建类 PracticeApplicationModule 继承于 AbpModule，并添加 PracticeApplicationContractsModule, PracticeDomainSharedModule, AbpAutoMapperModule 模块：

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

### 3.7 创建数据传输对象 DTO

- YuLinTu.Practice.Application.Contracts 中创建DTO和应用服务接口
- YuLinTu.Practice.Application 中实现应用服务接口

在 YuLinTu.Practice.Application.Contracts 项目中创建 BookDto。

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

- BookDto 继承自 AuditedEntityDto<Guid>，跟之前定义的 Book 实体一样具有一些审计属性。
- 数据传输对象（DTO）用于在应用层和表示层或其他类型的客户端之间传输数据。
- DTO 应该是可序列化的。
- 除验证代码外，不应包含任何业务逻辑。
- DTO 不要继承实体，也不要引用实体。
- BookDto 属于输出 DTO，请尽量**重用输出 DTO**。

创建 CreateUpdateBookDto。

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

- CreateUpdateBookDto 包括了创建与更新，在实际项目中，请尽量拆分成CreateDto 与 UpdateDto。
- CreateDto 与 UpdateDto 属于输入DTO，请不要在不同的应用程序服务方法之间重用输入 DTO。

### 3.8 创建 DTO 与实体的映射

在 YuLinTu.Practice.Application 项目中创建 BookStoreApplicationAutoMapperProfile 类中定义实体与DTO的映射。

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

- 将书籍返回到表示层时，需要将 Book 实体转换为 BookDto 对象，从表示层输入的 CreateUpdateBookDto 也需要转换为 Book 实体。AutoMapper 库可以在定义了正确的映射时自动执行此转换，有关 AutoMapper 更多内容，请查阅参考文档。

### 3.9 创建应用服务

在 YuLinTu.Practice.Application.Contracts 项目中定义一个名为 IBookAppService 的接口。

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

- ICrudAppService 定义了常见的 CRUD 方法：GetAsync, GetListAsync, CreateAsync, UpdateAsync 和 DeleteAsync. 你可以从空的IApplicationService 接口继承并手动定义自己的方法。
- PagedAndSortedResultRequestDto 实现了 IPagedResultRequest, ISortedResultRequest 接口，包括了分页与排序所需的属性：MaxResultCount, SkipCount, Sorting。

在 YuLinTu.Practice.Application 项目中创建名为 BookAppService 的 IBookAppService 实现。

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

### 3.10 搭建控制器层

新建一个基于 .NET 5.0 的类库 YuLinTu.Practice.HttpApi，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.HttpApi 项目中引用 YuLinTu.Practice.Application.Contracts 项目，并安装 ABP 组件：

```PM
Install-Package Volo.Abp.AspNetCore.Mvc
```

新建类 PracticeHttpApiModule 继承于 AbpModule，并添加 PracticeApplicationContractsModule, AbpAspNetCoreMvcModule模块：

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


### 3.11 创建控制器

> *ABP vNext 可以自动根据应用服务生成 RESTFULL 风格的 API*  

在 YuLinTu.Practice.HttpApi 项目中创建 Controllers 文件夹，并新建控制器 BookController 继承于 AbpController

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

### 3.12 搭建宿主

新建一个基于 ASP.NET Core Web 的应用程序 YuLinTu.Practice.HttpApi.Host，选择空模板，取消 HTTPS 配置，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.HttpApi.Host 项目中引用 YuLinTu.Practice.Application, YuLinTu.Practice.EntityFrameworkCore, YuLinTu.Practice.EntityFrameworkCore.DbMigrations, YuLinTu.Practice.HttpApi 项目，并安装 ABP 组件：

```PM
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Serilog.AspNetCore
Install-Package Serilog.Sinks.Async
Install-Package Volo.Abp.Autofac
Install-Package Volo.Abp.Caching
Install-Package Volo.Abp.Swashbuckle
Install-Package Volo.Abp.AspNetCore.Serilog
Install-Package Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared
```

新建类 PracticeHttpApiHostModule 继承于 AbpModule：

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

修改 Program.cs 文件：

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

新建类 HomeController 继承于 AbpController：

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

修改 Startup.cs 文件：

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

修改 appsettings.json 文件：

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

修改 Properties 目录下的 launchSettings.json 文件：

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

### 3.13 迁移数据

- YuLinTu.Practice.EntityFrameworkCore.DbMigrations 中生成数据迁移文件

打开 VS 菜单工具 -> NuGet包管理器 -> 程序包管理控制台，再选择默认项目为 YuLinTu.Practice.EntityFrameworkCore.DbMigrations， 然后执行以下命令：

```cmd
Add-Migration "Init"
```

命令执行后将在 Migrations 目录下生成迁移文件。

再执行以下命令更新数据库：

```cmd
Update-Database
```

命令执行后可以看到生成的数据库与数据表。

### 3.14 运行

将 YuLinTu.Practice.HttpApi.Host 设置为启动项，再运行此项目。

运行成功后将默认进入 Swagger 界面，并可以看到自动生成的 Book 接口。


## 4 测试

### 4.1 单元测试

将第3节创建的项目移动到 src 目录下，再新建 test 文件夹，在此文件夹下创建单元测试项目。

新建一个基于 xunit 的测试项目 YuLinTu.Practice.TestBase，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.TestBase 项目中引用 YuLinTu.Practice.Domain 项目，并安装 ABP 组件：

```PM
Install-Package xunit.extensibility.execution
Install-Package NSubstitute
Install-Package Shouldly
Install-Package Volo.Abp.TestBase
Install-Package Volo.Abp.Autofac
```

新建类 PracticeTestBaseModule 继承于 AbpModule：

```c#
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace YuLinTu.Practice
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpTestBaseModule),
        typeof(PracticeDomainModule)
        )]
    public class PracticeTestBaseModule : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            // 添加测试种子数据
            SeedTestData(context);
        }

        private static void SeedTestData(ApplicationInitializationContext context)
        {
            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}
```

新建测试基类 PracticeTestBase：

```c#
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.Abp.Testing;
using Volo.Abp.Uow;

namespace YuLinTu.Practice
{
    public abstract class PracticeTestBase<TStartupModule> : AbpIntegratedTest<TStartupModule>
         where TStartupModule : IAbpModule
    {
        protected override void SetAbpApplicationCreationOptions(AbpApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        protected virtual Task WithUnitOfWorkAsync(Func<Task> func)
        {
            return WithUnitOfWorkAsync(new AbpUnitOfWorkOptions(), func);
        }

        protected virtual async Task WithUnitOfWorkAsync(AbpUnitOfWorkOptions options, Func<Task> action)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

                using (var uow = uowManager.Begin(options))
                {
                    await action();

                    await uow.CompleteAsync();
                }
            }
        }

        protected virtual Task<TResult> WithUnitOfWorkAsync<TResult>(Func<Task<TResult>> func)
        {
            return WithUnitOfWorkAsync(new AbpUnitOfWorkOptions(), func);
        }

        protected virtual async Task<TResult> WithUnitOfWorkAsync<TResult>(AbpUnitOfWorkOptions options, Func<Task<TResult>> func)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

                using (var uow = uowManager.Begin(options))
                {
                    var result = await func();
                    await uow.CompleteAsync();
                    return result;
                }
            }
        }
    }
}
```

### 4.2 搭建 EntityFrameworkCore 测试

新建一个基于 xunit 的测试项目 YuLinTu.Practice.EntityFrameworkCore.Tests，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.EntityFrameworkCore.Tests 项目中引用 YuLinTu.Practice.EntityFrameworkCore.DbMigrations, YuLinTu.Practice.TestBase 项目，并安装 ABP 组件：

```PM
Install-Package Volo.Abp.EntityFrameworkCore.Sqlite
```

新建类 PracticeEntityFrameworkCoreTestModule 继承于 AbpModule，此类中配置 Sqlite 内存数据库作为测试的数据源：

```c#
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;
using YuLinTu.Practice.EntityFrameworkCore;

namespace YuLinTu.Practice
{
    [DependsOn(
        typeof(PracticeEntityFrameworkCoreDbMigrationsModule),
        typeof(PracticeTestBaseModule),
        typeof(AbpEntityFrameworkCoreSqliteModule)
        )]
    public class PracticeEntityFrameworkCoreTestModule : AbpModule
    {
        private SqliteConnection _sqliteConnection;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureInMemorySqlite(context.Services);
        }

        private void ConfigureInMemorySqlite(IServiceCollection services)
        {
            _sqliteConnection = CreateDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlite(_sqliteConnection);
                });
            });
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            _sqliteConnection.Dispose();
        }

        private static SqliteConnection CreateDatabaseAndGetConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<PracticeMigrationsDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new PracticeMigrationsDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }
    }
}
```

### 4.3 搭建领域层测试

新建一个基于 xunit 的测试项目 YuLinTu.Practice.Domain.Tests，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.Domain.Tests 项目中引用 YuLinTu.Practice.EntityFrameworkCore.Tests 项目。

新建类 PracticeEntityFrameworkCoreTestModule 继承于 AbpModule：

```c#
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(typeof(PracticeEntityFrameworkCoreTestModule))]
    public class PracticeDomainTestModule : AbpModule
    {
    }
}
```

新建领域层测试基类 PracticeDomainTestBase：

```c#
namespace YuLinTu.Practice
{
    public abstract class PracticeDomainTestBase : PracticeTestBase<PracticeDomainTestModule>
    {
    }
}
```

新建 Book 的测试类 BookRepositoryTests：

```c#
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace YuLinTu.Practice.Books
{
    public class BookRepositoryTests : PracticeDomainTestBase
    {
        private readonly IRepository<Book, Guid> bookRepository;

        public BookRepositoryTests()
        {
            bookRepository = GetRequiredService<IRepository<Book, Guid>>();
        }

        [Fact]
        public async Task Should_Create_A_Book()
        {
            var book = new Book
            {
                Name = "大话设计模式",
                Type = BookType.Science,
                PublishDate = new DateTime(2007, 12, 1)
            };

            var result = await bookRepository.InsertAsync(book, true);

            result.Id.ShouldNotBe(Guid.Empty);
        }
    }
}
```

此时可在测试资源管理器内进行领域层的测试。

### 4.4 搭建应用层测试

新建一个基于 xunit 的测试项目 YuLinTu.Practice.Application.Tests，并修改默认命名空间为 YuLinTu.Practice。

在 YuLinTu.Practice.Application.Tests 项目中引用 YuLinTu.Practice.Application, YuLinTu.Practice.Domain.Tests 项目。

新建类 PracticeApplicationTestModule 继承于 AbpModule：

```c#
using Volo.Abp.Modularity;

namespace YuLinTu.Practice
{
    [DependsOn(
       typeof(PracticeApplicationModule),
       typeof(PracticeDomainTestModule)
       )]
    public class PracticeApplicationTestModule : AbpModule
    {
    }
}
```

新应用层测试基类 PracticeApplicationTestBase：

```c#
namespace YuLinTu.Practice
{
    public abstract class PracticeApplicationTestBase : PracticeTestBase<PracticeApplicationTestModule>
    {
    }
}
```

新建 Book 的测试类 BookAppServiceTests：

```c#
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace YuLinTu.Practice.Books
{
    public class BookAppServiceTests : PracticeApplicationTestBase
    {
        private readonly IBookAppService bookAppService;

        public BookAppServiceTests()
        {
            bookAppService = GetRequiredService<IBookAppService>();
        }

        [Fact]
        public async Task Should_Create_A_Book()
        {
            var book = new CreateUpdateBookDto
            {
                Name = "大话设计模式",
                Type = BookType.Science,
                PublishDate = new DateTime(2007, 12, 1)
            };

            var result = await bookAppService.CreateAsync(book);

            result.Id.ShouldNotBe(Guid.Empty);
        }
    }
}
```

### 4.5 Postman

安装 Postman：

[Postman 下载](https://www.postman.com/downloads/)

运行 YuLinTu.Practice.HttpApi.Host 项目，运行 Postman。

Get 请求：  
> 输入 http://localhost:5001/api/app/book  
> 点击 Send 发送请求

Post 请求：  
> 选择 POST，输入 http://localhost:5001/api/app/book  
> 切换到 Body，选择 raw，再选择 JSON 格式，输入：
> ```json
> {
>   "name": "大话设计模式",
>   "type": 6,
>   "publishDate": "2007-12-01",
>   "price": 45
> }
> ```
> 点击 Send 发送请求


## 5 搭建 CI/CD

### 5.1 Docker

### 5.2 Jenkins

## 参考文献

- [Mircrosoft Docs](https://docs.microsoft.com/zh-cn/aspnet/core/web-api/?view=aspnetcore-5.0)
- [ABP Framework Docs](https://docs.abp.io/zh-Hans/abp/latest/Getting-Started?UI=MVC&DB=EF&Tiered=No)
- [AutoMapper Docs](https://docs.automapper.org/en/latest/)

<a name="f1">1</a>: 如果把聚合比作组织，聚合根则是组织的负责人，聚合根也叫做根实体，它不仅仅是实体，还是实体的管理者。让实体和值对象协同工作的组织就是聚合，用来确保这些领域对象在实现公共的业务逻辑的时候，可以保持数据的一致性。[?](#a1)