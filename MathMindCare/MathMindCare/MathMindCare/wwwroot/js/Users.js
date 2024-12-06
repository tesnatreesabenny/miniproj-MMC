
function JsValidateUser() {

    debugger;
    var fileData = new FormData();

    var UserID = $('#txtUserID').val();
    var Password = $('#txtPassword').val();

    fileData.append('UserID', UserID);
    fileData.append('Password', Password);
    fileData.append('userType', "1");
    var formURL = '/Login/ValidateUser1';

    /* start ajax submission process */
    $.ajax({
        url: formURL,
        type: "POST",
        data: fileData,
        processData: false,
        contentType: false,
        success: function (data, textStatus, jqXHR) {
            
            debugger;
            if (data.userid != null && data.userid != '') {
                if (data.logintype == "1") {
                 
                   
                    window.location.href = '/Home/addchild?ID=0';
                    
                }
                else if (data.logintype == "2") {
                    window.location.href = '/Home/dashboard';
                }
                else if (data.logintype == "0"){
                    window.location.href = '/Home/ManageUsers';
                }

            }
            else {

                alert("Login Failed");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert(errorThrown);
        }

    });
}




function JSAddUser() {
     
    debugger;
    var fileData = new FormData();

    var UserID = $('#username').val();
    var Password = $('#password').val();
    var EmailID = $('#email').val();
    var Name = $('#name').val();
    var Phone = $('#phone').val();


   // string UserID1, string Password1, string userType1, string EmailID1, string Name, string Phone)


    var userType;
    if (document.getElementById('rdoparentTeacher').checked == true) {
        userType = "1";
    }
    else {
        userType = "2";
    }

    if (UserID == "" || Password == "" || EmailID == "" || Name == "") {
        alert("All fields are mandatory!");
        return;
    }
    if (ValidateEmail(EmailID) == false) {
        alert("Invalid email");
        return;
    }

   

    if (Password.length < 8 || !/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/.test(Password)) {
        alert("Password must be at least 8 characters long and contain special characters!");
        return false;
    }

    fileData.append('UserID1', UserID);
    fileData.append('Password1', Password);
    fileData.append('userType1', userType);
    fileData.append('EmailID1', EmailID);
    fileData.append('Name', Name);
    fileData.append('Phone', Phone);

    var formURL = '/Login/NewUser';

    /* start ajax submission process */
    $.ajax({
        url: formURL,
        type: "POST",
        data: fileData,
        processData: false,
        contentType: false,
        success: function (data, textStatus, jqXHR) {
            debugger;
            if (data == "1") {
                alert("User ID alredy exists.");
            }
            else {
                debugger;
                alert(data);
                window.location.href = '/Home/Login';

            }

        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert(errorThrown);
        }

    });
}

function ValidateEmail(email) {
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}

function JSChangepwd() {

    debugger;
    var fileData = new FormData();

    var UserID = $('#username').val().trim();
    var Password1 = $('#npsd').val().trim();
    var Password2 = $('#cpsd').val().trim();
    var pwd = $('#hdbpwd').val().trim();
    var oldpwd = $('#oldpsd').val().trim();
    // string UserID1, string Password1, string userType1, string EmailID1, string Name, string Phone)

    if (Password1 == "" || Password2 == "" || oldpwd == "" ) {
        alert("All fields are mandatory!");
        return;
    }
   
    if (pwd != oldpwd) {
        alert("Enter correct password!");
        return;
    }
    if (Password1 != Password2) {
        alert("Password doesn't match!");
        return;
    }

   


    fileData.append('UserID', UserID);
    fileData.append('Password', Password1);

    var formURL = '/Login/Changepwd';

    /* start ajax submission process */
    $.ajax({
        url: formURL,
        type: "POST",
        data: fileData,
        processData: false,
        contentType: false,
        success: function (data, textStatus, jqXHR) {

            debugger;
            alert(data);
            //    if (data == 'OK') {
            //        alert("Login Success");
            window.location.href = '/Home/Login?ID=0';

            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert(errorThrown);
        }

    });
}




