call dotnet tool install -g NSwag.ConsoleCore
call nswag openapi2csclient /namespace:Wuphf.Api.Client /input:https://localhost:5001/odata/$openapi /output:Wuphf/Api/Wuphf.Client.cs /classname:WuphfApi /GenerateClientInterfaces:true