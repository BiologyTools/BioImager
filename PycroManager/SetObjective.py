from pycromanager import Core
import sys
core = Core()
core.set_config(sys.argv[1], sys.argv[2])
print("OK")

