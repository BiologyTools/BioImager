import Pyro4
import sys
stage = Pyro4.Proxy(sys.argv[0])
x_axis = stage.axes["x"]
y_axis = stage.axes["y"]
print("%d" % x_axis.position)
print("%d" % y_axis.position)
stage._pyroRelease()
print("OK")

