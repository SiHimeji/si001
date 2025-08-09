<?PHP
$idname = $_GET['u'];
?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>楽々果樹園勤怠</title>
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
	.auto-style7 {
	font-size:60px;
	text-align: center;
	}
}
</style>
	

<script>
function openWin(){
  url1 = "rakurakukintai01.php?u=<?PHP echo $idname?>";
  const options = "width=900,height=800,left=100,top=100";
  window.open(url1, "subWindow04", options);
}

</script>
</head>
<body>

  <div class="auto-style2" >
	<p class="auto-style6">
		<img alt="" height="63" src="ロゴ.jpg" width="299">
	</p>
	<p class="auto-style4">
		<span class="auto-style5"><strong>● 楽々果樹園　勤怠 ●</strong></span>
		<br><br>
	</p>
<center>
<input name="Button1" type="button" class="auto-style7" value="勤怠登録開始" style="width: 587px; height: 237px" onclick="openWin()"/>
</center>

</body>
</html>
