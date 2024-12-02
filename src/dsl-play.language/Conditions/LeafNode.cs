using dsl_play.language.Json.Converters;
using Newtonsoft.Json;

namespace dsl_play.language.Conditions;

[JsonConverter(typeof(TreeNodeConverter))]
public class LeafNode(Condition condition) : 
    TreeNode<LeafNode>(TreeNodeType.LeafNode), ILeafNode
{
    public static TreeNode With(Condition condition)
        => new LeafNode(condition);
    
    public Condition Condition { get; } = condition;

    public override bool AllMet(object model) 
        => Condition.IsMet(model);
}