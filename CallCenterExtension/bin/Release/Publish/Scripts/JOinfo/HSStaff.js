$(function () {
    $("#HsStaff").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/JOinfo/Autocomplete", // Endpoint to the Autocomplete action in JOinfoController
                dataType: "json",
                data: {
                    term: request.term                  
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.MedRepName,
                            //value: item.MedRepID
                        };
                    }));              
                }
            });
        },
        minLength: 1 // Minimum characters before triggering autocomplete
    });

});

