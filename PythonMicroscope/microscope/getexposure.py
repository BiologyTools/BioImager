import sys
import microscope
import microscope.clients
import microscope.simulators
from microscope.simulators import SimulatedCamera
exp = SimulatedCamera().get_exposure_time()
print(exp)
print("OK")