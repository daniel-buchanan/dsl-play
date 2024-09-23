namespace dsl_play.language.Conditions;

public class LeafNode(ICondition condition) : TreeNode(), ILeafNode
{
    public static ITreeNode With(ICondition condition)
        => new LeafNode(condition);
    
    public ICondition Condition { get; } = condition;

    public override bool AllMet(object model) 
        => Condition.IsMet(model);
}