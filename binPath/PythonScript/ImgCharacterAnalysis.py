# -*- coding: utf-8 -*-
"""
Created on Tue Jul 17 13:48:46 2018
@author: WangYan
Change:2018/8/29
"""
import io
import os
import sys
import cv2
import site
import subprocess
import numpy as np
from osgeo import gdal, gdalconst, osr
import shutil
from multiprocessing import Process

# 读取命令行参数
if len(sys.argv) == 3:
	infile = sys.argv[1].replace('?',' ')
	outfile = sys.argv[2].replace('?',' ')
else:
	print('参数个数不符合要求，提示：文件名（.tif） 输出文件名(.tif)')
	exit()
# infile='G:/GisData/shilong/正射/shilongshequ_resample.tif'
# outfile='G:/GisData/shilong/正射/shilongshequ_resample123.tif'
# infile='F:/TestData/你好 world/luoxing.tif'
# outfile='F:/TestData/你好 world/luoxing123.tif'

# gdal脚本路径
dirs=site.getsitepackages()
gdaldir=dirs[0]+'/Scripts'
gdalcalcpy_file=gdaldir+'/gdal_calc.py'
mergepy_file=gdaldir+'/gdal_merge.py'

# 中间文件保存路径
filedir = os.path.dirname(infile)
tiledir = os.path.join(filedir,'tile')
if(os.path.exists(tiledir)==False):
	os.makedirs(tiledir)
tempdir=os.path.join(filedir,'temp')
if (os.path.exists(tempdir)==False):
	os.makedirs(tempdir)
outdir=os.path.join(filedir,'out')
if (os.path.exists(outdir)==False):
	os.makedirs(outdir)
coorddir=os.path.join(filedir,'coord')
if (os.path.exists(coorddir)==False):
	os.makedirs(coorddir)

gdalretile_file=gdaldir+'/gdal_retile.py'
gdal_retile ='python "'+gdalretile_file+'" -ps 5000 5000 -co "ALPHA=YES" -r bilinear -targetDir "'+ tiledir +'"  "'+infile+'"'
os.system(gdal_retile)
print('----------裁剪完成---------------')

list = os.listdir(tiledir)
for i in range(0,len(list)):
	if os.path.splitext(list[i])[1] == '.tif':
		infile_1=os.path.join(tiledir,list[i])
		tempfile=os.path.join(tempdir,os.path.splitext(list[i])[0]+'-temp.tif')
		outfile_1=os.path.join(outdir,os.path.splitext(list[i])[0]+'-out.tif')
		file = os.path.join(coorddir,os.path.splitext(list[i])[0]+'coord.txt')
		f = open(file,'w')
		
		# img = cv2.imread(infile_1)
		img = cv2.imdecode(np.fromfile(infile_1,dtype=np.uint8),-1)
		if img.shape[2]==4:
			img=img[:,:,:3].copy()
		elif img.shape[2]==3:
			img=img.copy()
		else:
			print("the breand of picture can not be processd")
			exit()
		kernel_4 = np.ones((4,4),np.uint8)#4x4的卷积核

		hue_image = cv2.cvtColor(img, cv2.COLOR_BGR2HSV)

		low_range = np.array([200/2, 25*255/100, 75*255/100])
		high_range = np.array([215/2, 65*255/100, 96*255/100])
		mask = cv2.inRange(hue_image, low_range, high_range)

		dilation = cv2.dilate(mask,kernel_4,iterations = 1)

		# cv2.imwrite(tempfile,mask)
		# mask=cv2.imread(tempfile)
		cv2.imencode('.tif',mask)[1].tofile(tempfile)
		mask = cv2.imdecode(np.fromfile(tempfile,dtype=np.uint8),1)
		#target是把原图中的非目标颜色区域去掉剩下的图像
		target = cv2.bitwise_and(img, mask)

		ret, binary = cv2.threshold(dilation,127,255,cv2.THRESH_BINARY) 

		#在binary中发现轮廓，轮廓按照面积从小到大排列
		_, contours, hierarchy = cv2.findContours(binary,cv2.RETR_EXTERNAL,cv2.CHAIN_APPROX_SIMPLE)

		p=0
		for i in contours:#遍历所有的轮廓
		   x,y,w,h = cv2.boundingRect(i)#将轮廓分解为识别对象的左上角坐标和宽、高
		   #在图像上画上矩形（图片、左上角坐标、右下角坐标、颜色、线条宽度）
		   if w<25 and h<25:
		      continue
		   else:
		      cv2.rectangle(img,(x,y),(x+w,y+h),(0,100,255),8)
		   #给识别对象写上标号
		      font=cv2.FONT_HERSHEY_SIMPLEX
		      x0=(x+w)/2
		      y0=(y+h)/2
		   #str2='('+str(x0)+','+str(y0)+')'
		      str2 = str(p+1)
		      f.writelines(str2+'  '+str(x)+'  '+str(y)+'  '+str(w)+'  '+str(h)+'\n')
		      #cv2.putText(img,str2,(x,y-5), font, 1,(100,100,255),2)#加减10是调整字符位置
		   p+=1
		f.close()

		cv2.imencode('.tif',img)[1].tofile(outfile_1)
		# mask = cv2.imdecode(np.fromfile(tempfile,dtype=np.uint8),1)
		# cv2.imwrite(outfile_1, img)#将画上矩形的图形保存到当前目录  

		#设置坐标参考系统
		gdal.SetConfigOption("GDAL_FILENAME_IS_UTF8","YES")
		#注册驱动
		gdal.AllRegister()
		#打开Dom影像
		ods1=gdal.Open(infile_1,gdal.GA_ReadOnly)
		#更新影像
		ods2=gdal.Open(outfile_1,gdal.GA_Update)
		# 设置投影
		srs=ods1.GetProjectionRef()
		ods2.SetProjection(srs)
		#设置六参数
		geotransform=ods1.GetGeoTransform()
		ods2.SetGeoTransform(geotransform)
		# 使用FlushCache将数据写入文件
		ods2.FlushCache()
		print(i)

del infile_1,ods2,ods1

print('-----合并所有的outfile------')

def listdir(rootdir):
	filenames=[]
	for filename in os.listdir(rootdir):
		pathname = os.path.join(rootdir,filename)
		if (os.path.isfile(filename)):
			print(pathname)
			filenames.append(pathname)
	return filenames

def getfilelist(filepaths):
    files=''
    for item in filepaths:
        files+=' "'+item+'"'
    return files

files = ''
for filename in os.listdir(outdir):
	print(filename)
	files+=' "'+outdir+'/'+filename+'"'
gdal_merge = 'python "' + mergepy_file +'" -pct -of GTiff' + '  -o "' + outfile+'" '+ files
os.system(gdal_merge)
del files
print('---------合并完成---------')

#创建金字塔
print('---------创建金字塔----------')
gdaladdo= 'gdaladdo --config GDAL_FILENAME_IS_UTF8 NO -r average -ro "'+outfile+'" 2 4 8 16'  
os.system(gdaladdo)
print('---------金字塔创建成功---------')

# 删除中间文件
def remove_dir(dir_name):
	if os.path.exists(dir_name):
		shutil.rmtree(dir_name)
	else:
		print('no such dir:%s'%dir_name)

remove_dir(tiledir)
remove_dir(outdir)
remove_dir(tempdir)
remove_dir(coorddir)
print('-------执行完成--------')