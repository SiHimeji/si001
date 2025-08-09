<?php
  //DB名、ユーザー名、パスワードを変数に格納
  $conn=mysqli_connect('localhost','ss611756_db','sihimeji0251') or die("データベース接続に失敗しました。");
  mysqli_select_db($conn,'ss611756_db') or die("指定されたデータベースは存在しません。");

  // 接続確認メッセージ
  $name = $_POST['name'];
  echo "<p>受け取った値（名前）: " . $name . "</p>" ;
  $basyo = $_POST['basyo'];
   echo "<p>受け取った値（場所）: " . $basyo  . "</p>";
  $botan = $_POST['botan'];
   echo "<p>受け取った値（ボタン）: " . $botan  . "</p>";
   $d = date('Y-m-d H:i:s');

  //SQL作成
  if ( $botan =="作業開始") {
 	$sql="INSERT INTO t_timecard(adres, tm, gps, work) VALUES ('$name','$d','[$basyo]',1);";
  	$msg=  $botan . "：" . $d;
  }else{
        $sql="INSERT INTO t_timecard(adres, tm, gps, work) VALUES ('$name','$d','[$basyo]',2);";
  	$msg=  $botan . "：" . $d;
  }
  // DB更新
  if($result=mysqli_query($conn,$sql)){
   }
  else{
    $msg= "失敗しました";
  }
  mysqli_close($conn);
 ?>

<!DOCTYPE html>
<html lang="ja">

<head>
    <meta charset="UTF-8">
	<style type="text/css">
	.auto-style2 {
	font-size: xx-large;
	}
	.auto-style4 {
	text-align: center;
	}
	.auto-style5 {
	font-size:80px;
	}
	.auto-style6 {
	text-align: left;
}
	</style>
</head>

<body>
  <div class="auto-style2" >
	<p class="auto-style6">
		<img alt="" height="63" src="ロゴ.jpg" width="299">
	</p>
	<p class="auto-style4">
		<span class="auto-style5"><strong>● 楽々農園　勤怠 ●</strong></span>
		<br><br>
	</p>

	<!-- 名前・緯度経度・時刻 -->
    	<form action="test2.php" method="post" class="DB">
	
      	<div class="auto-style2" >
		<label class="label1"><span><strong> &nbsp;名 前:</strong></span></label>
	        &nbsp;<input type="text" class="form-control" name="name" id="" style="width: 533px; height: 50px; font-size: 45px; color: #0000FF;" value=<?= $name ?> readonly>
		<br>
      	</div>
	
	<div class="auto-style2" >
		<label class="label1"><span><strong> &nbsp;場 所:</strong></span></label>
	        &nbsp;<input type="text" class="form-control" name="basyo" id="" style="width: 533px; height: 50px; font-size: 45px; color: #0000FF;" value="緯度経度test" readonly>
	       <br>
	       <br>
	</div>
	
	<p class="auto-style4"><?php echo $msg; ?><br><br></p>

	</form>

	<!-- 電話番号表示 -->
	<p class="auto-style4">"勤怠に関する連絡先（TEL：079-269-0251）"<a href="079-269-025">079-269-0251</a><br><br></p>
　</div>
 </body>
</html>