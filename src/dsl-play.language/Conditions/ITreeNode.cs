using System.Collections.Generic;

namespace dsl_play.language.Conditions;

public interface ITreeNode
{
    IEnumerable<TreeNode> Children { get; }
    bool AllMet(object model);
}

public interface ITreeNode<out TNodeType> : ITreeNode
    where TNodeType : ITreeNode
{
    TNodeType AddChild(TreeNode child);
    TNodeType AddChildren(params TreeNode[] children);
}