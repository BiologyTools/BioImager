import Pyro4
import sys
stage = Pyro4.Proxy(sys.argv[1])
stage.enable()
print(sys.argv)
x_axis = stage.axes["x"]
y_axis = stage.axes["y"]
z_axis = stage.axes["z"]
x_axis.move_to(float(sys.argv[2]))
y_axis.move_to(float(sys.argv[3]))
z_axis.move_to(float(sys.argv[4]))
stage._pyroRelease()
print("OK")
