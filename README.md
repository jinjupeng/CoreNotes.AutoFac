# AutoFac依赖注入的学习

-------------

> 每天进步一点点

## 示例

把使用了该项目的案例放在这里。可以放下载链接，或者简单放几张截图。  
（示例一开始就放出来，方便浏览者一眼就看出是不是想找的东西）

### 开发环境

+ ASP.NET Core 3.0
+ Visual Studio 2019

### 特性（可选）

- 特性A

- 特性B

### 原理说明（可选）

阐述项目是基于什么思路设计的

### 下载安装

### 使用方法

怎么使用，有哪些步骤哪些接口。

### 项目整体架构

  + CoreNotes.AutoFac
    + CoreNotes.AutoFac.CoreApi：WebApi
    + CoreNotes.AutoFac.Ioc：依赖注入容器
    + CoreNotes.AutoFac.IRepository：仓储接口层
    + CoreNotes.AutoFac.IService：服务接口层
    + CoreNotes.AutoFac.Model：模型
    + CoreNotes.AutoFac.Repository：实现仓储接口
    + CoreNotes.AutoFac.Service：实现服务接口

比如混淆方法等

### TODO

- [ ] **开发计划**
  - [X] 微软内置DI
  - [X] AutoFac
  - [ ] AutoMapper
  - [x] Swagger UI
  - [ ] REST Apis
  - [ ] 单元测试

## 链接

[.NET Core3.0的官方教程](https://docs.microsoft.com/zh-cn/aspnet/core/?view=aspnetcore-3.0)

[.NET Core无处不在的依赖注入](https://juejin.im/post/5d6736fff265da03c128abca)
[ASP.NET Core依赖注入解读&使用Autofac替代实现](https://cloud.tencent.com/developer/article/1023209)

[服务的生命周期](https://docs.microsoft.com/zh-cn/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.0#service-lifetimes)

[.NET Core 3.0的AutoFac是实现1](https://github.com/aspnet/AspNetCore.Docs/issues/11441)
[.NET Core 3.0的AutoFac是实现2](https://stackoverflow.com/questions/56385277/configure-autofac-in-asp-net-core-3-0-preview-5-or-higher)
[.NET Core 3.0的AutoFac是实现3](https://stackoverflow.com/questions/37063652/autofac-module-registrations)

## License

MIT
