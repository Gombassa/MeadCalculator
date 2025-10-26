# MeadCalculator - Step-by-Step GCP Deployment Walkthrough

**Your GCP Project ID: `meadcalculator`**

This guide walks you through deploying MeadCalculator to Google Cloud Run step-by-step with your specific project.

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Step 1: Setup GCP Project](#step-1-setup-gcp-project)
3. [Step 2: Install Required Tools](#step-2-install-required-tools)
4. [Step 3: Enable Required APIs](#step-3-enable-required-apis)
5. [Step 4: Set Up Docker](#step-4-set-up-docker)
6. [Step 5: Deploy Using Script](#step-5-deploy-using-script)
7. [Step 6: Verify Deployment](#step-6-verify-deployment)
8. [Step 7: Set Up Cloud Build (Automated Deployment)](#step-7-set-up-cloud-build-automated-deployment)
9. [Step 8: Configure Custom Domain (Optional)](#step-8-configure-custom-domain-optional)
10. [Troubleshooting](#troubleshooting)

---

## Prerequisites

Before you start, ensure you have:

- ✅ A Google Cloud Platform account (https://console.cloud.google.com)
- ✅ Billing enabled on your GCP project
- ✅ Admin access to project "meadcalculator"
- ✅ Internet connection
- ✅ Administrative access to your local machine

---

## Step 1: Setup GCP Project

### 1.1 Verify Your Project

1. Go to [Google Cloud Console](https://console.cloud.google.com)
2. Click on the project dropdown (top of page)
3. Look for "meadcalculator" in the list
4. Click to select it

**Expected Result**: You should see "meadcalculator" in the top-left corner next to the Google Cloud logo.

### 1.2 Note Your Project ID

Your GCP Project ID is: **`meadcalculator`**

This will be used throughout the deployment process.

---

## Step 2: Install Required Tools

### For macOS / Linux

#### 2.1 Install Google Cloud SDK

```bash
# Download installer
curl https://sdk.cloud.google.com | bash

# Restart terminal
exec -l $SHELL

# Initialize SDK
gcloud init
```

#### 2.2 Install Docker

```bash
# macOS (using Homebrew)
brew install docker

# Or download Docker Desktop from:
# https://www.docker.com/products/docker-desktop
```

#### 2.3 Install Git

```bash
# macOS (using Homebrew)
brew install git

# Or download from: https://git-scm.com/download/mac
```

### For Windows (PowerShell)

#### 2.1 Install Google Cloud SDK

1. Download from: https://cloud.google.com/sdk/docs/install-sdk#windows
2. Run the installer: `GoogleCloudSDKInstaller.exe`
3. Follow the installation wizard
4. Open PowerShell (as Administrator) after installation

#### 2.2 Install Docker

1. Download Docker Desktop: https://www.docker.com/products/docker-desktop
2. Run the installer
3. Restart your computer when prompted

#### 2.3 Install Git

1. Download from: https://git-scm.com/download/win
2. Run the installer with default settings

---

## Step 3: Enable Required APIs

These APIs must be enabled for your project to work with Cloud Run.

### 3.1 Open Cloud Console

Go to: https://console.cloud.google.com

Make sure "meadcalculator" is selected in the top dropdown.

### 3.2 Enable APIs via Console

**Method A: Using Console (Easiest)**

1. Go to **APIs & Services** > **Enabled APIs & services**
2. Click **Enable APIs and Services** button
3. Search for and enable each API below:
   - **Cloud Run API**
   - **Artifact Registry API**
   - **Cloud Build API**

For each API:
1. Click on the API name
2. Click **Enable** button
3. Wait for it to say "API enabled" (may take a minute)

**Method B: Using gcloud CLI**

```bash
gcloud services enable run.googleapis.com
gcloud services enable artifactregistry.googleapis.com
gcloud services enable cloudbuild.googleapis.com
gcloud config set project meadcalculator
```

### 3.3 Verify APIs are Enabled

```bash
gcloud services list --enabled --project meadcalculator
```

You should see:
- `run.googleapis.com`
- `artifactregistry.googleapis.com`
- `cloudbuild.googleapis.com`

---

## Step 4: Set Up Docker

### 4.1 Login to Google Cloud

```bash
gcloud auth login
```

This opens a browser for you to authenticate. After logging in, you'll see a confirmation.

### 4.2 Set Your Project

```bash
gcloud config set project meadcalculator
```

### 4.3 Configure Docker Authentication

```bash
gcloud auth configure-docker us-central1-docker.pkg.dev
```

When prompted, type `Y` and press Enter to authorize Docker.

### 4.4 Verify Docker Installation

```bash
docker --version
```

Should output something like: `Docker version 24.0.0, build abc1234`

---

## Step 5: Deploy Using Script

### 5.1 Clone the Repository (If You Haven't Already)

```bash
git clone https://github.com/Gombassa/MeadCalculator.git
cd MeadCalculator
```

### 5.2 Choose Your Deployment Method

#### **Option A: Automated Script (Recommended)**

**On macOS / Linux:**

```bash
# Make script executable
chmod +x scripts/deploy-gcp.sh

# Run the deployment script
./scripts/deploy-gcp.sh meadcalculator us-central1
```

**On Windows (PowerShell):**

```powershell
# Run the deployment script
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator" -Region "us-central1"
```

**What the script does:**
1. Creates Artifact Registry repository
2. Configures Docker authentication
3. Builds backend Docker image
4. Pushes backend to Artifact Registry
5. Builds frontend Docker image
6. Pushes frontend to Artifact Registry
7. Deploys backend to Cloud Run
8. Deploys frontend to Cloud Run
9. Displays service URLs

**Expected Output:**
```
===> Authenticating with Google Cloud...
===> Setting up Artifact Registry repository...
✓ Repository created
===> Configuring Docker authentication...
===> Building backend Docker image...
[... build progress ...]
===> Pushing backend image to Artifact Registry...
[... push progress ...]
===> Building frontend Docker image...
[... build progress ...]
===> Pushing frontend image to Artifact Registry...
[... push progress ...]
===> Deploying backend to Cloud Run...
Service [meadcalc-api] created successfully.
✓ Backend deployed: https://meadcalc-api-[hash].a.run.app

===> Deploying frontend to Cloud Run...
Service [meadcalc-frontend] created successfully.
✓ Frontend deployed: https://meadcalc-frontend-[hash].a.run.app

Deployment Complete!
```

#### **Option B: Manual Step-by-Step Deployment**

If the script doesn't work, follow this manual process:

**Step 1: Create Artifact Registry Repository**

```bash
gcloud artifacts repositories create meadcalc-repo \
    --repository-format=docker \
    --location=us-central1 \
    --description="MeadCalculator Docker images" \
    --project=meadcalculator
```

**Step 2: Build Backend Image**

```bash
docker build \
    -t us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-api:latest \
    -f MeadCalculator.API/Dockerfile \
    .
```

Expected output: `Successfully built [image-id]`

**Step 3: Push Backend Image**

```bash
docker push us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-api:latest
```

**Step 4: Build Frontend Image**

```bash
docker build \
    -t us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-frontend:latest \
    -f frontend/Dockerfile \
    frontend/
```

**Step 5: Push Frontend Image**

```bash
docker push us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-frontend:latest
```

**Step 6: Deploy Backend to Cloud Run**

```bash
gcloud run deploy meadcalc-api \
    --image us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-api:latest \
    --region us-central1 \
    --platform managed \
    --allow-unauthenticated \
    --memory 512Mi \
    --cpu 1 \
    --timeout 3600 \
    --set-env-vars ASPNETCORE_ENVIRONMENT=Production \
    --project meadcalculator
```

Expected output:
```
Service [meadcalc-api] created successfully.
URL: https://meadcalc-api-[hash].a.run.app
```

**Step 7: Deploy Frontend to Cloud Run**

```bash
gcloud run deploy meadcalc-frontend \
    --image us-central1-docker.pkg.dev/meadcalculator/meadcalc-repo/meadcalc-frontend:latest \
    --region us-central1 \
    --platform managed \
    --allow-unauthenticated \
    --memory 256Mi \
    --cpu 1 \
    --timeout 3600 \
    --project meadcalculator
```

Expected output:
```
Service [meadcalc-frontend] created successfully.
URL: https://meadcalc-frontend-[hash].a.run.app
```

---

## Step 6: Verify Deployment

### 6.1 Check Services Are Running

```bash
gcloud run services list --region us-central1 --project meadcalculator
```

You should see both services listed:
```
SERVICE               STATUS  LAST DEPLOYED
meadcalc-api         ACTIVE  2024-10-26
meadcalc-frontend    ACTIVE  2024-10-26
```

### 6.2 Get Service URLs

```bash
# Get backend URL
gcloud run services describe meadcalc-api \
    --region us-central1 \
    --format='value(status.url)' \
    --project meadcalculator

# Get frontend URL
gcloud run services describe meadcalc-frontend \
    --region us-central1 \
    --format='value(status.url)' \
    --project meadcalculator
```

These URLs will look like:
- **Backend**: `https://meadcalc-api-xxxxxx.a.run.app`
- **Frontend**: `https://meadcalc-frontend-xxxxxx.a.run.app`

### 6.3 Test the Backend API

```bash
# Replace with your actual backend URL
curl https://meadcalc-api-xxxxxx.a.run.app/api/ingredients
```

You should get a JSON response with ingredients list.

### 6.4 Test the Frontend

1. Open your browser
2. Visit your frontend URL: `https://meadcalc-frontend-xxxxxx.a.run.app`
3. You should see the MeadCalculator landing page

### 6.5 Test the Calculator

1. Click "Open Calculator" or navigate to "/calculator"
2. Add some ingredients
3. Click "Calculate"
4. You should see results on the right side

---

## Step 7: Set Up Cloud Build (Automated Deployment)

Cloud Build automatically deploys your application whenever you push to the main branch.

### 7.1 Connect GitHub Repository

1. Go to [Cloud Build Triggers](https://console.cloud.google.com/cloud-build/triggers?project=meadcalculator)
2. Click **Create Trigger**
3. For "Source", select **GitHub**
4. Click **Authorize**
5. Log in to your GitHub account
6. Authorize Google Cloud Build
7. Select your repository: `MeadCalculator`

### 7.2 Configure Trigger

After connecting:

1. **Name**: `MeadCalculator-Deploy`
2. **Branch**: Select `^main$` (only deploy main branch)
3. **Build configuration**: Select `Cloud Build configuration file`
4. **Configuration file**: Type `cloudbuild.yaml`
5. **Substitution variables** (Optional - defaults are fine):
   - `_REGION`: `us-central1`
   - `_REPOSITORY`: `meadcalc-repo`

6. Click **Create**

### 7.3 Test Cloud Build

```bash
# Make a change to trigger build
echo "# Updated" >> README.md

# Commit and push
git add .
git commit -m "Trigger Cloud Build"
git push origin main
```

Then:
1. Go to [Cloud Build > History](https://console.cloud.google.com/cloud-build/builds?project=meadcalculator)
2. You should see a build running
3. Watch the build complete
4. Your services will automatically update

---

## Step 8: Configure Custom Domain (Optional)

To use your own domain instead of `a.run.app` URL:

### 8.1 Point Your Domain to Cloud Run

1. Go to [Cloud Run > Services](https://console.cloud.google.com/run?project=meadcalculator)
2. Click on `meadcalc-frontend` service
3. Click **Manage Custom Domains**
4. Click **Add Mapping**
5. Enter your domain: `meadcalc.yourdomain.com`
6. Follow the on-screen DNS instructions

### 8.2 Update DNS Records

Your domain registrar will ask you to create a DNS record. Follow their instructions to point your domain to Google Cloud Run.

### 8.3 Verify Domain

```bash
# Check if domain is mapped
gcloud run domain-mappings list --region us-central1 --project meadcalculator
```

---

## Troubleshooting

### Problem: "gcloud: command not found"

**Solution:**
```bash
# Check if gcloud is installed
which gcloud

# If not found, install Google Cloud SDK
# macOS: brew install google-cloud-sdk
# Windows: Download from https://cloud.google.com/sdk/docs/install
```

### Problem: "docker: command not found"

**Solution:**
```bash
# Check if Docker is installed
docker --version

# If not, install Docker Desktop
# https://www.docker.com/products/docker-desktop
```

### Problem: "ERROR: (gcloud.run.deploy) User does not have permission"

**Solution:**
```bash
# Check if you're using the right project
gcloud config get-value project

# Set the correct project
gcloud config set project meadcalculator

# Check your IAM permissions
gcloud projects get-iam-policy meadcalculator --flatten="bindings[].members" --filter="bindings.role:roles/owner"
```

### Problem: "Artifact Registry repository already exists"

**Solution:**
This is not an error. The script detected the repo already exists and skipped creation. Continue with deployment.

### Problem: Docker build fails with "no space left on device"

**Solution:**
```bash
# Clean up Docker
docker system prune -a

# Or increase Docker memory
# Docker Desktop > Preferences > Resources > Memory (increase to 4GB+)
```

### Problem: Cloud Run deployment times out

**Solution:**
```bash
# Check service logs
gcloud run logs read meadcalc-api \
    --region us-central1 \
    --limit 50 \
    --project meadcalculator

# Increase timeout
gcloud run services update meadcalc-api \
    --timeout 3600 \
    --region us-central1 \
    --project meadcalculator
```

### Problem: Frontend shows 502 Bad Gateway

**Solution:**
1. Check if backend is running:
```bash
curl https://meadcalc-api-xxxxxx.a.run.app/api/ingredients
```

2. Check nginx configuration is correct (should route /api/ to backend)

3. Redeploy frontend:
```bash
./scripts/deploy-gcp.sh meadcalculator us-central1
# or
.\scripts\Deploy-GCP.ps1 -ProjectId "meadcalculator"
```

### Problem: "Permission denied" on deploy script (macOS/Linux)

**Solution:**
```bash
# Make script executable
chmod +x scripts/deploy-gcp.sh

# Then run it
./scripts/deploy-gcp.sh meadcalculator us-central1
```

### Problem: Still having issues?

**Check logs:**
```bash
# Backend logs
gcloud run logs read meadcalc-api --region us-central1 --limit 100 --project meadcalculator

# Frontend logs
gcloud run logs read meadcalc-frontend --region us-central1 --limit 100 --project meadcalculator
```

**View in Cloud Console:**
1. Go to [Cloud Logging](https://console.cloud.google.com/logs?project=meadcalculator)
2. Filter by service name to see detailed logs

---

## After Deployment

### Monitor Your Services

**View Real-time Logs:**
```bash
gcloud run logs read meadcalc-api \
    --region us-central1 \
    --follow \
    --project meadcalculator
```

**Check Service Status:**
```bash
gcloud run services describe meadcalc-frontend \
    --region us-central1 \
    --project meadcalculator
```

### Update Your Application

**Make code changes and deploy:**

```bash
# Option 1: Push to main (if Cloud Build is set up)
git add .
git commit -m "Your change message"
git push origin main

# Option 2: Run deploy script again
./scripts/deploy-gcp.sh meadcalculator us-central1
```

### View Costs

1. Go to [Cloud Billing](https://console.cloud.google.com/billing?project=meadcalculator)
2. You should see charges (if over free tier)
3. Estimated: $2-10/month for typical usage

---

## Useful Commands Reference

```bash
# List all services
gcloud run services list --region us-central1 --project meadcalculator

# Get specific service URL
gcloud run services describe meadcalc-api --region us-central1 --format='value(status.url)' --project meadcalculator

# View logs
gcloud run logs read meadcalc-api --region us-central1 --limit 50 --project meadcalculator

# Delete a service
gcloud run services delete meadcalc-api --region us-central1 --project meadcalculator

# Update environment variables
gcloud run services update meadcalc-api --update-env-vars KEY=VALUE --region us-central1 --project meadcalculator

# Check build history
gcloud builds list --project meadcalculator

# View a specific build
gcloud builds log [BUILD_ID] --stream --project meadcalculator
```

---

## Summary

You've successfully deployed MeadCalculator! 🎉

**Your services are now running at:**
- **Backend API**: `https://meadcalc-api-[hash].a.run.app`
- **Frontend**: `https://meadcalc-frontend-[hash].a.run.app`

**Next steps:**
1. ✅ Test your services
2. ⏳ Set up Cloud Build for automated deployment
3. 🔒 (Optional) Configure custom domain
4. 📊 Monitor logs and costs in Cloud Console

**For more information:**
- See [GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md) for advanced configuration
- See [GCP-QUICK-START.md](GCP-QUICK-START.md) for quick reference

Happy brewing! 🍯
