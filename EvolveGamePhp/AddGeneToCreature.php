<?php
require 'ConnectionSettings.php';
//variables submitted
//creature name, user id, 
$creatureName = $_POST["creatureName"];
$diet = $_POST["CreatureDiet"];
$geneID = $_POST["geneID"];
$creatureID = $_POST["CreaturID"];
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
$sql = "INSERT INTO creaturesgenes () VALUES ('" .$geneID . "','" . $creatureID ."')";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($conn->query($sql2) === TRUE) {
    //tell user that name is already taken
    echo "creature already taken";
    
  } else {
    echo "creating creature";
  $conn->close();
?>