using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Wuphf.Data.Models;

namespace Wuphf.Api;

public class EdmModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<Server>("Servers");
        return builder.GetEdmModel();
    }
}