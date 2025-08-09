import time
from selenium import webdriver
from selenium.webdriver.common.keys import Keys

browser = webdriver.Edge(r"D:\01_Work\01_Source\python\driver\msedgedriver.exe")

#browser.get('https://www.yahoo.co.jp/')
browser.get("https://www.google.co.jp")
#browser.get("https://office54.net/")
time.sleep(3)
#element = browser.find_element_by_css_selector('body > header > div > div > ul > li.category-python > a')
#element.click()

location = input("場所入力：")
favorite_foods = ["カレー", "ラーメン", "チャーハン", "とんかつ", "お好み焼き"]

for i, food in enumerate(favorite_foods):
    if i > 0:
        # 新しいタブ
        browser.execute_script("window.open('','_blank');")
        browser.switch_to.window(browser.window_handles[i])

    # グーグルを開く
    browser.get("https://www.google.co.jp")

    # 検索ワード入力
    search_box = browser.find_element_by_name("q")
    search_words = location, food
    search_box.send_keys(" ".join(search_words))

    # 検索実行
    search_box.send_keys(Keys.RETURN)
    print(browser.title)
# 先頭のタブに戻る
browser.switch_to.window(browser.window_handles[0])

