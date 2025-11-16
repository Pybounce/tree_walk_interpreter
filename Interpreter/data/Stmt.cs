public abstract class Stmt
{
    public abstract R Accept<R>(Visitor<R> visitor);

    public interface Visitor<R>
    {
        R VisitExpressionStmt(Expression stmt);
        R VisitVarStmt(Var stmt);
        R VisitBlockStmt(Block stmt);
        R VisitIfStmt(If stmt);
        R VisitWhileStmt(While stmt);
        R VisitFunctionStmt(Function stmt);
        R VisitReturnStmt(Return stmt);
    }

    public class Expression : Stmt
    {
        public readonly Expr expression;

        public Expression(Expr expression) : base()
        {
            this.expression = expression;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitExpressionStmt(this);
        }
    }

    public class Var : Stmt
    {
        public readonly Token name;
        public readonly Expr initialiser;

        public Var(Token name, Expr initialiser) : base()
        {
            this.name = name;
            this.initialiser = initialiser;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitVarStmt(this);
        }
    }

    public class Block : Stmt
    {
        public readonly List<Stmt> statements;

        public Block(List<Stmt> statements) : base()
        {
            this.statements = statements;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitBlockStmt(this);
        }
    }

    public class If : Stmt
    {
        public readonly Expr Condition;
        public readonly Stmt ThenBranch;
        public readonly Stmt? ElseBranch;

        public If(Expr Condition, Stmt ThenBranch, Stmt? ElseBranch) : base()
        {
            this.Condition = Condition;
            this.ThenBranch = ThenBranch;
            this.ElseBranch = ElseBranch;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitIfStmt(this);
        }
    }

    public class While : Stmt
    {
        public readonly Expr Condition;
        public readonly Stmt Body;

        public While(Expr Condition, Stmt Body) : base()
        {
            this.Condition = Condition;
            this.Body = Body;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitWhileStmt(this);
        }
    }

    public class Function : Stmt
    {
        public readonly Token Name;
        public readonly List<Token> Params;
        public readonly List<Stmt> Body;

        public Function(Token Name, List<Token> Params, List<Stmt> Body) : base()
        {
            this.Name = Name;
            this.Params = Params;
            this.Body = Body;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitFunctionStmt(this);
        }
    }

    public class Return : Stmt
    {
        public readonly Token Keyword;
        public readonly Expr expression;

        public Return(Token Keyword, Expr expression) : base()
        {
            this.Keyword = Keyword;
            this.expression = expression;
        }

        public override R Accept<R>(Visitor<R> visitor)
        {
            return visitor.VisitReturnStmt(this);
        }
    }
}
