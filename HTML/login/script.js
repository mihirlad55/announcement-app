/********************************
**  Announcement Creator       ** 
**                             **
** Mihir lad			       **
** ICS3U                       **
** Ver 1.0 - October 18, 2016  **
********************************/

/*************************
**      Get Input       **
*************************/
var title, content, teacherName, subject, deadline, creationDate, creationTime;



// fill in other variables here

function saveData()
{
	"use strict";


	var currentDate = new Date();
	var year, month, day;
	year = currentDate.getFullYear();
	
	if (currentDate.getMonth().toString().length === 1)
	{
		month = "0" + currentDate.getMonth().toString(); 
	}
	else
	{
		month = currentDate.getMonth().toString();
	}
	
	if (currentDate.getDate().toString().length === 1)
	{
		day = "0" + currentDate.getDate().toString(); 
	}
	else
	{
		day = currentDate.getDate().toString();
	}
	
	creationDate = year + month + day;
	alert(creationDate);
	creationTime = currentDate.getHours() + ":" + currentDate.getMinutes();

	
	title = document.getElementById("txtTitle").value;
    content = document.getElementById("txtContent").value;
	teacherName = document.getElementById("txtTeacherName").value;
	subject = document.getElementById("txtSubject").value;
	
	alert("Your information has been saved");

	deadline = document.getElementById("txtDeadline").value;


	// Add code to store rest of input in variables here
    
// Store the information in localstorage
    localStorage.setItem("title", title);
    localStorage.setItem("content", content);
	localStorage.setItem("teacherName", teacherName);

    
    //alert to inform user data has been saved.

}


	function refreshDays()
	{
		"use strict";
		
		document.getElementById("dropDownDay").disabled = false;
		var dropDownMonth = document.getElementById("dropDownMonth");
		var dropDownDay = document.getElementById("dropDownDay");
		
		var numOfDays = 30;
		if (dropDownMonth.selectedIndex === 0 || dropDownMonth.selectedIndex === 2 || dropDownMonth.selectedIndex === 4 || dropDownMonth.selectedIndex === 6 || dropDownMonth.selectedIndex === 7 || dropDownMonth.selectedIndex === 9 || dropDownMonth.selectedIndex === 11)
		{
			numOfDays = 31;
		}
		
		while (dropDownDay.lastChild.value !== "Day")
		{
			dropDownDay.removeChild(dropDownDay.lastChild);
		}
		
		var docFrag = document.createDocumentFragment();		
		for (var i = 1; i<=numOfDays; i++)
		{
			docFrag.appendChild(new Option(i,i));
		}
		
		var select = document.getElementById("dropDownDay");
		select.appendChild(docFrag);
	}
	
	
	function onLoad()
	{
		"use strict";
		
		refreshYears();
	}
	
	function refreshYears()
	{
		"use strict";
		
		var currentYear = new Date().getFullYear();
		var docFrag = document.createDocumentFragment();		
		for (var i = currentYear; i <= currentYear + 1; i++)
		{
			docFrag.appendChild(new Option(i,i));
		}
		
		var select = document.getElementById("dropDownYear");
		select.appendChild(docFrag);
	}


function getData()
{
	"use strict";
    title = localStorage.getItem("Title");
    content = localStorage.getItem("Content");
    document.getElementById("title").innerHTML = title;
    document.getElementById("content").innerHTML = content;
}