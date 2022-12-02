import Pyro4
import sys
import microscope
camera = Pyro4.Proxy(sys.argv[1])
roi = camera.get_roi()
print(roi[2])
print(roi[3])
print("OK")