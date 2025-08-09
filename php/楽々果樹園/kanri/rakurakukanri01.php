<?PHP
include 'id_local.php';

$yyyymm = "";
$usrName = "";
if ($_SERVER["REQUEST_METHOD"] === "POST") {
  if (!empty($_POST["yearmm"]) && !empty($_POST["yearmm"])) {
    $yyyymm = $_POST["yearmm"];
  } else {
    $err = "入力されていない項目があります。";
  }
  if (!empty($_POST["nm"]) && !empty($_POST["nm"])) {
    $usrName = $_POST["nm"];
  } else {
    $err = "入力されていない項目があります。";
  }
}
$conn=mysqli_connect($db_host,$db_id,$db_pass) or die("データベース接続に失敗しました。");
mysqli_select_db($conn,$db_name) or die("指定されたデータベースは存在しません。");


?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="ja" http-equiv="Content-Language" />
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>楽々果樹園勤怠</title>
<style type="text/css">
.auto-style1 {
	text-align: left;
}
</style>



<script>

function openSubWindow02(no,nm) {

  window.name = "searchWin"; 
  
  // サブ画面で表示するURL
  const url = "rakurakukanri02.php?no="+no+"&nm="+nm;
//alert(url);
  // サブ画面の名前
  const name = "subWindow02";
  // サブ画面のオプション（幅、高さ、位置など）
  const options = "width=600,height=400,left=100,top=100";

  window.open(url, name, options);
}

function openSubWindow03(){
  // サブ画面で表示するURL
  const url = "rakurakukanri03.php";

  // サブ画面の名前
  const name = "subWindow03";
  // サブ画面のオプション（幅、高さ、位置など）
  const options = "width=600,height=800,left=100,top=100";

  // サブ画面を開く
  window.open(url, name, options);
}


function openSubWindow04(){
  url1 = "rakurakukanri04.php";
  const options = "width=600,height=400,left=100,top=100";
  window.open(url1, "subWindow04", options);
}

</script>


</head>

<body>


<form id="parentForm"  action="rakurakukanri01.php" method="post" >
<table style="width: 100%" border="0">
 	<tr>
		<td style="width: 1288px">
		<table style="width: 100%">
			<tr>
				<td>楽々果樹園　勤怠管理　Ver1.0.0<hr></td>
			</tr>
			<tr>

			<table style="width: 100%" border="0">
				<td style="width: 131px"> 作業年月：</td>
				<td style="width: 130px">
<select name="yearmm" style="width: 131px">
<?PHP
$month_firstday = strtotime($current_month . "-1");
//ループ用に
$firstday = $month_firstday;
for ($i=0; $i<3; $i++){
    $yymm=date("Y/m", $firstday);
   if(  $yymm == $yyyymm){
		echo "<option value=".date("Y/m", $firstday)." selected>";
   }else{
	    echo "<option value=".date("Y/m", $firstday).">";
	}
    echo date("Y年m月", $firstday);
    echo "</option>";
    $firstday = strtotime("-1 month", $firstday);
}
?>
</select>
				<td ></td>
			</table>
			</tr>
			
			<tr>
			<table style="width: 100%" border="0">
				<td style="width: 131px"> 氏名：</td>
				<td style="width: 130px">
				<select name="nm" style="width: 131px">
<?PHP
  $sql = "SELECT user FROM m_user  order by id asc;";
  if($result=mysqli_query($conn,$sql)){
	while ($row = mysqli_fetch_assoc($result) ) {
		if($row['user']==$usrName){
			echo "<option value=".$row['user']." selected >";
		}else{
			echo "<option value=".$row['user'].">";		
		}
		echo $row['user'];
		echo "</option>";
	}
  }
  else{
	echo "失敗しました<br>\n";
  }
?>
					</select></td>
				<td ></td>
			</table>
			</tr>
<br>
			<tr>
				<td>
				<input name="ButtonDisk" type="submit" style="width: 200px" type="button" value="表　示" />
				<div align="right">
				<input name="ButtonSyai" style="width: 100px" type="button" value="社　員" onclick="openSubWindow03()"/>
				<input name="ButtonCsvi" style="width: 100px" type="button" value="ＣＳＶ" onclick="openSubWindow04()"/>
				</div>
				</td>
			</tr>
			<tr>
				<td></td>
			</tr>
		</table>
		</td></tr>
<hr>
		<tr>
		<td>
		<table style="width: 100%" bgcolor="#FFFFFF" border="1" class="auto-style1">
			<tr>
				<td style="width: 40px"></td>
				<td style="width: 100px">選択</td>
				<td style="width: 200px">時間</td>
				<td style="width: 150px"></td>
				<td>&nbsp;</td>
			</tr>


			<tr>
				<td style="width: 40px"></td>
				<td style="width: 100px">

				<input name="Button10" style="width: 92px" type="button" value="新規" onclick="openSubWindow02(-1,<?PHP echo "'$usrName'" ?>)"/></td>

				<td style="width: 200px">&nbsp;</td>
				<td style="width: 150px">&nbsp;</td>
				<td></td>
			</tr>

<?PHP

  $sql = "SELECT * FROM v_all where v_all.user ='$usrName' and   DATE_FORMAT(v_all.tm, '%Y/%m')='$yyyymm' order by v_all.tm DESC;";
  if($result=mysqli_query($conn,$sql)){
	while ($row = mysqli_fetch_assoc($result) ) {
		$tm1   = $row['tm'];
		$id1 = $row['id'];
		if($row['work']=="1")
		{
			$work="作業開始";
		}
		if($row['work']=="2")
		{
			$work="作業終了";
		}
		echo "<tr><td style=\"width: 40px\">$id1</td>";
		echo "<td style=\"width: 100px\">";
		echo "<input name=\"Button$id1\" style=\"width: 92px\" type=\"button\" value=\"選択\" onclick=\"openSubWindow02($id1,'$usrName')\" /></td> \n";
		echo "<td style=\"width: 200px\">$tm1</td>";
		echo "<td style=\"width: 150px\">$work</td>";
		echo "<td></td</tr>\n";
	}
  }
  else{
	echo "失敗しました<br>\n";
  }
?>
		</table>
		</td>
		</tr>
</table>
<?PHP
//echo $sql;
?>
</form>
</body>
</html>
