# PowerShell Deployment for MeadCalculator

**Project:** meadcalculator
**Status:** Ready to deploy

## Prerequisites Setup (One-time)

Before running the PowerShell script, run these commands in PowerShell:

```powershell
# 1. Authenticate with Google Cloud
gcloud auth login

# 2. Set your project
gcloud config set project meadcalculator

# 3. Configure Docker
gcloud auth configure-docker us-central1-docker.pkg.dev --quiet

# 4. Create Artifact Registry (only needed once)
gcloud artifacts repositories create meadcalc-repo `
    --repository-format=docker `
    --location=us-central1 `
    --description="MeadCalculator Docker images" `
    --project=meadcalculator
```

**Note:** If you see "Repository already exists" - that's fine, the repo is ready!

## Deploy with PowerShell Script

After prerequisites are set up, run:

```powershell
# Navigate to repo directory
cd C:\Users\robin\Documents\GitHub\MeadCalculator

# Run deployment
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator"
```

The script will:
1. Check prerequisites ✓
2. Build Docker images ✓
3. Push to Artifact Registry ✓
4. Deploy to Cloud Run ✓
5. Show you service URLs ✓

## Manual Deployment (If Script Fails)

If the PowerShell script has issues, use these PowerShell commands directly:

### Build and Push Backend

```powershell
# Build backend image
docker build `
    -t us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-api:latest `
    -f MeadCalculator.API/Dockerfile `
    .

# Push backend image
docker push us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-api:latest

# Deploy backend
gcloud run deploy meadcalc-api `
    --image us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-api:latest `
    --region us-central1 `
    --platform managed `
    --allow-unauthenticated `
    --memory 512Mi `
    --cpu 1 `
    --timeout 3600 `
    --set-env-vars "ASPNETCORE_ENVIRONMENT=Production" `
    --project=meadcalculator
```

### Build and Push Frontend

```powershell
# Build frontend image
docker build `
    -t us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-frontend:latest `
    -f frontend/Dockerfile `
    frontend/

# Push frontend image
docker push us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-frontend:latest

# Deploy frontend
gcloud run deploy meadcalc-frontend `
    --image us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-frontend:latest `
    --region us-central1 `
    --platform managed `
    --allow-unauthenticated `
    --memory 256Mi `
    --cpu 1 `
    --timeout 3600 `
    --project=meadcalculator
```

### Get Service URLs

```powershell
# Backend URL
gcloud run services describe meadcalc-api `
    --region us-central1 `
    --format='value(status.url)' `
    --project=meadcalculator

# Frontend URL
gcloud run services describe meadcalc-frontend `
    --region us-central1 `
    --format='value(status.url)' `
    --project=meadcalculator
```

## Verify Deployment

### Check Services Are Running

```powershell
gcloud run services list --region us-central1 --project meadcalculator
```

You should see both services listed with ACTIVE status.

### Test Backend API

```powershell
# Replace with your actual backend URL
curl https://meadcalc-api-[hash].a.run.app/api/ingredients
```

You should get a JSON response with ingredients.

### Visit Frontend

Open in browser:
```
https://meadcalc-frontend-[hash].a.run.app
```

## View Logs

```powershell
# Backend logs
gcloud run logs read meadcalc-api --region us-central1 --limit 50 --project meadcalculator

# Frontend logs
gcloud run logs read meadcalc-frontend --region us-central1 --limit 50 --project meadcalculator

# Follow logs (live)
gcloud run logs read meadcalc-api --region us-central1 --follow --project meadcalculator
```

## Troubleshooting

### "docker: command not found"
Install Docker Desktop: https://www.docker.com/products/docker-desktop

### "gcloud: command not found"
Install Google Cloud SDK: https://cloud.google.com/sdk/docs/install

### Docker authentication fails
```powershell
gcloud auth configure-docker us-central1-docker.pkg.dev --quiet
```

### Repository not found error
```powershell
gcloud artifacts repositories create meadcalc-repo `
    --repository-format=docker `
    --location=us-central1 `
    --description="MeadCalculator Docker images" `
    --project=meadcalculator
```

### Deployment times out
Services might still be deploying. Check status:
```powershell
gcloud run services describe meadcalc-api --region us-central1 --project meadcalculator
```

### 502 Bad Gateway in frontend
Backend might not be responding. Check logs:
```powershell
gcloud run logs read meadcalc-api --region us-central1 --limit 50 --project meadcalculator
```

## Next Time

Once everything is set up, just run:

```powershell
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator"
```

Or to redeploy with code changes:

```powershell
# Push changes to git
git push origin main

# Cloud Build will auto-deploy
# OR run script again for manual deployment
```

## Useful Commands

```powershell
# List services
gcloud run services list --region us-central1 --project meadcalculator

# Delete service
gcloud run services delete meadcalc-api --region us-central1 --project meadcalculator

# Update environment
gcloud run services update meadcalc-api `
    --update-env-vars KEY=VALUE `
    --region us-central1 `
    --project meadcalculator

# View build history
gcloud builds list --project meadcalculator

# Describe service
gcloud run services describe meadcalc-api --region us-central1 --project meadcalculator
```

---

**Status:** ✅ Ready to Deploy
**Repository:** meadcalc-repo (Created)
**Docker Auth:** Configured
