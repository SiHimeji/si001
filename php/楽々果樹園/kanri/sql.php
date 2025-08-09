<?PHP
include 'id_local.php';

$sql="";
$select="0";
if ($_SERVER["REQUEST_METHOD"] === "POST") {
  if (!empty($_POST["sql"]) && !empty($_POST["sql"])) {
    $sql = $_POST["sql"];
  } else {
    $err = "入力されていない項目があります。";
  }
}
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>SQL</title>
 <style>
    .fixed {
      position: fixed;
      background-color: lightblue;
      padding: 10px;
    }
    .content {
      height: 100px;
    }
  </style>
<script type="text/javascript">
<!--
function setSQL(strSQL) {

　let sql1 = document.getElementById('sql');
  sql1.value = strSQL; //"SHOW TABLES";



}
</script>
</head>

<body>
<div class="fixed">
<p>SQL実行　Ver1.0.0</p>
<form method="post">
	<textarea name="sql" id="sql" style="width: 1047px; height: 84px"></textarea>
	<br><br>
	<input name="Submit1" style="width: 178px; height: 29px" type="submit" value="実行" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<input name="Button1" style="width: 138px; height: 29px" type="button" value="テーブル一覧"  onclick="setSQL('SHOW TABLES')" />
	<input name="Button2" style="width: 138px; height: 29px" type="button" value="SELECT"  onclick="setSQL('SELECT * FROM ')" />
	<input name="Button3" style="width: 138px; height: 29px" type="button" value="DELETE"  onclick="setSQL('DLETE FROM   WHERE  ')" />
	<input name="Button4" style="width: 138px; height: 29px" type="button" value="TRUNCATE"  onclick="setSQL('TRUNCATE TABLE  ')" />
	
</form>
</div>
<br><br><br><br><br><br>
<br><br><br><hr><br>

<?PHP
echo $sql;
echo "\n";
$select="0";

$result = strtolower( substr($sql, 0, 4) );
if (str_starts_with($result, "show")) {
    //echo "先頭文字は 'SELECT' です。";
	$select="1";
}
$result = strtolower( substr($sql, 0, 6) );
if (str_starts_with($result, "select")) {
    //echo "先頭文字は 'SELECT' です。";
	$select="1";
}
if (str_starts_with($result, "insert")) {
    //echo "先頭文字は 'SELECT' です。";
	$select="2";
}
if (str_starts_with($result, "delete")) {
    //echo "先頭文字は 'SELECT' です。";
	$select="2";
}
if (str_starts_with($result, "update")) {
    //echo "先頭文字は 'SELECT' です。";
	$select="2";
}
$result = strtolower( substr($sql, 0, 8) );
if (str_starts_with($result, "truncate")) {
    //echo "先頭文字は 'SELECT' です。";
	$select="2";
}

$conn=mysqli_connect($db_host,$db_id,$db_pass) or die("データベース接続に失敗しました。");
mysqli_select_db($conn,$db_name) or die("指定されたデータベースは存在しません。");

if($select=="1"){
	if($result=mysqli_query($conn,$sql)){		//SELECT
		if( mysqli_num_rows( $result ) > 0 )
		{
    		echo "<table border>";
			echo "<tr>";

 			$fields = $result->fetch_fields();
    		foreach ($fields as $field) {
    			echo "<td>" . $field->name . "</td>";
    		}
			echo "</tr>";
    		while( $row = mysqli_fetch_assoc( $result ) )
    		{
        		echo "<tr>";
	    		foreach ($fields as $field) {
    				echo "<td>" .  $row[$field->name] . "</td>";
    			}
        		echo "</tr>";
    		}
    		echo "</table>";
		}
		else
		{
    		echo "<p>データが存在しません。</p>";
		}
	}
	mysqli_close( $conn );
}
if($select=="2"){
	if($result=mysqli_query($conn,$sql)){	//
		echo "更新しました";
	}else{
		echo "更新エラー";
	}
}
?>
<hr>
</body>
</html>
