import Pyro4
import sys
import queue
import microscope
import microscope.clients
import microscope.simulators
from microscope.simulators import SimulatedCamera
camera = SimulatedCamera()
data_queue = queue.Queue()
camera.set_client(data_queue)
camera.enable()
camera.trigger()
img = data_queue.get()
file = open(sys.argv[1],"wb")
file.write(img.tobytes())
file.close()
camera.disable()
print("OK")
