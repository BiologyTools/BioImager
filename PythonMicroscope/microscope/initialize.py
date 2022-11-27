import Pyro4
import sys
import queue
import threading
import microscope
import microscope.clients

camera = microscope.clients.DataClient(sys.argv[1])
filterwheel = Pyro4.Proxy(sys.argv[2])
stage = Pyro4.Proxy(sys.argv[3])
stage.enable()
data_queue = queue.Queue()
pyro_daemon = Pyro4.Daemon()
queue_uri = pyro_daemon.register(data_queue)
camera.set_client(queue_uri)
camera.enable()
data_thread = threading.Thread(target=pyro_daemon.requestLoop, daemon=True)
data_thread.start()
counter = 0
def fetchLoop(self) -> None:
        while True:
            data = self._data_queue.get()
            while not self._data_queue.empty():
                data = self._data_queue.get()
            file = open("Image" + counter,"w")
            counter = counter + 1
            file.write(data.tobytes())
            file.close()

fetch_thread = threading.Thread(target=fetchLoop, daemon=True)
fetch_thread.start()
camera.trigger()
camera.disable()
img = data_queue.get_nowait()
print("OK")



