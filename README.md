
<div align="center">
<br/>

  <h1 align="center">
    Pear Admin Abp
  </h1>
  <h4 align="center">
    开 箱 即 用 的 .Net 快 速 开 发 平 台
  </h4> 

  [预 览](http://net.pearadmin.com)   |   [官 网](http://www.pearadmin.com/)   |   [群聊](https://jq.qq.com/?_wv=1027&k=5OdSmve)   |   [社区](http://forum.pearadmin.com/)

</div>

<p align="center">
    <a href="#">
        <img src="https://img.shields.io/badge/Pear Admin Layui-3.1.0+-green.svg" alt="Pear Admin Layui Version">
    </a>
    <a href="#">
        <img src="https://img.shields.io/badge/JQuery-2.0+-green.svg" alt="Jquery Version">
    </a>
      <a href="#">
        <img src="https://img.shields.io/badge/Layui-2.5.6+-green.svg" alt="Layui Version">
    </a>
</p>

<div align="center">
<img  width="92%" style="border-radius:10px;margin-top:20px;margin-bottom:20px;box-shadow: 2px 0 6px gray;" src="https://images.gitee.com/uploads/images/2020/1217/094633_7aeb79b3_4835367.png" />
</div>

#### 介绍
基于ABP框架封装一套MPA框架，使用Layui作为前端呈现，封装常用的功能。旨在设计快速入手，快速实践框架。  

#### 软件架构
基于Abp框架并接入Pear Admin前端框架，Pear Admin框架基于Layui封装，两者均开源免费。  
* Abp部分采用Mvc+分层架构，分层架构按照职责水平分层，而不是采用限界上下文垂直分层形式。  
Abp:https://github.com/aspnetboilerplate/aspnetboilerplate 
* 前端部分Mvc视图中采用三段式，顶部为css样式，中部为Html标签，底部为Js脚本,整体为Pear Admin提供的样式基础与功能。  
Pear Admin:https://gitee.com/Jmysy/Pear-Admin-Layui  

#### 项目结构

整体的解决方案结构划分如下，其中PearAdmin.AbpTemplate.Admin重命名了ABP默认提供的Web.Mvc项目，一是从结构上出发，看起来更显得从上至下的分层结构，二是从请求路径上一目了然。并额外增加了PearAdmin.AbpTemplate.Gateway，以此来增加防腐层，方便各限界上下文间不要直接请求，通过防腐层约束请求调用。

```
- PearAdmin.AbpTemplate
    -src
        - PearAdmin.AbpTemplate.Admin
        - PearAdmin.AbpTemplate.Application
        - PearAdmin.AbpTemplate.Core
        - PearAdmin.AbpTemplate.EntityFrameworkCore
        - PearAdmin.AbpTemplate.Gateway
    - test
        - PearAdmin.AbpTemplate.Admin.Tests
        - PearAdmin.AbpTemplate.Application.Tests
        - PearAdmin.AbpTemplate.Core.Tests
    - tool
        - PearAdmin.AbpTemplate.Migrator
        - PearAdmin.AbpTemplate.Shared
```

说明：Admin为UI层，Application和Core为应用层和领域层，EntityFrameworkCore和Gateway及ABP自身提供的辅助设施组合为基础设施层。  

<img  width="92%" style="border-radius:10px;margin-top:20px;margin-bottom:20px;box-shadow: 2px 0 6px gray;" src="https://images.gitee.com/uploads/images/2020/1125/224921_f574357b_890387.png" />

#### 代码模型
从DDD的角度考虑，抛弃UI层，以应用层，领域层，基础设施层去出发，按照文件夹隔离出限界上下文边界，内部代码模型如下

```
- PearAdmin.AbpTemplate.Application
    - AContext
        - AAggregate
    - BContext
        - BAggregate
        - ...
- PearAdmin.AbpTemplate.Core
    - AContext
        - AAggregate
    - BContext
        - BAggregate
        - ...
- PearAdmin.AbpTemplate.EntityFrameworkCore
- PearAdmin.AbpTemplate.Gateway
    - AContext
```

防腐层的建立，约定在领域层建立防腐层接口，在基础设施层-网关中实现接口，网关层对Application层有项目依赖，方便实例化下游上下文所需要的上游上下文。为避免出现循环引用，上下文映射图中最好不要出现环。

```
- PearAdmin.AbpTemplate.Application
    - AContext
    - BContext
- PearAdmin.AbpTemplate.Core
    - AContext
        - ...
        - AntiCorruption
            - IBService
    - BContext
- PearAdmin.AbpTemplate.EntityFrameworkCore
- PearAdmin.AbpTemplate.Gateway
    - AContext
        - AntiConrruption
            - BService
```
如限界上下文A需要访问上下文B中信息或需要B上下文协作，则通过A建立的防腐层接口与实现访问B上下文中的服务，以此来避免直接耦合。
![输入图片说明](https://images.gitee.com/uploads/images/2020/1121/173254_0c7d3c61_890387.png "屏幕截图.png")