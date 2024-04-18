$(document).ready(function () {

    $('#tablescroll').click(function () {
        
    });
})
var currentTime = new Date();
$(document).ready(function () {

    $('#JO-Save').click(function () {       
        InsertJO();

    });
})


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
            TrayNo: $('#Tray').val(), 
            SuppliesQty: $('#NoSupRec').val(),
            CheckStart: $('#StartChecking').val(),
            CheckEnd: $('#EndChecking').val(),
            VerifiedBy: $('#Checker').val(),
            VerifiedDt: $('#custom-date').val(),
            CheckBy: $('#Checker').val(),
            CheckDt: $('#custom-date').val(),
            Stat: '1',
            For_dispute_remarks: 'Submitted',
        },
        success: function (data) {
            if (data) {
               alert("Successful")
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // Display error message to the user
            alert(errorThrown);
        }

    })


}

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


$(document).ready(function () {

    $('#JO-Confirm').click(function () {
        VerifyJO();
    });
})


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
            Stat: '2',
            For_dispute_remarks: 'Verified',
        },
        success: function (data) {
            if (data) {
                alert("Successful")
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            // Display error message to the user
            alert(errorThrown);
        }

    })


}
