import openpyxl as excel

# 新規ワークブックを作ってシートを得る --- (*1)
book = excel.Workbook()
sheet = book.active

# 書き込み --- (*2)
sheet["A2"] = "こんにちは"

# ファイルを保存 --- (*3)
book.save("d:\web\hello.xlsx")