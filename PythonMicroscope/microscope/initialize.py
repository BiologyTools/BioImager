import Pyro4
import sys
stage = Pyro4.Proxy(sys.argv[2])
stage.enable()
print("OK")
