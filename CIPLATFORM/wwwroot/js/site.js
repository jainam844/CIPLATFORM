
function GetCity() {
    var countryId = $('#countryId').find(":selected").val();

    $.ajax({
        url: "/Platform/GetCitys",
        method: "GET",
        data: {
            "countryId": countryId
        },
        success: function (data) {
            data = JSON.parse(data);
            $("#selectCityList").empty();
            document.getElementById("selectCityList").innerHTML += `
        <option value=${name}> City </option>
        `;
            data.forEach((name) => {
                document.getElementById("selectCityList").innerHTML += `
        <option value=${name.CityId} >${name.Name}</option>
        `;
            })
        },
        error: function (error) {
            console.log("Bye city");
            console.log(error);
        }
    })
}


function Previous(pg) {
    pg = pg - 1;

    $.ajax({
        url: "/Platform/Filter",
        method: "POST",
        data: {
            "pg": pg,
        },
        success: function (data) {
            $("#filter").html(data);
        }
    });
}

function NextPage(pg) {
    pg = pg + 1;
    console.log(pg);
    $.ajax({
        url: "/Platform/Filter",
        method: "POST",
        data: {
            "pg": pg,
        },
        success: function (data) {
            $("#filter").html(data);
        }
    });
}


/*filter */
function temp(pg) {


    if (pg == undefined) {
        pg = 1;
    }
    console.log(pg);




    var checkedcntryvalues = [];
    var div1 = document.getElementById("countryId");
    var list = div1.getElementsByTagName("option");
    for (i = 0; i < list.length; i++) {
        if (list[i].selected) {
            checkedcntryvalues.push(list[i].value);
        }

    }
    console.log(checkedcntryvalues);


    var checkedvalues = [];
    var div = document.getElementById("selectCityList");
    var list = div.getElementsByTagName("option");
    for (i = 0; i < list.length; i++) {
        if (list[i].selected) {
            checkedvalues.push(list[i].value);
        }

    }
    console.log(checkedvalues);





    var checkedthemevalues = [];
    var div2 = document.getElementById("theme");
    var list = div2.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedthemevalues.push(list[i].value);
        }

    }
    console.log(checkedthemevalues);



    var checkedskillvalues = [];
    var div3 = document.getElementById("skill");
    var list = div3.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedskillvalues.push(list[i].value);
        }

    }
    console.log(checkedskillvalues);



    var search = document.getElementById("searchb").value;
    console.log(search)


    var sort = document.getElementById("sort").value;
    console.log(sort)


    $.ajax({
        type: "POST", // POST
        url: '/Platform/Filter',
        data: {
            'cityId': checkedvalues,
            'countryId': checkedcntryvalues,
            'themeId': checkedthemevalues,
            'skillId': checkedskillvalues,
            'search': search,
            'sort': sort,
            'pg': pg,

        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {

            $("#filter").empty();
            console.log("grid Hii");
            $("#filter").html(data);
            //$("#list-view").empty();
            //console.log("list Hii");
            //$("#list-view").html(data);


            var div1 = document.getElementById("list-view");
            div1.style.display = 'none';
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}


/*mission-listing page*/
function AddMissionToFavourite(missionId) {
    $.ajax({

        url: '/Platform/AddMissionToFavourite',
        method: "POST",
        data: {
            'missionId': missionId,
        },
        success: function (c) {

            if (c == true) {
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true
                };
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart-fill");
                $('#addToFav').css("color", "red");
                toastr.success("Added To the favourite");
                document.getElementById(missionId).className = "bi bi-heart-fill text-danger";
                toastr.success("Added To the favourite");

            }
            else {
                toastr.options = {
                    "closeButton": true,
                    "progressBar": true
                };
                $('#addToFav').css("color", "black");
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart");
                toastr.error('Remove From the favourite');
                document.getElementById(missionId).className = "bi bi-heart";
                toastr.error('Remove From the favourite');

            }

        },
        error: function (request, error) {
            console.log("Bye city");
            alert('Error');
        },

    });

}


function applyMission(missionId) {

    $.ajax({

        url: '/Platform/applyMission',
        method: "POST",
        data: {
            'missionId': missionId,
        },
        success: function (missions) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            if (missions == true) {


                $('#applyMission').prop('disabled', true);
                $('#applyMission').text("   Your Request has been sent for Approve...!!!");

                $('#applyMission').css("color", "red");

                /*                document.getElementById("okayyy").innerHTML += ` Applied Successfully...!!!`*/
                toastr.success('Applied  successfully');

            }


        },
        error: function (request, error) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log("function not working");
            alert('Error');
            toastr.error('function not working');
        },

    });

}



function comment(missionid) {
    var comnt = $("#comment_text").val();
    console.log("kkkkkkkkkkkkkkkkkkkkk");
    console.log(comnt);
    $.ajax({
        url: "/Platform/AddComment",
        type: "POST", // POST
        data: {
            'obj': missionid,
            'comnt': comnt
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            $("#comment").html();
            console.log("Added ");
            toastr.success('Comment Added  successfully');
           /* setTimeout(function () { window.location.reload(); }, 3000);*/


        },
        error: function (e) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log("Bye");
            toastr.error('function not working');
            alert('Error');
        },
    });
}

function recommandToCoWorker(x) {
    //var toUserId = $('#recommand').find(":checked").val();
    var Missiond = x;
    var toUserId = [];
    var recommand = document.getElementById("recommand");
    var list = recommand.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            toUserId.push(list[i].value);
        }

    }

    /* debugger;*/
    $.ajax({
        url: "/Platform/RecommandToCoWorker",
        method: "Post",
        data: {
            "toUserId": toUserId,
            "mid": Missiond
        },
        success: function (data) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log(toUserId);
            toastr.success('Email Sent  successfully');

        }
        ,
        error: function (e) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log("Bye");
            toastr.error('function not working');
            alert('Error');
        },
    });
}

function recommandStory(x) {
    //var toUserId = $('#recommand').find(":checked").val();
    var Storyd = x;
    var toUserId = [];
    var recommand = document.getElementById("recommand");
    var list = recommand.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            toUserId.push(list[i].value);
        }

    }

    /* debugger;*/
    $.ajax({
        url: "/Platform/RecommandStory",
        method: "Post",
        data: {
            "toUserId": toUserId,
            "sid": Storyd
        },
        success: function (data) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log(toUserId);
            toastr.success('Email Sent  successfully');
        }
        ,
        error: function (e) {
            toastr.options = {
                "closeButton": true,
                "progressBar": true
            };
            console.log("Bye");
            toastr.error('function not working');
            alert('Error');
        },
    });
}





function story(pg) {


    if (pg == undefined) {
        pg = 1;
    }
    console.log(pg);

    var search = document.getElementById("searchb").value;
    console.log(search)




    //debugger

    $.ajax({
        type: "POST", // POST
        url: '/Platform/StoryFilter',
        data: {

            'search': search,
            'pg': pg,


        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
            /*debugger*/

            console.log(data);
            $("#StoryFilter").empty();
            $("#StoryFilter").html(data);
            //$("#StoriesId").empty();
            //console.log("Filtered Story");
            //$("#StoriesId").html(data);




        },
        error: function (e) {
            /*    debugger*/
            console.log("Bye");
            alert('Error');
        },
    });
}




// Handles the hover over the stars



//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }