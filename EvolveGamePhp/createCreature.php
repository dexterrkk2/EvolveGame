<?php
require 'ConnectionSettings.php';
//variables submitted
//creature name, user id, 
$creatureName = $_POST["creatureName"];
$diet = $_POST["CreatureDiet"];
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
$sql = "SELECT name from creature where name ='" .$creatureName ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($result->num_rows > 0) {
    //tell user that name is already taken
    echo "creature already taken";
    
  } else {
    echo "creating creature";
    //insert creature
    $sql2 = "INSERT INTO creature (name, Damage, Health, Diet, AttackSpeed, MoveSpeed, Population) VALUES ('" .$creatureName . "',1,5, '" . $diet ."',1,1,5)";
    if ($conn->query($sql2) === TRUE) {
        echo "New creature created successfully";
        //get id
        //insert into creator table user id, creatuere id
      } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
      }
  }
 
  $conn->close();
?>