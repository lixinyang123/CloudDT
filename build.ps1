echo "======================"
echo "| Build Docker Image |"
echo "======================"

docker build -t lixinyang/cloudshell:clouddt ./CloudDT.ContainerAPI

echo "========================"
echo "| Build Desktop Client |"
echo "========================"

dotnet publish .\CloudDT.Shared\CloudDT.Shared.csproj -o ./CloudDT.Client/publish