# Selenium
from selenium import webdriver
from selenium.webdriver.common.by import By
import time

#browser = webdriver.Edge(r"D:\01_Work\01_Source\python\driver\msedgedriver.exe")
# Chrome Driver
browser = webdriver.Chrome(r"D:\01_Work\01_Source\python\driver\chromedriver.exe")

# リンクを開く
#browser.get("https://www.google.co.jp/")
#browser.get('https://www.google.com/')
browser.get('https://account.onamae.com/accountCreate')
time.sleep(10)

#キー入力
#browser.find_element_by_xpath('lnameML').send_keys("なかむら")
#browser.find_element_by_xpath('//*[@id="lnameML"]').send_keys("なかむら")
element = browser.find_element(By.XPATH, "//*[@id=\"editAccountForm\"]/div/table/tbody/tr[2]/td[1]/div/div[1]")
element.send_keys("なかむら")
#browser.find_element_by_xpath('//*[@id="fnameML"]').send_keys("しんご")
#browser.find_element_by_xpath('//*[@id="lname"]').send_keys("Nakamura")
#browser.find_element_by_xpath('//*[@id="fname"]').send_keys("Shingo")
#sleep(8)
#location = input("場所入力：")
#favorite_foods = ["カレー", "ラーメン", "チャーハン", "とんかつ", "お好み焼き"]


#element = browser.find_element(By.XPATH, "/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input")
#element.send_keys("銀座で美味しいイタリアン")
#element = browser.find_element(By.XPATH, "/html/body/div[1]/div[3]/form/div[1]/div[1]/div[3]/center/input[1]")

#element.click()

time.sleep(10)

browser.close()