<?php

//DB connection goes here

if ($_REQUEST['table'] === "shoes") { //I'm using REQUEST instead of GET, so it will work with both GET and POST
    $query = "SELECT * FROM `shoes` ORDER by `price` ASC LIMIT 10";
} elseif ($_REQUEST['table'] === "sneakers") { 
    $query = "SELECT * FROM `sneakers` ORDER by `price` ASC LIMIT 10";
}

$result = mysql_query($query) or die(mysql_error());

while ($row = mysql_fetch_assoc($result)) {
    echo  $row['shopname'] . "\t" . $row['price'] . "\n"; 
}
?>