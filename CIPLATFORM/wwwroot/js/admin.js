




function searchuser(pg) {
    if (pg == undefined) {
        pg = 1;
    }
    var search = document.getElementById("searchuser").value;
    console.log(search)

    $.ajax({
        type: "POST", // POST
        url: '/Admin/UserFilter',
        data: {
            'search': search,
             'pg': pg,
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {

            console.log(data);


            $("#hi").empty();
            $("#hi").html(data);
        },
        error: function (e) {
            /*    debugger*/
            console.log("Bye");
            alert('Error');
        },
    });
}