﻿


function searchuser(pg, key) {
    if (pg == undefined) {
        pg = 1;
    }
    if (key == "user") {
        var search = document.getElementById("searchuser").value;

    }
    if (key == "mission") {
        var search = document.getElementById("searchmission").value;
    }
   
    if (key == "cms") {
        var search = document.getElementById("searchcms").value;
    }
    if (key == "missiontheme") {
        var search = document.getElementById("searchtheme").value;
    }
    if (key == "missionskill") {
        var search = document.getElementById("searchskill").value;
    }
    if (key == "missionapplication") {
        var search = document.getElementById("searchma").value;
    }
    if (key == "story") {
        var search = document.getElementById("searchstory").value;
    }
    
    
    

    $.ajax({
        type: "POST",
        url: '/Admin/UserFilter',
        data: {
            'search': search,
            'pg': pg,
            'key': key,
        },
        dataType: "html",
        success: function (data)
        {

         
            if (key == "user") {
                $("#hi1").empty();
                $("#hi1").html(data);
            }
            else if (key == "cms") {
                $("#hi2").empty();
                $("#hi2").html(data);
            }
            else if (key == "mission") {
                $("#hi3").empty();
                $("#hi3").html(data);
            }
            else if (key == "missiontheme") {
                $("#hi4").empty();
                $("#hi4").html(data);
            }

            else if (key == "missionskill") {
                $("#hi5").empty();
                $("#hi5").html(data);
            }

            else if (key == "missionapplication") {
                $("#hi6").empty();
                $("#hi6").html(data);
            }
            else if (key == "story") {
                $("#hi7").empty();
                $("#hi7").html(data);
            }



        },
        error: function (e) {

            console.log("Bye");
            alert('Error');
        },
    });
}
