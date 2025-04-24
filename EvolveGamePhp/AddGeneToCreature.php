<?php
require 'ConnectionSettings.php';
//variables submitted
//creature name, user id,
$geneID = $_POST["geneID"];
$creatureID = $_POST["CreaturID"];
// Create connection

// Check connection
$sql = "INSERT INTO creaturesgenes (CreatureID, Geneid, onOFF) VALUES ('" .$creatureID . "','" . $geneID ."', true)";
$result = $conn->query($sql);
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
  $conn->close();
?>