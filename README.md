# AutoFac依赖注入的学习

基于仓储模式设计的API接口，遵循RESTful API规范，并为[前端](https://github.com/jinjupeng/CoreNotes.AutoFac)提供接口服务。

## 前言

把使用了该项目的案例放在这里。可以放下载链接，或者简单放几张截图。  
（示例一开始就放出来，方便浏览者一眼就看出是不是想找的东西）

### 开发环境

+ ASP.NET Core 3.x
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
  - [X] 配置数据库
  - [X] 全局异常过滤
  - [X] 日志框架Serilog
  - [ ] AutoMapper
  - [X] Swagger UI
  - [x] REST API
  - [ ] Redis/MemoryCache缓存
  - [ ] 定时任务Hangfire/Quartz.net
  - [ ] 文件上传（包括分片上传、断点续传等）
  - [ ] MailKit收发邮件
  - [ ] 单元测试
  - [x] 基于角色权限配置
  - [x] 新增Vue前端
  - [ ] 性能监测MiniProfiler
  - [ ] 部署到Docker中
  - [ ] 部署到Linux上
  - [ ] 替换ORM框架-Dapper

## Docker部署

### Dockerfile文件见项目文件夹

### 发布项目

将编译并打包好文件上传到Linux服务器上的某个文件夹中

### 构建镜像

```bash
cd xxx/
docker build -t autofac . # 镜像名autofac
```

### 启动容器

```bash
docker run --name=autofaccontain -d -p 80:5000 autofacimage
# --name：指定容器名称
# -p：指定主机端口映射容器端口，主机端口80，容器端口5000
# -d：指定容器后台运行
docker ps -a # 查看启动状态，STATUS为UP则成功
```



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
