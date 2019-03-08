# SJNScaffolding
ABP代码生成器

#### 本项目使用RazorEngine模板引擎技术，对于熟悉razor语法的开发者来说是非常容易的。

### 

 这里提供最简单的一个栗子。

demo1

CopyRightTemplate.cshtml模板代码如下
~~~~
    @model SJNScaffolding.Models.TemplateModels.CopyRightUserInfo
    //=============================================================
    // 创建人:              @Model.UserName
    // 创建时间:           @Model.CreateTime
    // 邮箱：             @Model.EmailAddress
    //==============================================================
~~~~

对应的实体类
~~~
    public class CopyRightUserInfo
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreateTime { get; set; }
        public string FileRemark { get; set; }
    }
~~~~

对应的test方法
~~~
        //根据路径。要根据自己实际情况调整
        private const string BasePath = @"..\..\..\SJNScaffolding\";
        [TestMethod]
        public void testCorpyRight()
        {
            var path = BasePath + "Templates\\CopyRightTemplate.cshtml";
            var template = File.ReadAllText(path);

            string content = Engine.Razor.RunCompile(template, "CopyRightTemplate", typeof(CopyRightUserInfo), new CopyRightUserInfo
            {
                CreateTime = DateTime.Now,
                EmailAddress = "710277267@qq.com",
                UserName = "IGeekFan"
            });

        }

~~~

下断点后运行，content变量

![avatar](Img/1.png)

解决了模板生成，我们就很容易的写出自己的代码生成器。


### 以上内容为.NET Framework版本的Razor，该项目已升级至.NET Core2.2 版本，旧版本已移除。不过新版本的Razor生成方式和语法都相差不大。

#### 接下来为代码生成器计划:
    1、中文转英文字段，自动起名字，主要关键字段转换。
    2、主分表代码生成
    3、下拉代码自动生成，勾选
    4、代码生成器部署到某一服务器中，生成后可下载生成后的代码，复制至项目中即可。



CodeLF帮程序员起变量名的网站:[https://unbug.github.io/codelf/](https://unbug.github.io/codelf/)