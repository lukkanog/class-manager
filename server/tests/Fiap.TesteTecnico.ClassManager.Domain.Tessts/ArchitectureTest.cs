using NetArchTest.Rules;
using System.Reflection;

namespace Fiap.TesteTecnico.ClassManager.Domain.Tests;

[Trait("Architecture", "Domain")]
public class ArchitectureTest
{
    [Fact]
    public void Domain_ShouldNot_DependOnOtherLayers()
    {

        string[] otherLayersNamespaces = [
            "Fiap.TesteTecnico.ClassManager.Api", 
            "Fiap.TesteTecnico.ClassManager.Infra", 
            "Fiap.TesteTecnico.ClassManager.Service"
        ];

        var domainAssembly = Assembly.Load("Fiap.TesteTecnico.ClassManager.Domain");

        var result = Types
          .InAssembly(domainAssembly)
          .ShouldNot()
          .HaveDependencyOnAny(otherLayersNamespaces)
          .GetResult();

        Assert.True(result.IsSuccessful);
    }
}