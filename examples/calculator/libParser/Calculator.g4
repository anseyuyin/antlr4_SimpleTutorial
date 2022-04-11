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