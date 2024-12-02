namespace dsl_play.language.Conditions;

public interface IBranchNode : ITreeNode<BranchNode>
{
    BranchOperator Operator { get; }
}