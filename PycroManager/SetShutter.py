import sys
from pycromanager import Core
core = Core()
if sys.argv[2] == "true":
    core.set_shutter_open(sys.argv[1],True)
else:
    core.set_shutter_open(sys.argv[1],False)
print("OK");