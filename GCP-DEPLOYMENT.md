# MeadCalculator - Google Cloud Run Deployment Guide

This guide covers deploying MeadCalculator to Google Cloud Platform (GCP) using Cloud Run, Artifact Registry, and Cloud Build.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Quick Start](#quick-start)
3. [Manual Deployment](#manual-deployment)
4. [Automated Deployment with Cloud Build](#automated-deployment-with-cloud-build)
5. [Configuration](#configuration)
6. [Monitoring and Logging](#monitoring-and-logging)
7. [Cost Optimization](#cost-optimization)
8. [Troubleshooting](#troubleshooting)

## Prerequisites

### Required Tools
- Google Cloud SDK (gcloud CLI)
- Docker Desktop
- Git
- PowerShell (Windows) or Bash (Linux/Mac)

### GCP Setup
- Active GCP Project
- Billing enabled
- Required APIs enabled:
  - Cloud Run API
  - Artifact Registry API
  - Cloud Build API

### Enable Required APIs

```bash
gcloud services enable run.googleapis.com
gcloud services enable artifactregistry.googleapis.com
gcloud services enable cloudbuild.googleapis.com
gcloud services enable containerregistry.googleapis.com
```

## Quick Start

### 1. Setup (One-time)

```bash
# Set your GCP project
export PROJECT_ID="your-gcp-project-id"
export REGION="us-central1"  # Change to your preferred region

# Authenticate
gcloud auth login
gcloud config set project $PROJECT_ID
```

### 2. Deploy Using Script (Recommended)

**On macOS/Linux:**
```bash
# Make script executable
chmod +x scripts/deploy-gcp.sh

# Deploy
./scripts/deploy-gcp.sh $PROJECT_ID $REGION
```

**On Windows (PowerShell):**
```powershell
# Execute deployment script
.\scripts\Deploy-GCP.ps1 -ProjectId "your-gcp-project-id" -Region "us-central1"
```

### 3. Access Your Application

After deployment, you'll see URLs like:
```
Backend API:  https://meadcalc-api.a.run.app
Frontend:     https://meadcalc-frontend.a.run.app
```

Test the API:
```bash
curl https://meadcalc-api.a.run.app/api/ingredients
```

## Manual Deployment

If you prefer step-by-step manual deployment:

### Step 1: Create Artifact Registry Repository

```bash
gcloud artifacts repositories create meadcalc-repo \
    --repository-format=docker \
    --location=$REGION \
    --description="MeadCalculator Docker images"
```

### Step 2: Configure Docker Authentication

```bash
gcloud auth configure-docker $REGION-docker.pkg.dev
```

### Step 3: Build Backend Image

```bash
docker build \
    -t $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-api:latest \
    -f MeadCalculator.API/Dockerfile \
    .
```

### Step 4: Push Backend Image

```bash
docker push $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-api:latest
```

### Step 5: Deploy Backend to Cloud Run

```bash
gcloud run deploy meadcalc-api \
    --image $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-api:latest \
    --region $REGION \
    --platform managed \
    --allow-unauthenticated \
    --memory 512Mi \
    --cpu 1 \
    --timeout 3600 \
    --set-env-vars ASPNETCORE_ENVIRONMENT=Production
```

### Step 6: Build and Deploy Frontend

```bash
# Build frontend image
docker build \
    -t $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-frontend:latest \
    -f frontend/Dockerfile \
    frontend/

# Push to registry
docker push $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-frontend:latest

# Deploy to Cloud Run
gcloud run deploy meadcalc-frontend \
    --image $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-frontend:latest \
    --region $REGION \
    --platform managed \
    --allow-unauthenticated \
    --memory 256Mi \
    --cpu 1 \
    --timeout 3600
```

### Step 7: Verify Deployment

```bash
# List deployed services
gcloud run services list --region $REGION

# Get service URLs
gcloud run services describe meadcalc-api --region $REGION --format='value(status.url)'
gcloud run services describe meadcalc-frontend --region $REGION --format='value(status.url)'
```

## Automated Deployment with Cloud Build

Cloud Build automatically builds and deploys when you push to the main branch.

### Setup Cloud Build Trigger

#### Option 1: Using Console
1. Go to Cloud Build > Triggers
2. Click "Create Trigger"
3. Select GitHub/GitLab as source
4. Authorize and select the repository
5. Configure trigger:
   - Name: `MeadCalculator-Deploy`
   - Branch: `^main$`
   - Build configuration: Cloud Build configuration file
   - Cloud Build configuration file: `cloudbuild.yaml`

#### Option 2: Using gcloud CLI

```bash
gcloud builds connect github \
    --repository-name=MeadCalculator \
    --repository-owner=Gombassa

gcloud builds triggers create github \
    --repo-name=MeadCalculator \
    --repo-owner=Gombassa \
    --branch-pattern="^main$" \
    --build-config=cloudbuild.yaml \
    --name=MeadCalculator-Deploy
```

### Customize Cloud Build Configuration

Edit `cloudbuild.yaml` to customize:

```yaml
substitutions:
  _REGION: 'us-central1'           # Your preferred region
  _REPOSITORY: 'meadcalc-repo'     # Your repository name
```

### Monitor Cloud Build

```bash
# View build history
gcloud builds list --limit 10

# Stream build logs
gcloud builds log [BUILD_ID] --stream

# View latest build status
gcloud builds log $(gcloud builds list --limit=1 --format='value(id)') --stream
```

## Configuration

### Environment Variables

Set environment variables for your Cloud Run services:

```bash
# Update backend environment variables
gcloud run services update meadcalc-api \
    --region $REGION \
    --update-env-vars \
    ASPNETCORE_ENVIRONMENT=Production,\
    CUSTOM_VAR=value

# Update frontend environment variables
gcloud run services update meadcalc-frontend \
    --region $REGION \
    --update-env-vars \
    VITE_API_URL=https://meadcalc-api.a.run.app
```

### Custom Domain

Map a custom domain to your Cloud Run service:

```bash
gcloud run domain-mappings create \
    --service meadcalc-frontend \
    --domain meadcalc.example.com \
    --region $REGION
```

Follow the on-screen instructions to update your DNS records.

### CORS Configuration

The backend is configured with CORS for Cloud Run URLs. Update `appsettings.CloudRun.json`:

```json
{
  "CORS": {
    "AllowedOrigins": [
      "https://meadcalc-frontend.a.run.app",
      "https://your-custom-domain.com"
    ]
  }
}
```

Then redeploy:

```bash
docker build -t ... .
docker push ...
gcloud run deploy meadcalc-api --image ...
```

## Monitoring and Logging

### View Cloud Run Logs

```bash
# Tail logs in real-time
gcloud run logs read meadcalc-api --region $REGION --limit 50 --follow

# View logs for specific time range
gcloud run logs read meadcalc-api \
    --region $REGION \
    --start-time "2024-10-26T00:00:00" \
    --end-time "2024-10-26T23:59:59"
```

### Cloud Logging

Access logs in Google Cloud Console:
1. Navigate to Cloud Logging
2. Filter by service name:
   - `resource.service.name="meadcalc-api"`
   - `resource.service.name="meadcalc-frontend"`

### Set Up Monitoring

```bash
# Create uptime check for API
gcloud monitoring uptime-checks create \
    --display-name="MeadCalculator API" \
    --http-check \
    --resource-type=uptime-url \
    --monitored-resource=https://meadcalc-api.a.run.app/health

# View metrics
gcloud monitoring metrics-descriptors list
```

## Cost Optimization

### Cloud Run Pricing (as of 2024)
- First 2M requests/month: Free
- vCPU: $0.00002400 per vCPU-second
- Memory: $0.00000250 per GB-second
- Network egress: $0.12 per GB

### Optimization Tips

1. **Right-size containers:**
   ```bash
   # Use minimum CPU for frontend
   gcloud run deploy meadcalc-frontend \
       --memory 256Mi \
       --cpu 1
   ```

2. **Set max instances to prevent overspending:**
   ```bash
   gcloud run services update meadcalc-api \
       --max-instances 10 \
       --region $REGION
   ```

3. **Use Cloud CDN for static assets:**
   ```bash
   # Enable CDN on frontend
   gcloud run services update meadcalc-frontend \
       --enable-cdn \
       --region $REGION
   ```

4. **Monitor costs:**
   - Use Cloud Billing to set up budget alerts
   - Review usage in Cloud Console > Billing

### Example Cost Estimate (Monthly)

For 100K requests/month with default config:
- Compute: ~$2-5
- Network: ~$0.12-1
- **Estimated total: $2-10/month**

## Troubleshooting

### Services Won't Deploy

Check deployment errors:
```bash
# Get service details
gcloud run services describe meadcalc-api --region $REGION

# View recent revisions
gcloud run revisions list --region $REGION
```

### Connection Issues Between Services

Cloud Run services can't communicate via private networking by default. Options:

1. **Frontend calls backend via public URL** (default)
   - Update nginx.conf with backend public URL
   - Redeploy frontend

2. **Use Service-to-Service Authentication:**
   ```bash
   # Grant frontend permission to call backend
   gcloud run services add-iam-policy-binding meadcalc-api \
       --member=serviceAccount:frontend-sa@PROJECT_ID.iam.gserviceaccount.com \
       --role=roles/run.invoker
   ```

### Images Not Found in Artifact Registry

Verify image was pushed:
```bash
gcloud artifacts docker images list $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo

gcloud artifacts docker images list \
    $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-api
```

### API CORS Errors

1. Check backend CORS configuration in `appsettings.CloudRun.json`
2. Verify frontend URL matches AllowedOrigins
3. Check browser console for exact error
4. Redeploy backend after configuration changes

### Cloud Build Failures

```bash
# View detailed build logs
gcloud builds log [BUILD_ID] --stream

# Check if service account has permissions
gcloud projects get-iam-policy $PROJECT_ID \
    --flatten="bindings[].members" \
    --filter="bindings.role:roles/run.admin"
```

### High Memory Usage

Check container memory:
```bash
# Get CPU/memory metrics
gcloud monitoring time-series list \
    --filter='metric.type="run.googleapis.com/request_latencies"'
```

If using too much memory:
1. Optimize application code
2. Increase memory allocation temporarily
3. Monitor with profiling tools

## Scaling Configuration

### Auto-scaling (Default)

Cloud Run automatically scales from 0 to max instances:

```bash
# Set min instances (for faster response)
gcloud run services update meadcalc-api \
    --min-instances 1 \
    --region $REGION

# Set max instances (to prevent cost overruns)
gcloud run services update meadcalc-api \
    --max-instances 100 \
    --region $REGION
```

### Concurrency

Limit concurrent requests per instance:

```bash
gcloud run deploy meadcalc-api \
    --concurrency 80 \
    --region $REGION
```

## Backup and Recovery

### Container Registry Backup

Keep images in Artifact Registry as backups:

```bash
# List all images
gcloud artifacts docker images list $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo
```

### Rollback to Previous Version

```bash
# List revisions
gcloud run revisions list --region $REGION --service meadcalc-api

# Deploy previous revision
gcloud run deploy meadcalc-api \
    --image $REGION-docker.pkg.dev/$PROJECT_ID/meadcalc-repo/meadcalc-api:PREVIOUS_TAG \
    --region $REGION
```

## Next Steps

1. **Custom Domain:** Set up custom domain with Cloud Run
2. **SSL/TLS:** Automatically configured by GCP
3. **CDN:** Enable Cloud CDN for static assets
4. **Security:** Review IAM permissions and service accounts
5. **Monitoring:** Set up alerts for errors and latency
6. **Database:** Add Cloud SQL or Firestore when needed

For more information, visit the [Google Cloud Run Documentation](https://cloud.google.com/run/docs).
