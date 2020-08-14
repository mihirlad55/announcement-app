
var announcementList;


function getAnnouncements()
{	
	"use strict";
	
     $.ajax({
        url: 'getAnnouncements.php',
        type: 'GET',
        success: function(response){
			alert(response);
			announcementList = JSON.parse(response);
			announcementList.reverse();
			addAnnouncementsToTable();
       }
   });
}



function addAnnouncementsToTable()
{
	"use strict";
		
	var tableBody = document.createElement("tbody");
	for (var i = 0; i < announcementList.length; i++)
	{

		var tableRow = document.createElement("tr");
		
		var titleCell = document.createElement("td");
		var subjectCell = document.createElement("td");
		var dateCreatedCell = document.createElement("td");
		var timeCreatedCell = document.createElement("td");
		var locationCell = document.createElement("td");
		var deadlineCell = document.createElement("td");	
		
		titleCell.innerHTML = announcementList[i].title;
		subjectCell.innerHTML = announcementList[i].subjectOf;
		dateCreatedCell.innerHTML = announcementList[i].dateCreated;
		timeCreatedCell.innerHTML = announcementList[i].timeCreated;
		locationCell.innerHTML = announcementList[i].location;
		deadlineCell.innerHTML = announcementList[i].deadline;
		
		tableRow.appendChild(titleCell);
		tableRow.appendChild(subjectCell);
		tableRow.appendChild(dateCreatedCell);
		tableRow.appendChild(timeCreatedCell);
		tableRow.appendChild(locationCell);
		tableRow.appendChild(deadlineCell);
		
		tableBody.appendChild(tableRow);
	}
	document.getElementById("tableAnnouncements").appendChild(tableBody);
}



function addClickEvents()
{
	"use strict";
	
	
			
}



function onLoad(){
	"use strict";
	
    $('#test').addClass('animated bounce');
}