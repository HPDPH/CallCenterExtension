window.onload = function () {
    var table = document.getElementById('JOInfo');
    var rows = table.getElementsByTagName('tr');

    for (var i = 0; i < rows.length; i++) {
        rows[i].addEventListener('click', function (e) {
            var cells = this.getElementsByTagName('td');

            // Siguruhing may sapat na cells para sa pagkuha ng data
            if (cells.length >= 8) {
                document.getElementById("Tray").value = cells[4].innerText; // Tray Number
                document.getElementById("StartChecking").value = cells[5].innerText; // Time start of checking
                document.getElementById("EndChecking").value = cells[6].innerText; // Time end of checking
                document.getElementById("Checker").value = cells[7].innerText; // Name of checker
                document.getElementById("NoSupRec").value = cells[8].innerText; // # of supplies received
            }
        });
    }
};