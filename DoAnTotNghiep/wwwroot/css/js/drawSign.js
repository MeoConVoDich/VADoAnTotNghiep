var canvas, ctx, flag = false,
    prevX = 0,
    currX = 0,
    prevY = 220,
    currY = 220,
    dot_flag = false;

let coord = { x: 0, y: 0 };

var x = "black",
    y = 4;
const fontSizeSelect = document.getElementById("fontSize");
const fontFamilySelect = document.getElementById("fontFamily");
const boldCheckbox = document.getElementById("boldCheckbox");
const italicCheckbox = document.getElementById("italicCheckbox");
const textToStyle = document.getElementById("textToStyle");
let singleSignCanvasImgId;
let multipleSignCanvasImgId;
let isSignleSign;

function toggleBold(id, value) {
    if (value) {
        document.getElementById(id).style.fontWeight = "bold";
    } else {
        document.getElementById(id).style.fontWeight = "normal";
    }
}
function toggleItalic(id, value) {
    if (value) {
        document.getElementById(id).style.fontStyle = "italic";
    } else {
        document.getElementById(id).style.fontStyle = "normal";
    }
}
function changeFontSize(id, value) {
    document.getElementById(id).style.fontSize = value + "px";
}
function changeFontFamily(id, value) {
    document.getElementById(id).style.fontFamily = value;
}


function initOld() {
    canvas = document.getElementById('can');
    ctx = canvas.getContext("2d");
    w = canvas.width;
    h = canvas.height;

    canvas.addEventListener("mousemove", function (e) {
        findxy('move', e)
    }, false);
    canvas.addEventListener("mousedown", function (e) {
        findxy('down', e)
    }, false);
    canvas.addEventListener("mouseup", function (e) {
        findxy('up', e)
    }, false);
    canvas.addEventListener("mouseout", function (e) {
        findxy('out', e)
    }, false);
}

function init(idInit, canvasImg, isSignle) {
    canvas = document.getElementById(idInit);
    ctx = canvas.getContext("2d");
    w = canvas.width;
    h = canvas.height;
    isSignleSign = isSignle;
    if (isSignleSign) {
        singleSignCanvasImgId = canvasImg;
    }
    else {
        multipleSignCanvasImgId = canvasImg;
    }
    document.addEventListener("mousedown", start);
    document.addEventListener("mouseup", stop);
    window.addEventListener("resize", resize);
    resize();
}

function resize() {
    ctx.canvas.width = 400;
    ctx.canvas.height = 400;
}
function reposition(event) {
    coord.x = event.clientX - canvas.offsetLeft;
    coord.y = event.clientY - canvas.offsetTop - 50;
}
function start(event) {
    document.addEventListener("mousemove", draw);
    reposition(event);
}
function stop() {
    document.removeEventListener("mousemove", draw);
    save();
}
function draw(event) {
    ctx.beginPath();
    ctx.lineWidth = 5;
    ctx.lineCap = "round";
    ctx.strokeStyle = "#000";
    ctx.moveTo(coord.x, coord.y);
    reposition(event);
    ctx.lineTo(coord.x, coord.y);
    ctx.stroke();
}

function color(obj) {
    switch (obj.id) {
        case "green":
            x = "green";
            break;
        case "blue":
            x = "blue";
            break;
        case "red":
            x = "red";
            break;
        case "yellow":
            x = "yellow";
            break;
        case "orange":
            x = "orange";
            break;
        case "black":
            x = "black";
            break;
        case "white":
            x = "white";
            break;
    }
    if (x == "white") y = 14;
    else y = 2;

}

function draw1() {
    ctx.beginPath();
    ctx.moveTo(prevX, prevY);
    ctx.lineTo(currX, currY);
    ctx.strokeStyle = "#000";
    ctx.lineWidth = 5;
    ctx.lineCap = "round";
    ctx.stroke();
    ctx.closePath();
}

function erase() {
    ctx.clearRect(0, 0, w, h);
    save();
}
function erase1() {
    ctx.clearRect(0, 0, w, h);
}
window.drawFunction = {
    saveDraw: function (dotnetHelper, nameFunc) {
        let canvasImg;
        if (isSignleSign) {
            canvasImg = singleSignCanvasImgId;
        }
        else {
            canvasImg = multipleSignCanvasImgId;
        }
        document.getElementById(canvasImg).style.border = "0px solid";
        var dataURL = canvas.toDataURL();
        document.getElementById(canvasImg).src = dataURL;
        document.getElementById(canvasImg).style.display = "inline";
        if (isCanvasEmpty()) {
            dataURL = null;
            dotnetHelper.invokeMethodAsync(nameFunc, dataURL);
            return;
        }
        dotnetHelper.invokeMethodAsync(nameFunc, dataURL);
    }
}

function save() {

    if (isSignleSign == true) {
        document.getElementById(singleSignCanvasImgId).style.border = "0px solid";
        var dataURL = canvas.toDataURL();
        document.getElementById(singleSignCanvasImgId).src = dataURL;
        document.getElementById(singleSignCanvasImgId).style.display = "inline";
    }
    else if (isSignleSign == false) {

        document.getElementById(multipleSignCanvasImgId).style.border = "0px solid";
        var dataURL = canvas.toDataURL();
        document.getElementById(multipleSignCanvasImgId).src = dataURL;
        document.getElementById(multipleSignCanvasImgId).style.display = "inline";
    }

}

function findxy(res, e) {
    if (res == 'down') {
        prevX = currX;
        prevY = currY;
        currX = e.clientX - canvas.offsetLeft - 15;
        currY = e.clientY - canvas.offsetTop - 10;

        flag = true;
        dot_flag = true;
        if (dot_flag) {
            ctx.beginPath();
            ctx.fillStyle = x;
            ctx.fillRect(currX, currY, 2, 2);

            ctx.closePath();
            dot_flag = false;
        }
    }
    if (res == 'up' || res == "out") {
        flag = false;
        save();
    }
    if (res == 'move') {
        if (flag) {
            prevX = currX;
            prevY = currY;
            currX = e.clientX - canvas.offsetLeft - 15;
            currY = e.clientY - canvas.offsetTop - 10;
            draw();
        }
    }

}

function loadCanvasFromImg(path) {
    var image = new Image();
    image.src = path;
    image.id = "bnc";
    image.onload = function () {
        document.body.appendChild(image);
        image.style.display = "none";
        ctx.drawImage(image, 0, 0);
        ctx.fillStyle = "rgba(250, 0, 0, 0)";
        ctx.fillRect(0, 0, 500, 500);
        save();
    }
}

function changeDrawTab(idNone, idBlock) {
    document.getElementById(idNone).style.display = "none";
    document.getElementById(idBlock).style.display = "block";
}
function changeImageTab(idNone, idBlock) {
    document.getElementById(idNone).style.display = "none";
    document.getElementById(idBlock).style.display = "block";
}

function isCanvasEmpty() {
    var pixelData = ctx.getImageData(0, 0, canvas.width, canvas.height).data;
    for (var i = 0; i < pixelData.length; i++) {
        if (pixelData[i] !== 0) {
            return false;
        }
    }
    return true;
}