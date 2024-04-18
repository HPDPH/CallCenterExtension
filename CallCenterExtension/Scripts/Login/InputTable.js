var JoNo;
$(document).ready(function () {
    // Listener para sa click event sa bawat row ng table
    $('#JOInfo').on('click', 'tr', function () {
        var currentTime = new Date(); // Kumuha ng kasalukuyang oras
        var formattedDateTime = formatDate(currentTime);

        // I-set ang petsa at oras ng pag-click sa #StartChecking textbox
        $('#StartChecking').val(formattedDateTime);

        // Kumuha ng lahat ng mga cell sa napiling row
        var cells = $(this).find('td');

        // Siguruhing may sapat na cells para sa pagkuha ng data para sa JO1
        if (cells.length >= 9) {
            $('#PatientID').val(cells.eq(4).text()); // PatientID
            $('#JO1').val(cells.eq(5).text()); // J.O Number  
            $('#Tray').val(cells.eq(6).text()); // Tray
            $('#Checker').val(cells.eq(10).text()); // checker
            $('#NoSupRec').val(cells.eq(7).text()); // NoSupRec
            $('#StartChecking').val(cells.eq(8).text()); // Schecking
            $('#EndChecking').val(cells.eq(9).text()); // Echecking      
            $('#DisputeRemarks').val(cells.eq(11).text()); // Echecking    
        }
    });
});

$(document).ready(function () {
    // Listener para sa click event sa bawat row ng table
    $('#JOInfoMT').on('click', 'tr', function () {
        var currentTime = new Date(); // Kumuha ng kasalukuyang oras
        var formattedDateTime = formatDate(currentTime);

        // Kumuha ng lahat ng mga cell sa napiling row
        var cells = $(this).find('td');

        // Siguruhing may sapat na cells para sa pagkuha ng data para sa JO1
        if (cells.length >= 9) {
            $('#PatientID').val(cells.eq(3).text()); // PatientID
            $('#JO1').val(cells.eq(4).text()); // J.O Number  
            $('#Tray').val(cells.eq(5).text()); // Tray
            $('#Checker').val(cells.eq(9).text()); // checker
            $('#NoSupRec').val(cells.eq(6).text()); // NoSupRec
            $('#StartChecking').val(cells.eq(7).text()); // Schecking
            $('#EndChecking').val(cells.eq(8).text()); // Echecking
        }
    });
});

// Function para i-format ang petsa at oras
function formatDate(date) {
    var year = date.getFullYear();
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var day = date.getDate().toString().padStart(2, '0');
    var hours = date.getHours().toString().padStart(2, '0');
    var minutes = date.getMinutes().toString().padStart(2, '0');
    var seconds = date.getSeconds().toString().padStart(2, '0');

    return `${year}-${month}-${day}  ${hours}:${minutes}:${seconds}`;
}