
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


$('#page li a').click(function () {

    var pageIndex = $(this).text();
    console.log(pageIndex)
   
    temp(pageIndex);
});



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



//$("#comment").click(function () {
//    var commenttext = $("#commenttext textarea").val();
//    var userid = @Convert.ToInt32(ViewBag.UserId);
//   /* var userId = (int)HttpContext.Session.GetInt32("userid");*/
//    var missionid = $("input[type='hidden']#mid").val();
//    console.log(commenttext);
//    console.log(userid);
//    console.log(missionid);
//    $.ajax({
//        type: 'POST',
//        url: '/Platform/Comment',
//        data: {
//            missionid: missionid,
//            UserId: userid,
//            commenttext: commenttext
//        },
//        success: function () {
//        }
//    });
//});
//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }









function comment(missionid)
{

    //var crd = document.getElementById("comment");
    var comnt = $("#comment_text").val();

    /*    var missionId = document.getElementsByClassName("mission_id").value;*/
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


        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}