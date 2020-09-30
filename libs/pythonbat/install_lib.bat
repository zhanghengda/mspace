@echo off
echo "begin install python libs"
rem cd /d %~dp0 µ±Ç°Ä¿Â¼
cd /d %~dp0

python -m pip install --upgrade pip
pip install -i https://pypi.tuna.tsinghua.edu.cn/simple numpy==1.15.2
pip install -i https://pypi.tuna.tsinghua.edu.cn/simple scipy==1.1.0
pip install GDAL-2.2.4-cp36-cp36m-win_amd64.whl
pip install -i https://pypi.tuna.tsinghua.edu.cn/simple Pillow==5.3.0
pip install -i https://pypi.tuna.tsinghua.edu.cn/simple opencv_python==3.4.3.18



