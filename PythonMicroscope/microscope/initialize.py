import microscope
import Pyro4
import sys
stage = Pyro4.Proxy(sys.argv[1])
stage.enable()
print("OK")
