#-*- coding:utf-8 –*-
import sqlite3,os,time,json,sys,signal
from exit_error import MyExitException
class sql(object):
	"""处理数据库的类"""
	def __init__(self):
		"""获取数据库连接"""
		super(sql, self).__init__()
		db = 'monsters.db'
		self.conn = sqlite3.connect(db)

		print "Open",db,"Success"
		
	def __del__(self):
		"""关闭数据库连接"""
		self.conn.close()

	def show_table(self, table_name):
		"""显示表中所有数据"""
		if table_name == None:
			table_name = "None"
		table = self.execute("SELECT *  from " + table_name)
		if table != None:
			print table.fetchall()

	def execute(self, seq):
		"""执行数据库语句"""
		# print seq
		return self.conn.execute(seq)

	def get_monster(self, id):
		"""在MONSTER表中查询数据"""
		table = self.execute("SELECT * from MONSTER where id =" + str(id))
		if table != None:
			return table.fetchone()

	def get_item(self, id):
		"""在ITEM表中查询数据"""
		table = self.execute("SELECT * from item where id =" + str(id))
		if table != None:
			return table.fetchone()

	keys = ["id",'name','hp','attack','defence','gold']
	def get_item_json(self, id):
		d = {"type":"item"}
		l = self.get_item(id)
		if(l != None):
			d['body'] = dict(zip(self.keys,l))
		return json.dumps(d)

	def get_monster_json(self, id):
		d = {"type":"monster"}
		l = self.get_monster(id)
		if(l != None):
			d['body'] = dict(zip(self.keys,l))
		return json.dumps(d)

def get_json_from_message(protocol, sql_connection, string):
	d = json.loads(string)
	if d["type"] == 'item':
		return sql_connection.get_item_json(d['body']['id'])
	elif d["type"] == 'monster':
		return sql_connection.get_monster_json(d['body']['id'])
	elif d["type"] == 'exit':
		protocol.transport.loseConnection()
		protocol.reactor.l.stopListening()
		os.kill(os.getpid(), signal.SIGKILL)

if __name__ == '__main__':
	"""创建怪物表"""
	s = sql()
	# s.get_monster(1)
	# s.get_item(1)
	# dict1 = {}
	# dict1['type'] = "monster"
	# table = s.execute("SELECT * from MONSTER where id =" + str(1))
	# dict1['body'] = dict(zip(["id",'name','hp','attack','defence','gold'],table.fetchone()))
	# print json.dumps(dict1)
	print s.get_item_json(1),
	print get_json_from_message(s, s.get_item_json(1))
	


