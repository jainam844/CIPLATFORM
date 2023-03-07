////function verifyPassword() {
////    var pw = document.getElementById("password").value;
////    var cpw = document.getElementById("cpassword").value;


////    if (pw != cpw) {

////        document.getElementById("Message").innerHTML = "Password does not matched";
////        document.getElementById('Message').style.color = 'red';
////        document.getElementById('submit').disabled = true;
////        document.getElementById('submit').style.opacity = (0.5);
////    }
////    else {
////        document.getElementById('Message').style.color = '';
////        document.getElementById('Message').innerHTML = '';
////        document.getElementById('submit').disabled = false;
////        document.getElementById('submit').style.opacity = (1);
////    }

////}

function GetCity() {
    var countryId = $('#countryId').find(":selected").val();
    debugger;
    $.ajax({
        url: "/Platform/GetCity",
        method: "GET",
        data: {
            "countryId": countryId
        },
        success: function (data) {
            data = JSON.parse(data);
            $("#selectCityList").empty();
            data.forEach((name) => {
                document.getElementById("selectCityList").innerHTML += `
        <option value=${name} >${name.Name}</option>
        `;
            })
        },
        error: function (request, error) {
            console.log(error);
        }
    })
}

   
//function preventBack() { window.history.forward(); }
//setTimeout("preventBack()", 0);
//window.onunload = function () { null }

