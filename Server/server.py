#-*- coding:utf-8 –*-
from twisted.internet import reactor
from twisted.internet.protocol import Protocol, Factory
import sqldata,json,sys

s = sqldata.sql()
class SimpleLogger(Protocol, object):
	def __init__(self, _reactor):
		super(SimpleLogger, self).__init__()
		self.reactor = _reactor

	def connectionMade(self):
		print 'Got connection from', self.transport.client
		# memberList[]
		# self.transport.write("0"+'\n'.join(self.memberList))
		# self.transport.write("456\n")

	def connectionLost(self, reason):
		print self.transport.client, 'disconnected'
		# for x in memberList.keys():
		# 	if memberList[x] == self.transport:
		# 		del memberList[x]
		# for x in memberList.values():
		# 	x.write("0"+'\n'.join(memberList))

	def dataReceived(self, data):
		message = sqldata.get_json_from_message(self, s, data)
		# print message
		self.transport.write(message)
		# print data
		# if data[0] == '0':
		# 	memberList[data[1:len(data)]] = self.transport
		# 	print memberList
		# 	# self.memberList.append(data[1:len(data)])
		# 	for x in memberList.values():
		# 		x.write("0"+'\n'.join(memberList))
		# else:
		# 	for x in memberList.values():
		# 		x.write(data)

class MyFactory(Factory, object):
	def __init__(self, _reactor):
		super(MyFactory, self).__init__()
		self.reactor = _reactor

	def buildProtocol(self, addr):
		return SimpleLogger(self.reactor)

class my_reactor(object):
	"""docstring for my_reactor"""
	def __init__(self):
		super(my_reactor, self).__init__()

	def listenTCP(self, _port, _factory):
		self.l = reactor.listenTCP(_port, _factory)

	def run(self):
		reactor.run()

if __name__ == '__main__': 
	r = my_reactor()
	r.listenTCP(10019, MyFactory(r))
	r.run()