function insertTextToTextarea(id, text) {
    var curPos = document.getElementById(id).selectionStart;
    console.log(curPos);
    let x = $("#" + id).val();
    return { cursorPosition: curPos, text: x.slice(0, curPos) + text + x.slice(curPos) };
}

function setCursorPosition(curPos) {
    $("#" + id).selectionStart = curPos;
}

function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}