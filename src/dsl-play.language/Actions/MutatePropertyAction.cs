using System;
using System.Threading.Tasks;
using dsl_play.language.Descriptors;
using dsl_play.models;

namespace dsl_play.language.Actions;

public abstract class MutatePropertyAction<TModel>(
    string name, 
    IPropertyDescriptor<TModel> descriptor) : 
    ModelAction<TModel>(name) 
    where TModel : class, IDataModel
{
    public IPropertyDescriptor<TModel> Property { get; } = descriptor;

    protected override Func<object, Task> Action => async (m) => await Mutator(Property, m as TModel);
    
    protected abstract Func<IPropertyDescriptor<TModel>, TModel, Task> Mutator { get; }
}