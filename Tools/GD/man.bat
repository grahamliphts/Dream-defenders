@echo off
if exist content.xml (
	del content.xml
	echo Del content.xml
)
if exist stats.ods (
    rename stats.ods stats.zip
	"./7-Zip/7z.exe" e stats.zip
	md "..\..\TowerDefense\Assets\Editor\XML" >nul 2>&1
	copy content.xml "..\..\TowerDefense\Assets\Editor\XML"
	echo "Finish"
) else (
    echo "File stats.ods does not exist"
)

pause
