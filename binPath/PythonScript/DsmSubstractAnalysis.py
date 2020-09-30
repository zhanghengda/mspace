import io
import os
import site
import subprocess
import sys

import gdal
import numpy as np
from gdalconst import *
from osgeo import gdal, ogr
import datetime
starttime = datetime.datetime.now()

# 读取两张影像
inputfile1=sys.argv[1].replace('?',' ')
inputfile2=sys.argv[2].replace('?',' ')
outputfile=sys.argv[3].replace('?',' ')

# inputfile1='F:/TestData/你好 world/beefore-DEM-cut.tif'
# inputfile2='F:/TestData/你好 world/after-DEM-cut.tif'
# outputfile='F:/TestData/你好 world/result123.tif'

middleData=[]
middleDir =[]

dirs=site.getsitepackages()
gdaldir=dirs[0]+'/Scripts'
gdalcalcpy_file=gdaldir+'/gdal_calc.py'
mergepy_file=gdaldir+'/gdal_merge.py'

print('1/10 ,converting  negative value to positive value...')
# 注册驱动

gdal.AllRegister()

gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8","YES")
#打开图像数据集
ds=gdal.Open(inputfile1)
#获取波段
band=ds.GetRasterBand(1)

min = band.GetMinimum()
max = band.GetMaximum()
if not min or not max:
    (min,max) = band.ComputeRasterMinMax(True)
print("min={:.3f}, max={:.3f}".format(min,max))
#负值转正
if(min<-10):
    addfile1=inputfile1.replace('.tif','_add.tif')
    gdal_calc ='python "'+gdalcalcpy_file+'" -A "'+inputfile1+'" --outfile="'+addfile1+'" --calc="A+'+str(-min)+'" --overwrite'
    os.system(gdal_calc)
    inputfile1=addfile1
    middleData.append(addfile1)

# # 注册驱动
# gdal.AllRegister()
#打开图像数据集
ds=gdal.Open(inputfile2)
#获取波段
band=ds.GetRasterBand(1)

min = band.GetMinimum()
max = band.GetMaximum()

if not min or not max:
    (min,max) = band.ComputeRasterMinMax(True)
print("min={:.3f}, max={:.3f}".format(min,max))
#负值转正
if(min<-10):
    addfile1=inputfile1.replace('.tif','_add.tif')
    gdal_calc ='python "'+gdalcalcpy_file+'" -A "'+inputfile2+'" --outfile="'+addfile1+'" --calc="A+'+str(-min)+'" --overwrite'
    os.system(gdal_calc)
    inputfile2=addfile1
    middleData.append(addfile1)

print('2/10 ,converting  float32 type to 8bit...')
#转成citymaker可识别的8bit数据
singlebitfile1=inputfile1.replace('.tif','_8bit1.tif')
tran2Byte= 'gdal_translate --config GDAL_FILENAME_IS_UTF8 NO -a_nodata 0.0 -ot Byte -of GTiff  "'+inputfile1+'" "'+singlebitfile1  +'"'
os.system(tran2Byte)
middleData.append(singlebitfile1)

#转成citymaker可识别的8bit数据
singlebitfile12=inputfile2.replace('.tif','_8bit2.tif')
tran2Byte= 'gdal_translate --config GDAL_FILENAME_IS_UTF8 NO -a_nodata 0.0 -ot Byte -of GTiff  "'+inputfile2+'"  "'+singlebitfile12  +'"'
os.system(tran2Byte)
middleData.append(singlebitfile12)

print('3/10 ,resample two to the same size...')
# 获取当前文件的父目录
father_path = os.path.dirname(inputfile1)
reSamplefile=father_path+'/raster_resample.tif'
middleData.append(reSamplefile)
middleData.append(reSamplefile+'.ovr')

inputfile=''
#注册删除驱动
gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8","YES")
gdal.AllRegister()
#打开dsm影像
ods1=gdal.Open(singlebitfile1)
ods2=gdal.Open(singlebitfile12)
#获取dsm图像大小
w1=ods1.RasterXSize
h1=ods1.RasterYSize

w2=ods2.RasterXSize
h2=ods2.RasterYSize
w=300
h=300
if(w1<=w2):
    w=w1
    h=h1
    inputfile=singlebitfile12
    singlebitfile1=singlebitfile1
    singlebitfile12=reSamplefile
else:
    w=w2
    h=h2
    inputfile=singlebitfile1
    singlebitfile1=singlebitfile12
    singlebitfile12=reSamplefile

#获取最大最小值
brand1=ods1.GetRasterBand(1)
min1 = brand1.GetMinimum()
max1 = brand1.GetMaximum()
if not min1 or not max1:
    (min1,max1) = brand1.ComputeRasterMinMax(True)
print("Min1={:.3f}, Max1={:.3f}".format(min1,max1))

brand2=ods2.GetRasterBand(1)
min2 = brand2.GetMinimum()
max2 = brand2.GetMaximum()
if not min2 or not max2:
    (min2,max2) = brand2.ComputeRasterMinMax(True)
print("Min2={:.3f}, Max2={:.3f}".format(min2,max2))

min=min1 if min1<=min2 else min2
max=max1 if max1>=max2 else max2

print("min={:.3f}, max={:.3f}".format(min,max))

# 将两张影像进行同尺寸重采样
print('creating reSample...')
subprocess.call('gdal_translate --config GDAL_FILENAME_IS_UTF8 NO -of GTiff -outsize '+str(w)+' '+str(h)+' -r bilinear "'+inputfile+'"  "'+reSamplefile+'"')

#创建金字塔
print('4/, creating pyramids...')
gdaladdo= 'gdaladdo --config GDAL_FILENAME_IS_UTF8 NO -r average -ro "'+reSamplefile+'" 2 4 8 16'  
os.system(gdaladdo)

# 两张影像进行切块
# 第二种方式
print('5/10, retile tif Image to small...')
spitdir = os.path.dirname(inputfile)
comparedirFile=''
if(inputfile==singlebitfile1):
    comparedirFile=singlebitfile12
else:
     comparedirFile=singlebitfile1
print('reSamplefile= '+reSamplefile)
comparedir1=reSamplefile.replace('.tif','')
if(os.path.exists(comparedir1)==False):
    os.makedirs(comparedir1)
comparedir2=comparedirFile.replace('.tif','')
if(os.path.exists(comparedir2)==False):
    os.makedirs(comparedir2)

print('spitdir= '+spitdir)

print('comparedir1= '+comparedir1)

gdalretile_file=gdaldir+'/gdal_retile.py'

gdal_retile1 ='python "'+gdalretile_file+'" -ps 5000 5000 -co "ALPHA=YES" -r bilinear -targetDir "'+comparedir1+'" "'+reSamplefile+'"'
os.system(gdal_retile1)

gdal_retile2 ='python "'+gdalretile_file+'" -ps 5000 5000 -co "ALPHA=YES" -r bilinear -targetDir "'+comparedir2+'" "'+comparedirFile+'"'
os.system(gdal_retile2)

# 各切块进行计算
print('6/10, calc difference for each small image...')
comparedirFile=''
if(inputfile==singlebitfile1):
    comparedirFile=singlebitfile12
else:
     comparedirFile=singlebitfile1
print('reSamplefile= '+reSamplefile)
comparedir1=reSamplefile.replace('.tif','')
comparedir2=comparedirFile.replace('.tif','')
middleDir.append(comparedir1)
middleDir.append(comparedir2)

print('spitdir= '+spitdir)
print('comparedir1= '+comparedir1)

resultDir=father_path+'/result'
if(os.path.exists(resultDir)==False):
    os.makedirs(resultDir)
middleDir.append(resultDir)

#读取对比图像文件夹1的tif文件
print(comparedir1)
files1=[]
for filename in os.listdir(comparedir1):
    print(filename)
    files1.append(comparedir1+'/'+filename)

#读取分割文件夹2的tif文件
print(comparedir2)
files2=[]
for filename in os.listdir(comparedir2):
    print(filename)
    files2.append(comparedir2+'/'+filename)
#结果文件夹名称+result文件
index=0
for file in files1:
    outputResfile=resultDir+'/result'+str(index)+'.tif'
    gdal_calc ='python "'+gdalcalcpy_file+'" -A "'+files1[index]+'" -B "'+files2[index]+'" --outfile="'+outputResfile+'" --calc="A-B"  --overwrite'
    print(gdal_calc)
    os.system(gdal_calc)
    index+=1    

# 计算结果进行合并
print('7/10, mearge images...')
def listDir(rootDir):
    filenames=[]
    for filename in os.listdir(rootDir):
        pathname = os.path.join(rootDir, filename)
        if (os.path.isfile(filename)):
            print(pathname)
            filenames.append(pathname)
    return filenames          

def getfilelist(filepaths):
    files=''
    for item in filepaths:
        files+=' '+item
    return files

rootdir=resultDir

mergeImg_file=outputfile.replace('.tif','_merge.tif')

print(rootdir)
files=''
for filename in os.listdir(rootdir):
    print(filename)
    files+=' "'+rootdir+'/'+filename+'"'

if(os.path.exists(mergeImg_file)):
    os.remove(mergeImg_file)
print(files)
middleData.append(mergeImg_file)
middleData.append(mergeImg_file+'.ovr')

gdal_merge ='python "'+mergepy_file+'"  -o "'+mergeImg_file+'" '+files
os.system(gdal_merge)


#创建金字塔
print('8/10, creating pyramids...')
gdaladdo= 'gdaladdo --config GDAL_FILENAME_IS_UTF8 NO -r average -ro "'+mergeImg_file+'" 2 4 8 16'  
os.system(gdaladdo)

#处理颜色
print('9/10, color relief for result...')
colortxtfile=os.path.dirname(os.path.realpath(__file__))+'/color_relief.txt'
print(colortxtfile)
pycolorrelief='gdaldem --config GDAL_FILENAME_IS_UTF8 NO color-relief "'+mergeImg_file+'" "'+colortxtfile+'"  "'+outputfile+'" -alpha -of GTiff -b 1'  #加双引号处理空格
os.system(pycolorrelief)

#创建金字塔
print('10/10, creating pyramids...')
gdaladdo= 'gdaladdo --config GDAL_FILENAME_IS_UTF8 NO -r average -ro "'+outputfile+'"  2 4 8 16'  
os.system(gdaladdo)

endtime = datetime.datetime.now()
print(str((endtime - starttime).seconds)+'s')

del ds,ods1,ods2

for item in middleData:
    if(os.path.exists(item)):
        # os.remove(item)
        temp = 'del "'+item+'"'
        os.system(temp.replace('/','\\'))

for dire in middleDir:
    # if(os.path.exists(dire)):
    temp = 'rmdir /s/q "'+dire.replace('/','\\') +'"'
    os.system(temp)