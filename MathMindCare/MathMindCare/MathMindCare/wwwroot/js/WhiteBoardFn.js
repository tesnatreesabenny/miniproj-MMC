
// JavaScript code to execute when the page loads
window.onload = function () {

};

function getNumber1(number) {
    debugger;
    document.getElementById('htnDrawNumber').value = number;
}




function pasteimage(img, x, y) {
    debugger;
    var images = new Array();
    var canvas = document.getElementById('drcanvas');
    var ctx = canvas.getContext('2d');
    images[0] = new Image();
    images[0].src = ('../img/Numbers/' + img + '.png');
    ctx.drawImage(images[0], x, y);

    
    //// select canvas elements
    //debugger;
    //var sourceCanvas = document.getElementById("btn" + img);
    //var destCanvas = document.getElementById("drcanvas");

    ////copy canvas by DataUrl
    //var sourceImageData = sourceCanvas.toDataURL("image/png");
    //var destCanvasContext = destCanvas.getContext('2d');

    //var destinationImage = new Image;
    //destinationImage.onload = function () {
    //    destCanvasContext.drawImage(destinationImage, x, y);
    //};
    //destinationImage.src = sourceImageData;
}