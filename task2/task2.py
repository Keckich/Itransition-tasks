import os, hashlib
fileNames = [fileName for fileName in os.listdir(os.getcwd())]
for file in fileNames:
    with open(file) as f:
        data = f.read()
        bdata = hashlib.sha3_256(data.encode())
        print(file + ' ' + bdata.hexdigest())