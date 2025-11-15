public abstract class Stmt
{
public abstract R Accept<R>(Visitor<R> visitor);
public interface Visitor<R>
{
public R VisitExpressionStmt(Expression stmt);
public R VisitVarStmt(Var stmt);
public R VisitBlockStmt(Block stmt);
public R VisitIfStmt(If stmt);
public R VisitWhileStmt(While stmt);
public R VisitFunctionStmt(Function stmt);
public R VisitReturnStmt(Return stmt);
}
public class Expression: Stmt
{
public readonly Expr expression;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitExpressionStmt(this);
}
public Expression(Expr expression): base()
{
this.expression = expression;
}
}
public class Var: Stmt
{
public readonly Token name;
public readonly Expr initialiser;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitVarStmt(this);
}
public Var(Token name, Expr initialiser): base()
{
this.name = name;
this.initialiser = initialiser;
}
}
public class Block: Stmt
{
public readonly List<Stmt> statements;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitBlockStmt(this);
}
public Block(List<Stmt> statements): base()
{
this.statements = statements;
}
}
public class If: Stmt
{
public readonly Expr Condition;
public readonly Stmt ThenBranch;
public readonly Stmt? ElseBranch;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitIfStmt(this);
}
public If(Expr Condition, Stmt ThenBranch, Stmt? ElseBranch): base()
{
this.Condition = Condition;
this.ThenBranch = ThenBranch;
this.ElseBranch = ElseBranch;
}
}
public class While: Stmt
{
public readonly Expr Condition;
public readonly Stmt Body;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitWhileStmt(this);
}
public While(Expr Condition, Stmt Body): base()
{
this.Condition = Condition;
this.Body = Body;
}
}
public class Function: Stmt
{
public readonly Token Name;
public readonly List<Token> Params;
public readonly List<Stmt> Body;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitFunctionStmt(this);
}
public Function(Token Name, List<Token> Params, List<Stmt> Body): base()
{
this.Name = Name;
this.Params = Params;
this.Body = Body;
}
}
public class Return: Stmt
{
public readonly Token Keyword;
public readonly Expr expression;
public override R Accept<R>(Visitor<R> visitor)
{
return visitor.VisitReturnStmt(this);
}
public Return(Token Keyword, Expr expression): base()
{
this.Keyword = Keyword;
this.expression = expression;
}
}
}
