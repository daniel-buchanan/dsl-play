namespace dsl_play.language.Conditions;

public class BranchNode(BranchOperator op) : 
    TreeNode(), IBranchNode
{
    public static ITreeNode With(BranchOperator op)
        => new BranchNode(op);
    
    public BranchOperator Operator { get; } = op;

    public override bool AllMet(object model)
    {
        return Operator == BranchOperator.And 
            ? AllMetAnd(model) 
            : AllMetOr(model);
    }

    private bool AllMetOr(object model)
    {
        var met = false;
        foreach (var c in NodeChildren) met |= c.AllMet(model);
        return met;
    }

    private bool AllMetAnd(object model)
    {
        var met = true;
        foreach (var c in NodeChildren) met &= c.AllMet(model);
        return met;
    }
}