import os
import shutil
import subprocess

def main():

    subprocess.run("E:\\ProgramFiles\\Anaconda\\envs\\CPS3320\\python.exe E:\\GitHub\\Game_Projects\\BaseEnvironment\\yoloV5\yolov5-mask-42-master\\detect.py --weights E:\\GitHub\\Game_Projects\\BaseEnvironment\\yoloV5\yolov5-mask-42-master\\runs\\train\\exp\\weights\\best.pt --source  E:\\GitHub\\Game_Projects\\BaseEnvironment\\yoloV5\\yolov5-mask-42-master\\data\\images\\lego\\lego.jpg --save-txt")
    
    src = "E:\\GitHub\\Game_Projects\\BaseEnvironment\\yoloV5\yolov5-mask-42-master\\runs\\detect\\exp\\labels\\BlockDefine.txt"
    dst = "E:\\GitHub\\Game_Projects\\BaseEnvironment\\Data"
    shutil.copy(src,dst)
    print('Complete')

if __name__ == '__main__':
    main()