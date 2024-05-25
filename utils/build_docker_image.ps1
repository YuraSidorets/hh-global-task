$projectName = "JobService.WebAPI"
$imageName = "job-service"

Set-Location -Path ../$projectName

dotnet restore

dotnet build --configuration Release --no-restore

dotnet publish --configuration Release --output ./bin/Release/net8.0/publish --no-build

Set-Location -Path ../

docker build -t $imageName -f ./Dockerfile .