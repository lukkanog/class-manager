using NetArchTest.Rules;
using System.Reflection;

namespace Fiap.TesteTecnico.ClassManager.Infra.Tests;

[Trait("Architecture", "Infra")]
public class ArchitectureTest
{
    [Fact]
    public void Infra_ShouldNot_DependOnOtherLayers()
    {

        string[] otherLayersNamespaces = [
            "Fiap.TesteTecnico.ClassManager.Api", 
            "Fiap.TesteTecnico.ClassManager.Service", 
        ];

        var domainAssembly = Assembly.Load("Fiap.TesteTecnico.ClassManager.Infra");

        var result = Types
          .InAssembly(domainAssembly)
          .ShouldNot()
          .HaveDependencyOnAny(otherLayersNamespaces)
          .GetResult();

        Assert.True(result.IsSuccessful);
    }
}