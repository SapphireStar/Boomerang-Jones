import threading
import time
import subprocess

def main():
    subprocess.run("conda init cmd.exe;conda activate CPS3320",capture_output=True,shell=True)
    # for i in range(31):
    #     time.sleep(0.1)
    # print('helloworld')

if __name__ == '__main__':
    main()