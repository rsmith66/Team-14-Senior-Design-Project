from paramiko import SSHClient
from scp import SCPClient
import time

ssh = SSHClient()
ssh.load_system_host_keys()
ssh.connect(hostname='10.192.35.20', 
            username='gamer',
            password='gamer',)
while 1:
    a,OUT,b = ssh.exec_command('cat /home/gamer/Desktop/data.txt')
    OUT=OUT.readlines()
    x = 0
    for line in OUT:
        with open('data.txt','w') as fout:
            fout.write(line)
        
        # print(line)
        
    time.sleep(0.5)
# scp = SCPClient(ssh.get_transport())

# while 1:
# 	scp.get('/home/gamer/Desktop/data.txt','data.txt')
# 	time.sleep(0.5)

#scp.get('file_path_on_remote_machine', 'file_path_on_local_machine')

#scp.close()