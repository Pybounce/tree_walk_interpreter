


public class Interpreter : Expr.Visitor<Object>
{

    public void Interpret(Expr expression)
    {
        try
        {
            var value = Evaluate(expression);
            Console.WriteLine(Stringify(value));
        }
        catch (RuntimeError error)
        {
            Lox.RuntimeError(error);
        }
    }

    private string Stringify(object obj)
    {
        if (obj == null) { return "nil"; }
        if (obj is double)
        {
            var text = obj.ToString();
            if (text.EndsWith(".0")) { text = text.Substring(0, text.Length - 2); }
            return text;
        }
        return obj.ToString();
    }

    public object VisitBinaryExpr(Expr.Binary expr)
    {
        var left = Evaluate(expr.left);
        var right = Evaluate(expr.right);

        switch (expr.op.TokenType)
        {
            case TokenType.MINUS:
                CheckNumberOperands(expr.op, left, right);
                return (double)left - (double)right;
            case TokenType.STAR:
                CheckNumberOperands(expr.op, left, right);
                return (double)left * (double)right;
            case TokenType.SLASH:
                CheckNumberOperands(expr.op, left, right);
                return (double)left / (double)right;
            case TokenType.PLUS:
                if (left is double && right is double) { return (double)left + (double)right; }
                if (left is string && right is string) { return (string)left + (string)right; }
                throw new RuntimeError(expr.op, "Operands must both be numbers or strings.");
            case TokenType.GREATER:
                CheckNumberOperands(expr.op, left, right);
                return (double)left > (double)right;
            case TokenType.GREATER_EQUAL:
                CheckNumberOperands(expr.op, left, right);
                return (double)left >= (double)right;
            case TokenType.LESS:
                CheckNumberOperands(expr.op, left, right);
                return (double)left < (double)right;
            case TokenType.LESS_EQUAL:
                CheckNumberOperands(expr.op, left, right);
                return (double)left <= (double)right;
            case TokenType.EQUAL: return IsEqual(left, right);
            case TokenType.BANG_EQUAL: return !IsEqual(left, right);
        }

        return null;
    }

    public object VisitGroupingExpr(Expr.Grouping expr)
    {
        return Evaluate(expr.expression);
    }

    public object VisitLiteralExpr(Expr.Literal expr)
    {
        return expr.value;
    }

    public object VisitUnaryExpr(Expr.Unary expr)
    {
        var right = Evaluate(expr.right);
        switch (expr.op.TokenType)
        {
            case TokenType.MINUS:
                CheckNumberOperand(expr.op, right);
                return -(double)right;
            case TokenType.BANG:
                return !IsTruthy(right);
        }
        return null;
    }

    private object Evaluate(Expr expr)
    {
        return expr.Accept(this);
    }

    private bool IsTruthy(Object obj)
    {
        if (obj == null) { return false; }
        if (obj is bool) { return (bool)obj; }
        return true;
    }

    private bool IsEqual(object a, object b)
    {
        if (a == null && b == null) { return true; }
        if (a == null) { return false; }
        return a.Equals(b);
    }

    private void CheckNumberOperand(Token op, object operand)
    {
        if (operand is double) { return; }
        throw new RuntimeError(op, "Operand must be a number.");
    }
    private void CheckNumberOperands(Token op, object leftOperand, object rightOperand)
    {
        if (leftOperand is double && rightOperand is double) { return; }
        throw new RuntimeError(op, "Operands must both be numbers.");
    }

}