$(document).ready(function () {

    $('#tablescroll').click(function () {
        
    });
})

//var currentTime = new date();

//$(document).ready(function () {

//    $('#JO-Save').click(function () {       
//        InsertJO();

//    });
//})

$(document).ready(function () {
    $('#JO-Save').click(function () {
        var joNumber = $('#JO1').val();
        var patientID = $('#PatientID').val();
        var trayNumber = $('#Tray').val();
        var checkerName = $('#Checker').val();
        var suppliesReceived = $('#NoSupRec').val();
        var timeStartChecking = $('#StartChecking').val();
        var disputeRemarks = $('#DisputeRemarks').val();
        //var timeEndChecking = $('#EndChecking').val();
        //var timeEndChecking = new date(); // Getting current date and time

        var message = "<strong>J.O Number:</strong> &nbsp;" + joNumber + "<br>" +
            "<br>" +
            "<strong>Patient ID:</strong>&nbsp; " + patientID + "<br>" +
            "<br>" +
            "<strong>Tray Number:</strong>&nbsp; " + trayNumber + "<br>" +
            "<br>" +
            "<strong>Supplies Received:</strong>&nbsp;" + suppliesReceived + "<br>" +
            "<br>" +
            "<strong>Name of Checker:</strong> &nbsp;" + checkerName + "<br>" +
            "<br>" +
            "<strong>Time Start of Checking:</strong>&nbsp;" + timeStartChecking + "<br>" +
            "<br>";

        // Check if disputeRemarks has a value
        if (disputeRemarks.trim() !== '') {
            message += "<strong>Dispute Remarks:</strong>&nbsp;" + disputeRemarks + "<br>" + "<br>";
        }

        // You can include the "Time End of Checking" section here, following the same approach as before

        message += "<strong>Are you sure you want to save?</strong>";

        $('.popupContent .message').html(message);

        $('#popupContainer').fadeIn();
    });

    // Delegate event binding for close button
    $(document).on('click', '.close', function () {
        $('#popupContainer').fadeOut();
    });

    // Delegate event binding for revert button
    $(document).on('click', '.revertButton', function () {
        // Add logic for revert button here
        $('#popupContainer').fadeOut();
    });

    // Delegate event binding for confirm save button
    $(document).on('click', '.SaveButton', function () {
        // Add logic for confirm save button here
        InsertJO();
           
    });
});

function InsertJO() {
    $.ajax({
        url: '/JOinfo/InsertJO',
        async: false,
        type: 'POST',
        dataType: 'json',
        data: {
            HsStaff: $('#HsStaff').val(),
            schedDate: $('#custom-date').val(),
            TrxNo: $('#JO1').val(),
            patientName: $('#PatientName').val(),
            patientID: $('#PatientID').val(),
            TrayNo: $('#Tray').val(), 
            SUPPLIES_REMARKS: $('#NoSupRec').val(),
            CheckStart: $('#StartChecking').val(),
            CheckEnd: $('#EndChecking').val(),
            VerifiedBy: $('#Checker').val(),
            VerifiedDt: $('#custom-date').val(),
            CheckBy: $('#Checker').val(),
            CheckDt: $('#custom-date').val(),
            For_dispute_remarks: $('#DisputeRemarks').val(),
            Stat: '1',
            //For_dispute_remarks: $('#DisputeRemarks').val(),
        },
        success: function (data) {
            if (data) {
                alert("Successful")
                $('#popupContainer').fadeOut();
                loaditems();
                //alert("Successful")
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // Display error message to the user
            alert(errorThrown);
        }

    })


}

$(document).ready(function () {
    $('#JO-Confirm').click(function () {



        var joNumber = $('#JO1').val();
        var patientID = $('#PatientID').val();
        var trayNumber = $('#Tray').val();
        var checkerName = $('#Checker').val();
        var suppliesReceived = $('#NoSupRec').val();
        var verfiedBy = $('#VerifiedBy').val();
        var verfiedDate = $('#verfiedDate').val();
        //var timeStartChecking = $('#StartChecking').val();
        //var timeEndChecking = new Date(); // Getting current date and time

        var message = "<strong>J.O Number:</strong> &nbsp;" + joNumber + "<br>" +
            "<br>" +
            "<strong>Patient ID:</strong>&nbsp; " + patientID + "<br>" +
            "<br>" +
            "<strong>Tray Number:</strong>&nbsp; " + trayNumber + "<br>" +
            "<br>" +
            "<strong>Name of Checker:</strong> &nbsp;" + checkerName + "<br>" +
            "<br>" +
            "<strong>Supplies Received:</strong>&nbsp;" + suppliesReceived + "<br>" +
            "<br>" +
            "<strong>Verified By:</strong>&nbsp;" + verfiedBy + "<br>" +
            "<br>" +
            "<strong>Verfied Date:</strong>&nbsp;" + verfiedDate + "<br>" +
            "<br>" +
            //"<strong>Time Start of Checking:</strong>&nbsp;" + timeStartChecking + "<br>" +
            //"<strong>Time End of Checking:</strong>&nbsp; " + timeEndChecking + "<br><br>" +
            "<strong>Are you sure you want to save?</strong>";

        $('.popupContent .message').html(message);

        $('#popupContainer .ConfirmButton').show();
        $('#popupContainer .DisputeButton').hide();
        $('#popupContainer').fadeIn();
    });

    // Delegate event binding for close button
    $(document).on('click', '.close', function () {
        $('#popupContainer').fadeOut();
    });

    // Delegate event binding for revert button
    $(document).on('click', '.revertButton', function () {
        // Add logic for revert button here
        $('#popupContainer').fadeOut();
    });

    // Delegate event binding for confirm save button
    $(document).on('click', '.ConfirmButton', function () {
        // Add logic for confirm save button here
        VerifyJO();
    });
});

// Function para i-format ang petsa at oras
function formatEndTime(date) {
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var day = date.getDate().toString().padStart(2, '0');
    var hours = date.getHours().toString().padStart(2, '0');
    var minutes = date.getMinutes().toString().padStart(2, '0');
    var seconds = date.getSeconds().toString().padStart(2, '0');

    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
}


function VerifyJO() {

    var currentDate = new Date();

    // Format ng petsa at oras
    var formattedDate = currentDate.getFullYear() + '-' + ('0' + (currentDate.getMonth() + 1)).slice(-2) + '-' + ('0' + currentDate.getDate()).slice(-2) + ' ' + ('0' + currentDate.getHours()).slice(-2) + ':' + ('0' + currentDate.getMinutes()).slice(-2) + ':' + ('0' + currentDate.getSeconds()).slice(-2);
    $.ajax({
        url: '/JOinfo/VerifyJO',
        dataType: 'json',
        data: {
            TrxNo: $('#JO1').val(),
            VerifiedDt: formattedDate,
            Stat: '3',
            For_dispute_remarks: $('#DisputeRemarks').val(),
        },
        success: function (data) {
            if (data) {
                alert("Successful")
                $('#popupContainer').fadeOut();
                loaditemsmt()
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // Display error message to the user
            alert(errorThrown);
        }

    })


}



//$(document).ready(function () {
//    $('#JO-Dispute').click(function () {

//        var joNumber = $('#JO1').val();
//        var patientID = $('#PatientID').val();
//        var trayNumber = $('#Tray').val();
//        var checkerName = $('#Checker').val();
//        var suppliesReceived = $('#NoSupRec').val();
//        var verfiedBy = $('#VerifiedBy').val();
//        //var verfiedDate = $('#verfiedDate').val();
        
//        //var timeStartChecking = $('#StartChecking').val();
//        //var timeEndChecking = new Date(); // Getting current date and time

//        var message = "<strong>J.O Number:</strong> &nbsp;" + joNumber + "<br>" +
//            "<strong>Patient ID:</strong>&nbsp; " + patientID + "<br>" +
//            "<strong>Tray Number:</strong>&nbsp; " + trayNumber + "<br>" +
//            "<strong>Name of Checker:</strong> &nbsp;" + checkerName + "<br>" +
//            "<strong>Supplies Received:</strong>&nbsp;" + suppliesReceived + "<br>" +
//            "<strong>Verified By:</strong>&nbsp;" + verfiedBy + "<br>" +
//            //"<strong>Verfied Date:</strong>&nbsp;" + verfiedDate + "<br>" +
//            "<strong>Remarks for dispute::</strong>&nbsp;" + DisputeRemarks + "<br>" +
//            //"<strong>Time Start of Checking:</strong>&nbsp;" + timeStartChecking + "<br>" +
//            //"<strong>Time End of Checking:</strong>&nbsp; " + timeEndChecking + "<br><br>" +
//            "<strong>Are you sure you want to save?</strong>";

//        $('.popupContent .message').html(message);

//        $('#popupContainer .ConfirmButton').hide();
//        $('#popupContainer .DisputeButton').show();
//        $('#popupContainer').fadeIn();
//    });

//    // Delegate event binding for close button
//    $(document).on('click', '.close', function () {
//        $('#popupContainer').fadeOut();
//    });

//    // Delegate event binding for revert button
//    $(document).on('click', '.revertButton', function () {
//        // Add logic for revert button here
//        $('#popupContainer').fadeOut();
//    });

//    // Delegate event binding for confirm save button
//    $(document).on('click', '.DisputeButton', function () {
//        // Add logic for confirm save button here
//        DisputeJO();
//    });
//});


$(document).ready(function () {
    $('#JO-Dispute').click(function () {

        var joNumber = $('#JO1').val();
        var patientID = $('#PatientID').val();
        var trayNumber = $('#Tray').val();
        var checkerName = $('#Checker').val();
        var suppliesReceived = $('#NoSupRec').val();
        var verfiedBy = $('#VerifiedBy').val();

   

        // Ipapakita ang textbox para sa Dispute Remarks
        $('#DRemarks').css('display', 'block');

        var message = "<strong>J.O Number:</strong> &nbsp;" + joNumber + "<br>" +
            "<br>" +
            "<strong>Patient ID:</strong>&nbsp; " + patientID + "<br>" +
            "<br>" +
            "<strong>Tray Number:</strong>&nbsp; " + trayNumber + "<br>" +
            "<br>" +
            "<strong>Name of Checker:</strong> &nbsp;" + checkerName + "<br>" +
            "<br>" +
            "<strong>Supplies Received:</strong>&nbsp;" + suppliesReceived + "<br>" +
            "<br>" +
            "<strong>Verified By:</strong>&nbsp;" + verfiedBy + "<br>" +
            "<br>" +
            "<strong>Remarks for dispute:</strong> " + "&nbsp;" + "<br>" +
            "<textarea id='DRemarks' placeholder='Remarks for dispute' rows='4' cols='30'></textarea><br><br>" + // Include the textarea element
            "<br>" +
            "<strong>Are you sure you want to save?</strong>";


        $('.popupContent .message').html(message);

        $('#popupContainer .ConfirmButton').hide();
        $('#popupContainer .DisputeButton').show();
        $('#popupContainer').fadeIn();
    });

    // Delegate event binding for close button
    $(document).on('click', '.close', function () {
        $('#popupContainer').fadeOut();
    });

    // Delegate event binding for revert button
    $(document).on('click', '.revertButton', function () {
        // Itago ang textbox para sa Dispute Remarks
        $('#DRemarks').css('display', 'none');
        $('#popupContainer').fadeOut();
    });

    // Delegate event binding for confirm save button
    $(document).on('click', '.DisputeButton', function () {
        // Add logic for confirm save button here
        DisputeJO();
    });
});

// Function para i-format ang petsa at oras
function formatEndTime(date) {
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var day = date.getDate().toString().padStart(2, '0');
    var hours = date.getHours().toString().padStart(2, '0');
    var minutes = date.getMinutes().toString().padStart(2, '0');
    var seconds = date.getSeconds().toString().padStart(2, '0');

    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
}


function DisputeJO() {

    var currentDate = new Date();

    // Format ng petsa at oras
    var formattedDate = currentDate.getFullYear() + '-' + ('0' + (currentDate.getMonth() + 1)).slice(-2) + '-' + ('0' + currentDate.getDate()).slice(-2) + ' ' + ('0' + currentDate.getHours()).slice(-2) + ':' + ('0' + currentDate.getMinutes()).slice(-2) + ':' + ('0' + currentDate.getSeconds()).slice(-2);
    var disputeRemarks = $('#DRemarks').val();
    $.ajax({
        url: '/JOinfo/DisputeJo',
        dataType: 'json',
        data: {
            patientID: $('#PatientID').val(),
            VerifiedDt: formattedDate,
            Stat: '2',
            For_dispute_remarks: disputeRemarks,
        },
        success: function (data) {
            if (data) {
                alert("Successful")
                $('#popupContainer').fadeOut();
                loaditemsmt()
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // Display error message to the user
            alert(errorThrown);
        }

    })


}


function getCountAndUpdateBadge() {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/JOinfo/GetCount", true);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {
            var count = JSON.parse(xhr.responseText);
            document.getElementById("countBadge").innerText = count;
        }
    };
    xhr.send();
}

// Initial count update
getCountAndUpdateBadge();

// Set interval to update count every 5 seconds (5000 milliseconds)
setInterval(getCountAndUpdateBadge, 5000);
