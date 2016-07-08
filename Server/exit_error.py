class MyExitException(Exception):
	"""docstring for MyExitException"""
	def __init__(self):
		super(MyExitException, self).__init__()
		
if __name__ == '__main__':
	try:
		raise MyExitException()
	except MyExitException, e:
		raise e
	finally:
		pass