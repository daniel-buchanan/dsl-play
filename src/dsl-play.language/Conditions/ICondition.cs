using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.language.Descriptors;
using dsl_play.models;

namespace dsl_play.language.Conditions
{
    public interface ICondition
    {
        bool IsMet(object model);
        Task<bool> IsMetAsync(object model, CancellationToken cancellationToken = default);
    }

    public interface ICondition<TModel> : ICondition 
        where TModel : IDataModel
    {
        IPropertyDescriptor<TModel> Property { get; }
        
        Expression<Func<TModel, bool>> Constraint { get; }
        
        bool IsMet(TModel model);
        Task<bool> IsMetAsync(TModel model, CancellationToken cancellationToken = default);
    }
}