FROM node:18.16.0-alpine3.17

RUN apk add --no-cache bash

RUN addgroup -S usuariocomum && adduser -S usuariocomum -G usuariocomum -s /bin/bash

WORKDIR /app

RUN mkdir /app/node_modules && chown usuariocomum:usuariocomum /app/node_modules

COPY --chown=usuariocomum:usuariocomum package*.json ./

RUN npm i npm && npm ci

COPY --chown=usuariocomum:usuariocomum . .

USER usuariocomum

EXPOSE 3000

CMD ["npm", "start"]
