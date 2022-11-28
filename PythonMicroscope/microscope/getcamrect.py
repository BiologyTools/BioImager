import sys
import microscope
import microscope.clients
import microscope.simulators
from microscope.simulators import SimulatedCamera
cam = SimulatedCamera()
roi = cam._get_roi()
print(roi.width)
print(roi.height)
print("OK")