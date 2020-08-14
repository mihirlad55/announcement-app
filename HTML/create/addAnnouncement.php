<?php
	//create a default announcement object for if php script is run without being invoked by the html pages
	$announcement["title"] = "phptest";
	$announcement["teacherName"] = "phptest";
	$announcement["subject"] = "General";
	$announcement["content"] = "phptest";
	$announcement["deadline"] = "20000101";
	$announcement["location"] = "phptest";
	
	//if a post request has been sent ot the html, then set the announcement object to the post object
	 if ($_SERVER['REQUEST_METHOD'] === 'POST')
    {
		$announcement = $_POST;
    }
    else
    {

    }
	
	//define mysql server credentials and settings as constants
	define("DB_NAME", "id69779_announcements");
	define("DB_USER", "id69779_root");
	define("DB_HOST", "localhost");
	define("DB_PASSWORD", "145willow21");
	
	//connect to sql server
	$link = mysqli_connect(DB_HOST, DB_USER, DB_PASSWORD);
	
	//if connection error, exit and send error message
	if (!$link) {
		die("Could not connect: " . mysqli_error());
	}
	
	//select the databse
	$db = mysqli_select_db($link, DB_NAME);
	
	if (!$db) {
		die("Can't use " . DB_NAME . ": " . mysqli_error());
	}
	
	echo "Connected to Database Successfully!\n";
		
	//escape any quotation marks to avoid sql injections and command failures due to inappropriate characters in query
	$announcement["title"] = mysqli_real_escape_string($link, $announcement["title"]);
	$announcement["teacherName"] = mysqli_real_escape_string($link, $announcement["teacherName"]);
	$announcement["subject"] = mysqli_real_escape_string($link, $announcement["subject"]);
	$announcement["content"] = mysqli_real_escape_string($link, $announcement["content"]);
	$announcement["deadline"] = mysqli_real_escape_string($link, $announcement["deadline"]);
	$announcement["location"] = mysqli_real_escape_string($link, $announcement["location"]);

	//define the query being sent to the sql server and insert the row
	$command = "INSERT INTO " . DB_NAME . ".announcementsTable VALUES (NULL, '" . $announcement["title"] . "','" . $announcement["teacherName"] . "','" . $announcement["subject"] . "','" . $announcement["content"] . "','" . $announcement["location"] . "',NULL,'" . $announcement["deadline"] . "')";
	
	//if failed to send query exit and send error message
	if (!mysqli_query($link, $command)) {
		die("Error: " . mysqli_error($link));	
	}
	
	//test
	else {
		echo "Announcement Added Successfully! \nTitle: " . $announcement["title"] . "\nTeacher Name: " . $announcement["teacherName"] . "\nSubject: " . $announcement["subject"] . "\nContent: " . $announcement["content"] . "\nDeadline: " . $announcement["deadline"] . "\nDate Created: " . $announcement["dateCreated"] . "\nTime Created: " . $announcement["timeCreated"] . "\n" . $command;	
	}
?>