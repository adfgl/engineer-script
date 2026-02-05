using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptLib.Parsing
{
    public interface INodeVisitor<T>
    {
        T Visit(ExprError node);
        T Visit(ExprLiteral node);
        T Visit(ExprBinary node);
        T Visit(ExprGroup node);
        T Visit(ExprIdentifier node);

        T Visit(StmtProg node);
        T Visit(StmtBlock node);
        T Visit(StmtExpr node);
    }
}
