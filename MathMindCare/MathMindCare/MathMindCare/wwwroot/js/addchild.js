function JSAddChild() {

    debugger;
    var fileData = new FormData();

    var ChildID = $('#hdnChildID').val();
    var Name = $('#txtname').val();
    var DOB = $('#txtDOB').val();
    var Gender = $('#ddlgender').val();
    var Address = $('#txtaddress').val();
    var Phone = $('#txtPhone').val();
    var Email = $('#txtemail').val();
    var agecategory = $('#ddlAge').val();
    if (!Name || !DOB  || !Gender || !Address  || !Phone || !Email) {
        alert("All fields are mandatory!");
        return;
    }

    if (!Email.endsWith(".com")) {
        alert("Write valid email!");
        return;
    }

    if (Phone.length !== 10) {
        alert("Phone number must be exactly 10 characters long!");
        return;
    }

    if (ChildID == "")
        ChildID = 0;
    fileData.append('CHILDID', ChildID);
    fileData.append('NAME', Name);
    fileData.append('DOB', DOB);
    fileData.append('GENDER', Gender);
    fileData.append('ADDRESS', Address);
    fileData.append('PHNO', Phone);
    fileData.append('EMAIL', Email);
    debugger;
    fileData.append('AGECATEGORY', agecategory);

    var formURL = '/addchild/NewChild';

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
            window.location.href = '/Home/addchild';

            //    }
            //    else {

            //        alert("Login Failed");
            //    }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert(errorThrown);
        }

    });
}

function JSEditChild(ChildID) {


    if (ChildID == "")
        ChildID = 0;
    window.location.href = '/Home/addchild?ID='+ChildID;

}

function SearchChild() {
    debugger;
    var text = $('#txtSearch').val();
    var ChildID = $('#hdnChildID').val();

    window.location.href = '/Home/addchild?ID=' + ChildID +'&search='+text;
}

function JSQuestionnare(ChildID) {      
    window.location.href = '/Home/levels?ID=' + ChildID;

}
