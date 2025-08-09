### zip to 緯度経度変換
### Chiharu.F SI 2021
###
### python -m pip install requests
### python -m pip install json
###
### http://geoapi.heartrails.com/api/xml?method=searchByPostal&postal=6750044
###
import time
import requests
import json

f = open('D:\\01_Work\\01_Source\\python\\zip.txt', 'r')
datalist = f.readlines()
for data in datalist:
    postal = data[0:7]   ##調べたい郵便番号  
    try:
        url = 'http://geoapi.heartrails.com/api/json?method=searchByPostal&postal='
        res_dict = requests.get(url+postal).json()['response']['location'][0]
        #地理情報
        print('%s, %s, %s, %s, %s, %s ' % (res_dict['postal'], res_dict['prefecture'], res_dict['city'], res_dict['town'], res_dict['y'],res_dict['x']))
    except:
        print('Error:',postal)
        time.sleep(2)
f.close()

print('終了 (EnyKey)\n')
a = input()
