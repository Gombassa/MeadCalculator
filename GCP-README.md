# MeadCalculator - Google Cloud Platform Deployment

**Project ID: `meadcalculator`**
**Status: ✅ Ready for Deployment**

Welcome! This directory contains everything you need to deploy MeadCalculator to Google Cloud Run.

---

## 🚀 Quick Start (2 Minutes)

### macOS / Linux
```bash
chmod +x scripts/deploy-gcp.sh
./scripts/deploy-gcp.sh meadcalculator us-central1
```

### Windows (PowerShell)
```powershell
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator"
```

Done! Your services will be live in 5-10 minutes.

---

## 📚 Documentation Guide

Choose your starting point:

### **Just Want to Deploy?**
→ Read: **[GCP-STEP-BY-STEP.md](GCP-STEP-BY-STEP.md)**
- Complete walkthrough for your "meadcalculator" project
- Step-by-step with commands you can copy/paste
- Includes prerequisites and verification steps
- Troubleshooting guide included

### **5-Minute Quick Reference?**
→ Read: **[GCP-QUICK-START.md](GCP-QUICK-START.md)**
- Rapid deployment instructions
- Common tasks and commands
- Basic troubleshooting

### **Need a Cheat Sheet?**
→ Read: **[DEPLOYMENT-QUICK-REFERENCE.md](DEPLOYMENT-QUICK-REFERENCE.md)**
- Quick reference card
- Essential commands
- Useful links and checklists

### **Want All the Details?**
→ Read: **[GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md)**
- Comprehensive 400+ line guide
- Manual step-by-step deployment
- Cloud Build automation setup
- Advanced configuration
- Cost optimization
- Detailed troubleshooting

### **Prefer General Info?**
→ Read: **[README.md](../README.md)**
- Project overview
- Features and tech stack
- API documentation

---

## 📋 What You Need

✅ **Installed:**
- Google Cloud SDK (gcloud)
- Docker Desktop
- Git

✅ **Access:**
- GCP Project "meadcalculator"
- Billing enabled
- Admin permissions

✅ **Prepared:**
- GitHub repository cloned
- APIs enabled (script can do this)

---

## 🎯 The 4-Step Process

```
1. Setup (Login + Configure)
        ↓
2. Deploy (Run script)
        ↓
3. Verify (Test services)
        ↓
4. Monitor (View logs & metrics)
```

---

## 🔄 Deployment Scripts

Both scripts handle the complete deployment:

| Script | OS | Purpose |
|--------|----|---------|
| `scripts/deploy-gcp.sh` | macOS/Linux | Bash deployment script |
| `scripts/Deploy-GCP.ps1` | Windows | PowerShell deployment script |

**What they do:**
1. Create Artifact Registry repository
2. Build Docker images
3. Push to Artifact Registry
4. Deploy backend to Cloud Run
5. Deploy frontend to Cloud Run
6. Output service URLs

---

## 📊 Expected Result

After deployment, you'll have:

**Services:**
- ✅ Backend API: `https://meadcalc-api-[hash].a.run.app`
- ✅ Frontend: `https://meadcalc-frontend-[hash].a.run.app`

**Features:**
- ✅ Auto-scaling (0 to N instances)
- ✅ Health checks enabled
- ✅ Monitoring integrated
- ✅ Logs in Cloud Console
- ✅ Always-free tier eligible

**Costs:**
- ✅ First 2M requests/month: FREE
- ✅ Estimated: $2-10/month for normal usage

---

## 📁 GCP Files Overview

### Configuration Files
```
.gcloudignore                    # Files to exclude from Cloud Build
.env.example                     # Environment variable template
cloudbuild.yaml                  # Cloud Build CI/CD pipeline
```

### Dockerfiles
```
MeadCalculator.API/Dockerfile    # Backend container (Alpine-based)
frontend/Dockerfile              # Frontend container (nginx on Alpine)
frontend/nginx.conf              # Web server configuration
```

### Deployment Scripts
```
scripts/deploy-gcp.sh            # Bash deployment (macOS/Linux)
scripts/Deploy-GCP.ps1           # PowerShell deployment (Windows)
```

### Configuration
```
MeadCalculator.API/appsettings.CloudRun.json  # Production settings
```

---

## 🚀 First Time Setup

### Prerequisites (one-time)

```bash
# 1. Install gcloud
# Go to: https://cloud.google.com/sdk/docs/install

# 2. Install Docker
# Go to: https://www.docker.com/products/docker-desktop

# 3. Clone repo
git clone https://github.com/Gombassa/MeadCalculator.git
cd MeadCalculator

# 4. Authenticate
gcloud auth login
gcloud config set project meadcalculator

# 5. Enable APIs
gcloud services enable run.googleapis.com
gcloud services enable artifactregistry.googleapis.com
gcloud services enable cloudbuild.googleapis.com
```

### Deploy

```bash
# macOS/Linux
./scripts/deploy-gcp.sh meadcalculator us-central1

# Windows PowerShell
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator"
```

---

## 🔄 Subsequent Deployments

### Option 1: Automated (Cloud Build)
```bash
git push origin main
# Cloud Build automatically deploys!
```

### Option 2: Manual Script
```bash
./scripts/deploy-gcp.sh meadcalculator us-central1
```

---

## 📊 Monitoring

### View Logs
```bash
gcloud run logs read meadcalc-api --region us-central1 --follow --project meadcalculator
gcloud run logs read meadcalc-frontend --region us-central1 --follow --project meadcalculator
```

### Check Services
```bash
gcloud run services list --region us-central1 --project meadcalculator
gcloud run services describe meadcalc-api --region us-central1 --project meadcalculator
```

### Cloud Console
- [Cloud Run Services](https://console.cloud.google.com/run?project=meadcalculator)
- [Cloud Logging](https://console.cloud.google.com/logs?project=meadcalculator)
- [Cloud Build](https://console.cloud.google.com/cloud-build?project=meadcalculator)

---

## 🎯 Common Tasks

### Deploy New Changes
```bash
git push origin main  # Cloud Build auto-deploys
# OR
./scripts/deploy-gcp.sh meadcalculator us-central1
```

### View Deployment History
```bash
gcloud builds list --project meadcalculator
```

### Delete Services
```bash
gcloud run services delete meadcalc-api --region us-central1 --project meadcalculator
gcloud run services delete meadcalc-frontend --region us-central1 --project meadcalculator
```

### Update Configuration
```bash
gcloud run services update meadcalc-api \
  --set-env-vars KEY=VALUE \
  --region us-central1 \
  --project meadcalculator
```

---

## 💡 Pro Tips

1. **Speed up deploys**: Set min-instances to 1 (costs ~$10/month but instant starts)
   ```bash
   gcloud run services update meadcalc-api --min-instances 1 --region us-central1 --project meadcalculator
   ```

2. **Limit costs**: Set max-instances to 10
   ```bash
   gcloud run services update meadcalc-api --max-instances 10 --region us-central1 --project meadcalculator
   ```

3. **Enable CDN**: Caches static assets globally
   ```bash
   gcloud run services update meadcalc-frontend --enable-cdn --region us-central1 --project meadcalculator
   ```

4. **Custom domain**: Map your domain
   ```bash
   gcloud run domain-mappings create --service meadcalc-frontend --domain meadcalc.yourdomain.com --region us-central1 --project meadcalculator
   ```

---

## 🆘 Troubleshooting

**Common Issue: "docker: command not found"**
```bash
# Install Docker Desktop: https://www.docker.com/products/docker-desktop
```

**Common Issue: "gcloud: command not found"**
```bash
# Install Google Cloud SDK: https://cloud.google.com/sdk/docs/install
```

**Common Issue: Script has permission error**
```bash
chmod +x scripts/deploy-gcp.sh
```

**Common Issue: 502 Bad Gateway in frontend**
- Backend might be down: `curl https://meadcalc-api-[hash].a.run.app/api/ingredients`
- Check logs: `gcloud run logs read meadcalc-api --region us-central1 --limit 50 --project meadcalculator`

For more help, see **[GCP-STEP-BY-STEP.md](GCP-STEP-BY-STEP.md)** - Troubleshooting section.

---

## 📚 Reading Order

1. **Start here**: This file (you are here!)
2. **Then read**: [GCP-STEP-BY-STEP.md](GCP-STEP-BY-STEP.md)
3. **For reference**: [DEPLOYMENT-QUICK-REFERENCE.md](DEPLOYMENT-QUICK-REFERENCE.md)
4. **For details**: [GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md)
5. **For context**: [README.md](../README.md)

---

## 🎓 Learn More

- [Google Cloud Run Docs](https://cloud.google.com/run/docs)
- [Cloud Build Docs](https://cloud.google.com/build/docs)
- [Artifact Registry Docs](https://cloud.google.com/artifact-registry/docs)
- [Google Cloud Free Tier](https://cloud.google.com/free)

---

## 📊 Architecture

```
GitHub Repository
        ↓
   [Push to main]
        ↓
   Cloud Build
   (Automatic)
        ↓
   [Build Images]
        ↓
   Artifact Registry
        ↓
   [Deploy Services]
        ↓
   Cloud Run
        ├─ Backend API (meadcalc-api)
        └─ Frontend (meadcalc-frontend)
```

---

## ✅ Deployment Checklist

- [ ] GCP project "meadcalculator" created
- [ ] Billing enabled
- [ ] Google Cloud SDK installed
- [ ] Docker installed
- [ ] Git installed
- [ ] Repository cloned
- [ ] Logged in to gcloud: `gcloud auth login`
- [ ] Project set: `gcloud config set project meadcalculator`
- [ ] APIs enabled
- [ ] Ready to deploy!

---

## 🚀 Ready to Deploy?

```bash
# Pick your platform:

# macOS / Linux:
chmod +x scripts/deploy-gcp.sh
./scripts/deploy-gcp.sh meadcalculator us-central1

# Windows PowerShell:
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator"
```

---

## 📞 Need Help?

1. Check **[GCP-STEP-BY-STEP.md](GCP-STEP-BY-STEP.md)** - Troubleshooting section
2. Check **[GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md)** - Advanced troubleshooting
3. View logs: `gcloud run logs read meadcalc-api --follow --project meadcalculator`
4. Check [Cloud Console](https://console.cloud.google.com?project=meadcalculator)

---

**Status**: ✅ Ready to Deploy
**Last Updated**: 2024-10-26
**Project**: meadcalculator
**Region**: us-central1

🍯 **Happy Brewing!**
