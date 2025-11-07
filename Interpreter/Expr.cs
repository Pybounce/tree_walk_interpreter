
public abstract class Expr
{
    public abstract R Accept<R>(Visitor<R> visitor);
    public interface Visitor<R>
    {
        public R VisitBinaryExpr(Binary expr);
        public R VisitGroupingExpr(Grouping expr);
        public R VisitLiteralExpr(Literal expr);
        public R VisitUnaryExpr(Unary expr);
    }
    public class Binary : Expr
    {
        public readonly Expr left;
        public readonly Token op;
        public readonly Expr right;
        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
        public Binary(Expr left, Token op, Expr right) : base()
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }
    }
    public class Grouping : Expr
    {
        public readonly Expr expression;
        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }
        public Grouping(Expr expression) : base()
        {
            this.expression = expression;
        }
    }
    public class Literal : Expr
    {
        public readonly Object value;
        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }
        public Literal(Object value) : base()
        {
            this.value = value;
        }
    }
    public class Unary : Expr
    {
        public readonly Token op;
        public readonly Expr right;
        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }
        public Unary(Token op, Expr right) : base()
        {
            this.op = op;
            this.right = right;
        }
    }
}
