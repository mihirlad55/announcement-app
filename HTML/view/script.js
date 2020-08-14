/********************************
**  Announcement View       ** 
**                             **
** Mihir lad			       **
** ICS3U                       **
** Ver 1.0 - October 18, 2016  **
********************************/

var announcementList;

// get announcements from php script on server
function getAnnouncements()
{	
	"use strict";
	
     $.ajax({
        url: 'getAnnouncements.php',
        type: 'GET',
        success: function(response){
			announcementList = JSON.parse(response);
			announcementList.reverse(); // reverse array to make newer announcements come first
			addAnnouncementCards(); 
			addAnnouncementsToTable();
       }
   });
}


//create all table body, row, and cell nodes with announcement information and add it to table.
function addAnnouncementsToTable()
{
	"use strict";
		
	var tableBody = document.createElement("tbody");
	for (var i = 0; i < announcementList.length; i++)
	{

		var tableRow = document.createElement("tr");
		
		tableRow.id = "announcementRow" + i.toString(); //give each announcement row unique id based on array index
		
		var titleCell = document.createElement("td");
		var subjectCell = document.createElement("td");
		var dateCreatedCell = document.createElement("td");
		var locationCell = document.createElement("td");
		var deadlineCell = document.createElement("td");	
		
		titleCell.innerHTML = announcementList[i].title;
		subjectCell.innerHTML = announcementList[i].subjectOf;
		dateCreatedCell.innerHTML = getInformalTime(announcementList[i].timestamp.toString().substring(9,11));
		locationCell.innerHTML = announcementList[i].location;
		deadlineCell.innerHTML = announcementList[i].deadline;
		
		

		tableRow.appendChild(titleCell);
		tableRow.appendChild(subjectCell);
		
		if (!isMobileDevice())
		{
			tableRow.appendChild(dateCreatedCell);
		}

		tableRow.appendChild(locationCell);
		tableRow.appendChild(deadlineCell);
		
		tableBody.appendChild(tableRow);
	}
	document.getElementById("tableAnnouncements").appendChild(tableBody);
	addClickEvents();
}


function getInformalTime(date)
{
	"use strict";
	
	var timeDifference = (parseInt(new Date().getDate()) - parseInt(date));
		switch (parseInt(timeDifference))
		{
			case 1:
				return "Yesterday";
			case 2:
				return "Day Before Yesterday";
			case 3:
				return "3 Days Ago";
			case 4:
				return "4 Days Ago";
			case 5:
				return "5 Days Ago";
			case 6:
				return "6 Days Ago";
			case 7:
				return "1 Week Ago";
			default:	
		}
		
		if (timeDifference % 7 >= 0)
		{
			return (timeDifference / 7).toString() + " Weeks Ago";
		}
		
}


//go through every announcement and add announcement cards
function addAnnouncementCards()
{
	"use strict";
	
	for (var i = 0; i < announcementList.length; i++)
	{
		createNewCard(announcementList[i], i);
	}
}

//create the card html and add it to the #main div. add announcement details of specified announcement within their particular areas in the html.
function createNewCard(announcement, num)
{
	"use strict";
	
	var cardHtml = "<div class='announcementBox invisible' id='announcementBox" + num + "'>" + //give announcement an id
            "<img class='announcementIcon' src='/soccer.png'>" +
                "<h3 class='announcementTitle'>" + announcement.title + "</h3>" +
                "<p class='announcementDateTime'>" + announcement.timestamp + "</p>" +
                "<div class='mainProperties'>" +
                    "<div class='announcementHeader lblSubject'>Subject: </div> <div class='announcementValue announcementSubject'>" + announcement.subjectOf + 				
				 	"</div>" +
                    "<div class='announcementHeader lblTeacher'>Teacher: </div> <div class='announcementValue announcementTeacherName'>" + announcement.teacherName +
				  "</div>" +
                    "<div class='announcementHeader lblDeadline'>Deadline: </div> <div class='announcementValue announcementDeadline'>" + announcement.deadline+		
				   "</div>" +
                "</div>" + 
                "<div class='announcementValue announcementContent'>" + announcement.content + "</div>" +
            "</div>";
	$('#main').append(cardHtml);
	animateControl('#announcementBox' + num.toString(), "slideInDown");
}

//add a click event to each table row to bring up more information
function addClickEvents()
{
	"use strict";
	
	for(var i = 0; i < announcementList.length; i++)
	{
		$('#announcementRow' + i.toString()).attr('onClick', 'onRowClick(' + i.toString() + ');');
	}
}

function onRowClick(i)
{
	"use strict";
	
	//show modal with selected announcement's content
	$('#modal').css('display', 'block');
	$('#modalAnnouncementContent').text(announcementList[i].content);
	$('#modalAnnouncementSubject').text(announcementList[i].subjectOf);
	$('#modalAnnouncementTitle').text(announcementList[i].title);
	$('#modalAnnouncementTeacherName').text(announcementList[i].teacherName);
	
	var timestampDate = announcementList[i].timestamp.toString().substring(0, 10);
	var timestampTime = announcementList[i].timestamp.toString().substring(11, 16);
		
	var date = new Date(parseInt(timestampDate.substring(0,4)), parseInt(timestampDate.substring(5,7)), parseInt(timestampDate.substring(9,11)));
	
	$('#modalAnnouncementDateCreated').text(getDateString(date));
	
	//if deadline is "0000-00-00", make the deadline blank
	if(announcementList[i].deadline.toString() !== "0000-00-00")
	{
		$('#modalAnnouncementDeadline').text(announcementList[i].deadline);
	}
	else 
	{
		$('#modalAnnouncementDeadline').text('');
	}


}


function getDateString(date)
{
	"use strict";
	
	var months = [ "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" ];
	var days = [ "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" ];
	
	var dateString = days[date.getDay()].toString() + ", " + months[date.getMonth() - 1].toString() + " " + date.getDate().toString() + ", " + date.getFullYear().toString();
	return dateString.toString();
}


function showTable()
{
	"use strict";
	
	//change appearance and function of view button
	$('#btnToggleView').css('background-image', 'url(/images/cardview.png)');	
	$('#btnToggleView').attr('onClick', 'showCards();');
	
	//add announcement cards
	for(var i = 0; i < announcementList.length; i++)
	{
		$('#announcementBox' + i.toString()).remove(this);
	}
	
	animateControl('#tableAnnouncements', 'slideInDown');
}

function showCards()
{
	"use strict";
	
	//change appearance and function of view button
	$('#btnToggleView').css('background-image', 'url(/images/tableview.png)');
	$('#btnToggleView').attr('onClick', 'showTable();');
	
	$('#tableAnnouncements').addClass(' invisible');
	
	
	for(var i = 0; i < announcementList.length; i++)
	{
		animateControl('#announcementBox' + i.toString(), 'slideInDown');
	}
}



function onLoad()
{
	"use strict";
	
	$('#main').css('height', '10000px');
	//set padding of main to height of fixed header to avoid it being covered by the header
	$('#main').css('padding-top', $('#containerHeader').height());
	animateControl('#main','slideInDown');	//get announcements 1 second after the page loads
	window.setTimeout(getAnnouncements, 1000);
	$('#main').css('height', '');
	alert($(window).width() * 0.65);
	$('#main').css('width', parseInt($(window).width() * 0.65, 10));
/*	$('#logo').css('height', parseInt($('#blueHeader').width(), 10) - parseInt($('#websiteHeader').width(), 10));
	$('#logo').css('width', parseInt($('#blueHeader').width(), 10) - parseInt($('#websiteHeader').width(), 10));
	$('#logo').css('max-height', (parseInt($('#blueHeader').width(), 10) * 0.1));
	$('#logo').css('min-width', (parseInt($('#blueHeader').width(), 10) * 0.1));	*/
	$('#logo').css('height', (parseInt($('#blueHeader').height()) + parseInt($('#blueHeader').css('padding-bottom'))).toString());
	$('#logo').css('width', (parseInt($('#blueHeader').height()) + parseInt($('#blueHeader').css('padding-bottom'))).toString());
	$('#websiteHeader').css('margin-left', $('#logo').width());
	
	if (isMobileDevice())
	{
		$('#main').css('margin-right', "0px");
		$('#main').css('margin-left', "0px");
		$('#containerHeader').css('position', 'relative');
		$('#containerHeader').removeClass('navbar-fixed-top');
		$('#containerHeader').css('width', '100%');
		$('#websiteHeader').css('font-size', '2em');
		$('#main').css('padding-top', '1%');
		$('#main').css('width', '100%');
		$('#containerHeader').css('width', '100%');
		$('#tableHeaderDateCreated').remove();
	}
	$('#main').css('height', '');
}


//add animation to control
function animateControl(control, animation)
{
	"use strict";
	
	//remove the invisiblity class if there
	$(control.toString()).removeClass('invisible');	
	//add the animated class and the specified animation class. when animation is finished, remove the animated class and remove the specified animation class
	$(control.toString()).addClass(' animated ' + animation.toString()).one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function(){
      $(this).removeClass('animated').removeClass(animation);
    });
		
}


//close the modal by hiding it
function closeModal() {
	"use strict";
	
	$('#modal').css('display', 'none');
}

//if user clicks off the screen, close the modal by hiding it
window.onclick = function(event) {
	"use strict";
	
	if(event.target === document.getElementById('modal'))
	{
		$('#modal').css('display', 'none');
	}
};


//if this is not a mobile device and the user scrolls to the top, add the header to navigation bar so it is not hidden by the fixed navigation bar. When the user begins to scroll back down, add the header back to the body.
window.onscroll = function combineHeaderWithNavbar()
{
	"use strict";
	
	if (!isMobileDevice())
	{
		if ($(window).scrollTop() > 65)
		{
			$('#blueHeader').prependTo('body');
		}
		else if ($(window).scrollTop() < 65)
		{
			$('#blueHeader').prependTo('#containerHeader');
		}
		
		$('#main').css('padding-top', ((parseInt($('#containerHeader').height(), 10) / parseInt($(window).height(), 10) * 60)).toString() + "%");
	}
};


//function to check for mobile device
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

//adjust size of logo according to screen size
window.onresize = function() {
	"use strict";
	
	//$('#logo').css('width', parseInt($('#blueHeader').width(), 10) * 0.1);
	//$('#logo').css('height', parseInt($('#blueHeader').width(), 10)* 0.1);
	
	$('#logo').css('height', (parseInt($('#blueHeader').height()) + parseInt($('#blueHeader').css('padding-bottom'))).toString());
	$('#logo').css('width', (parseInt($('#blueHeader').height()) + parseInt($('#blueHeader').css('padding-bottom'))).toString());
	$('#websiteHeader').css('margin-left', $('#logo').width());	
};