namespace dsl_play.language.Conditions;

public interface ILeafNode : ITreeNode
{
    Condition Condition { get; }
}