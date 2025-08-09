import requests
from bs4 import BeautifulSoup
import openpyxl

# ニュースのカテゴリを入力させる
category = input("ニュースのカテゴリは？:").rstrip()

# Webページの情報を取得する
site_data = requests.get("https://news.yahoo.co.jp/categories/" + category)
soup = BeautifulSoup(site_data.content, 'html.parser')

# エクセルを開く
wb = openpyxl.Workbook()
sheet = wb.active
sheet .title = "スクレイピング結果"

# シートの番号用変数
cnt = 1

# 特定のクラスだけを抜き出して表示
for a in soup.select(".sc-kIPQKe.eMCmdt"):
	data = str(a.string).rstrip()
	# Noneを除外する
	if data == "None":
		continue
	
	# シートの番号を決める
	sel1 = "A"+str(cnt)
	sel2 = "B"+str(cnt)
	sheet[sel1].value = data
	sheet[sel2].value = a.get("href")
	cnt += 1
	
# エクセルにデータを保存する
wb.save("text.xlsx")

wb.close()