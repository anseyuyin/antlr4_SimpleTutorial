### 说明
这个是一个，使用antlr4 ，实现支持四则运算的计算器应用项目。

### 环境
语言 ：C#
IDE工具：Visual Studio 2022
.net版本：core 3.1

### 项目
- libParser : 该项目引入依赖了 Antlr4 、 Antlr4.Runtime , 是存放 .g4 文件的位置，通过“生成” 操作，可获得 词法、语法 分析器。
- calculatorApp : 该项目依赖 libParser ,通过 Calculator.g4 生成的分析器，构成了一个控制台式的“计算器”程序。