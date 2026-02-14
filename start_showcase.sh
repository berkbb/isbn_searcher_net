#!/usr/bin/env bash
set -euo pipefail

PROJECT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
APP_URL="http://localhost:5230"
LOG_FILE="/tmp/isbn_showcase.log"

if ! command -v dotnet >/dev/null 2>&1; then
  echo "dotnet bulunamadi. Lutfen .NET SDK kurun."
  exit 1
fi

# Uygulama zaten calisiyorsa sadece ac
if curl -s -o /dev/null "$APP_URL"; then
  if command -v xdg-open >/dev/null 2>&1; then
    xdg-open "$APP_URL" >/dev/null 2>&1 || true
  fi
  echo "Uygulama zaten calisiyor: $APP_URL"
  exit 0
fi

echo "ISBN Searcher Blazor baslatiliyor..."
nohup dotnet run --project "$PROJECT_DIR/ShowCase/ShowCase.csproj" --no-launch-profile --urls "$APP_URL" >"$LOG_FILE" 2>&1 &
APP_PID=$!

for _ in $(seq 1 30); do
  if curl -s -o /dev/null "$APP_URL"; then
    if command -v xdg-open >/dev/null 2>&1; then
      xdg-open "$APP_URL" >/dev/null 2>&1 || true
    fi
    echo "Uygulama acildi: $APP_URL"
    echo "PID: $APP_PID"
    echo "Log: $LOG_FILE"
    exit 0
  fi
  sleep 1
done

echo "Uygulama zamaninda baslamadi. Log: $LOG_FILE"
exit 1
