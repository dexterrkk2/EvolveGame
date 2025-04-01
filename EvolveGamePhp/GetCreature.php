<?php
require 'ConnectionSettings.php';

$creatureID = $_POST["id"];
// Check connection
$sql = "SELECT * from creature where id ='" .$creatureID ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($result->num_rows > 0) {
    // output data of each row
    
    $rows = array();
    while($row = $result->fetch_assoc()) {
      $rows[] = $row;
    }
  } else {
    echo "0 creatures";
  }
  echo json_encode($rows);
  $conn->close();
?>