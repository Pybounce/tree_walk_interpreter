

using System.Text;

public class AstPrinter : Expr.Visitor<string>
{
    public string Print(Expr expr)
    {
        return expr.Accept(this);
        
    }

    public string VisitAssignExpr(Expr.Assign expr)
    {
        throw new NotImplementedException();
    }

    public string VisitBinaryExpr(Expr.Binary expr)
    {
        return Parenthesize(expr.op.Lexeme, expr.left, expr.right);
    }

    public string VisitCallExpr(Expr.Call expr)
    {
        throw new NotImplementedException();
    }

    public string VisitGroupingExpr(Expr.Grouping expr)
    {
        return Parenthesize("group", expr.expression);
    }

    public string VisitLiteralExpr(Expr.Literal expr)
    {
        if (expr.value == null) { return null; }
        return expr.value.ToString();
    }

    public string VisitLogicalExpr(Expr.Logical expr)
    {
        throw new NotImplementedException();
    }

    public string VisitUnaryExpr(Expr.Unary expr)
    {
        return Parenthesize(expr.op.Lexeme, expr.right);
    }

    public string VisitVariableExpr(Expr.Variable expr)
    {
        throw new NotImplementedException();
    }

    private string Parenthesize(string name, params Expr[] exprs)
    {
        var builder = new StringBuilder();
        builder.Append("(").Append(name);
        foreach (var expr in exprs)
        {
            builder.Append(" ");
            builder.Append(expr.Accept(this));
        }
        builder.Append(")");
        return builder.ToString();
    }
}