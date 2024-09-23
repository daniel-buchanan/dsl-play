using System.Collections.Generic;

namespace dsl_play.language.Conditions;

public abstract class TreeNode(ITreeNode parent = null) : ITreeNode
{
    protected readonly List<ITreeNode> NodeChildren = new();

    public ITreeNode Parent { get; private set; } = parent;

    public IEnumerable<ITreeNode> Children => NodeChildren;

    public ITreeNode AddChild(ITreeNode child)
    {
        child.SetParent(this);
        NodeChildren.Add(child);
        return this;
    }

    public ITreeNode AddChildren(params ITreeNode[] children)
    {
        foreach(var c in children) AddChild(c);
        return this;
    }

    public virtual bool AllMet(object model)
    {
        var met = true;
        foreach (var c in NodeChildren)
            met &= c.AllMet(model);

        return met;
    }

    public void SetParent(ITreeNode parent)
        => Parent = parent;
}