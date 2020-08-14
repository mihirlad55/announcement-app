<?php

	//create default announcement object in case php is run without being invoked by html to test
	$announcement["title"] = "phptest";
	$announcement["teacherName"] = "phptest";
	$announcement["subject"] = "General";
	$announcement["content"] = "phptest";
	$announcement["deadline"] = "20000101";
	$announcement["location"] = "phptest";
	$announcement["dateCreated"] = "20000202";
	$announcement["timeCreated"] = "01:01";
	
	//if a post request is sent, set the announcement object to the object send by post
	 if ($_SERVER['REQUEST_METHOD'] === 'POST')
    {
		$announcement = $_POST;
    }
    else
    {

    }
	define("DB_NAME", "id69779_announcements");
	define("DB_USER", "id69779_root");
	define("DB_HOST", "localhost");
	define("DB_PASSWORD", "145willow21");
	
	$link = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD);
	
	if (!$link) {
		die("Could not connect: " . mysqli_error());
	}
	
	$db = mysqli_select_db($link, DB_NAME);
	
	if (!$db) {
		die("Can't use " . DB_NAME . ": " . mysqli_error());
	}
	
	echo "Connected Successfully";
		
	$announcement["title"] = mysqli_real_escape_string($link, $announcement["title"]);
	$announcement["teacherName"] = mysqli_real_escape_string($link, $announcement["teacherName"]);
	$announcement["subject"] = mysqli_real_escape_string($link, $announcement["subject"]);
	$announcement["content"] = mysqli_real_escape_string($link, $announcement["content"]);
	$announcement["deadline"] = mysqli_real_escape_string($link, $announcement["deadline"]);
	$announcement["location"] = mysqli_real_escape_string($link, $announcement["location"]);

	$command = "INSERT INTO " . DB_NAME . ".announcementsTable VALUES (NULL, '" . $announcement["title"] . "','" . $announcement["teacherName"] . "','" . $announcement["subject"] . "','" . $announcement["content"] . "','" . $announcement["location"] . "','" . $announcement["dateCreated"] . "','" . $announcement["deadline"] . "','" . $announcement["timeCreated"] . "')";
	

	if (!mysqli_query($link, $command)) {
		die("Error: " . mysqli_error($link));	
	}
?>