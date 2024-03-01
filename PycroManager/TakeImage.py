from pycromanager import Core
import sys
core = Core()
core.snap_image()
ch = core.get_number_of_camera_channels()
for x in range(ch):
	open(sys.argv[1] + str(x), 'wb').write(core.get_image(x))
print(core.get_image_width())
print(core.get_image_height())
print(core.get_image_bit_depth())
print(core.get_bytes_per_pixel())
print(ch)
print("OK");