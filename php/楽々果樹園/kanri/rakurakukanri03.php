<?PHP
include 'id_local.php';
$err="";
$nm="";
$adres="";
$id="";
$uid="";
$sql="";

if ($_SERVER["REQUEST_METHOD"] === "POST") {
	if (!empty($_POST["nm"]) && !empty($_POST["nm"])) {
    	$nm = $_POST["nm"];
  	}
  	if (!empty($_POST["adres"]) && !empty($_POST["adres"])) {
    	$adres = $_POST["adres"];
  	}
  	if (!empty($_POST["id"]) && !empty($_POST["id"])) {
    	$id = $_POST["id"];
  	}
  	if (!empty($_POST["uid"]) && !empty($_POST["uid"])) {
    	$uid = $_POST["uid"];
  	}


$conn=mysqli_connect($db_host,$db_id,$db_pass) or die("データベース接続に失敗しました。");
mysqli_select_db($conn,$db_name) or die("指定されたデータベースは存在しません。");

	if(strlen($id)==0 )
	{
		if(strlen($nm) >0 && strlen($adres) >0){
	  		$sql = "INSERT INTO m_user(user,adres,uid)VALUES('$nm','$adres','$uid')";
		}
	}else{
 		$sql = "update m_user  set user='$nm',uid ='$uid' where id ='$id';";
	}
	if(strlen($sql) >0){
		if($result=mysqli_query($conn,$sql)){
  			$err="更新OK";
		}else{
			$err="Error";
		}
	}
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


<script>

function DataSet01(id,nm,adres,uid){

//alert(nm);
　let text1 = document.getElementById('nm');
  text1.value = nm;

　let text2 = document.getElementById('adres');
  text2.value = adres;
 
　let text3 = document.getElementById('id');
  text3.value = id;

　let text4 = document.getElementById('uid');
  text4.value = uid;

	urldsp(adres);
}

function rnd01()
{
// 使用する英数字を変数charに指定
const chars = 'abcdefghijklmnopqrstuvwxyz0123456789';

// 空文字列を用意
let randomStr = '';

// 用意した空文字列にランダムな英数字を格納（7桁）
for(let i = 0; i < 10; i++) {
  while(true) {
    // ランダムな英数字を一文字生成
    const random = chars.charAt(Math.floor(Math.random() * chars.length));

    // randomStrに生成されたランダムな英数字が含まれるかチェック
    if(!randomStr.includes(random)) {
      // 含まれないなら、randomStrにそれを追加してループを抜ける
      randomStr += random;
      break;
    }
  }
}
　let text2 = document.getElementById('adres');
  text2.value = randomStr;
//urldsp(randomStr);
}
function urldsp(url)
{
　let text2 = document.getElementById('ur');
  text2.value = "https://ss611756.stars.ne.jp/rakukin.php?u="+url;

}

function tojiru(){
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

}


</script>


<body>
<table style="width: 100%" border="0">
		<table style="width: 100%">
			<tr>
				<td>楽々果樹園　勤怠管理　Ver1.0.0</td>
			</tr>
		</table>
</table>
<form method="POST">

<input type="hidden" id="id" name="id" value="">
	<br />
	<table style="width: 100%">
		<tr>
			<td style="width: 242px">氏名<br>
			<input name="nm" id ="nm" style="width: 160px" type="text" maxlength="10" /></td>
			<td>アドレス<br><input name="adres" id="adres" style="width: 190px" type="text" maxlength="10" />
			<input name="Button1" style="width: 90px" type="button" value="ランダム" onclick="rnd01()"/></td>

		</tr>
		<tr>
	</table>
	<table style="width: 100%">
			<td style="width: 100px">
			ID<BR><input name="uid" id ="uid" style="width: 50px" type="text" maxlength="10" /></td>
		</tr>
	</table>
	<br>
	<input name="Submit1" style="width: 190px" type="submit" value="更新" /><br />
</form>
	<br />
	<br />
	<a href= "https://qr.quel.jp/url.php" >QR作成</a> <----<input name="ur" id ="ur" style="width: 400px" type="text"  /><?PHP if($err <>""){ echo "[".$err."]";};  ?>
	<br />
	
	<input name="Button2" style="width: 90px" type="button" value="閉じる" onclick="tojiru()"/></td>
	
	
	
	<br />
<hr>
	<br />
	<br />

	<table style="width: 100%" border="1" style="border-collapse: collapse">
		<tr>
			<td style="width: 40px"></td>
			<td style="width: 50px">選択</td>
			<td style="width: 100px">氏名</td>
			<td style="width: 100px">アドレス</td>
		</tr>
<?PHP

 $conn=mysqli_connect($db_host,$db_id,$db_pass) or die("データベース接続に失敗しました。");
 mysqli_select_db($conn,$db_name) or die("指定されたデータベースは存在しません。");


  $sql = "SELECT * FROM m_user order by id;";
  if($result=mysqli_query($conn,$sql)){
	while ($row = mysqli_fetch_assoc($result) ) {

		echo "<tr><td style=\"width: 40px\">".$row['id']."</td>\n";
		echo "<td style=\"width: 50px\">";
		echo "<input name=\"Button".$row['id']."\" style=\"width: 92px\" type=\"button\" value=\"選択\" onclick=\"DataSet01('".$row['id']."','".$row['user']."','".$row['adres']."','".$row['uid']."')\" /></td>\n";
		echo "<td style=\"width: 100px\">".$row['user']."</td>\n";
		echo "<td style=\"width: 100px\">".$row['adres']."</td></tr>\n";
	}
}
?>

	</table>

</body>
</html>
