//$(document).ready(function () {
//    // Bind event listener to the input event of HSStaff textbox
//    $('#HsStaff').on('input', function () {
//        fetchDataFromDatabase(medRepID);
//    });
//});
var globalMedRepID; // Declare a global variable to store medRepID
$(document).ready(function () {

    var today = new Date();

    // Kunin ang petsa sa format na "YYYY-MM-DD"
    var formattedDate = today.toISOString().split('T')[0];

    // Ilagay ang formattedDate bilang value ng input
    document.getElementById("custom-date").value = formattedDate

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

    loaditemsmt();

    $('#btnLoad').click(function () {
        loaditems();
    })

    $('#btnDispute').click(function () {
        loaditemsdispute();
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
                    tblBind += '<td>' + tmp.STAT_DESC + '</td>';
                    tblBind += '<td>' + tmp.MEDREPNAME + '</td>';
                    tblBind += '<td>' + tmp.PATIENTNAME + '</td>';
                    tblBind += '<td>' + tmp.PATIENTID + '</td>';
                    tblBind += '<td>' + tmp.JONO + '</td>';
                    tblBind += '<td>' + tmp.TrayNo + '</td>';
                    tblBind += '<td>' + tmp.SUPPLIES_REMARKS + '</td>';
                    tblBind += '<td>' + tmp.CheckStart + '</td>';
                    tblBind += '<td>' + tmp.CheckEnd + '</td>';
                    tblBind += '<td>' + tmp.CHECKBY + '</td>';
                    tblBind += '<td>' + tmp.REMARKS + '</td>';
                    //tblBind += '<td>' + tmp.CHECKDT + '</td>';
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

function loaditemsdispute() {
    try {
        $.ajax({
            url: '/JOinfo/LoadDisputeTable',
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
                    tblBind += '<td>' + tmp.STAT_DESC + '</td>';
                    tblBind += '<td>' + tmp.MEDREPNAME + '</td>';
                    tblBind += '<td>' + tmp.PATIENTNAME + '</td>';
                    tblBind += '<td>' + tmp.PATIENTID + '</td>';
                    tblBind += '<td>' + tmp.JONO + '</td>';
                    tblBind += '<td>' + tmp.TrayNo + '</td>';
                    tblBind += '<td>' + tmp.SUPPLIES_REMARKS + '</td>';
                    tblBind += '<td>' + tmp.CheckStart + '</td>';
                    tblBind += '<td>' + tmp.CheckEnd + '</td>';
                    tblBind += '<td>' + tmp.CHECKBY + '</td>';
                    tblBind += '<td>' + tmp.REMARKS + '</td>';
                    //tblBind += '<td>' + tmp.CHECKDT + '</td>';
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


$(document).ready(function () {
    var today = new Date();

    // Kunin ang petsa sa format na "YYYY-MM-DD"
    var formattedDate = today.toISOString().split('T')[0];

    // Ilagay ang formattedDate bilang value ng input
    document.getElementById("custom-date-status").value = formattedDate

    // Attach datepicker functionality to custom-date input
    $("#custom-date-status").datepicker({
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

    loaditemsmt();

    $('#btnLoadstatus').click(function () {
        loaditemsmt();
    })
});

function loaditemsmt() {
    try {
        $.ajax({
            url: '/JOinfo/LoadDataTable',
            dataType: 'json',
            async: false,
            data: {
                status: $('#status').val(),
                schedDatestatus: $('#custom-date-status').val(),
            },
            success: function (data) {
                // Clear existing table data
                $('#JOInfoMT').empty();
                // Iterate over the data and append rows to the table
                var tblBind = '';

                $.each(data, function (key, tmp) {
                    tblBind += '<tr>';
                    tblBind += '<td>' + tmp.JODATE + '</td>';
                    tblBind += '<td>' + tmp.STAT_DESC + '</td>';
                    tblBind += '<td>' + tmp.MEDREPNAME + '</td>';
                    tblBind += '<td>' + tmp.PATIENTID + '</td>';
                    tblBind += '<td>' + tmp.JONO + '</td>';
                    tblBind += '<td>' + tmp.TrayNo + '</td>';
                    tblBind += '<td>' + tmp.SUPPLIES_REMARKS + '</td>';
                    tblBind += '<td>' + tmp.CheckStart + '</td>';
                    tblBind += '<td>' + tmp.CheckEnd + '</td>';
                    tblBind += '<td>' + tmp.CHECKBY + '</td>';
                    tblBind += '<td>' + tmp.REMARKS + '</td>';
                    // tblBind += '<td>' + tmp.CHECKDT + '</td>'; // Uncomment if needed
                    tblBind += '</tr>';
                });

                $('#JOInfoMT').append(tblBind);
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


