param (
    [string]$namespace = "jobservice"
)

helm uninstall jobservice --namespace $namespace