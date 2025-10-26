#!/bin/bash

# Google Cloud Run Deployment Script for MeadCalculator
# Usage: ./scripts/deploy-gcp.sh [project-id] [region]

set -e

# Configuration
PROJECT_ID="${1:-}"
REGION="${2:-us-central1}"
REPOSITORY="meadcalc-repo"
API_SERVICE="meadcalc-api"
FRONTEND_SERVICE="meadcalc-frontend"

# Color output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Validate inputs
if [ -z "$PROJECT_ID" ]; then
    echo -e "${RED}Error: Project ID is required${NC}"
    echo "Usage: ./scripts/deploy-gcp.sh <project-id> [region]"
    echo "Example: ./scripts/deploy-gcp.sh my-gcp-project us-central1"
    exit 1
fi

echo -e "${GREEN}MeadCalculator - Google Cloud Run Deployment${NC}"
echo "=========================================="
echo "Project ID: $PROJECT_ID"
echo "Region: $REGION"
echo "Repository: $REPOSITORY"
echo ""

# Step 1: Authenticate
echo -e "${YELLOW}Step 1: Authenticating with Google Cloud...${NC}"
gcloud auth login
gcloud config set project $PROJECT_ID

# Step 2: Create Artifact Registry repository (if it doesn't exist)
echo -e "${YELLOW}Step 2: Creating Artifact Registry repository...${NC}"
gcloud artifacts repositories create $REPOSITORY \
    --repository-format=docker \
    --location=$REGION \
    --description="MeadCalculator Docker images" \
    2>/dev/null || echo "Repository already exists"

# Step 3: Configure Docker authentication
echo -e "${YELLOW}Step 3: Configuring Docker authentication...${NC}"
gcloud auth configure-docker $REGION-docker.pkg.dev

# Step 4: Build and push backend image
echo -e "${YELLOW}Step 4: Building and pushing backend image...${NC}"
docker build \
    -t $REGION-docker.pkg.dev/$PROJECT_ID/$REPOSITORY/$API_SERVICE:latest \
    -f MeadCalculator.API/Dockerfile \
    .

docker push $REGION-docker.pkg.dev/$PROJECT_ID/$REPOSITORY/$API_SERVICE:latest

# Step 5: Build and push frontend image
echo -e "${YELLOW}Step 5: Building and pushing frontend image...${NC}"
docker build \
    -t $REGION-docker.pkg.dev/$PROJECT_ID/$REPOSITORY/$FRONTEND_SERVICE:latest \
    -f frontend/Dockerfile \
    frontend/

docker push $REGION-docker.pkg.dev/$PROJECT_ID/$REPOSITORY/$FRONTEND_SERVICE:latest

# Step 6: Deploy backend to Cloud Run
echo -e "${YELLOW}Step 6: Deploying backend to Cloud Run...${NC}"
gcloud run deploy $API_SERVICE \
    --image $REGION-docker.pkg.dev/$PROJECT_ID/$REPOSITORY/$API_SERVICE:latest \
    --region $REGION \
    --platform managed \
    --allow-unauthenticated \
    --memory 512Mi \
    --cpu 1 \
    --timeout 3600 \
    --set-env-vars ASPNETCORE_ENVIRONMENT=Production

# Get backend service URL
API_URL=$(gcloud run services describe $API_SERVICE --region $REGION --format='value(status.url)')
echo -e "${GREEN}Backend deployed at: $API_URL${NC}"

# Step 7: Deploy frontend to Cloud Run
echo -e "${YELLOW}Step 7: Deploying frontend to Cloud Run...${NC}"
gcloud run deploy $FRONTEND_SERVICE \
    --image $REGION-docker.pkg.dev/$PROJECT_ID/$REPOSITORY/$FRONTEND_SERVICE:latest \
    --region $REGION \
    --platform managed \
    --allow-unauthenticated \
    --memory 256Mi \
    --cpu 1 \
    --timeout 3600

# Get frontend service URL
FRONTEND_URL=$(gcloud run services describe $FRONTEND_SERVICE --region $REGION --format='value(status.url)')
echo -e "${GREEN}Frontend deployed at: $FRONTEND_URL${NC}"

# Step 8: Summary
echo ""
echo -e "${GREEN}Deployment Complete!${NC}"
echo "=========================================="
echo -e "Backend API:  ${GREEN}$API_URL${NC}"
echo -e "Frontend:     ${GREEN}$FRONTEND_URL${NC}"
echo ""
echo "Next steps:"
echo "1. Test the backend API:"
echo "   curl $API_URL/api/ingredients"
echo ""
echo "2. Visit the frontend:"
echo "   open $FRONTEND_URL"
echo ""
echo "3. To view logs:"
echo "   gcloud run logs read $API_SERVICE --region $REGION --limit 50"
echo "   gcloud run logs read $FRONTEND_SERVICE --region $REGION --limit 50"
echo ""
echo "4. To manage services:"
echo "   gcloud run services list --region $REGION"
echo ""
