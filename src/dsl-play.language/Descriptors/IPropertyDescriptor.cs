using System;
using System.Linq.Expressions;
using dsl_play.models;

namespace dsl_play.language.Descriptors
{
    public interface IPropertyDescriptor<TModel, TValue> : IPropertyDescriptor<TModel>
        where TModel : IDataModel
    {
        new Expression<Func<TModel, TValue>> Of { get; }
    }

    public interface IPropertyDescriptor<TModel> : IDescriptor
        where TModel : IDataModel
    {
        string Name { get; }

        Type Type { get; }

        Expression<Func<TModel, object>> Of { get; }
    }
}