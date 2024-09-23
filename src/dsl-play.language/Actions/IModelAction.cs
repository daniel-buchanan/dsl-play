using System.Threading;
using System.Threading.Tasks;
using dsl_play.language.Descriptors;
using dsl_play.models;

namespace dsl_play.language.Actions
{
    public interface IModelAction
    {
        string Name { get; }

        void Execute(params object[] args);

        Task ExecuteAsync(object[] args, CancellationToken cancellationToken = default);
    }

    public interface IModelAction<TModel> : IModelAction
        where TModel : IDataModel
    {
        
    }

    public interface IPropertyAction : IModelAction
    {
        IDescriptor Property { get; }
    }

    public interface IPropertyAction<TModel> : IPropertyAction
        where TModel : IDataModel
    {
        new IPropertyDescriptor<TModel> Property { get; }
    }

    public interface IPropertyAction<TModel, TProperty> : IPropertyAction<TModel>
        where TModel : IDataModel
    {
        new IPropertyDescriptor<TModel, TProperty> Property { get; }
    }
}