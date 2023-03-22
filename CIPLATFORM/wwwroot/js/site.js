
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


$('#page li a').click(function () {

    var pageIndex = $(this).text();
    console.log(pageIndex)
   
    temp(pageIndex);
});

/*filter */
function temp(z) {


    var pageIndex = z;
    if (pageIndex == undefined) {
        pageIndex = 1;
    }




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
            "pageIndex": pageIndex
        },  
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {
            document.getElementById("selectCityList").innerHTML += `
        <option value=${name}> City </option>
        `;
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
        success: function (missions) {

            if (missions == true) {
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart-fill");
                $('#addToFav').css("color", "red");

               
            }
            else {
                $('#addToFav').css("color", "black");
                $('#addToFav').removeClass();
                $('#addToFav').addClass("bi bi-heart");

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
     
            if (missions == true) {
                document.getElementById("okayyy").innerHTML += `
      Applied Successfully...!!!
        `
                $('#applyMission').prop('disabled', true);
                $('#applyMission').text("Applied");
                $('#applyMission').css("color", "red");
            }
            else {
                document.getElementById("fail").innerHTML += `
     You've already Applied...!!! 
        `
                $('#addToFav').css("color", "black");

            }

        },
        error: function (request, error) {
            console.log("function not working");
            alert('Error');
        },

    });

}



function comment(missionid)
{
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

            $("#comment").html();
            console.log("Added ");
            window.location.reload();

        },
        error: function (e) {
            console.log("Bye");
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
            console.log(toUserId);

        }
        ,
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}


//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }