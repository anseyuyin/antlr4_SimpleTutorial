### antlr4 是什么
- antlr 是一个由java编写的语法识别工具，官方介绍为Antlr4是一款强大的解析器生成工具，可用来读取、处理、执行和翻译结构化文本或二进制文件，一般通过在.g4 中编写产生式 ，来生成相应的词法分析器和语法分析器代码，编写产生式的语言 被称为元语言（meta language），及编写语言的语言。

### antlr4 的工作方式：
- 新建 xxx.g4 文件，编写产生式。
- antrl4 通过 .g4 格式的产生式文本文件，执行生成 词法分析器、语法分析器代码。
- 利用 词法分析器、语法分析器代码，构建自己的语言应用程序，例如，计算器、json解析器、SVG绘图器...

### 应用场景
需要逻辑文本转换为计算机程序执行的应用场景。
- 计算器 ，输入 算式文本，执行 输出计算结果。
- SVG绘图器 ，输入 SVG文本，执行 输出绘制图。
- javaScript 引擎，输入 js代码文本，执行 js脚本逻辑。

### 怎么做
##### 环境配置
- java 版 
> 1.[JDK 环境配置](https://blog.csdn.net/weixin_37601546/article/details/88623530)
> 2.获取 antlr4 java程序 [antlr-xxx-complete.jar](https://www.antlr.org/download.html)
> 3.window 平台 环境变量设置 [(官方)](https://github.com/antlr/antlr4/blob/master/doc/getting-started.md#windows)

- c# 版
>1.Visual Studio 新建C#项目
>2.NuGet 管理器，添加 Antlr4 、 Antlr4.Runtime

##### 简单样例
目标，制作一个支持四则运算的计算器。[(完整样例链接)](https://github.com/anseyuyin/antlr4_SimpleTutorial/tree/main/examples/calculator)

项目环境: Visual Studio 2022 、C# .net core 3.1

详细请看样例工程,关键点代码如下：
- 新建 calculator.g4 文件,编写 产生式。
```` g4
grammar Calculator;

//根节点
prog: expr;

//表达式（定义了四则运算规则）
expr:
	expr ('*' | '/') expr		//乘除
	| expr ('+' | '-') expr		//加减
	| '(' expr ')'				//圆括号优先
	| INT						
	| FLOAT					
	;

INT: DIGIT+;					//整数
FLOAT:
	DIGIT+ '.' DIGIT*			//浮点数
	| '.' DIGIT+;
DIGIT: [0-9];					//单个阿拉伯数字
NR : ('\n'|'\r') -> skip;		//skip 跳过 换行
SPEED : ' ' -> skip;			//跳过空格
````

- 生成 词法语法分析器代码 (直接生成项目)。
- 用 visit模式，编写计算器逻辑。
```` CSharp
class calculatorAppVisitor : CalculatorBaseVisitor<double>
    {
        //访问 prog 节点
        public override double VisitProg(CalculatorParser.ProgContext context)
        {
            //prog 不需要处理,往下一层
            return base.Visit(context.GetChild(0));
        }

        //访问 expr 节点
        public override double VisitExpr(CalculatorParser.ExprContext context)
        {

            if (context.ChildCount == 3)
            {
                if (context.GetChild(0).GetText() == "(")
                {
                    //圆括号 往下一层
                    return base.Visit(context.GetChild(1));
                }

                var op = context.GetChild(1).ToString();
                var left = base.Visit(context.GetChild(0));
                var right = base.Visit(context.GetChild(2));
                if (op == "+")
                    return left + right;
                else if (op == "-")
                    return left - right;
                else if (op == "*")
                    return left * right;
                else if (op == "/")
                    return left / right;
            } else if (context.ChildCount == 1) {
                //往下一层(INT or FLOAT)
                return base.Visit(context.GetChild(0));
            }

            throw new NotSupportedException();
        }

        //访问 末尾 节点 （INT 、 FLOAT）
        public override double VisitTerminal(ITerminalNode node)
        {
            var lexer = node.Symbol.TokenSource as Lexer;
            var type = lexer.Vocabulary.GetSymbolicName(node.Symbol.Type);
            var text = node.GetText();
            if (type == "INT")
                return int.Parse(text);
            else if (type == "FLOAT")
                return double.Parse(text);
            throw new Exception("unkown value:" + type + "=" + text);
        }

        public override double VisitErrorNode(IErrorNode node)
        {
            return base.VisitErrorNode(node);
        }
    }

````

- 算式文本获取计算结果。
```` CSharp
/// <summary>
/// 算式文本 计算求值
/// </summary>
/// <param name="text">算式文本</param>
/// <returns>计算值</returns>
public static string calcValue(string text)
{
    string reuslt = "";
    var calcu = new calculatorAppVisitor();
    var l = GetAntlrLexer(text, typeof(CalculatorLexer));
    var p = GetAntlrParser(l, typeof(CalculatorParser)) as CalculatorParser;
    var ctx = p.expr();

    reuslt = calcu.Visit(ctx).ToString();
    return reuslt;
}
````

### 资料
书籍：《antlr4权威指南》
[(官方java)初步使用演示](https://github.com/antlr/antlr4/blob/master/doc/getting-started.md)
[各语言 antrl4 实现仓库](https://github.com/antlr/grammars-v4)
