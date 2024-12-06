function JSNextQuestion(id) {
    var totalPoints=0
    $('input:radio').each(function () {
        if ($(this).is(':checked')) {
            totalPoints = parseInt(totalPoints) + parseInt($(this).attr('weight'));
        }
        else {

        }
    });

    alert('Total Points : '+totalPoints);
}

function Submitques(id) {
    window.location.href = '/Home/levels?ID='+id;
}

function SaveQustionare(QID, SLNO,SCORE) {
    debugger;
    var fileData = new FormData();

    fileData.append('QID', QID);
    fileData.append('ANS', SLNO);
    fileData.append('POINTS', SCORE);

    var formURL = '/Questionnaire/AddQuestResult';

    /* start ajax submission process */
    $.ajax({
        url: formURL,
        type: "POST",
        data: fileData,
        processData: false,
        contentType: false,
        success: function (data, textStatus, jqXHR) {

            debugger;
            //alert(data);

            //window.location.href = '/Home/ManageQuestionnaire';

        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert(errorThrown);
        }

    });
}

function JSEditQuestion(QID) {


    if (QID == "")
        QID = 0;
    window.location.href = '/Home/ManageQuestionnaire?ID=' + QID;

}

function JSAddQuestionnaire() {

    debugger;
    var fileData = new FormData();

    var qid = $('#hdnQID').val();
    var question = $('#txtques').val().trim();
    var options1 = $('#txtopts1').val().trim();
    var options2 = $('#txtopts2').val().trim();
    var options3 = $('#txtopts3').val().trim();
    var options4 = $('#txtopts4').val().trim();

    var points1 = $('#txtpts1').val().trim();
    var points2 = $('#txtpts2').val().trim();
    var points3 = $('#txtpts3').val().trim();
    var points4 = $('#txtpts4').val().trim();
    var active = "1";


    if (question == "") {
        alert("Please enter the question!");
        return;
    }
    if (options1 == "") {
        alert("Please enter the first option!");
        return;
    }
    if (options2 == "") {
        alert("Please enter the second option!");
        return;
    }
    if (points1 == "") {
        alert("Please enter the point for first option!");
        return;
    }
    if (points2 == "") {
        alert("Please enter the point for the second option!");
        return;
    }


    if (qid == "")
        qid = 0;
    fileData.append('QID', qid);
    fileData.append('QUESTION', question);
    fileData.append('ANS1', options1);
    fileData.append('ANS2', options2);
    fileData.append('ANS3', options3);
    fileData.append('ANS4', options4);

    fileData.append('POINTS1', points1);
    fileData.append('POINTS2', points2);
    fileData.append('POINTS3', points3);
    fileData.append('POINTS4', points4);

    fileData.append('ACTIVE', active);
  
    var formURL = '/Questionnaire/AddQuestionnaire';

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
            
            window.location.href = '/Home/ManageQuestionnaire';

        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert(errorThrown);
        }

    });
}


function JSDelQues(QID) {
    debugger;
    var fileData = new FormData();
    fileData.append('questionid', QID);
    var formURL = '/Questionnaire/DeleteQuestionnaire';


    $.ajax({
        url: formURL,
        type: "POST",
        data: fileData,
        processData: false,
        contentType: false,

        success: function (data, textStatus, jqXHR) {

            debugger;
            alert(data);

            window.location.href = '/Home/ManageQuestionnaire';

        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            alert(errorThrown);
        }


    });

}