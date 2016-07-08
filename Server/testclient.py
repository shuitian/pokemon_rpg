#-*- coding:utf-8 –*-
import socket,time,json,sys
import server
import multiprocessing

def send_message(sock,str):
	sock.sendall(str)

def send_exit_message(sock):
	d = {"type":"exit"}
	send_message(sock,json.dumps(d))

def check_server_on(ip, port):
	s=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
	flag = True
	try:
		s.connect((ip,port))
	except Exception, e:
		flag = False
		print e
	finally:
		# s.close()
		pass
	return (flag,s)

def worker():
    print "worker"
    r = server.my_reactor()
    r.listenTCP(10019, server.MyFactory(r))
    r.run()

if __name__ == "__main__":
	print sys.argv
	if len(sys.argv) == 1:
		ip = '127.0.0.1'
		port = 10019
		mode = "restart"
	elif len(sys.argv) == 3:
		ip = sys.argv[1]
		port = int(sys.argv[2])
		mode = "restart"
	elif len(sys.argv) == 4:
		ip = sys.argv[1]
		port = int(sys.argv[2])
		mode = sys.argv[3]
	print ip,port,mode
	(flag,s) = check_server_on(ip,port)
	print flag
	if flag and mode == "start":
		print "Server is already running!"
		exit(0);
	elif (not flag) and mode == "stop":
		print "Server is not running!"
		exit(0)
	elif flag:
		print "Server is running, send exit message!"
		send_exit_message(s)

	if mode != "stop":
		print "Start server!"

		p1 = multiprocessing.Process(target = worker)
		p1.start()
