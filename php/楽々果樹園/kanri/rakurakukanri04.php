<?PHP
include 'id_local.php';

$host = $db_host;
$db   = $db_name;
$user = $db_id ;
$pass = $db_pass ;
$charset = 'utf8mb4';

$tuki="";
$csv="";
$sql="";
$count=0;
$i=0;
if ($_SERVER["REQUEST_METHOD"] === "POST") {
	if (!empty($_POST["tuki"]) && !empty($_POST["tuki"])) {
    		$tuki = $_POST["tuki"];
  	}
//echo $tuki;

	if(strlen($tuki)>0){
		$dsn = "mysql:host=$host;dbname=$db;charset=$charset";
		$options = [
		    PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
		    PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
		    PDO::ATTR_EMULATE_PREPARES   => false,
		];
		try {
		    $pdo = new PDO($dsn, $user, $pass, $options);
		} catch (\PDOException $e) {
		    throw new \PDOException($e->getMessage(), (int)$e->getCode());
		}

		$sql="SELECT * FROM v_all where DATE_FORMAT(tm, '%Y-%m') ='" .$tuki."' order by user,tm";

		// SQLクエリ
		//$stmt = $pdo->query('SELECT * FROM v_all');
		$stmt = $pdo->query($sql);
		
		$data = $stmt->fetchAll();
		$count = count($data);
		if($count>0){

			// CSVファイル出力
			$filename = '勤怠_' . date('YmdHis') . '.csv';
			header('Content-Type: application/octet-stream');
			header('Content-Disposition: attachment; filename="' . $filename . '"');
			header('Content-Transfer-Encoding: binary');
			$fp = fopen('php://output', 'w');
			// ヘッダー行
			$header = array_keys($data[0]);
			mb_convert_variables('SJIS', 'UTF-8', $header);
//			fputcsv($fp, $header);
			$i++;
			foreach ($data as $row) {
				//$text=substr($row['tm'], 0, 7);
				//if($text == $tuki){
        		//$csv .= '' . $row['tm'] . '' . $row['uid'] . ''. $row['work'] . '';
				fputs($fp, ' ');
				$wk = $row['tm'].'00';
				$wk =str_replace(' ', '', $wk);
				$wk =str_replace('-', '', $wk);
				$wk =str_replace(':', '', $wk);
				$wk= substr($wk,2,14);
				fputs($fp, $wk);
				$wk = $row['uid'] .'     ';
				$wk= substr($wk,0,5);
				fputs($fp, $wk);
				$wk = $row['work'];
				fputs($fp, $wk);
        		$wk="        0               0 SV012304138000";
				fputs($fp, $wk);
				$wk = '000'. $i ;
				$wk= substr($wk, -3); 
				fputs($fp, $wk);

				fputs($fp, '' . PHP_EOL);
				$i++;
			//}
    		}
			// データ行
			$csv="out";
			fclose($fp);
		}
	}
}
if(strlen($csv)>0){
//echo $sql;
//echo $csv;
}else{
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
<form method="post">
	<input type="month" name="tuki" id="tuki"><br><br>
	<input name="Button1" style="width: 184px" type="submit" value="CSVダウンロード" />
</form>
<?PHP if(strlen($tuki)>0){echo $tuki; echo "データなし";} ?>
</body>
</html>
<?PHP
}
?>