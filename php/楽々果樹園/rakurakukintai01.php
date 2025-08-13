<?php
include 'id_local.php';

$dataok ="0";
header('Content-Type: text/html; charset=UTF-8');

// 個人識別値としてパラメータを取得する
//$name = $_GET['u'];
$idname = $_GET['u'];

// 検索結果でボタンの切り替えを行う
//DB名、ユーザー名、パスワードを変数に格納
$conn=mysqli_connect($db_host,$db_id,$db_pass) or die("データベース接続に失敗しました。");
mysqli_select_db($conn,$db_name) or die("指定されたデータベースは存在しません。");

$sql = "SELECT * FROM v_all where adres='". $idname . "' order by tm desc;";

if($result=mysqli_query($conn,$sql)){
 	if($row = mysqli_fetch_assoc($result))
	{


		$name =	$row['user'];
		$work =	$row['work'];

		if($work=="")$work="0";
		//echo '['.$work.']';

		if($work=="2" || $work=="0"){
 			$bt ="作業開始";
        	}else{
			$bt ="作業終了";
        }
	}else{
		$dataok ="1";
	}
}else{
	echo "失敗しました<br>\n";
}
mysqli_close($conn);

//ブラウザバックした時の処理

if($dataok =="0"){
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


// ボタンの有効・向こうの切り替え
    let basyo = document.getElementById('basyo');
    let botan = document.getElementById('botan');
    //alert(ido.value);
    //ido.value='';
    if (ido.value === '') {
          //alert('test1');
          botan.disabled = true;  //無効
    }else{
          //alert('test2');
          botan.disabled =false;  //有効
    }
    //alert(botan.value);
    if(botan.value==='作業開始'){
        //alert('test3');
    	botan.style.backgroundColor= "#0066FF";		//作業開始：ブルー
    }else{
        //alert('test4');
        botan.style.backgroundColor= "#FF9900";        //作業終了：オレンジ
    }

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
	<script >
	</script>

  <div class="auto-style2" >
	<p class="auto-style6">
		<img alt="" height="63" src="ロゴ.jpg" width="299">
	</p>
	<p class="auto-style4">
		<span class="auto-style5"><strong>● 楽々果樹園　勤怠 ●</strong></span>
		<br><br>
	</p>

	<!-- ボタンを押したらDB更新 -->
    	<form action="rakurakukintai02.php" method="post" class="DB">
	
      	<div class="auto-style2" >
		<label class="label1"><span><strong> &nbsp;名 前:</strong></span></label>
	        &nbsp;<input type="text" class="form-control" name="name" id="name" style="width: 533px; height: 50px; font-size: 45px; color: #0000FF; border: none;" value=<?= $name ?> readonly>
		<br>
		<p hidden><input type="text" class="form-control" name="idname" id="idname" value=<?= $idname ?>></p>
      	</div>
	
	<div class="auto-style2" >
		<label class="label1"><span><strong> &nbsp;場 所:</strong></span></label>
	        &nbsp;<input type="text" class="form-control" name="basyo" id="basyo" style="width: 533px; height: 50px; font-size: 45px; color: #0000FF; border: none;"  readonly>
	       <br>
	       <br>
	</div>
	
	<div class="auto-style4" >
	    <input type="submit" class="btn-test3" value=<?= $bt ?> name="botan" id="botan" style="width: 900px; height: 300px; font-family: 'ＭＳ ゴシック'; font-size: 100px; background-color: #0066FF; color: #FFFFFF;" > 
	    
  	    <!-- 位置情報取得-->
	    <script type="text/javascript">
		getGeoLocation();
	    </script>
	    
	</div >

<!--<div class="auto-style4">
    <input type="submit" class="btn-test3" value=<?= $bt ?> name="botan" style="width: 900px; height: 300px; font-family: 'ＭＳ ゴシック'; font-size: 100px; background-color: #0066FF; color: #FFFFFF;" value="">
    <br>
    <br>
</div> -->
<!--<div class="auto-style4">
<input type="submit" class="btn-test3" value="作業終了" name="botan" style="width: 900px; height: 300px; font-family: 'ＭＳ ゴシック'; font-size: 100px; background-color: #FF9900; color: #FFFFFF;" value="">
<br>
</div> -->

	</form>
	<!-- 電話番号表示 -->
	<p class="auto-style4"><a href="tel:0792690251">勤怠に関する連絡先（TEL：079-269-0251）</a><br><br></p>
　</div>

 </body>
</html>
<?PHP
}else{
echo "不正アクセス";
}
?>