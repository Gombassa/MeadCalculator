# Google Cloud Run Deployment Script for MeadCalculator (PowerShell)
# Usage: .\Deploy-GCP.ps1 -ProjectId "meadcalculator" -Region "us-central1"

param(
    [Parameter(Mandatory=$true)]
    [string]$ProjectId,

    [Parameter(Mandatory=$false)]
    [string]$Region = "us-central1",

    [Parameter(Mandatory=$false)]
    [string]$Repository = "meadcalc-repo"
)

# Configuration
$API_SERVICE = "meadcalc-api"
$FRONTEND_SERVICE = "meadcalc-frontend"
$DOCKER_REGISTRY = "$Region-docker.pkg.dev"

# Color output functions
function Write-Status {
    param([string]$Message)
    Write-Host "`n===> $Message" -ForegroundColor Green
}

function Write-Error-Custom {
    param([string]$Message)
    Write-Host "ERROR: $Message" -ForegroundColor Red
    exit 1
}

# Header
Write-Host "`nMeadCalculator - Google Cloud Run Deployment`n" -ForegroundColor Green
Write-Host "========================================="
Write-Host "Project ID:   $ProjectId"
Write-Host "Region:       $Region"
Write-Host "Repository:   $Repository"
Write-Host "=========================================`n"

# Step 1: Check prerequisites
Write-Status "Checking prerequisites..."
$gcloud = Get-Command gcloud -ErrorAction SilentlyContinue
$docker = Get-Command docker -ErrorAction SilentlyContinue

if (-not $gcloud) {
    Write-Error-Custom "gcloud CLI not found. Please install Google Cloud SDK."
}

if (-not $docker) {
    Write-Error-Custom "Docker not found. Please install Docker Desktop."
}

Write-Host "✓ gcloud CLI found" -ForegroundColor Green
Write-Host "✓ Docker found" -ForegroundColor Green

# Step 2: Authenticate
Write-Status "Authenticating with Google Cloud..."
gcloud auth login
gcloud config set project $ProjectId

# Step 3: Create Artifact Registry repository
Write-Status "Setting up Artifact Registry repository..."
$repoExists = gcloud artifacts repositories describe $Repository --location=$Region --project=$ProjectId 2>$null
if ($repoExists) {
    Write-Host "✓ Repository already exists" -ForegroundColor Green
} else {
    gcloud artifacts repositories create $Repository `
        --repository-format=docker `
        --location=$Region `
        --description="MeadCalculator Docker images" `
        --project=$ProjectId
    if ($LASTEXITCODE -ne 0) {
        Write-Error-Custom "Failed to create Artifact Registry repository"
    }
    Write-Host "✓ Repository created" -ForegroundColor Green
}

# Step 4: Configure Docker authentication
Write-Status "Configuring Docker authentication..."
gcloud auth configure-docker "$Region-docker.pkg.dev" --quiet

if ($LASTEXITCODE -ne 0) {
    Write-Error-Custom "Failed to configure Docker authentication"
}

# Step 5: Build and push backend
Write-Status "Building backend Docker image..."
$backendImage = "$DOCKER_REGISTRY/$ProjectId/$Repository/$API_SERVICE`:latest"
docker build `
    -t $backendImage `
    -f MeadCalculator.API/Dockerfile `
    .

if ($LASTEXITCODE -ne 0) {
    Write-Error-Custom "Failed to build backend image"
}

Write-Status "Pushing backend image to Artifact Registry..."
docker push $backendImage

if ($LASTEXITCODE -ne 0) {
    Write-Error-Custom "Failed to push backend image"
}

# Step 6: Build and push frontend
Write-Status "Building frontend Docker image..."
$frontendImage = "$DOCKER_REGISTRY/$ProjectId/$Repository/$FRONTEND_SERVICE`:latest"
docker build `
    -t $frontendImage `
    -f frontend/Dockerfile `
    frontend/

if ($LASTEXITCODE -ne 0) {
    Write-Error-Custom "Failed to build frontend image"
}

Write-Status "Pushing frontend image to Artifact Registry..."
docker push $frontendImage

if ($LASTEXITCODE -ne 0) {
    Write-Error-Custom "Failed to push frontend image"
}

# Step 7: Deploy backend
Write-Status "Deploying backend to Cloud Run..."
gcloud run deploy $API_SERVICE `
    --image $backendImage `
    --region $Region `
    --platform managed `
    --allow-unauthenticated `
    --memory 512Mi `
    --cpu 1 `
    --timeout 3600 `
    --set-env-vars "ASPNETCORE_ENVIRONMENT=Production" `
    --project=$ProjectId

if ($LASTEXITCODE -ne 0) {
    Write-Error-Custom "Failed to deploy backend"
}

# Get backend URL
Write-Host "Retrieving backend service URL..." -ForegroundColor Gray
$apiUrl = gcloud run services describe $API_SERVICE `
    --region $Region `
    --format='value(status.url)' `
    --project=$ProjectId 2>$null

Write-Host "✓ Backend deployed: $apiUrl" -ForegroundColor Green

# Step 8: Deploy frontend
Write-Status "Deploying frontend to Cloud Run..."
gcloud run deploy $FRONTEND_SERVICE `
    --image $frontendImage `
    --region $Region `
    --platform managed `
    --allow-unauthenticated `
    --memory 256Mi `
    --cpu 1 `
    --timeout 3600 `
    --project=$ProjectId

if ($LASTEXITCODE -ne 0) {
    Write-Error-Custom "Failed to deploy frontend"
}

# Get frontend URL
Write-Host "Retrieving frontend service URL..." -ForegroundColor Gray
$frontendUrl = gcloud run services describe $FRONTEND_SERVICE `
    --region $Region `
    --format='value(status.url)' `
    --project=$ProjectId 2>$null

Write-Host "✓ Frontend deployed: $frontendUrl" -ForegroundColor Green

# Step 9: Summary
Write-Host "`n=========================================" -ForegroundColor Green
Write-Host "Deployment Complete!" -ForegroundColor Green
Write-Host "=========================================`n"
Write-Host "Backend API:  $apiUrl" -ForegroundColor Cyan
Write-Host "Frontend:     $frontendUrl" -ForegroundColor Cyan
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Test the backend API:"
Write-Host "   curl $apiUrl/api/ingredients"
Write-Host ""
Write-Host "2. Visit the frontend in your browser:"
Write-Host "   $frontendUrl"
Write-Host ""
Write-Host "3. View logs:"
Write-Host "   gcloud run logs read $API_SERVICE --region $Region --limit 50 --project $ProjectId"
Write-Host "   gcloud run logs read $FRONTEND_SERVICE --region $Region --limit 50 --project $ProjectId"
Write-Host ""
Write-Host "4. Manage services:"
Write-Host "   gcloud run services list --region $Region --project $ProjectId"
Write-Host ""
