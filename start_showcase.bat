@echo off
setlocal

set "PROJECT_DIR=%~dp0"
set "APP_URL=http://localhost:5230"
set "LOG_FILE=%TEMP%\isbn_showcase.log"

where dotnet >nul 2>nul
if errorlevel 1 (
  echo dotnet bulunamadi. Lutfen .NET SDK kurun.
  pause
  exit /b 1
)

REM Uygulama zaten calisiyor mu kontrol et
powershell -NoProfile -Command "try { $r = Invoke-WebRequest -Uri '%APP_URL%' -Method Head -TimeoutSec 2; exit 0 } catch { exit 1 }"
if not errorlevel 1 (
  start "" "%APP_URL%"
  echo Uygulama zaten calisiyor: %APP_URL%
  exit /b 0
)

echo ISBN Searcher Blazor baslatiliyor...
start "ISBN Searcher" /min cmd /c "dotnet run --project \"%PROJECT_DIR%ShowCase\ShowCase.csproj\" --no-launch-profile --urls %APP_URL% > \"%LOG_FILE%\" 2>&1"

REM En fazla 30 saniye boyunca ayaga kalkmasini bekle
for /L %%i in (1,1,30) do (
  powershell -NoProfile -Command "try { $r = Invoke-WebRequest -Uri '%APP_URL%' -Method Head -TimeoutSec 2; exit 0 } catch { exit 1 }"
  if not errorlevel 1 (
    start "" "%APP_URL%"
    echo Uygulama acildi: %APP_URL%
    echo Log: %LOG_FILE%
    exit /b 0
  )
  timeout /t 1 /nobreak >nul
)

echo Uygulama zamaninda baslamadi. Log: %LOG_FILE%
pause
exit /b 1
