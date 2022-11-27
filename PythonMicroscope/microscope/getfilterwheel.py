import Pyro4
import sys
filterwheel = Pyro4.Proxy(sys.argv[0])
print("%d" % filterwheel.position)
filterwheel._pyroRelease()
print("OK")

