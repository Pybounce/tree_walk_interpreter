public abstract class Expr
{
    public abstract R Accept<R>(Visitor<R> visitor);

    public interface Visitor<R>
    {
        R VisitBinaryExpr(Binary expr);
        R VisitGroupingExpr(Grouping expr);
        R VisitLiteralExpr(Literal expr);
        R VisitUnaryExpr(Unary expr);
        R VisitVariableExpr(Variable expr);
        R VisitAssignExpr(Assign expr);
        R VisitLogicalExpr(Logical expr);
        R VisitCallExpr(Call expr);
    }

    public class Binary : Expr
    {
        public readonly Expr left;
        public readonly Token op;
        public readonly Expr right;

        public Binary(Expr left, Token op, Expr right) : base()
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
    }

    public class Grouping : Expr
    {
        public readonly Expr expression;

        public Grouping(Expr expression) : base()
        {
            this.expression = expression;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }
    }

    public class Literal : Expr
    {
        public readonly Object value;

        public Literal(Object value) : base()
        {
            this.value = value;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }
    }

    public class Unary : Expr
    {
        public readonly Token op;
        public readonly Expr right;

        public Unary(Token op, Expr right) : base()
        {
            this.op = op;
            this.right = right;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }
    }

    public class Variable : Expr
    {
        public readonly Token name;

        public Variable(Token name) : base()
        {
            this.name = name;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitVariableExpr(this);
        }
    }

    public class Assign : Expr
    {
        public readonly Token name;
        public readonly Expr value;

        public Assign(Token name, Expr value) : base()
        {
            this.name = name;
            this.value = value;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitAssignExpr(this);
        }
    }

    public class Logical : Expr
    {
        public readonly Expr Left;
        public readonly Token Op;
        public readonly Expr Right;

        public Logical(Expr Left, Token Op, Expr Right) : base()
        {
            this.Left = Left;
            this.Op = Op;
            this.Right = Right;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitLogicalExpr(this);
        }
    }

    public class Call : Expr
    {
        public readonly Expr Callee;
        public readonly Token Paren;
        public readonly List<Expr> Arguments;

        public Call(Expr Callee, Token Paren, List<Expr> Arguments) : base()
        {
            this.Callee = Callee;
            this.Paren = Paren;
            this.Arguments = Arguments;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitCallExpr(this);
        }
    }
}
