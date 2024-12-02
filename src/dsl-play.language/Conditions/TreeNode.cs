using System.Collections.Generic;
using System.Linq;
using dsl_play.language.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace dsl_play.language.Conditions;

public enum TreeNodeType
{
    RootNode,
    BranchNode,
    LeafNode
}

public class TreeNode : ITreeNode
{
    protected readonly List<TreeNode> NodeChildren = [];

    protected TreeNode(TreeNodeType nodeType)
        => NodeType = nodeType;
    
    [JsonConverter(typeof(StringEnumConverter))]
    public TreeNodeType NodeType { get; }
    
    public IEnumerable<TreeNode> Children => NodeChildren.AsEnumerable();
    
    public virtual bool AllMet(object model)
    {
        var met = true;
        foreach (var c in NodeChildren)
            met &= c.AllMet(model);

        return met;
    }
}

[JsonConverter(typeof(TreeNodeConverter))]
public abstract class TreeNode<TNodeType>(TreeNodeType nodeType) : TreeNode(nodeType), ITreeNode<TNodeType>
    where TNodeType : class, ITreeNode
{
    public TNodeType AddChild(TreeNode child)
    {
        NodeChildren.Add(child);
        return this as TNodeType;
    }

    public TNodeType AddChildren(params TreeNode[] children)
    {
        foreach(var c in children) AddChild(c);
        return this as TNodeType;
    }
}