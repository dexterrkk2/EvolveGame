<?php
require 'ConnectionSettings.php';

$creatureID = $_POST["id"];
// Check connection
$sql = "SELECT * from creature where id ='" .$creatureID ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
$survivalNum;
//Show users
if ($result->num_rows > 0) {
    // output data of each row
    
    $rows = array();
    while($row = $result->fetch_assoc()) {
      $rows[] = $row;
    }
    //get all data
    $damage = $rows["Damage"];
    $health = $rows["Health"];
    $AttackSpeed = $rows["AttackSpeed"];
    $MoveSpeed = $rows["MoveSpeed"];
    $effectiveDamage = $damage * $AttackSpeed;
    //do survival Num Math
    $survivalNum = $effectiveDamage + $MoveSpeed + $health;
    echo ($survivalNum);
  } else {
    echo "0 creatures";
  }
  $conn->close();
?>