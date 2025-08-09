from spire.pdf.common import *
from spire.pdf import *
 
# PdfStandardsConverterオブジェクトを作成し、パラメータとしてPDF文書を渡す
converter = PdfStandardsConverter("E:\\Temp\\ISO27001.pdf")
 
# PDFをPDF/A-1aに変換する
converter.ToPdfA1A("E:\\Temp\\PdfA1A.pdf")

print("END")
