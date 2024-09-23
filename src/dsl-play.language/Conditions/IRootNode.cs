using System.Collections.Generic;

namespace dsl_play.language.Conditions;

public interface IRootNode
{
    IEnumerable<ITreeNode> Children { get; }
    IRootNode AddChild(ITreeNode child);
    IRootNode AddChildren(params ITreeNode[] children);
    bool AllMet(object model);
}