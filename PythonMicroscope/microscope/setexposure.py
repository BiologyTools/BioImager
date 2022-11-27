import Pyro4
import sys
camera = Pyro4.Proxy(sys.argv[1])
camera.set_exposure_time(float(sys.argv[2]))
camera._pyroRelease()
print("OK")