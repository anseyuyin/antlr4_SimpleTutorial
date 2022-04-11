using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using libParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorApp
{
    public class tool
    {
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

        /// <summary>
        /// 获取 语法分析器
        /// </summary>
        /// <param name="lex">词法分析器实例</param>
        /// <param name="parserType">语法分析器 type</param>
        /// <returns>语法分析器实例</returns>
        public static Parser GetAntlrParser(Lexer lex, Type parserType)
        {
            var tokenstream = new Antlr4.Runtime.CommonTokenStream(lex);
            var parser = Activator.CreateInstance(parserType, tokenstream) as Parser;
            return parser;
        }
        /// <summary>
        /// 获取 词法分析器
        /// </summary>
        /// <param name="text">输入的文本</param>
        /// <param name="lexerType">词法分析器 type</param>
        /// <returns>词法分析器实例</returns>
        public static Lexer GetAntlrLexer(string text, Type lexerType)
        {
            Antlr4.Runtime.AntlrInputStream input = new Antlr4.Runtime.AntlrInputStream(text);
            var lex = Activator.CreateInstance(lexerType, input) as Lexer;
            return lex;
        }
    }

    /// <summary>
    /// 访问模式 ICalculatorVisitor 接口的实现类
    /// </summary>
    /// <typeparam name="result">返回类型定义</typeparam>
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
}
