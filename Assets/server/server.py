#-*- coding:utf-8 –*-
from twisted.internet import reactor
from twisted.internet.protocol import Protocol, Factory
import sqldata,json

s = sqldata.sql()
class SimpleLogger(Protocol):
	def connectionMade(self):
		print 'Got connection from', self.transport.client
		# memberList[]
		# self.transport.write("0"+'\n'.join(self.memberList))
		# self.transport.write("456\n")

	# def connectionLost(self, reason):
	# 	print self.transport.client, 'disconnected'
	# 	for x in memberList.keys():
	# 		if memberList[x] == self.transport:
	# 			del memberList[x]
	# 	for x in memberList.values():
	# 		x.write("0"+'\n'.join(memberList))

	def dataReceived(self, data):
		message = sqldata.get_json_from_message(s, data)
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

class MyFactory(Factory):
	def buildProtocol(self, addr):
		return SimpleLogger()

reactor.listenTCP(10019, MyFactory())

reactor.run()