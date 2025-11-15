public abstract class Expr
{
public abstract R Accept<R>(Visitor<R> visitor);
public interface Visitor<R>
{
public R VisitBinaryExpr(Binary expr);
public R VisitGroupingExpr(Grouping expr);
public R VisitLiteralExpr(Literal expr);
public R VisitUnaryExpr(Unary expr);
public R VisitVariableExpr(Variable expr);
public R VisitAssignExpr(Assign expr);
public R VisitLogicalExpr(Logical expr);
public R VisitCallExpr(Call expr);
}
public class Binary: Expr
{
public readonly Expr left;
public readonly Token op;
public readonly Expr right;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitBinaryExpr(this);
}
public Binary(Expr left, Token op, Expr right): base()
{
this.left = left;
this.op = op;
this.right = right;
}
}
public class Grouping: Expr
{
public readonly Expr expression;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitGroupingExpr(this);
}
public Grouping(Expr expression): base()
{
this.expression = expression;
}
}
public class Literal: Expr
{
public readonly Object value;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitLiteralExpr(this);
}
public Literal(Object value): base()
{
this.value = value;
}
}
public class Unary: Expr
{
public readonly Token op;
public readonly Expr right;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitUnaryExpr(this);
}
public Unary(Token op, Expr right): base()
{
this.op = op;
this.right = right;
}
}
public class Variable: Expr
{
public readonly Token name;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitVariableExpr(this);
}
public Variable(Token name): base()
{
this.name = name;
}
}
public class Assign: Expr
{
public readonly Token name;
public readonly Expr value;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitAssignExpr(this);
}
public Assign(Token name, Expr value): base()
{
this.name = name;
this.value = value;
}
}
public class Logical: Expr
{
public readonly Expr Left;
public readonly Token Op;
public readonly Expr Right;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitLogicalExpr(this);
}
public Logical(Expr Left, Token Op, Expr Right): base()
{
this.Left = Left;
this.Op = Op;
this.Right = Right;
}
}
public class Call: Expr
{
public readonly Expr Callee;
public readonly Token Paren;
public readonly List<Expr> Arguments;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitCallExpr(this);
}
public Call(Expr Callee, Token Paren, List<Expr> Arguments): base()
{
this.Callee = Callee;
this.Paren = Paren;
this.Arguments = Arguments;
}
}
}
