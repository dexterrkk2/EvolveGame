<?php
require 'ConnectionSettings.php';
//variables submitted
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];
$displayName = $_POST["displayName"];
// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
$sql = "SELECT username from playertable where username ='" .$loginUser ."'";
$result = $conn->query($sql);
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
//Show users
if ($result->num_rows > 0) {
    //tell user that name is already taken
    echo "user already taken";
    
  } else {
    //echo "creating user";
    $sql2 = "INSERT INTO playertable (username, password,displayName) VALUES ('" .$loginUser . "', '" . $loginPass ."', '" . $displayName."')";
    if ($conn->query($sql2) === TRUE) {
        //echo "New record created successfully";
        // Check connection
        $sql3 = "SELECT password, id from playertable where username ='" .$loginUser ."'";
        $result = $conn->query($sql3);
        if ($conn->connect_error) {
          die("Connection failed: " . $conn->connect_error);
        }
        //Show users
        if ($result->num_rows > 0) {
            // output data of each row
            while($row = $result->fetch_assoc()) {
              if($row["password"] == $loginPass)
              {
                echo $row["id"];
                //get user data

                //get player info

                //get inventory

                //modify player data

                //update inventory
              }
              else{
                echo "wrong creditinals";
              }
            }
          } else {
            echo "user does not exit";
          }
      } else {
        echo "Error: " . $sql2 . "<br>" . $conn->error;
      }
  }
 
  $conn->close();
?>