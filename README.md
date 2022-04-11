### antlr4 是什么
- antlr 是一个由java编写的语法识别工具，官方介绍为Antlr4是一款强大的解析器生成工具，可用来读取、处理、执行和翻译结构化文本或二进制文件，一般通过在.g4 中编写产生式 ，来生成相应的词法分析器和语法分析器代码，编写产生式的语言 被称为元语言（meta language），及编写语言的语言。

### antlr4 的工作方式：
- 新建 xxx.g4 文件，编写产生式。
- antrl4 通过 .g4 格式的产生式文本文件，执行生成 词法分析器、语法分析器代码。
- 利用 词法分析器、语法分析器代码，构建自己的语言应用程序，例如，计算器、json解析器、SVG绘图器...

### 什么地方用
需要逻辑文本转换为计算机程序执行的应用场景。
- 计算器 ，输入 算式文本，执行 输出计算结果。
- SVG绘图器 ，输入 SVG文本，执行 输出绘制图。
- javaScript 引擎，输入 js代码文本，执行 js脚本逻辑。

### 怎么做
##### 环境配置

##### 简单样例
目标，制作一个支持四则运算的计算器。
- 新建 calculator.g4 文件。
- 在 calculator.g4 中编写 产生式。
- 生成 词法语法分析器代码。
- 编译 分析器代码。
- 用 view模式，编写计算器逻辑。
- 生成计算器程序。

### 资料
《antlr4权威指南》
[各语言 antrl4 实现仓库](https://github.com/antlr/grammars-v4)
