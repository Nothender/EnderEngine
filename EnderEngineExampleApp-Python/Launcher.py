import platform
import os

# todo : readme file, use mono for linux and or mac

system = platform.system()
if system == "Windows": #The app is running on Windows
    os.system("start IronPython/ipy.exe Main.py")
elif system == "Linux": #The app is running on a Linux distro
    os.system("mono IronPython/ipy.exe Main.py")
elif system == "Darwin": #The app is running on Mac
    os.system("mono IronPython/ipy.exe Main.py")
else: #The os is unknown : unsupported
    print("Unkown/Unsupported os, can't start ender engine")