using dsl_play.language.Json.Converters;
using Newtonsoft.Json;

namespace dsl_play.language.Conditions;

[JsonConverter(typeof(TreeNodeConverter))]
public class RootNode : TreeNode<RootNode>, IRootNode
{
    [JsonConstructor]
    private RootNode() : base(TreeNodeType.RootNode) { }

    private RootNode(LeafNode condition) : 
        this() => AddChild(condition);
    
    public static RootNode Create()
        => new();
    
    public static RootNode Create(LeafNode condition)
        => new(condition);
}