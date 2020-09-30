# -*- coding:utf-8 -*-

import subprocess
import gdal
import os
import sys
import site

# read gdal file 
dirs=site.getsitepackages()
gdaldir=dirs[0]+'/Scripts'

inputfile=sys.argv[1].replace('?',' ')
outputfile=sys.argv[2].replace('?',' ')

# inputfile='F:/TestData/world/XuCun.tif'
# outputfile='F:/TestData/world/thumb265.png'

gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8","YES")
gdal.AllRegister()

dom = gdal.Open(inputfile)
bang = dom.GetRasterBand(1)

width = 1920
height = 1080
# 将两张影像进行同尺寸重采样
print('creating reSample...')
subprocess.call('gdal_translate  --config GDAL_FILENAME_IS_UTF8 NO -of GTiff -outsize '+str(width)+' '+str(height)+' -r bilinear "'+inputfile+'"  "'+outputfile+'"')