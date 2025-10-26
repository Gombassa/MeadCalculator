# MeadCalculator - Google Cloud Run Quick Start

Deploy MeadCalculator to Google Cloud Run in 5 minutes.

## Prerequisites

- Google Cloud SDK: https://cloud.google.com/sdk/docs/install
- Docker Desktop: https://www.docker.com/products/docker-desktop
- Active GCP project with billing enabled

## One-Command Deployment

Choose your operating system:

### macOS / Linux

```bash
# Clone repo (if you haven't already)
git clone https://github.com/Gombassa/MeadCalculator.git
cd MeadCalculator

# Deploy
chmod +x scripts/deploy-gcp.sh
./scripts/deploy-gcp.sh YOUR_GCP_PROJECT_ID us-central1
```

### Windows (PowerShell)

```powershell
# Clone repo (if you haven't already)
git clone https://github.com/Gombassa/MeadCalculator.git
cd MeadCalculator

# Deploy
.\scripts\Deploy-GCP.ps1 -ProjectId "MeadCalculator" -Region "us-central1"
```

## What Gets Deployed

- **Backend API** (meadcalc-api): https://meadcalc-api.a.run.app
- **Frontend** (meadcalc-frontend): https://meadcalc-frontend.a.run.app

## Verify Deployment

```bash
# Test the API
curl https://meadcalc-api.a.run.app/api/ingredients

# Visit the frontend
open https://meadcalc-frontend.a.run.app
```

## View Logs

```bash
# Backend logs
gcloud run logs read meadcalc-api --region us-central1 --limit 50 --follow

# Frontend logs
gcloud run logs read meadcalc-frontend --region us-central1 --limit 50 --follow
```

## Common Tasks

### Deploy New Changes

```bash
# Just push to main branch - Cloud Build will automatically deploy!
git push origin main
```

### Redeploy Manually

```bash
# macOS/Linux
./scripts/deploy-gcp.sh YOUR_PROJECT_ID us-central1

# Windows
.\scripts\Deploy-GCP.ps1 -ProjectId "your-project-id"
```

### Delete Services

```bash
# Delete backend
gcloud run services delete meadcalc-api --region us-central1

# Delete frontend
gcloud run services delete meadcalc-frontend --region us-central1
```

### Check Service Status

```bash
gcloud run services describe meadcalc-api --region us-central1
gcloud run services describe meadcalc-frontend --region us-central1
```

### View Cloud Build History

```bash
gcloud builds list --limit 10
```

## Costs

- **Free tier includes**: 2M requests/month
- **Estimated cost** (100K requests/month): $2-10
- **Always-free tier** value: $2.50/month

See [GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md) for cost optimization tips.

## Troubleshooting

### "gcloud: command not found"
Install Google Cloud SDK: https://cloud.google.com/sdk/docs/install

### "docker: command not found"
Install Docker Desktop: https://www.docker.com/products/docker-desktop

### "Permission denied" on deploy script (macOS/Linux)
```bash
chmod +x scripts/deploy-gcp.sh
./scripts/deploy-gcp.sh YOUR_PROJECT_ID
```

### Deployment fails with authentication error
```bash
gcloud auth login
gcloud config set project YOUR_PROJECT_ID
```

### Services won't start
```bash
# Check logs
gcloud run logs read meadcalc-api --region us-central1 --limit 100

# Check service details
gcloud run services describe meadcalc-api --region us-central1
```

## Next Steps

1. **Add custom domain**: See [GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md) section on "Custom Domain"
2. **Enable monitoring**: Set up Cloud Monitoring alerts
3. **Configure auto-scaling**: Adjust min/max instances
4. **Set up CI/CD**: Enable Cloud Build automation on git push

## Full Documentation

See [GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md) for:
- Detailed step-by-step instructions
- Cloud Build automation setup
- Configuration options
- Monitoring and logging
- Cost optimization
- Troubleshooting guide

## Support

For more help:
- Cloud Run docs: https://cloud.google.com/run/docs
- Cloud Build docs: https://cloud.google.com/build/docs
- Artifact Registry docs: https://cloud.google.com/artifact-registry/docs
