<?php
require 'ConnectionSettings.php';

$geneID = $_POST["id"];
// Check connection
$sql = "SELECT * from genes where id ='" .$geneID ."'";
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
    echo json_encode($rows);
  } else {
    echo "0 creatures";
  }
  $conn->close();
?>