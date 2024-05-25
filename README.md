# HH Global task

## Problem Statement
At HH Global a "job" is a group of print items.  For example, a job can be a run of business cards, envelopes, and letterhead together.

Some items qualify as being sales tax free, whereas, by default, others are not.  Sales tax is 7%.

HH Global also applies a margin, which is the percentage above printing cost that is charged to the customer.  
For example, an item that costs $100 to print that has a margin of 11% will cost:
item: $100 -> $7 sales tax = $107
job:  $100 -> $11 margin
total: $100 + $7 + $11 = $118

The base margin is 11% for all jobs this problem.  Some jobs have an "extra margin" of 5%.  These jobs that are flagged as extra margin have an additional 5% margin (16% total) applied.

The final cost is rounded to the nearest even cent.  
Individual items are rounded to the nearest cent.

Develop a web application (web service) that exposes API to accept jobs in JSON format, calculates the total charge to a customer for a job and returns result in JSON.
(Bonus: try to pack application it to a docker container to be deployed into Kubernetes cluster with a Helm 3 Chart. Note that cluster nodes are running under Ubuntu 18.04)

## Deployment



### Prerequisites

- [Docker](https://www.docker.com/products/docker-desktop)
- [Kubernetes](https://kubernetes.io/docs/tasks/tools/install-kubectl/) (comes with Docker Desktop)
- [Helm](https://helm.sh/docs/intro/install/)
- PowerShell

### Files and Directories

- `utils/`
  - `build_docker_image.ps1`: Script to build the Docker image.
  - `deploy.ps1`: Script to deploy the application to Kubernetes using Helm.
  - `stop.ps1`: Script to stop and remove the deployment.
  - `port-forward.ps1`: Script to forward a local port to the Kubernetes service for local access.

### Step-by-Step Instructions

#### 1. Build the Docker Image

```poweshell
cd utils
./build_docker_image.ps1
```

#### 2. Deploy the app
```poweshell
cd utils
./deploy.ps1
```
#### 3. Access the app locally on port 5000

```poweshell
./port-forward.ps1
```

#### 3. Stop the deployment

```poweshell
./stop.ps1
```


## Testing

#### Unit and component tests are available at `JobService.Tests` project.

#### Example Request 

#### Url
POST `http://localhost:5000/api/job/total`

```json
{
    "extraMargin": true,
    "items": [
        {
            "name": "envelopes",
            "cost": 520.00,
            "exempt": false
        },
        {
            "name": "letterhead",
            "cost": 1983.37,
            "exempt": true
        }
    ]
}