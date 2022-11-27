import Pyro4
import sys
stage = Pyro4.Proxy(sys.argv[1])
x_axis = stage.axes["x"]
y_axis = stage.axes["y"]
x_axis.move_to(float(sys.argv[2]))
y_axis.move_to(float(sys.argv[3]))
stage._pyroRelease()
print("OK")