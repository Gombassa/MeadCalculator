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

# Copy built application to nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Copy nginx configuration
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Remove default nginx config to avoid conflicts
RUN rm -f /etc/nginx/conf.d/default.conf.orig

# Cloud Run requires port 8080
EXPOSE 8080

# Set PORT for Cloud Run compatibility
ENV PORT=8080

# Start nginx in foreground (required for Cloud Run)
CMD ["nginx", "-g", "daemon off;"]
