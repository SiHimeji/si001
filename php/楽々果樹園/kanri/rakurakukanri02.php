<?PHP
include 'id_local.php';
$pt="";
$id="";
$nm ="";

if ($_SERVER["REQUEST_METHOD"] == "GET") {
  if (!empty($_GET["no"]) && !empty($_GET["no"])) {
    	$id = $_GET["no"];	
	}else{
    $err = "入力されていない項目があります。";
  }
  if (!empty($_GET["nm"]) && !empty($_GET["nm"])) {
    	$nm = $_GET["nm"];	
	}else{
    $err = "入力されていない項目があります。";
  }
$pt="1";
 }

$tm="";
$wk="";
 
if ($_SERVER["REQUEST_METHOD"] === "POST") {
  if (!empty($_POST["tm"]) && !empty($_POST["tm"])) {
    $tm = $_POST["tm"];
  }
  if (!empty($_POST["nm"]) && !empty($_POST["nm"])) {
    $nm = $_POST["nm"];
  }
  if (!empty($_POST["wk"]) && !empty($_POST["wk"])) {
    $wk = $_POST["wk"];
  }
  if (!empty($_POST["id"]) && !empty($_POST["id"])) {
    $id = $_POST["id"];
  }
  /*
    $tm = $_POST["tm"];
    $nm = $_POST["nm"];
    $wk = $_POST["wk"];
    $id = $_POST["id"];

  echo $tm;
  echo $id;
*/
$pt="2";
} 

?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="ja" http-equiv="Content-Language" />
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>楽々果樹園勤怠</title>
<script>
</script>
</head>

<body>
<table style="width: 100%" border="0">
		<table style="width: 100%">
			<tr>
				<td>楽々果樹園　勤怠管理　Ver1.0.0</td>
			</tr>
		</table>
</table>
<hr>
<form method="POST">
<?PHP
if($pt =="1")
{

if($id !=-1 ){
$conn=mysqli_connect($db_host,$db_id,$db_pass) or die("データベース接続に失敗しました。");
mysqli_select_db($conn,$db_name) or die("指定されたデータベースは存在しません。");

  $sql = "SELECT * FROM v_all where id = $id;";
 	$tm="";
	$nm ="";
	$wk="";
	if($result=mysqli_query($conn,$sql)){
		if($row = mysqli_fetch_assoc($result) ) {
			$nm = $row["user"];
			$tm = $row["tm"];
			$wk = $row["work"];
		}
	}
}
?>
<table style="width: 100%">
	<tr>
		<td style="width: 194px">氏名</td>
		<td>
<?PHP		echo "<input name=\"nm\" id=\"nm\" type=\"text\"  value=\"$nm\"  readonly />";
?>		</td>
	</tr>
	<tr>
		<td style="width: 194px; height: 20px">時間</td>
		<td style="height: 20px">
<?PHP
			echo "<input type=\"datetime-local\"  name=\"tm\" id=\"tm\"  type=\"text\" value=\"$tm\"/>";
?>
		</td>
	</tr>
	<tr>
		<td style="width: 194px; height: 27px">作業</td>
		<td style="height: 27px">
			<select name="wk" id ="wk" style="width: 161px">
<?PHP
	if($wk =="1"){
		echo "<option value=\"1\" selected>開始</option>";
		echo "<option value=\"2\">終了</option>";	
	}else{
		echo "<option value=\"1\">開始</option>";
		echo "<option value=\"2\"selected>終了</option>";
	}
?>
			</select>
		</td>
	</tr>
	<tr>
		<td style="width: 194px">&nbsp;</td>
		<td>
			<input type="submit"  style="width: 164px" type="button" value="更新" />
		</td>
	</tr>
</table>
<?PHP
echo "<input type=\"hidden\" name=\"id\" value=\"$id\">";

}else{
?>

<?PHP

 $conn=mysqli_connect($db_host,$db_id,$db_pass) or die("データベース接続に失敗しました。");
 mysqli_select_db($conn,$db_name) or die("指定されたデータベースは存在しません。");

  if($id == -1){
  		$sql = "select * from m_user where user ='$nm';"; 
 	if($result=mysqli_query($conn,$sql)){
		if($row = mysqli_fetch_assoc($result) ) {
			$adres = $row["adres"];
		}
	}
  		$sql = "INSERT INTO t_timecard(adres, tm, gps, work)VALUES('$adres','$tm','-管理-','$wk')";
	}else{
		$sql = "update t_timecard set tm ='$tm' ,work='$wk' where id ='$id';";
	}
	
	if($result=mysqli_query($conn,$sql)){
  		echo "更新しました。\n";
?>
<script>
//alert("ok");

if (window.opener && !window.opener.closed) {
	const parentForm = window.opener.document.getElementById('parentForm');
	if (parentForm) {
		parentForm.submit();
	} else {
		alert('親フォームが見つかりません。');
	}
} else {
	alert('親ウィンドウが閉じられています。');
}

  	window.close(); /* ウィンドウを閉じる */  



</script>

<?PHP
	
	}else{
	  echo "更新エラー";
	}
?>
<?PHP
}
?>
</form>
</body>
</html>
