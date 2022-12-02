import microscope
import Pyro4
import sys
stage = Pyro4.Proxy("SimulatedStage@127.0.0.1")
stage.enable()
print("OK")
