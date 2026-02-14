#!/bin/zsh
set -e

PROJECT_DIR="/Users/berkbabadogan/Documents/GitHub/isbn_searcher_net"
APP_URL="http://localhost:5230"

cd "$PROJECT_DIR"

echo "ISBN Searcher Blazor baslatiliyor..."

# Uygulama zaten calisiyorsa sadece ac
if curl -s -o /dev/null "$APP_URL"; then
  open "$APP_URL"
  echo "Uygulama zaten calisiyor: $APP_URL"
  exit 0
fi

# Uygulamayi arkaplanda baslat
nohup dotnet run --project "$PROJECT_DIR/ShowCase/ShowCase.csproj" --no-launch-profile --urls "$APP_URL" >/tmp/isbn_showcase.log 2>&1 &
APP_PID=$!

# Uygulamanin ayaga kalkmasini bekle
for i in {1..30}; do
  if curl -s -o /dev/null "$APP_URL"; then
    open "$APP_URL"
    echo "Uygulama acildi: $APP_URL"
    echo "PID: $APP_PID"
    exit 0
  fi
  sleep 1
done

echo "Uygulama zamaninda baslamadi. Log: /tmp/isbn_showcase.log"
exit 1
