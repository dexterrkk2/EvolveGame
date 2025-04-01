<?php
require 'ConnectionSettings.php';
//get number of creatures to spawn
$numCreatures = $_POST["numCreatures"];
//get max creatures
$maxCreatures;
$sql = "SELECT count(id) from creature'";
$result = $conn->query($sql);
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($result->num_rows > 0) {
    // output data of each row
    $maxCreatures = $result->fetch_assoc();
} else {
    echo "0 creatures";
}
//randomly get creatures
for(int i=0; i< $numCreatures; i++)
{
    $creatureID = rand(0, $maxCreatures)
    // Check connection
    $sql2 = "SELECT * from creature where id ='" .$creatureID ."'";
    $result = $conn->query($sql2);
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
    //add difficulty checker and if too hard run for loop again
    //make sure it is unique
    echo json_encode($rows);
}
$conn->close();
?>