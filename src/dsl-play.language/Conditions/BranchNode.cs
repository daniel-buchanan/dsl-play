using System.Linq;
using dsl_play.language.Json.Converters;
using Newtonsoft.Json;

namespace dsl_play.language.Conditions;

[JsonConverter(typeof(TreeNodeConverter))]
public class BranchNode(BranchOperator op) : 
    TreeNode<BranchNode>(TreeNodeType.BranchNode), IBranchNode
{
    public static BranchNode With(BranchOperator op)
        => new (op);
    
    public BranchOperator Operator { get; } = op;

    public override bool AllMet(object model)
    {
        return Operator == BranchOperator.And 
            ? AllMetAnd(model) 
            : AllMetOr(model);
    }

    private bool AllMetOr(object model) 
        => NodeChildren.Aggregate(false, (current, c) => current || c.AllMet(model));

    private bool AllMetAnd(object model) 
        => NodeChildren.Aggregate(true, (current, c) => current && c.AllMet(model));
}