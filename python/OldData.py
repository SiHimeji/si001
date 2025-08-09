import os
from datetime import datetime, timedelta

def find_folders_days_ago(directory, days_ago):
    target_date = datetime.now() - timedelta(days=days_ago)
    folders = []

    for root, dirs, files in os.walk(directory):
        for dir_name in dirs:
            dir_path = os.path.join(root, dir_name)
            dir_mtime = datetime.fromtimestamp(os.path.getmtime(dir_path))
            print(dir_path)
            print(dir_mtime)
            if dir_mtime.date() == target_date.date():
                folders.append(dir_path)

    return folders

# 使用例
#directory = '\\\\192.168.1.102\\disk1\\HIMEJI-BACKUP\'
directory='D:\\01_Work'
days_ago = 3
folders = find_folders_days_ago(directory, days_ago)
for folder in folders:
    print(folder)
