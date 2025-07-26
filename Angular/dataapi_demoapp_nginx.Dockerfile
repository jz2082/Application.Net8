### STAGE 1: Build ###
FROM node:22.12.0-alpine AS build
WORKDIR /usr/src/app
COPY ./DemoApp/ .
RUN npm install
RUN npm install @angular/cli@18.2
RUN npm run build-dockerdataapi

### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
COPY dataapi_demoapp_nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build /usr/src/app/dist/demoapp/browser /usr/share/nginx/html