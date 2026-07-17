@echo off
title Khoi dong Auto Overtime Scheduler
echo ===================================================
echo   DANG KHOI DONG UNG DUNG XEP LICH TANG CA TU DONG
echo ===================================================
echo.
cd /d "%~dp0OvertimeScheduler"
dotnet run
if %ERRORLEVEL% neq 0 (
    echo.
    echo [LOI] Khong the khoi chay ung dung. Vui long kiem tra lai!
    pause
)
