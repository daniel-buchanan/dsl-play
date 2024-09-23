using System.Collections.Generic;

namespace dsl_play.language.Conditions;

public interface ITreeNode
{
    ITreeNode Parent { get; }
    IEnumerable<ITreeNode> Children { get; }
    ITreeNode AddChild(ITreeNode child);
    ITreeNode AddChildren(params ITreeNode[] children);
    bool AllMet(object model);
    void SetParent(ITreeNode parent);
}