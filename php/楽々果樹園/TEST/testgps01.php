<?php
$work ="0";
$user ="";
$tm ="";
if(isset($_GET['u']) && $_GET['u'] != ''){
  $Ares= $_GET['u'];

 $conn=mysqli_connect('localhost','ss611756_db','sihimeji0251') or die("データベース接続に失敗しました。");
 mysqli_select_db($conn,'ss611756_db') or die("指定されたデータベースは存在しません。");

  $sql="SELECT * FROM v_all where adres ='$Ares' order by tm;"; //データベースのレコードを後ろから前に向かって読み込んでいます。
  if($result=mysqli_query($conn,$sql)){
	$row = mysqli_fetch_assoc($result);
	$user =	$row['user'];
	$work =	$row['work'];
	$tm =	$row['tm'];
   }
}
?>
<!DOCTYPE html>
<html lang="ja">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>SI楽々農園</title>
  <style>
    #map {
      height: 50vh; /* 画面全体の高さ */
      width: 50%;   /* 横幅を全体に */
    }
    <meta charset="UTF-8">
	<style type="text/css">
	.auto-style2 {
	font-size: xx-large;
	}
	.auto-style4 {
	text-align: center;
	}
	.auto-style5 {
	font-size:40px;
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
/*    alert('緯度: ' + coords.latitude + ', 経度: ' + coords.longitude);*/
    let ido = document.getElementById('gps');
    ido.value = coords.latitude +","+coords.longitude;
    // iframe要素を作成
   const iframe = document.createElement('iframe');
// src属性を設定
//iframe.src="https://www.google.com/maps?output=embed&z=15&ll="+coords.latitude+","+coords.longitude+"&z=17&q="+coords.latitude+", "+coords.longitude+"\""
iframe.src="https://www.google.com/maps?output=embed&z=15&ll="+coords.latitude+","+coords.longitude+"&z=17&q="+coords.latitude+", "+coords.longitude+"\""

// 幅と高さを設定 (任意)
iframe.style.width = '370px';
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
  <div class="auto-style2" >
	<p class="auto-style6">
		<img alt="" height="40" src="ロゴ.jpg" width="299">
	</p>
	<p class="auto-style4">
		<span class="auto-style5"><strong>楽々農園　勤怠 </strong></span>
		<br><br>
	</p>

<script>
	getGeoLocation();
</script>
<label class="label1"><span><strong> &nbsp;名 前:</strong></span></label>
<input type="text" id="nm" name="nm" size=20/ value="
<?PHP
echo $user;
?>
">
<br>
<label class="label1"><span><strong> &nbsp;場 所:</strong></span></label>
<input type="text" id="gps" name="gps" size=20/>
<br>
<?PHP
if($work =="0"){
?>
	<div class="auto-style4">
	        <input type="submit" class="btn-test3" value="作業開始" name="botan" style="width: 370px; height: 200px; font-family: 'ＭＳ ゴシック'; font-size: 80px; background-color: #0066FF; color: #FFFFFF;" value="1">
		<br>
	</div>
<?PHP
echo "前回  作業終了:";echo $tm;
}else{
?>
	<div class="auto-style4">
		<input type="submit" class="btn-test3" value="作業終了" name="botan" style="width: 370px; height: 200px; font-family: 'ＭＳ ゴシック'; font-size: 80px; background-color: #FF9900; color: #FFFFFF;" value="2">
		<br>
	</div>
<?PHP
	echo "前回  作業開始:";echo $tm;
}
?>
<hr>
<!--
<iframe
	src="https://www.google.com/maps?output=embed&z=15&ll=34.862969, 134.608107&z=17&q=34.862969, 134.608107"
	width="600"
	height="450"
	frameborder="0"
	style="border: 0;"
	allowfullscreen=""
	aria-hidden="false"
	tabindex="0"
/>
-->
</hr>
</body>
</html>

