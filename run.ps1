docker build . -f .\src\Dockerfile -t pastabin:dev
docker run -p 8037:80 -p 8036:443 -d pastabin:dev