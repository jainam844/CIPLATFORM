
function GetsCity() {
    var countryId = $('#countryId').find(":selected").val();

    $.ajax(
        {
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
function GetCity() {

    var countryIds = [];
    var countrydiv = document.getElementById("countryId");
    var list = countrydiv.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            countryIds.push(list[i].id);
        }

    }
    console.log("countryids:" + countryIds);
    /* var countryId = $('#countryId').find(":selected").val();*/

    $.ajax({
        url: "/Platform/GetCitys",
        method: "POST",
        data: {
            'countryId': countryIds
        },
        success: function (data) {
            data = JSON.parse(data);
            console.log(data);
            $("#selectCityList").empty();
            //    document.getElementById("selectCityList").innerHTML += `
            //<option value=${name}> City </option>
            //`;

            document.getElementById("selectCityList").innerHTML += `
          <ul class="dropdown-menu">
        `;
            data.forEach((name) => {
                //        document.getElementById("selectCityList").innerHTML += `
                //<option value=${name.CityId} >${name.Name}</option>
                //`;

                document.getElementById("selectCityList").innerHTML += `
                     <li><input type="checkbox" name="city" id="${name.CityId}" class="city_${name.CityId}" value="${name.Name}" />${name.Name}</li>
                    `;
            })
            document.getElementById("selectCityList").innerHTML += `
          </ul>
        `;
        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}
function filterBadges() {

    $("#filter-button").empty();
    $('input[name="country"]:checked').each(function () {
        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}" >
<div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'country')" class="bi bi-x"></i></div>
</button>
`
    });
    $('input[name="city"]:checked').each(function () {

        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}" >
<div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'city')" class="bi bi-x"></i></div>
</button>
`
    });
    $('input[name="theme"]:checked').each(function () {

        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}">
<div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'theme')" class="bi bi-x"></i></div>
</button>
`
    });

    $('input[name="skill"]:checked').each(function () {

        document.getElementById("filter-button").innerHTML += `
<button class="filter rounded-pill border" id="${this.value}">
<div style="width:max-content">${this.value} <i onclick="removeFilter(${this.id},'skill')" class="bi bi-x"></i></div>
</button>
`
    });

    document.getElementById("filter-button").innerHTML += `
<button class="clearall p-0 rounded-pill border" onclick="clearAll()">Clear all</button>
`
}

function removeFilter(checkboxId, type) {
    console.log("rm" + checkboxId);
    if (type == 'country') {
        $(".country_" + checkboxId).prop("checked", false);
        GetCity();
    }
    if (type == 'city') {
        $(".city_" + checkboxId).prop("checked", false);
    }
    if (type == 'theme') {
        $(".theme_" + checkboxId).prop("checked", false);
    }
    if (type == 'skill') {
        $(".skill_" + checkboxId).prop("checked", false);
    }

    temp();
    filterBadges();

}
function clearAll() {

    $('input[name="country"]:checked').each(function () {

        $(".country_" + this.id).prop("checked", false);
    });
    $('input[name="city"]:checked').each(function () {

        $(".city_" + this.id).prop("checked", false);
    });
    $('input[name="theme"]:checked').each(function () {

        $(".theme_" + this.id).prop("checked", false);
    });
    $('input[name="skill"]:checked').each(function () {

        $(".skill_" + this.id).prop("checked", false);
    });

    filterBadges();
    temp();
    $(".clearall").addClass("d-none");
}


function GetProfileCity() {

    var countryId = $('#countryId').find(":selected").val();
    debugger
    $.ajax({
        url: "/Platform/GetCitys",
        method: "POST",
        data: {
            'countryId': countryId
        },
        success: function (data) {
            data = JSON.parse(data);
            console.log(data);
            $("#selectCityList").empty();
            document.getElementById("selectCityList").innerHTML += `
            <option value=${name}> City </option>
            `;


            data.forEach((name) => {
                document.getElementById("selectCityList").innerHTML += `
                <option value=${name.CityId} >${name.Name}</option>
                `;

            });
        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}

var view = 1;

$(document).ready(function () {
    $("#list").click(function () {
        view = 2;
        temp();
        console.log(view);
    });

    $("#grid").click(function () {

        view = 1;
        temp();
    });
})

/*filter */
function temp(pg) {

    if (pg == undefined) {
        pg = 1;
    }
    console.log(pg);
    var checkedcntryvalues = [];
    var div1 = document.getElementById("countryId");
    var list = div1.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedcntryvalues.push(list[i].id);
        }
    }
    console.log(checkedcntryvalues);


    var checkedvalues = [];
    var div = document.getElementById("selectCityList");
    var list = div.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedvalues.push(list[i].id);
        }

    }
    console.log(checkedvalues);





    var checkedthemevalues = [];
    var div2 = document.getElementById("theme");
    var list = div2.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedthemevalues.push(list[i].id);
        }

    }
    console.log(checkedthemevalues);



    var checkedskillvalues = [];
    var div3 = document.getElementById("skill");
    var list = div3.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            checkedskillvalues.push(list[i].id);
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
            'view': view,

        },
        dataType: "html",
        success: function (data) {

            $("#filter").empty();
            console.log("grid Hii");
            $("#filter").html(data);

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

                document.getElementById("add_" + missionId).className = "bi bi-heart-fill text-danger";


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
                document.getElementById("add_" + missionId).className = "bi bi-heart";


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

    var Missiond = x;
    var toUserId = [];
    var recommand = document.getElementById("recommand");
    var list = recommand.getElementsByTagName("input");
    for (i = 0; i < list.length; i++) {
        if (list[i].checked) {
            toUserId.push(list[i].value);
        }

    }

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





        },
        error: function (e) {
            /*    debugger*/
            console.log("Bye");
            alert('Error');
        },
    });
}



function getActivity(x) {
    console.log("TimeSheet!!!!!!!!");
    {
        if (x > 0) {
            $.ajax({
                url: "/Profile/getActivity",
                method: "Post",
                data:
                {
                    "tid": x,
                },
                success: function (data) {
                    console.log(data);

                    $("#TimesheetTime").empty();
                    $("#TimesheetTime").html(data);
                },
                error: function (e) {
                    console.log("Bye");
                    alert('Error');
                },
            });
        }
        else {
            const myForm = document.querySelector('#timesheetform');

            myForm.querySelectorAll('.form-control').forEach((element, index) => {
                element.value = "";
            });
        }
    }
}

function getgoalActivity(x) {
    console.log("TimeSheet!!!!!!!!");
    {
        if (x > 0) {
            $.ajax({
                url: "/Profile/getGoalActivity",
                method: "Post",
                data:
                {
                    "tid": x,
                },
                success: function (data) {
                    console.log(data);
                    debugger
                    $("#TimesheetGoal").empty();
                    $("#TimesheetGoal").html(data);
                },
                error: function (e) {
                    console.log("Bye");
                    alert('Error');
                },
            });
        }
        else {
            const myForm = document.querySelector('#goalsheetform');

            myForm.querySelectorAll('.form-control').forEach((element, index) => {
                element.value = "";
            });
        }
    }
}




function settingsForNotification() {
    var Value;
    var settings = [];
    $('input[name="sn"]:checked').each(function () { Value = this.value; settings.push(Value); });
    console.log(settings);
    $.ajax(
        {
            url: "/Platform/settings",
            method: "post",
            data: { settings: settings },
            success: function () { toastr.success("You have changed your notification setting!!"); },
            error: function () { toastr.error("Something went wrong!!"); }
        });
}
function getsettings() {
    $.ajax({
        url: "/Platform/getsettings",
        method: "post", data: {},
        success: function (data) {
            data = JSON.parse(data);
            console.log(data);
            $("#RecommendedMission").prop("checked", data.RecommendedMission);
            $("#Story").prop("checked", data.Story);
            $("#NewMission").prop("checked", data.NewMission);
            $("#RecommendedStory").prop("checked", data.RecommendedStory);
            $("#MissionApplication").prop("checked", data.MissionApplication);
            $("#EmailNotification").prop("checked", data.EmailNotification);
        }, error: function () { toastr.error("Something went wrong!!"); }
    });
}


function getnotification() {
    $.ajax({
        url: "/Platform/getnotification",
        method: "post",
        data: {},
        success: function (data) {
            console.log(data);
            $("#newnoti").empty();
            $("#newnoti").html(data);
            console.log($("#todisplay"));
            console.log($("#fromdisplay"));
            var abc = document.querySelector("#fromdisplay").textContent;
            console.log(abc);
            $("#todisplay").text(abc);
        },
        error: function () {
            toastr.error("Something went wrong!!");
        }
    });
}

function readNotification(x, y) {
 
    $.ajax({
        url: "/Platform/readNotification",
        method: "post",
        data: {
            "id": y,
            "status": x
        },
        success: function (data) {

        },
        error: function () {

            toastr.error("Something went wrong!!");

        }

    });
    return true;
}