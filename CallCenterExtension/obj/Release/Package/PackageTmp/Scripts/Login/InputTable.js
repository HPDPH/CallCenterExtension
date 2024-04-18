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
            $('#JO1').val(cells.eq(3).text()); // J.O Number
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