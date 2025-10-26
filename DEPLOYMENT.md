# MeadCalculator Deployment Guide

This guide covers deploying MeadCalculator to various environments.

## Quick Links

- **Google Cloud Run (Recommended):** See [GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md)
- **Docker Compose (Local):** See [Docker Deployment](#docker-deployment) below
- **Traditional Server:** See [Traditional Linux Server](#traditional-linux-server) below

## Local Development

### Quick Start
```bash
# Terminal 1: Start Backend
cd MeadCalculator.API
dotnet run
# Runs on https://localhost:5001

# Terminal 2: Start Frontend
cd frontend
npm run dev
# Runs on http://localhost:5173
```

### Development with Hot Reload
- Frontend: Changes automatically refresh in browser
- Backend: Use `dotnet watch run` for automatic restart on code changes

## Docker Deployment

### Build and Run with Docker Compose
```bash
# Build both images
docker-compose build

# Run services
docker-compose up

# Access the application
# Frontend: http://localhost
# API: http://localhost:5000/api
```

### Individual Docker Builds

**Backend:**
```bash
cd MeadCalculator.API
docker build -t meadcalculator-api .
docker run -p 5000:8080 meadcalculator-api
```

**Frontend:**
```bash
cd frontend
docker build -t meadcalculator-frontend .
docker run -p 8080:8080 meadcalculator-frontend
```

## Google Cloud Run (Recommended)

For detailed GCP deployment instructions, see [GCP-DEPLOYMENT.md](GCP-DEPLOYMENT.md).

### Quick Deployment

```bash
# Using provided deployment script
./scripts/deploy-gcp.sh your-gcp-project-id us-central1

# Or PowerShell on Windows
.\scripts\Deploy-GCP.ps1 -ProjectId "your-gcp-project-id"
```

Benefits of Cloud Run:
- ✅ Serverless - pay only for usage
- ✅ Auto-scaling - handles traffic spikes
- ✅ No infrastructure management
- ✅ Integrated monitoring and logging
- ✅ $2.50/month always-free tier
- ✅ Global CDN included
- ✅ Custom domains supported

## Production Deployment

### Azure App Service

#### Backend
```bash
# Install Azure CLI tools
# az login

# Create resource group
az group create --name meadcalc-rg --location eastus

# Create App Service Plan
az appservice plan create --name meadcalc-plan --resource-group meadcalc-rg --sku B1 --is-linux

# Create App Service
az webapp create --resource-group meadcalc-rg --plan meadcalc-plan --name meadcalc-api --runtime "DOTNETCORE|9.0"

# Deploy from git
az webapp up --resource-group meadcalc-rg --name meadcalc-api --runtime "DOTNETCORE|9.0" --sku B1 --os-type linux
```

#### Frontend
```bash
# Build static files
cd frontend
npm run build

# Deploy to Azure Static Web Apps (recommended)
# Or use Azure Blob Storage + CDN

# Update API URL in frontend env config
VITE_API_URL=https://meadcalc-api.azurewebsites.net
```

### Heroku Deployment

```bash
# Install Heroku CLI
# heroku login

# Create app
heroku create meadcalc-api

# Set buildpacks
heroku buildpacks:set https://github.com/jincod/dotnetcore-buildpack.git --app meadcalc-api

# Deploy
git push heroku main
```

### Traditional Linux Server

**Prerequisites:**
- .NET 9.0 Runtime
- Node.js 18+ (for building frontend)
- Nginx (reverse proxy)
- Systemd

**Backend Setup:**
```bash
# SSH to server
ssh user@server

# Clone repository
git clone https://github.com/Gombassa/MeadCalculator.git
cd MeadCalculator

# Build
dotnet publish -c Release

# Create systemd service
sudo nano /etc/systemd/system/meadcalc-api.service
```

**Sample Systemd Service File:**
```ini
[Unit]
Description=MeadCalculator API
After=network.target

[Service]
Type=notify
ExecStart=/home/user/MeadCalculator/MeadCalculator.API/bin/Release/net9.0/MeadCalculator.API
WorkingDirectory=/home/user/MeadCalculator/MeadCalculator.API
Restart=always
User=www-data
Environment="ASPNETCORE_ENVIRONMENT=Production"
Environment="ASPNETCORE_URLS=http://localhost:5000"

[Install]
WantedBy=multi-user.target
```

**Frontend Setup (Nginx):**
```bash
# Build frontend
cd frontend
npm run build

# Copy to web root
sudo cp -r dist/* /var/www/html/

# Configure Nginx (see next section)
```

**Nginx Configuration:**
```nginx
server {
    listen 80;
    server_name meadcalc.example.com;

    root /var/www/html;
    index index.html;

    location / {
        try_files $uri $uri/ /index.html;
    }

    location /api/ {
        proxy_pass http://localhost:5000/api/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
    }

    # SSL configuration with Let's Encrypt
    listen 443 ssl;
    ssl_certificate /etc/letsencrypt/live/meadcalc.example.com/fullchain.pem;
    ssl_certificate_key /etc/letsencrypt/live/meadcalc.example.com/privkey.pem;
}
```

**Enable and Start Services:**
```bash
sudo systemctl enable meadcalc-api
sudo systemctl start meadcalc-api
sudo systemctl restart nginx
```

## Environment Variables

### Backend (.NET)
```bash
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://localhost:5000
CORS_Origins=https://yourdomain.com
```

### Frontend
```bash
VITE_API_URL=https://api.yourdomain.com
```

## Database Integration (Future)

When adding database support:

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update

# Connection string in appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=your-db-server;Database=meadcalc;..."
}
```

## SSL/TLS Certificates

### Let's Encrypt (Recommended)
```bash
# Install certbot
sudo apt-get install certbot python3-certbot-nginx

# Get certificate
sudo certbot certonly --nginx -d meadcalc.example.com

# Auto-renewal
sudo systemctl enable certbot.timer
```

## Monitoring & Logging

### Application Insights (Azure)
```csharp
// In Program.cs
builder.Services.AddApplicationInsightsTelemetry();
```

### Serilog (Self-hosted)
```csharp
// Add to Program.cs
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

## Performance Optimization

### Frontend
- Minified and gzipped (Vite handles this)
- CSS tree-shaking with Tailwind
- Code splitting for routes
- Image optimization

### Backend
- Response caching headers
- CORS properly configured
- Input validation
- Database connection pooling (when DB added)

## Security Considerations

- ✅ HTTPS/TLS in production
- ✅ CORS configured for allowed origins
- ✅ Input validation on backend
- ✅ No sensitive data in logs
- ⚠️ Add authentication when users can save recipes
- ⚠️ Rate limiting for API endpoints
- ⚠️ CSRF protection when forms added

## Backup & Recovery

### Database Backups (when applicable)
```bash
# Daily automated backups
0 2 * * * /usr/local/bin/backup-db.sh
```

### Code Backups
- Git history serves as backup
- Consider weekly full server snapshots

## Troubleshooting

### API Not Responding
```bash
# Check service status
sudo systemctl status meadcalc-api

# View logs
sudo journalctl -u meadcalc-api -f

# Test connectivity
curl http://localhost:5000/api/ingredients
```

### Frontend Build Issues
```bash
# Clear cache and rebuild
rm -rf node_modules dist
npm install
npm run build
```

### CORS Errors
- Check allowed origins in Program.cs
- Verify frontend URL matches CORS policy
- Check browser console for exact error

## Maintenance

### Regular Updates
```bash
# Update .NET
sudo apt-get update
sudo apt-get upgrade dotnet-sdk-9.0

# Update Node dependencies
npm audit
npm update
```

### Monitoring Endpoints
- API health check: `GET /health` (add if needed)
- Frontend status: Check 200 response from index.html
