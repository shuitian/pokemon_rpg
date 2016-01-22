function luaFunc(message)
	print(message)
	return 432
end

Acc = {b =0,c=2};

function get_acc( ... )
	return Acc.b,Acc.c
end