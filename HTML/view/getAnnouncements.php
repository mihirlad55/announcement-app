<?php

	//if the script is called by a get request, then continue
	 if ($_SERVER['REQUEST_METHOD'] === 'GET')
    {
		//create constant variables to hold the details of the sql server and credentials
		define("DB_NAME", "id69779_announcements");
		define("DB_USER", "id69779_root");
		define("DB_HOST", "localhost");
		define("DB_PASSWORD", "145willow21");
		
		//connect to the sql databse
		$conn = new mysqli(DB_HOST, DB_USER, DB_PASSWORD, DB_NAME);
		
		//if connection fails, exit and send error message
		if ($conn->connect_error) {
			die("Could not connect: " . $conn->connect_error);
		}
		
		$command = "SELECT * FROM " . DB_NAME . ".announcementsTable";
		$announcementList = array();
		
		//send command query to sql to retreive announcements and store the announcemetns in the result variable
		$result = $conn->query($command);
		
			$resultNum = 0;
			//fetch an associative array from the result and insert each row into the announcementList array
			while ($row = $result->fetch_assoc())
			{
				$announcementList[$resultNum] = $row;
				$resultNum = $resultNum + 1;
			}
			
			//send the array back to the requester
			echo json_encode($announcementList);
	}
    else
    {

    }
?>