# SJNScaffolding
ABP代码生成器

#### 本项目使用RazorEngine模板引擎技术，对于熟悉razor语法的开发者来说是非常容易的。



1. 最简单的一个例子

~~~~
    @model SJNScaffolding.Models.TemplateModels.CopyRightUserInfo
    //=============================================================
    // 创建人:              @Model.UserName
    // 创建时间:           @Model.CreateTime
    // 邮箱：             @Model.EmailAddress
    //==============================================================
~~~~

