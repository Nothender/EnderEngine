#
# This is where your application begins, and where you can write your code
# 
# To run your project you must run `launcher.py`. If you have IronPython you can run this file directly with it
#
import Includes
import EnderEngine
import time

#Nothing is quite working in here...
engine = EnderEngine.Engine()
engine.logger.Log("test", EnderEngine.Logger.LogLevel.FATAL);

input("Program ended. press enter to exit or close the window. ")

# Console/File logging, global value -> call override
# Disable logging levels