import glob
import os
import subprocess
user_name = 'siadmin'
password  = '0251'
net_drive_path = r'\\HIMEJI-BACKUP\landisk\IMF\03_仕様\運賃管理'

#接続
def mount_network_drive():
    try:
        win32wnet.WNetAddConnection2(
            win32netcon.RESOURCETYPE_DISK,
            None,
            net_drive_path,
            None,
            user_name,
            password
        )
        print(f'ネットワークドライブ {net_drive_path} に接続しました')
    except Exception as e:
        print(f'ネットワークドライブの接続に失敗しました: {e}')


# 接続を切る場合
def unmount_network_drive():
    try:
        win32wnet.WNetCancelConnection2(net_drive_path, 0, True)
        print(f'ネットワークドライブ {net_drive_path} の接続を切りました')
    except Exception as e:
        print(f'ネットワークドライブの接続解除に失敗しました: {e}')

#
def filedel(file_name):
    try:
        with open(file_name, 'rb') as bin_file:
    except Exception as e:
        print(f'ERROR')
 
#Main
# ネットワークドライブのマウント
mount_network_drive()
# 必要な操作を行う
for root, dirs, files in os.walk(net_drive_path):
    for file in files:
        if(file=='Thumbs.db'):
            filedel(os.path.join(root, file))


# ネットワークドライブのアンマウント
unmount_network_drive()
print(f'END')
val = input()