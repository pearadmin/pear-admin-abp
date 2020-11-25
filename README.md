# Partner.Surround

#### 介绍
基于ABP框架封装一套MPA框架，使用Layui作为前端呈现，封装常用的功能。旨在设计快速入手，快速实践框架。  
稳定地址(新版)：http://119.3.138.127/  
测试地址(新版)：http://119.3.138.127:9527/  
稳定地址(老版)：http://119.3.138.127:9500/  

#### 软件架构
基于Abp框架并接入Pear Admin前端框架，Pear Admin框架基于Layui封装，两者均开源免费。  
* Abp部分采用Mvc+分层架构，分层架构按照职责水平分层，而不是采用限界上下文垂直分层形式。  
Abp:https://github.com/aspnetboilerplate/aspnetboilerplate 
* 前端部分Mvc视图中采用三段式，顶部为css样式，中部为Html标签，底部为Js脚本,整体为Pear Admin提供的样式基础与功能。  
Pear Admin:https://gitee.com/Jmysy/Pear-Admin-Layui  

#### 项目结构

整体的解决方案结构划分如下，其中Surround.Admin重命名了ABP默认提供的Web.Mvc项目，一是从结构上出发，看起来更显得从上至下的分层结构，二是从请求路径上一目了然。并额外增加了Surround.Gateway，以此来增加防腐层，方便各限界上下文间不要直接请求，通过防腐层约束请求调用。

```
- Surround
    -src
        - Surround.Admin
        - Surround.Application
        - Surround.Core
        - Surround.EntityFrameworkCore
        - Surround.Gateway
    - test
        - Surround.Admin.Tests
        - Surround.Application.Tests
        - Surround.Core.Tests
    - tool
        - Surround.Migrator
        - Surround.Shared
```

说明：Admin为UI层，Application和Core为应用层和领域层，EntityFrameworkCore和Gateway及ABP自身提供的辅助设施组合为基础设施层。  

![输入图片说明](https://images.gitee.com/uploads/images/2020/1121/171553_bb9520e7_890387.png "屏幕截图.png")

#### 代码模型
从DDD的角度考虑，抛弃UI层，以应用层，领域层，基础设施层去出发，按照文件夹隔离出限界上下文边界，内部代码模型如下

```
- Surround.Application
    - AContext
        - AAggregate
    - BContext
        - BAggregate
        - ...
- Surround.Core
    - AContext
        - AAggregate
    - BContext
        - BAggregate
        - ...
- Surround.EntityFrameworkCore
- Surround.Gateway
    - AContext
```

防腐层的建立，约定在领域层建立防腐层接口，在基础设施层-网关中实现接口，网关层对Application层有项目依赖，方便实例化下游上下文所需要的上游上下文。为避免出现循环引用，上下文映射图中最好不要出现环。

```
- Surround.Application
    - AContext
    - BContext
- Surround.Core
    - AContext
        - ...
        - AntiCorruption
            - IBService
    - BContext
- Surround.EntityFrameworkCore
- Surround.Gateway
    - AContext
        - AntiConrruption
            - BService
```
如限界上下文A需要访问上下文B中信息或需要B上下文协作，则通过A建立的防腐层接口与实现访问B上下文中的服务，以此来避免直接耦合。
![输入图片说明](https://images.gitee.com/uploads/images/2020/1121/173254_0c7d3c61_890387.png "屏幕截图.png")

#### 部署方式
老版采用Jenkins与Docker Compose进行持续集成，利用腾讯云Coding中的制品库作为镜像存储仓库。  
新版采用Coding直接完成持续集成。  
Coding:https://coding.net/

#### 快速部署
下载镜像  
`docker pull starcity-docker.pkg.coding.net/partner.surround/imageservice/partnersurround:latest`

简化名称  
`docker tag starcity-docker.pkg.coding.net/partner.surround/imageservice/partnersurround partnersurround`

运行网站  
`docker run -dit -p 9527:80 --env ASPNETCORE_ENVIRONMENT=Development partnersurround partnersurround`

注:数据库链接使用的测试环境地址，方便直接部署运行。

#### 整体流程
![输入图片说明](https://images.gitee.com/uploads/images/2020/0705/200516_e24c4bcd_890387.png "屏幕截图.png")

#### 使用文档
采用宿主-单租户模式，分为宿主登录页面与租户登录页面  
* 宿主登录页面，登录url为Account/HostLogin  
http://119.3.138.127:9527/Account/HostLogin  
* 租户登录页面，登录url为Account/Login  
http://119.3.138.127:9527/Account/Login

注：对于两者登录页面均相同，仅在url上体现差异，以此来作为宿主与租户差异

#### 开发文档
《ABP开发规范》https://shimo.im/docs/JHjThwvvyRKXtYKJ/ 