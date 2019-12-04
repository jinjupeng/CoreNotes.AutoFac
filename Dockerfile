# 基础镜像信息 
# FROM -指定所创建镜像的基础镜像
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
# 描述
LABEL description="基于.NET CORE 3.1构建的API服务"
LABEL version="1.0.0"
# WORKDIR -配置工作目录
WORKDIR /app
# EXPOSE -声明镜像内服务监听的端口
# EXPOSE 指令是声明运行时容器提供服务端口，这只是一个声明，在运行时并不会因为这个声明应用就会开启这个端口的服务。
# EXPOSE 仅仅是声明容器打算使用什么端口而已,并不会自动在宿主进行端口映射，但可以通过 -p 设置来指定映射
EXPOSE 80
# COPY -将当前目录下的文件，复制到WORKDIR目录中
COPY . .
# ENTRYPOINT - 运行容器的命令
ENTRYPOINT ["dotnet", "CoreNotes.AutoFac.dll"]