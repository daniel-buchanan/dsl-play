namespace dsl_play.language.Conditions;

public interface IBranchNode : ITreeNode
{
    BranchOperator Operator { get; }
}