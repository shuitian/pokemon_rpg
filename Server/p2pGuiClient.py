import sys
from twisted.internet import reactor
from twisted.internet.protocol import Protocol, Factory

class Echo(Protocol, object):

	def __init__(self):
		super(Echo, self).__init__()

	def dataReceived(self, data):
		print data

	def connectionMade (self):
		print "connection made"
		# sys.exit(0)

	def connectionLost (self, reason):
		print('connection lost\n') 

class EchoClientFactory(Factory, object):

	def __init__(self):
		super(EchoClientFactory, self).__init__()

	def buildProtocol (self, addr):
		return Echo()
    
	def clientConnectionLost(self, connector, reason):
		print('connection lost')
        
	def clientConnectionFailed(self, connector, reason):
		print('connection failed')
        
	def startedConnecting(self, connector):
		print('started connecting') 

factory = EchoClientFactory()
reactor.connectTCP('localhost', 10019, factory)
reactor.run() 