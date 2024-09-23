using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.language.Actions;
using dsl_play.language.Conditions;
using dsl_play.models;

namespace dsl_play.language
{
    public interface IStatement
    {
        IRootNode Conditions { get; }
        IEnumerable<IModelAction> Actions { get; }
        
        object Apply(object model);
        Task<object> ApplyAsync(object model, CancellationToken cancellationToken = default);
    }

    public interface IStatement<TModel> : IStatement 
        where TModel : class, IDataModel
    {
        new IEnumerable<IModelAction<TModel>> Actions { get; }
        TModel Apply(TModel model);
        Task<TModel> ApplyAsync(TModel model, CancellationToken cancellationToken = default);
    }
}