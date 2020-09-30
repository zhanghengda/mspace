@echo off
echo "begin uninstall python libs"
rem cd /d %~dp0 当前目录
cd /d %~dp0

python -m pip uninstall GDAL-2.2.4-cp36-cp36m-win_amd64.whl -y
pip uninstall opencv_python -y
pip uninstall numpy -y
pip uninstall scipy -y
pip uninstall Pillow -y

