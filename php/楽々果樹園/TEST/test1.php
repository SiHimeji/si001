<?php
header('Content-Type: text/html; charset=UTF-8');

// 個人識別値としてパラメータを取得する
$name = $_GET['u'];

// 検索結果でボタンの切り替えを行う
  //DB名、ユーザー名、パスワードを変数に格納
  $conn=mysqli_connect('localhost','ss611756_db','sihimeji0251') or die("データベース接続に失敗しました。");
  mysqli_select_db($conn,'ss611756_db') or die("指定されたデータベースは存在しません。");

  $bt ="作業開始";
  $sql = "SELECT work FROM t_timecard WHERE t_timecard.adres='". $name . "' order by tm asc;";
  if($result=mysqli_query($conn,$sql)){
	while ($row = mysqli_fetch_assoc($result) ) {
	      if($row['work']=="2"){
			$bt ="作業開始";
	      }else{
			$bt ="作業終了";
	      }
	}
  }
  else{
	echo "失敗しました<br>\n";
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
	
	<script type="text/javascript">
<!--
function showMap(position) {
    /*位置情報を表示する*/
    var coords = position.coords;
    //alert('緯度: ' + coords.latitude + ', 経度: ' + coords.longitude);
    let ido = document.getElementById('basyo');
    ido.value = coords.latitude +","+coords.longitude;
    // iframe要素を作成
   const iframe = document.createElement('iframe');
// src属性を設定
iframe.src="https://www.google.com/maps?output=embed&z=15&ll="+coords.latitude+","+coords.longitude+"&q="+coords.latitude+", "+coords.longitude+"\""

// 幅と高さを設定 (任意)
iframe.style.width = '900px';
iframe.style.height = '300px';
	//iframe.frameborder="0";
	//iframe.style="border: 0;";
	//iframe.allowfullscreen="";
	//iframe.aria-hidden="false";

// ドキュメントに追加
document.body.appendChild(iframe);

}

function handleError(error) {
    /*取得失敗のアラートを表示する*/
    alert('位置情報を取得できません。');
}

function getGeoLocation(){
    if (navigator.geolocation && typeof navigator.geolocation.getCurrentPosition == 'function') {
        /*位置情報を取得可能な場合*/
        navigator.geolocation.getCurrentPosition(showMap,handleError);
    }
    else {
        /*位置情報を取得不可能な場合*/
        handleError();
    }
}
//-->
</script>

</head>

<body>
	<!-- 位置情報取得-->
	<script>
		getGeoLocation();
	</script>

  <div class="auto-style2" >
	<p class="auto-style6">
		<img alt="" height="63" src="ロゴ.jpg" width="299">
	</p>
	<p class="auto-style4">
		<span class="auto-style5"><strong>● 楽々農園　勤怠 ●</strong></span>
		<br><br>
	</p>

	<!-- ボタンを押したらDB更新 -->
    	<form action="test2.php" method="post" class="DB">
	
      	<div class="auto-style2" >
		<label class="label1"><span><strong> &nbsp;名 前:</strong></span></label>
	        &nbsp;<input type="text" class="form-control" name="name" id="name" style="width: 533px; height: 50px; font-size: 45px; color: #0000FF;" value=<?= $name ?> readonly>
		<br>
      	</div>
	
	<div class="auto-style2" >
		<label class="label1"><span><strong> &nbsp;場 所:</strong></span></label>
	        &nbsp;<input type="text" class="form-control" name="basyo" id="basyo" style="width: 533px; height: 50px; font-size: 45px; color: #0000FF;"  readonly>
	       <br>
	       <br>
	</div>

	<div class="auto-style4">
	        <input type="submit" class="btn-test3" value=<?= $bt ?> name="botan" style="width: 900px; height: 300px; font-family: 'ＭＳ ゴシック'; font-size: 100px; background-color: #0066FF; color: #FFFFFF;" value="">
	      	<br>
		<br>
	</div>

<!--	<div class="auto-style4">
	        <input type="submit" class="btn-test3" value="作業終了" name="botan" style="width: 900px; height: 300px; font-family: 'ＭＳ ゴシック'; font-size: 100px; background-color: #FF9900; color: #FFFFFF;" value="">
		<br>
	</div> -->
	</form>
	
	<!-- 電話番号表示 -->
	<p class="auto-style4"><a href="https://si-himeji.co.jp/">勤怠に関する連絡先（TEL：079-269-0251）</a><br><br></p>
　</div>
 </body>
</html>
