import threading
import time
import subprocess
import os



def main():

    p1= os.system("E:\\ProgramFiles\\Anaconda\\envs\\CPS3320\\python.exe detect.py \
        --weights runs\\train\\exp\\weights\\best.pt \
            --source  data\\images\\lego\\lego.jpg \
                --save-txt")
    print(p1)

if __name__ == '__main__':
    main()