import Pyro4
import sys
filterwheel = Pyro4.Proxy(sys.argv[0])
filterwheel.position = int(sys.argv[1])
filterwheel._pyroRelease()
print("OK")

