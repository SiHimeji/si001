# pipコマンドでpsycopg2をインストール
# pip install psycopg2

import psycopg2

oldcd1 : str = ""
oldcd2 : str = ""
jcd : str = ""
# PostgreSQLに接続するための情報
params = {
    'dbname': 'postgres',
    'user': 'postgres',
    'password': '123456',
    'host': '192.168.1.166',
    'port': 5432
}

# 接続
conn = psycopg2.connect(**params)

# カーソルオブジェクトの生成
cursor = conn.cursor()

# 住所最古のコードを求める
print("OPEN")
cursor.execute("select city_section_cd from jyusyo.tb_jusho_11_new order by city_section_cd;")
rows = cursor.fetchall()
for row in rows:
    jcd = row[0]
    print(jcd)
    cursor.execute("select city_section_cd,new_city_section_cd  from jyusyo.tb_jusho_11_new where new_city_section_cd ='"+jcd+"' and city_section_cd <> new_city_section_cd;")
    r_dt = cursor.fetchall()
    oldcd2 = jcd
    while r_dt.count == 1:
        oldcd1 = r_dt[0][0]        
        oldcd2 = oldcd1
        cursor.execute("select city_section_cd,new_city_section_cd  from jyusyo.tb_jusho_11_new where new_city_section_cd ='"+oldcd1+"' and city_section_cd <> new_city_section_cd;")
        r_dt = cursor.fetchall()
    print(jcd +":"+ oldcd2)
    cursor.execute("update jyusyo.tb_jusho_11_new  set first_city_section_cd ='"+oldcd2+"'where city_section_cd ='"+jcd+"';")
    
# 接続の終了
conn.close()
