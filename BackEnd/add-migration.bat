pushd Wuphf.Api
dotnet ef migrations add %1 --context AppDbContext --project ../Wuphf.Data
popd
