# SJNScaffolding
ABP代码生成器

### *旧版本使用WPF进行开发，对应的项目名为：SJNScaffolding.WPF，已弃用*

----------


### 这里主要介绍的是SJNScaffolding.RazorPage新项目的使用，RazorEngine模板引擎开发的技术，对于熟悉razor语法的开发者来说是非常容易的。


> #首先你需要像这样格式的一个数据字典

  ![avatar](Img/2.png)


> #运行程序，然后首先来到配置界面，这里可以配置你要生成的表名，以及项目名称等，配置完成之后点击**保存配置**

  ![avatar](Img/3.png)

> #表结构设置：在这里将你需要生成的字段从数据字典里面复制进来如图：
  注意：ID，IsDeleted，DeleterUserId，DeletionTime等字段是ABP自动生成的字段这里不必复制进来

  ![avatar](Img/4.png)

> #字段复制进来后点击导入字段，生成如下图列表，自行确定每个字段是否需要后点击*生成代码*

  ![avatar](Img/5.png)

> #代码生成之后会在对应的目录下面生成对应的文件，只需要手动添加到项目中即可使用（这里还有待改进）

  ![avatar](Img/6.png)
