import cv2

def get_photo():
    cap = cv2.VideoCapture(1)           # 开启摄像头
    f, frame = cap.read()               # 将摄像头中的一帧图片数据保存
    imgFile = 'D:\yoloV5\yolov5-mask-42-master\data\images\lego\lego.jpg'
    cv2.imwrite(imgFile, frame)     # 将图片保存为本地文件
    cap.release()                       # 关闭摄像头


if __name__ == '__main__':
    get_photo()                 # 开启摄像头获取照片
