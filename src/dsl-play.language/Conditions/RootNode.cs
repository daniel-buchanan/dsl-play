namespace dsl_play.language.Conditions;

public class RootNode(BranchOperator op) : BranchNode(op), IRootNode
{
    public static ITreeNode Create(BranchOperator op)
        => new RootNode(op);

    public new IRootNode AddChild(ITreeNode child)
    {
        base.AddChild(child);
        return this;
    }

    public new IRootNode AddChildren(params ITreeNode[] children)
    {
        base.AddChildren(children);
        return this;
    }
}