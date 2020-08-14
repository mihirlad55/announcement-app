/********************************
 **  Announcement Creator       ** 
 **                             **
 ** Mihir lad			       **
 ** ICS3U                       **
 ** Ver 1.0 - October 18, 2016  **
 ********************************/


var title, content, teacherName, subject, deadline, creationDate, creationTime, place;




//save dataa
function saveData()
{
	"use strict";

	var dropDownMonth = document.getElementById("dropDownMonth");
	var dropDownDay = document.getElementById("dropDownDay");
	var dropDownYear = document.getElementById("dropDownYear");

	//get announcement details from textboxes
	title = document.getElementById("txtTitle").value;
	content = document.getElementById("txtContent").value;
	teacherName = document.getElementById("txtTeacherName").value;
	place = document.getElementById("txtLocation").value;
	subject = document.getElementById("dropDownSubject").options[document.getElementById("dropDownSubject").selectedIndex].value.toString();


	var deadlineMonth = dropDownMonth.selectedIndex;

	//if month is a digit, add zero before it to make it 2 digits to avoid errors in sql query
	if (deadlineMonth.toString().length === 1)
	{
		deadlineMonth = "0" + deadlineMonth.toString();
	}

	//test
	alert(deadlineMonth.toString());

	//get announcement deadline from boxes
	deadline = dropDownYear.options[dropDownYear.selectedIndex].value + deadlineMonth + dropDownDay.options[dropDownDay.selectedIndex].value.toString();


	//create announcement object
	var announcement = {
		"title": title,
		"teacherName": teacherName,
		"subject": subject,
		"content": content,
		"location": place,
		"deadline": deadline
	};

	sendAnnouncementToPHP(announcement);

}



//send post request to php script with announcement details
function sendAnnouncementToPHP(announcement)
{
	"use strict";

	$.ajax(
	{
		url: 'addAnnouncement.php',
		type: 'POST',
		data:
		{
			"title": announcement.title,
			"teacherName": announcement.teacherName,
			"subject": announcement.subject,
			"content": announcement.content,
			"location": announcement.location,
			"deadline": announcement.deadline,
		},
		//alert response from php
		success: function(response)
		{
			alert(response);
		}
	});
}



//refresh the number of days in the dropdown based on which month it is. e.g. January: add 31 days, April: add 30 days
function refreshDays()
{
	"use strict";

	document.getElementById("dropDownDay").disabled = false;
	var dropDownMonth = document.getElementById("dropDownMonth");
	var dropDownDay = document.getElementById("dropDownDay");

	var numOfDays = 30;

	//if index of month selected is a month with 31 days, set numOfDays to 31
	if (dropDownMonth.selectedIndex === 1 || dropDownMonth.selectedIndex === 3 || dropDownMonth.selectedIndex === 5 || dropDownMonth.selectedIndex === 7 || dropDownMonth.selectedIndex === 8 || dropDownMonth.selectedIndex === 10 || dropDownMonth.selectedIndex === 12)
	{
		numOfDays = 31;
	}

	//clear dropdown, but keep "Day" option
	while (dropDownDay.lastChild.value !== "Day")
	{
		dropDownDay.removeChild(dropDownDay.lastChild);
	}

	
	
	//add days to dropDownDay
	var docFrag = document.createDocumentFragment();
	for (var i = 1; i <= numOfDays; i++)
	{
		var number;
		//if day is less than 10 (1 digit), add 0 before it to make it 2 digits and avoid sql query error
		if (i.toString().length === 1)
		{
			number = "0" + i.toString();
		}
		else
		{
			number = i;
		}
		//add a new day option to the dropdown var
		docFrag.appendChild(new Option(number, number));
	}

	//add all days to dropdown
	var select = document.getElementById("dropDownDay");
	select.appendChild(docFrag);
}



function onLoad()
{
	"use strict";

	refreshYears();
	animateMain();

	//adjust padding of main based on height of fixed header
	$('#main').css('padding-top', $('#containerHeader').height());
	$('#headerTitle').css('top', ("-" + parseInt($('#containerHeader').height()) * 0.36).toString() + "px");
	$('#headerLogo').css('height', parseInt($('#containerHeader').height()));
	$('#headerLogo').css('width', parseInt($('#containerHeader').height()));
	$('#main').css('width', parseInt($(window).width() * 0.6, 10));
	$('#main').css('height', 'initial');
	$('#navbar').css('top', ("-" + parseInt($('#containerHeader').height()) * 0.36).toString() + "px");
	
	//adjust difference between white background and grey background based on screen size or device type
	if (isMobileDevice())
	{
		$('#main').css('width', '100%');
	}
	else
	{
		$('#main').css('min-width', (parseInt($(window).width()) * 0.3).toString() + "px");
	}
}



//add year options based on current year. add only this year and next year.
function refreshYears()
{
	"use strict";

	var currentYear = new Date().getFullYear();
	var docFrag = document.createDocumentFragment();
	for (var i = currentYear; i <= currentYear + 1; i++)
	{
		docFrag.appendChild(new Option(i, i));
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



function animateMain()
{
	"use strict";
	$('#main').removeClass('invisible');
	$('#main').addClass(' visible');
	$('#main').addClass(' slideInDown');
}



function animateHeader()
{
	"use strict";

	$('#container').addClass(' animated slideInLeft');

}



function isMobileDevice()
{
	"use strict";

	if (/Android|SamsungBrowser|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent))
	{
		return true;
	}
	else
	{
		return false;
	}
}



//if this is not a mobile device and the user scrolls to the top, add the header to navigation bar so it is not hidden by the fixed navigation bar. When the user begins to scroll back down, add the header back to the body.
window.onscroll = function combineHeaderWithNavbar()
{
	"use strict";
	
	if (!isMobileDevice())
	{
		if ($(window).scrollTop() > 65)
		{
			$('#web').prependTo('body');
		}
		else if ($(window).scrollTop() < 65)
		{
			$('#blueHeader').prependTo('#containerHeader');
		}
		
		$('#main').css('padding-top', ((parseInt($('#containerHeader').height(), 10) / parseInt($(window).height(), 10) * 60)).toString() + "%");
	}
};



window.onresize = function() {
	"use strict";
	
	$('#headerTitle').css('top', ("-" + parseInt($('#containerHeader').height()) * 0.36).toString() + "px");
	$('#headerLogo').css('height', parseInt($('#containerHeader').height()));
	$('#headerLogo').css('width', parseInt($('#containerHeader').height()));
	
	if (isMobileDevice())
	{
		$('#main').css('width', '100%');
		$('#txtTitle').css('width', '85%');
		$('#txtTeacherName').css('width', '85%');
		$('#txtLocation').css('width', '85%');
		$('#dropDownMonth').css('width', '22%');
		$('#dropDownDay').css('width', '22%');
		$('#dropDownYear').css('width', '22%');
		$('#dropDownSubject').css('width', '85%');
		$('#txtContent').css('width', '85%');
	}
	else
	{
		$('#main').css('width', parseInt($(window).width() * 0.6, 10));
	}
};