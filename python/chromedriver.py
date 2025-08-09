#
# pip install selenium chromedriver-binary
#
#

from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from time import sleep


# ブラウザを開く。
driver = webdriver.Chrome("D:\01_Work\01_Source\python\driver\chromedriver.exe")


#import chromedriver_binary # <- これでChromeDriverをPATHに自動追加してくれる
#from selenium import webdriver
#driver = webdriver.Chrome()
#driver.get('https://google.com')
