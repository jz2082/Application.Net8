## set up HTTPS with Nginx in a Docker environment

# Create a directory for your Nginx configuration files and SSL certificates

    mkdir ssl

# Generate self-signed SSL certificates --------------------

    openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ssl/nginx.key -out ssl/nginx.crt -subj "/CN=localhost"

    openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ssl/nginx18.key -out ssl/nginx18.crt -subj "/CN=192.168.10.18"

    openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ssl/nginx20.key -out ssl/nginx20.crt -subj "/CN=192.168.10.20"

    openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ssl/nginx30.key -out ssl/nginx30.crt -subj "/CN=192.168.1.30"

    openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ssl/nginxgld.key -out ssl/nginxgld.crt -subj "/CN=jp78f9b.glddns.com"

# production APP NGINX -------------------------------------

    docker compose -f demoapp_compose.yml up -d
    docker compose -f demoapp_compose.yml down

---

## Angular App on localhost, 192.168.10.20, 192.168.1.30

# 3. DemoApp using DataApi on Docker:

    http://localhost:4203
    http://localhost:4233
    https://angularappnet8.azurewebsites.net/DemoApp
