from pathlib import Path
import PyPDF2
import os

path ="E:\PDF\\"
dirs =os.listdir(path)
for dirname in dirs:
    dr = path + dirname
    pdf_dir = Path(dr)
    print(dr)

    pdf_files = sorted(pdf_dir.glob("*.pdf"))
    pdf_wrier = PyPDF2.PdfFileWriter()
    pg = 0
    for pdf_file in pdf_files:
        pdf_readre = PyPDF2.PdfFileReader(str(pdf_file),strict=False)
        print(pdf_file)
        for i in range(pdf_readre.getNumPages()):
            pdf_wrier.addPage(pdf_readre.getPage(i))
            pg = pg + 1
    marged_file = path + dirname + ".pdf"
    print("出力ファイル:" , marged_file)
    print("総ページ:", pg)
    with open (marged_file,"wb") as f:
        pdf_wrier.write(f)
