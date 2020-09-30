import numpy as np
import cv2
import tkinter as tk
from tkinter import *
from PIL import Image, ImageTk


inputfile=sys.argv[1].replace('?',' ')
# inputfile='F:/TestData/你好 world/liugangqiao.mp4'
cap = cv2.VideoCapture(inputfile)
kernel_2 = np.ones((2,2),np.uint8)#2x2的卷积核
kernel_3 = np.ones((3,3),np.uint8)#3x3的卷积核
kernel_4 = np.ones((4,4),np.uint8)#
#视频宽度
width=int(cap.get(cv2.CAP_PROP_FRAME_WIDTH))
#视频高度
print(width)
height = int(cap.get(cv2.CAP_PROP_FRAME_HEIGHT))
print(height)
#视频帧率
fps = cap.get(cv2.CAP_PROP_FPS)
print(fps)
#视频的编码
fourcc = int(cap.get(cv2.CAP_PROP_FOURCC))  #cv2.VideoWriter_fourcc(*'XVID')
print(fourcc)
out = cv2.VideoWriter('out.mp4', fourcc, 20, (width,height))


#Set up GUI
window=tk.Tk()
window.title("Video")
#Graphics window

w1=window.winfo_screenwidth()
h1=window.winfo_screenheight()
w1=int(w1*0.6)
h1=int(h1*0.6)
#window.geometry(str(w1)+'×'+str(h1))
imageFrame = tk.Frame(window, width=w1, height=h1)
window.geometry(str(w1)+'x'+str(h1))
imageFrame.grid(row=0, column=0, padx=10, pady=2)

#Capture video frames
lmain = tk.Label(imageFrame)
lmain.grid(row=0, column=0)



def resize(w, h, w_box, h_box, pil_image):  
	f1 = 1.0*w_box/w # 1.0 forces float division in Python2  
	f2 = 1.0*h_box/h  
	factor = min([f1, f2])  
  #print(f1, f2, factor) # test  
  # use best down-sizing filter  
	width = int(w*factor)
	height =h*factor 
	return pil_image.resize((width, height), Image.ANTIALIAS)  
def show_frame():
	(ret,frame)=cap.read()
	hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
	lower_blue=np.array([200/2, 30*255/100, 70*255/100])
	upper_blue=np.array([215/2, 70*255/100, 95*255/100])
	#lower_blue=np.array([100,50,50])
	#upper_blue=np.array([130,255,255])
	mask=cv2.inRange(hsv,lower_blue,upper_blue)
	dilation = cv2.dilate(mask,kernel_4,iterations = 1)
	ret, binary = cv2.threshold(dilation,127,255,cv2.THRESH_BINARY) 
	_, contours, hierarchy = cv2.findContours(binary,cv2.RETR_EXTERNAL,cv2.CHAIN_APPROX_SIMPLE)
	p=0
	for i in contours:#遍历所有的轮廓
		x,y,w,h = cv2.boundingRect(i)#将轮廓分解为识别对象的左上角坐标和宽、高
   #在图像上画上矩形（图片、左上角坐标、右下角坐标、颜色、线条宽度）
		if w>15 and h>15:
			cv2.rectangle(frame,(x,y),(x+w,y+h),(0,100,255),4)
		   #给识别对象写上标号
			font=cv2.FONT_HERSHEY_SIMPLEX
			x0=(x+w)/2
			y0=(y+h)/2
		   #str2='('+str(x0)+','+str(y0)+')'
			str1 = str(p+1)
			str2='('+str1+'  '+str(x)+'  '+str(y)+'  '+str(w)+'  '+str(h)+'\n'+')'
			#cv2.putText(frame,str1,(x,y-5), font, 1,(100,100,255),2)#加减10是调整字符位置
			p+=1

	out.write(frame)
	window.update()
	w_box=window.winfo_width()
	h_box=window.winfo_height()
	frame2 = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
	#frame_res = resize(width, height, w_box, h_box, frame2)  
	frame_res = cv2.resize(frame2,(w_box,h_box))
	img = Image.fromarray(frame_res)
	imgtk = ImageTk.PhotoImage(image=img)
	lmain.imgtk = imgtk
	lmain.configure(image=imgtk)
	lmain.after(5, show_frame) 
#Slider window (slider controls stage position)
sliderFrame = tk.Frame(window, width=0, height=0)
sliderFrame.grid(row = 0, column=0, padx=10, pady=2)


show_frame()  #Display 2
window.mainloop()  #Starts GUI
cap.release()
out.release()