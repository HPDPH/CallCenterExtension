$(document).ready(function () {

    $('#JO-Save').click(function () {
        InsertJO();
    });
})


function InsertJO() {
    $.ajax({
        url: '/JOinfo/InsertJO',
        dataType: 'json',
        data: {
            TrxNo: $('#JO1').val(),
            docdate: $('#HsSched').val(),
            patientID: $('#JO1').val(),
            TrayNo: $('#Tray').val(), 
            SuppliesQty: $('#NoSupRec').val(),
            CheckStart: $('#StartChecking').val(),
            CheckEnd: $('#EndChecking').val(),
            VerifiedBy: $('#Checker').val(),
            VerifiedDt: $('#HsSched').val(),
            CheckBy: $('#Checker').val(),
            CheckDt: $('#HsSched').val(),
            Stat: $('#JO1').val(),
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
