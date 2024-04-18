//$(document).ready(function () {
//    // Bind event listener to the input event of HSStaff textbox
//    $('#HsStaff').on('input', function () {
//        fetchDataFromDatabase(medRepID);
//    });
//});
var globalMedRepID; // Declare a global variable to store medRepID
$(document).ready(function () {

    // Attach datepicker functionality to custom-date input
    $("#custom-date").datepicker({
        dateFormat: "yy-mm-dd", // Specify date format
        changeMonth: true, // Allow changing of months
        changeYear: true, // Allow changing of years
        monthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        onSelect: function (dateText, inst) {
            var selectedDate = dateText; // Get the selected date
            //updateMedRepID();
            //loaditems(globalMedRepID, selectedDate); // Call fetchDataFromDatabase with selectedDate
        }
    });
});

$(document).ready(function () {
    $('#custom-date').focus(function () {
        $('#tablehide').hide(); // Hide the table when the custom date textbox is focused
    });

    $('#custom-date').change(function () {
        if ($(this).val() !== '') {
            $('#tablehide').show(); // Show the table when a date is selected in the custom date textbox
        }
    });

    $('#btnLoad').click(function () {
        loaditems();
    })
});

function loaditems() {
    try {
        $.ajax({
            url: '/JOinfo/LoadDataTable',
            dataType: 'json',
            async: false,
            data: {
                medRepID: $('#HsStaff').val(),
                schedDate: $('#custom-date').val(),
            },
            success: function (data) {
                // Clear existing table data
                $('#JOInfo').empty();
                // Iterate over the data and append rows to the table
                var tblBind = '';

                for (key in data) {
                    var tmp = data[key]

                    tblBind += '<tr>';
                    tblBind += '<td>' + tmp.JODATE + '</td>';
                    tblBind += '<td>' + tmp.MEDREPNAME + '</td>';
                    tblBind += '<td>' + tmp.PATIENTNAME + '</td>';
                    tblBind += '<td>' + tmp.JONO + '</td>';
                    tblBind += '<td>' + tmp.TrayNo + '</td>';
                    tblBind += '<td>' + tmp.CheckStart + '</td>';
                    tblBind += '<td>' + tmp.CheckEnd + '</td>';
                    tblBind += '<td>' + tmp.CHECKBY + '</td>';
                    tblBind += '<td>' + tmp.CHECKDT + '</td>';
                    tblBind += '</tr>';
                };

                $('#JOInfo').append(tblBind);
                tblBind = '';
            }
        });
    } catch (err) {
        alert(err.message);
    }
}


// Function to update MedRepID value using AJAX


function updateMedRepID() {
    var medRepName = document.getElementById("HsStaff").value;

    // Send AJAX request to fetch MedRepID
    $.ajax({
        url: "/JOinfo/GetMedRepID",
        type: 'GET',
        async: false,
        data: { medRepName: medRepName },
        success: function (medRepID) {
            globalMedRepID = medRepID; // Store medRepID in global variable
        }
    });
}

//function fetchDataFromDatabase(schedDate) {
//    // Send AJAX request to fetch data using medRepID and schedDate
//    $.ajax({
//        url: "/JOinfo/LoadDataTable",
//        type: 'GET',
//        data: { medRepID: globalMedRepID, schedDate: schedDate }, // Use globalMedRepID instead of medRepID
//        success: function (data) {
//            // Handle data returned from the server
//            loaditems(data);
//        }
//    });
//}