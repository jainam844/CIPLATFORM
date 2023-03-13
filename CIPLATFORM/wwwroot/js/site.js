
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


//function temp() {
//    var checkedvalues = [];
//    var div = document.getElementById("SelectCityList");
//    var list = div.getElementsByTagName("option");
//    for (i = 0; i < list.length; i++) {
//        if (list[i].selected) {
//            checkedvalues.push(list[i].value);
//        }

//    }
   

//    var checkedcntryvalues = [];
//    var div1 = document.getElementById("countryId");
//    var list = div1.getElementsByTagName("option");
//    for (i = 0; i < list.length; i++) {
//        if (list[i].selected) {
//            checkedcntryvalues.push(list[i].value);
//        }

//    }
//    console.log(checkedcntryvalues);

//    var checkedthemevalues = [];
//    var div2 = document.getElementById("theme");
//    var list = div2.getElementsByTagName("option");
//    for (i = 0; i < list.length; i++) {
//        if (list[i].checked) {
//            checkedthemevalues.push(list[i].value);
//        }

//    }
//    console.log(checkedthemevalues);

//    var checkedskillvalues = [];
//    var div3 = document.getElementById("skill");
//    var list = div3.getElementsByTagName("option");
//    for (i = 0; i < list.length; i++) {
//        if (list[i].checked) {
//            checkedskillvalues.push(list[i].value);
//        }

//    }
//    console.log(checkedskillvalues);


//    var search = document.getElementById("searchb").value;
//    console.log(search)

//    var sort = document.getElementById("sort").value;
//    console.log(sort)

//    $.ajax({
//        type: "POST", 
//        url: '/Platform/Filter',
//        data: {
//            'cityId': checkedvalues,
          
//            'themeId': checkedthemevalues,
//            'skillId': checkedskillvalues,
//            'search': search,
//            'sort': sort
//            'countryId': checkedcntryvalues
//        },
//        dataType: "html", 
//        success: function (data) {

//            $("#grid-view").empty();
//            console.log("grid Hii");
//            $("#grid-view").html(data);
//            $("#list-view").empty();
//            console.log("list Hii");
//            $("#list-view").html(data);
//        },
//        error: function (e) {
//            console.log("Bye");
////            alert('Error');
////        },

////    });
////}

   
//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }














/*function temp() {*/
    /*    var checkedvalues = [];*/
    //var div = document.getElementById("zxc");
    //var list = div.getElementsByTagName("input");
    //for (i = 0; i < list.length; i++) {
    //    if (list[i].checked) {
    //        checkedvalues.push(list[i].value);
    //    }

    //}
    // console.log(checkedvalues);







    //var checkedcntryvalues = [];
    //var div1 = document.getElementById("countryId");
    //var list = div1.getElementsByTagName("option");
    //for (i = 0; i < list.length; i++) {
    //    if (list[i].selected) {
    //        checkedcntryvalues.push(list[i].value);
    //    }

    //}
    //console.log(checkedcntryvalues);





    //var checkedthemevalues = [];
    //var div2 = document.getElementById("theme");
    //var list = div2.getElementsByTagName("input");
    //for (i = 0; i < list.length; i++) {
    //    if (list[i].checked) {
    //        checkedthemevalues.push(list[i].value);
    //    }

    //}
    //console.log(checkedthemevalues);



    //var checkedskillvalues = [];
    //var div3 = document.getElementById("skill");
    //var list = div3.getElementsByTagName("input");
    //for (i = 0; i < list.length; i++) {
    //    if (list[i].checked) {
    //        checkedskillvalues.push(list[i].value);
    //    }

    //}
    //console.log(checkedskillvalues);



    //var search = document.getElementById("searchb").value;
    //console.log(search)
    //var sort = document.getElementById("sort").value;
    //console.log(sort)
    //$.ajax({
    //    type: "POST",
    //    url: '/Platform/Filter',
    //    data: {
    
    //        'countryId': checkedcntryvalues,
    //        'themeId': checkedthemevalues,
    //        'skillId': checkedskillvalues,
            //'search': search,
            //'sort': sort
        //},
        //dataType: "html",
      
        //success: function (data) {
        //    console.log("jainam");
         
        //        console.log("jainammmmmmmm");
        //        $("#grid-view").empty();
        //        console.log("grid Hii");
        //        $("#grid-view").html(data);
                //$("#list-view").empty();
                //console.log("list Hii");
                //$("#list-view").html(data);
            
//        },
//        error: function (e) {
//            console.log("Bye");
//            alert('Error');
//        },
//    });
//}





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


    //var sort = document.getElementById("sort").value;
    //console.log(sort)


    $.ajax({
        type: "POST", // POST
        url: '/Platform/Filter',
        data: {
            'cityId': checkedvalues,
            'countryId': checkedcntryvalues,
            'themeId': checkedthemevalues,
            'skillId': checkedskillvalues,
            'search': search,
            //'sort': sort
        },
        dataType: "html", // return datatype like JSON and HTML
        success: function (data) {

            $("#grid-view").empty();
            console.log("grid Hii");
            $("#grid-view").html(data);
            //$("#list-view").empty();
            //console.log("list Hii");
            //$("#list-view").html(data);
        },
        error: function (e) {
            console.log("Bye");
            alert('Error');
        },
    });
}
