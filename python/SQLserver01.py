#pip install pyodbc
import pyodbc

instance = "192.168.1.217"
user = "sa"
password = "0251"
db = "kcon"

#接続文字列の組み立て
conn_str = "DRIVER={SQL Server};SERVER=" + instance + \
     ";uid=" + user + \
     ";pwd=" + password + \
     ";DATABASE=" + db

#データベースへ接続
conn = pyodbc.connect(conn_str)

cursor = conn.cursor()
cursor.execute("SELECT * FROM HSIJISYO")
rows = cursor.fetchall()
cursor.close()

for r in rows:
    print(r[0])
