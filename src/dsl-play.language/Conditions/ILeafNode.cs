namespace dsl_play.language.Conditions;

public interface ILeafNode : ITreeNode
{
    ICondition Condition { get; }
}