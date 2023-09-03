$dataSource = "localhost";
$port = "5432";
$initialCatalog = "db_user";
$userId = "postgres";
$password = "postgres";
$provider = "Npgsql.EntityFrameworkCore.PostgreSQL";
$entityFolderPath = "Entities";

$connectionString = "Host=$($dataSource); Port=$($port); Database=$($initialCatalog); Username=$($userId); Password=$($password);";
$dbContextName = "DataContext";

If (!(Test-Path $entityFolderPath))
{
    New-Item -ItemType Directory -Force -Path $entityFolderPath;
}

Set-Location $entityFolderPath;
Remove-Item *.cs;
Set-Location ..

$command = "dotnet ef dbcontext scaffold ""$ConnectionString"" $provider -d -f -c $dbContextName -v -o $entityFolderPath --no-onconfiguring"


Invoke-Expression "$command";