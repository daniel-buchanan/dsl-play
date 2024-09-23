// See https://aka.ms/new-console-template for more information

using dsl_play.language;
using dsl_play.language.Actions;
using dsl_play.language.Conditions;
using dsl_play.language.Descriptors;
using dsl_play.models;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

var statement = Statement.With(RootNode.Create(BranchOperator.And)
    .AddChildren(
        BranchNode.With(BranchOperator.Or)
        .AddChildren(
            LeafNode.With(
                Condition.Create<TestModel>(
                    PropertyDescriptor.Create<TestModel>(m => m.Age), 
                    m => m.Age < 42)
            ),
            LeafNode.With(
                Condition.Create(
                    PropertyDescriptor.Create<TestModel>(m => m.Name),
                    m => m.Name.StartsWith("Andrew", StringComparison.InvariantCultureIgnoreCase)
                )
            )
        )
    ) as IRootNode, 
    [
        //new SetPropertyAction<TestModel>("stuff", PropertyDescriptor.Create<TestModel>(m => m.Name))
    ]
);

var json = JsonConvert.SerializeObject(statement);
Console.WriteLine(json);