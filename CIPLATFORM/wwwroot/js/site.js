
function GetCity() {
    var countryId = $('#countryId').find(":selected").val();
/*    debugger;*/
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





function temp() {







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
            'sort': sort
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



window.onload = opengrid();
function opengrid() {
    console.log("Grid");
    var div1 = document.getElementById("list-view");
    var div2 = document.getElementById("grid-view");
    div1.style.display = 'none';
    div2.style.display = 'block';
    console.log("Done");
}
function openlist() {
    console.log("List");
    var div1 = document.getElementById("list-view");
    var div2 = document.getElementById("grid-view");
    div1.style.display = 'block';
    div2.style.display = 'none';
    console.log("Done");
}





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



//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }