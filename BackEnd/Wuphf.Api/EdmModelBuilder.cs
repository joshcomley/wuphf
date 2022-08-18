using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace Wuphf.Api;

public class EdmModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        return builder.GetEdmModel();
    }
}