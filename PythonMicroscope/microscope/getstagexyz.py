import Pyro4
import sys
stage = Pyro4.Proxy(sys.argv[1])
x_axis = stage.axes["x"]
y_axis = stage.axes["y"]
z_axis = stage.axes["z"]
print(x_axis.position)
print(y_axis.position)
print(z_axis.position)
stage._pyroRelease()
print("OK")

