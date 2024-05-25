param (
    [string]$namespace = "jobservice",
    [string]$helmChartPath = "../helm/jobservice"
)

kubectl get namespace $namespace 2>$null || kubectl create namespace $namespace

helm install jobservice $helmChartPath --namespace $namespace