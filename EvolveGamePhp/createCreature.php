<?php
require 'ConnectionSettings.php';
//variables submitted
//creature name, user id, 
$creatureName = $_POST["creatureName"];
$diet = $_POST["CreatureDiet"];
$userid = $_POST["PlayerID"];
$creatureID = $_POST["CreaturID"];
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
    $sql2 = "INSERT INTO creature (name, Damage, Health, Diet, AttackSpeed, MoveSpeed, Population) VALUES ('" .$creatureName . "',1,5, '" . $diet ."',1,2,5)";
    if ($conn->query($sql2) === TRUE) {
        $sql = "SELECT id from creature where name ='" .$creatureName ."'";
        $result = $conn->query($sql);

        while($row = $result->fetch_assoc()) 
        {
            $creatureID = $row["id"];
            //get user data
    
            //get player info
    
            //get inventory
    
            //modify player data
    
            //update inventory
        }
        echo "New creature created successfully";
        $sql3 = "INSERT INTO creatortable (PlayerID,CreaturID) VALUES ('" .$userid. "', '" . $creatureID ."')";
        if ($conn->query($sql3) === TRUE) 
        {
          echo "creature added to table";
        } else {
          echo "Error: " . $sql3 . "<br>" . $conn->error;
        }
        //get id
        //insert into creator table user id, creatuere id
      } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
      }
  }
  $conn->close();
?>