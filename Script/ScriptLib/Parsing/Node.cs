using ScriptLib.Data;
using ScriptLib.Scanning;

namespace ScriptLib.Parsing
{
    public abstract class Node
    {
        public abstract T Accept<T>(INodeVisitor<T> visitor);
    }

    public abstract class Expr : Node { }
    public abstract class Stmt : Node { }

    // -------------------- Program / Blocks --------------------

    public sealed class StmtProg(List<Stmt> statements) : Stmt
    {
        public IReadOnlyList<Stmt> Statements { get; } = statements;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class StmtBlock(Token open, List<Stmt> statements, Token close) : Stmt
    {
        public Token Open { get; } = open;
        public IReadOnlyList<Stmt> Statements { get; } = statements;
        public Token Close { get; } = close;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class StmtExpr(Expr expr) : Stmt
    {
        public Expr Expr { get; } = expr;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class StmtAssign(Token name, UnitExpr? unitAnnotation, Token equals, Expr value) : Stmt
    {
        public Token Name { get; } = name;                 // identifier token
        public UnitExpr? UnitAnnotation { get; } = unitAnnotation; // e.g. MPa or kN/mm^2
        public Token Equals { get; } = equals;
        public Expr Value { get; } = value;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class ExprNumber(Token token, double value) : Expr
    {
        public Token Token { get; } = token;
        public double Value { get; } = value;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class ExprName(Token name) : Expr
    {
        public Token Name { get; } = name;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class ExprGroup(Token open, Expr inner, Token close) : Expr
    {
        public Token Open { get; } = open;
        public Expr Inner { get; } = inner;
        public Token Close { get; } = close;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class ExprUnary(Token op, Expr right) : Expr
    {
        public Token Op { get; } = op; // + or -
        public Expr Right { get; } = right;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class ExprBinary(Expr left, Token op, Expr right) : Expr
    {
        public Expr Left { get; } = left;
        public Token Op { get; } = op; 
        public Expr Right { get; } = right;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class ExprAs(Expr value, Token asToken, UnitExpr unit) : Expr
    {
        public Expr Value { get; } = value;
        public Token AsToken { get; } = asToken;
        public UnitExpr Unit { get; } = unit;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class ExprError(Token token, string message) : Expr
    {
        public Token Token { get; } = token;
        public string Message { get; } = message;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }


    public abstract class UnitExpr : Node { }

    public sealed class UnitName(Token name) : UnitExpr
    {
        public Token Name { get; } = name; // identifier token
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class UnitBinary(UnitExpr left, Token op, UnitExpr right) : UnitExpr
    {
        public UnitExpr Left { get; } = left;
        public Token Op { get; } = op; // * / · .
        public UnitExpr Right { get; } = right;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class UnitPow(UnitExpr baseUnit, Token caret, int power, Token powerToken) : UnitExpr
    {
        public UnitExpr Base { get; } = baseUnit;
        public Token Caret { get; } = caret;         // '^'
        public int Power { get; } = power;           // integer exponent
        public Token PowerToken { get; } = powerToken;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }

    public sealed class UnitGroup(Token open, UnitExpr inner, Token close) : UnitExpr
    {
        public Token Open { get; } = open;
        public UnitExpr Inner { get; } = inner;
        public Token Close { get; } = close;
        public override T Accept<T>(INodeVisitor<T> visitor) => visitor.Visit(this);
    }
}
