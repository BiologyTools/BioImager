from pycromanager import Core
import sys
core = Core()
print(core.get_current_config(sys.argv[1]))
print("OK")

