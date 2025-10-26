# Multi-stage build optimized for Google Cloud Run
# Build stage
FROM node:18-alpine AS build
WORKDIR /app

# Copy package files
COPY package*.json ./

# Install dependencies
RUN npm ci --only=production && \
    npm ci --only=development

# Copy source
COPY . .

# Build for production
RUN npm run build

# Runtime stage - serve with nginx on Cloud Run
FROM nginx:alpine

# Create non-root user for security
RUN addgroup -S nginx && adduser -S nginx -G nginx

# Copy built application
COPY --from=build /app/dist /usr/share/nginx/html

# Copy nginx configuration optimized for Cloud Run and SPA routing
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Cloud Run requires port 8080 for load balancer
EXPOSE 8080

# Health check for Cloud Run
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD wget --quiet --tries=1 --spider http://localhost:8080/ || exit 1

# Start nginx in foreground
CMD ["nginx", "-g", "daemon off;"]
