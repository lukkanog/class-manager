using NetArchTest.Rules;
using System.Reflection;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests;

[Trait("Architecture", "Service")]
public class ArchitectureTest
{
    [Fact]
    public void Service_ShouldNot_DependOnOtherLayers()
    {

        string[] otherLayersNamespaces = [
            "Fiap.TesteTecnico.ClassManager.Api", 
            "Fiap.TesteTecnico.ClassManager.Infra", 
        ];

        var domainAssembly = Assembly.Load("Fiap.TesteTecnico.ClassManager.Service");

        var result = Types
          .InAssembly(domainAssembly)
          .ShouldNot()
          .HaveDependencyOnAny(otherLayersNamespaces)
          .GetResult();

        Assert.True(result.IsSuccessful);
    }
}