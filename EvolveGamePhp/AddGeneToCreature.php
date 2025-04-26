<?php
require 'ConnectionSettings.php';
//variables submitted
//creature name, user id,
$geneID = $_POST["geneID"];
$creatureID = $_POST["CreaturID"];
// Create connection
$sql1 = "Select * from creaturesgenes where CreatureID ='" .$creatureID . "'and Geneid ='" . $geneID ."'";
$result = $conn->query($sql1);
if ($result->num_rows > 0) {
  //tell user that name is already taken
  echo "creature already taken";
  
} else {
// Check connection
$sql2 = "INSERT INTO creaturesgenes (CreatureID, Geneid, onOFF) VALUES ('" .$creatureID . "','" . $geneID ."', true)";
$result = $conn->query($sql2);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($conn->query($sql2) === TRUE) {
    //tell user that name is already taken
    echo "Gene Added";
    
  } else {
    echo "did not give gene";
  }
}
  $conn->close();
?>