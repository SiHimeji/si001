<!DOCTYPE html>
<html>
<head>
<meta charset="utf-8">
</head>
<body>

<script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key={API キー}&
sensor=false"></script>
<script type="text/javascript">
function load() {
    var latlng = new google.maps.LatLng( defLat, defLng );
    var myOptions = {
        zoom: 10,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map( document.getElementById( "map" ), myOptions );

    var iconFlag_1 = new google.maps.Marker({
        position: new google.maps.LatLng( defLat, defLng ),
        map: map
    });
}
</script>

<div id="map" style="width:480px; height:520px"></div>

<?php
echo 'My username is ' .getenv("USERNAME") . '!';
print("日本語");

  $conn=mysqli_connect('localhost','ss611756_db','sihimeji0251') or die("データベース接続に失敗しました。");
  mysqli_select_db($conn,'ss611756_db') or die("指定されたデータベースは存在しません。");


	$sql="INSERT INTO `t_timecard`(`adres`, `tm`, `gps`, `work`) VALUES ('端末１',now(),'34.862751','134.608041','2');";	

/*	if(!mysqli_query($conn,$sql)){
   		echo $sql;
    		echo "書き込みに失敗しました。<br>\n";
    		exit();
  	}
*/
print("<hr>");
print($sql);
echo "--------------";
 		
  $sql="SELECT * FROM v_all;"; //データベースのレコードを後ろから前に向かって読み込んでいます。
  if($result=mysqli_query($conn,$sql)){
    $cnt =0;
    echo  "<hr>\n";
    
    while ($row = mysqli_fetch_assoc($result) ) {
      echo "<span style=\"color:blue;\">{$row['user']}:</span>\n
            <span style=\"color:black;\">{$row['adres']}</span>
            <span style=\"color:black;\">{$row['gps']}</span>
            <span style=\"color:black;\">{$row['work']}</span>
	    <span style=\"color:red;\"></span>\n
            <hr>\n";
    }
  }
  else{
    echo "失敗しました<br>\n";
  }
  mysqli_close($conn);

?>

</body>
</html>