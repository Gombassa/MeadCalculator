# MeadCalculator - Deployment Quick Reference Card

**Project ID: `meadcalculator`**
**Region: `us-central1`**

---

## 🚀 One-Command Deployment

### macOS / Linux
```bash
chmod +x scripts/deploy-gcp.sh
./scripts/deploy-gcp.sh meadcalculator us-central1
```

### Windows (PowerShell)
```powershell
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator" -Region "us-central1"
```

---

## 📋 Prerequisites Checklist

- [ ] Google Cloud SDK installed (`gcloud --version`)
- [ ] Docker installed (`docker --version`)
- [ ] Git installed (`git --version`)
- [ ] GCP Project "meadcalculator" created
- [ ] Billing enabled on project
- [ ] You're logged into gcloud (`gcloud auth list`)
- [ ] APIs enabled:
  - [ ] Cloud Run API
  - [ ] Artifact Registry API
  - [ ] Cloud Build API

### Enable APIs (if needed)
```bash
gcloud services enable run.googleapis.com artifactregistry.googleapis.com cloudbuild.googleapis.com --project=meadcalculator
```

---

## 🔑 Essential Commands

### Setup
```bash
gcloud auth login
gcloud config set project meadcalculator
gcloud auth configure-docker us-central1-docker.pkg.dev
```

### Deploy
```bash
./scripts/deploy-gcp.sh meadcalculator us-central1
```

### List Services
```bash
gcloud run services list --region us-central1 --project meadcalculator
```

### Get Service URLs
```bash
gcloud run services describe meadcalc-api --region us-central1 --format='value(status.url)' --project meadcalculator
gcloud run services describe meadcalc-frontend --region us-central1 --format='value(status.url)' --project meadcalculator
```

### View Logs
```bash
gcloud run logs read meadcalc-api --region us-central1 --limit 50 --project meadcalculator --follow
gcloud run logs read meadcalc-frontend --region us-central1 --limit 50 --project meadcalculator --follow
```

### Delete Services
```bash
gcloud run services delete meadcalc-api --region us-central1 --project meadcalculator
gcloud run services delete meadcalc-frontend --region us-central1 --project meadcalculator
```

---

## 📚 Documentation Files

| File | Purpose |
|------|---------|
| **GCP-STEP-BY-STEP.md** | Complete walkthrough with screenshots |
| **GCP-QUICK-START.md** | 5-minute quick start |
| **GCP-DEPLOYMENT.md** | Comprehensive reference (400+ lines) |
| **DEPLOYMENT.md** | Multi-platform deployment options |
| **README.md** | Project overview |

---

## 🌐 Service URLs (After Deployment)

- **Backend API**: `https://meadcalc-api-[hash].a.run.app/api/`
- **Frontend**: `https://meadcalc-frontend-[hash].a.run.app`
- **Health Check**: `https://meadcalc-api-[hash].a.run.app/health`

### Test Endpoints
```bash
# Get all ingredients
curl https://meadcalc-api-[hash].a.run.app/api/ingredients

# Calculate ABV
curl -X POST https://meadcalc-api-[hash].a.run.app/api/calculator/calculate \
  -H "Content-Type: application/json" \
  -d '{...}'
```

---

## 💰 Cost Estimate

| Metric | Cost |
|--------|------|
| First 2M requests/month | **FREE** ✓ |
| 100K requests/month | ~**$2-10** |
| Always-free tier value | **$2.50/month** |

---

## 🐛 Common Issues & Fixes

| Issue | Solution |
|-------|----------|
| "gcloud: command not found" | Install Google Cloud SDK |
| "docker: command not found" | Install Docker Desktop |
| Permission denied on script | `chmod +x scripts/deploy-gcp.sh` |
| Deployment times out | Increase timeout: `--timeout 3600` |
| 502 Bad Gateway | Check backend logs: `gcloud run logs read meadcalc-api...` |
| Repository exists error | Normal - script skips existing repos |

---

## 📊 Monitoring Dashboard

Access in Google Cloud Console:
1. [Cloud Run Services](https://console.cloud.google.com/run?project=meadcalculator)
2. [Cloud Logging](https://console.cloud.google.com/logs?project=meadcalculator)
3. [Cloud Build History](https://console.cloud.google.com/cloud-build/builds?project=meadcalculator)
4. [Artifact Registry](https://console.cloud.google.com/artifacts/repositories?project=meadcalculator)
5. [Cloud Billing](https://console.cloud.google.com/billing?project=meadcalculator)

---

## 🔄 Continuous Deployment with Cloud Build

After setting up Cloud Build trigger:

```bash
# Just push to main branch
git add .
git commit -m "Your changes"
git push origin main

# Cloud Build automatically:
# 1. Builds Docker images
# 2. Pushes to Artifact Registry
# 3. Deploys to Cloud Run
```

Monitor builds:
```bash
gcloud builds list --project meadcalculator
gcloud builds log [BUILD_ID] --stream --project meadcalculator
```

---

## 🔒 Custom Domain (Optional)

```bash
# Map custom domain
gcloud run domain-mappings create \
  --service meadcalc-frontend \
  --domain meadcalc.yourdomain.com \
  --region us-central1 \
  --project meadcalculator

# Verify mapping
gcloud run domain-mappings list --region us-central1 --project meadcalculator
```

---

## 📝 Configuration Files

| File | Purpose |
|------|---------|
| `cloudbuild.yaml` | Cloud Build pipeline |
| `.gcloudignore` | Files to exclude from Cloud Build |
| `appsettings.CloudRun.json` | Backend production config |
| `MeadCalculator.API/Dockerfile` | Backend container config |
| `frontend/Dockerfile` | Frontend container config |
| `frontend/nginx.conf` | Frontend web server config |

---

## 🔗 Useful Links

- [Google Cloud Console](https://console.cloud.google.com)
- [Cloud Run Documentation](https://cloud.google.com/run/docs)
- [Cloud Build Documentation](https://cloud.google.com/build/docs)
- [Artifact Registry](https://cloud.google.com/artifact-registry)
- [Google Cloud Free Tier](https://cloud.google.com/free)

---

## 📋 Pre-Deployment Checklist

Before running deployment script:

- [ ] Cloned repository: `git clone https://github.com/Gombassa/MeadCalculator.git`
- [ ] Inside repo directory: `cd MeadCalculator`
- [ ] Latest code: `git pull origin main`
- [ ] APIs enabled: `gcloud services list --enabled`
- [ ] gcloud configured: `gcloud config get-value project` returns "meadcalculator"
- [ ] Docker running: `docker ps` works without errors
- [ ] Script executable (Linux/Mac): `ls -la scripts/deploy-gcp.sh` shows `x`

---

## ✅ Post-Deployment Checklist

After running deployment script:

- [ ] Both services running: `gcloud run services list --region us-central1 --project meadcalculator`
- [ ] Backend accessible: `curl https://meadcalc-api-[hash].a.run.app/api/ingredients`
- [ ] Frontend accessible: Visit `https://meadcalc-frontend-[hash].a.run.app` in browser
- [ ] Calculator works: Test ABV calculation in frontend
- [ ] Logs are clean: `gcloud run logs read meadcalc-api --limit 20 --project meadcalculator`

---

## 🎯 Next Steps

1. **Immediate**: Test both services
2. **Within 24 hours**: Set up Cloud Build automation
3. **Optional**: Configure custom domain
4. **Optional**: Set up monitoring alerts
5. **Future**: Add database when needed

---

## 📞 Support

- See **GCP-STEP-BY-STEP.md** for detailed walkthrough
- See **GCP-DEPLOYMENT.md** for troubleshooting section
- Check [Cloud Build Logs](https://console.cloud.google.com/cloud-build/builds?project=meadcalculator)
- Check [Cloud Logging](https://console.cloud.google.com/logs?project=meadcalculator)

---

**Last Updated**: 2024-10-26
**For**: MeadCalculator on `meadcalculator` GCP project
**Status**: ✅ Production Ready
