using System;
using System.Linq.Expressions;
using dsl_play.models;

namespace dsl_play.language.Descriptors;

public interface IPropertyDescriptor : IDescriptor
{
    string Name { get; }
}
    
public interface IPropertyDescriptor<TModel> : IPropertyDescriptor
    where TModel : IDataModel
{
    Type Type { get; }

    Expression<Func<TModel, object>> Of { get; }
}
    
public interface IPropertyDescriptor<TModel, TValue> : IPropertyDescriptor<TModel>
    where TModel : IDataModel
{
    new Expression<Func<TModel, TValue>> Of { get; }
}