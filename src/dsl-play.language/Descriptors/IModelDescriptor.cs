using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using dsl_play.models;

namespace dsl_play.language.Descriptors
{
    public interface IModelDescriptor<TModel> : IDescriptor
        where TModel : IDataModel
    {
        Type Of { get; }
        
        IEnumerable<IPropertyDescriptor<TModel>> Properties { get; }

        void AddProperty(IPropertyDescriptor<TModel> property);
        Task AddPropertyAsync(IPropertyDescriptor<TModel> property, CancellationToken cancellationToken = default);
    }
}