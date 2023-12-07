from pycromanager import Core
import sys
core = Core()
core.set_xy_position(float(sys.argv[1]),float(sys.argv[2]))
core.set_position(float(sys.argv[3]))
print("OK")
