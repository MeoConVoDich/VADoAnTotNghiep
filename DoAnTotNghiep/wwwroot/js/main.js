(function ($) {

    "use strict";



})(jQuery);

function downloadFileSimple(url, duongDan, nameFile, method) {
    if (method == undefined) {
        method = "POST";
    }
    $.ajax({
        url: url,
        method: method,
        data: {
            '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value,
            duongDan: duongDan
        },
        dataType: 'binary',
        processData: 'false',
        responseType: 'arraybuffer',
        headers: { 'X-Requested-With': 'XMLHttpRequest' },
        success: function (data, textStatus, jqXHR) {
            if (textStatus == "success") {
                const type = jqXHR.getResponseHeader('Content-Type');
                var blob = new Blob([data], { type });
                var downloadUrl = URL.createObjectURL(blob);
                var a = document.createElement("a");
                a.href = downloadUrl;
                a.download = nameFile;
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
            }
        },
        error: function () {
            console.log("Lỗi tải file");
        }
    });
}

function downloadFile(url, method, filterData, fileName, dotNetHelper, methodName) {
    if (method == undefined) {
        method = "POST";
    }
    $.ajax({
        url: url,
        method: method,
        dataType: 'binary',
        data: {
            '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value,
            reportModel: JSON.stringify(filterData),
        },
        processData: 'false',
        responseType: 'arraybuffer',
        success: function (data, textStatus, jqXHR) {
            if (textStatus == "success") {
                const type = jqXHR.getResponseHeader('Content-Type');
                var blob = new Blob([data], { type });
                var downloadUrl = URL.createObjectURL(blob);
                var a = document.createElement("a");
                a.href = downloadUrl;
                a.download = fileName;
                document.body.appendChild(a);
                a.click();
                document.body.removeChild(a);
                dotNetHelper.invokeMethodAsync(methodName, fileName);
            }
        },
        error: function () {
            dotNetHelper.invokeMethodAsync(methodName, null);
        }
    });
}

$(".toggle-password").click(function () {

    $(this).toggleClass("fa-eye fa-eye-slash");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});

function reloadIframe(id) {
    var iframe = document.getElementById(id);
    if (iframe != null) {

        iframe.contentWindow.location.reload();
    }
}
function login(url, username, password, isremember, dotNetHelper, methodName) {
    $.ajax({
        url: url,
        method: "POST",
        data: {
            '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value,
            'Input.UserName': username,
            'Input.Password': password,
            'Input.IsRemember': isremember
        },
        success: function (result) {
            if (result == "true") {
                dotNetHelper.invokeMethodAsync(methodName, "true");
                return;
            }
            dotNetHelper.invokeMethodAsync(methodName, "false");
        },
        error: function () {
            dotNetHelper.invokeMethodAsync(methodName, null);
        }
    });
}

function openNewTab(url) {
    var a = document.createElement("a");
    a.href = url;
    a.target = "_blank";
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
}

function addCookies(key, value) {
    document.cookie = `${key}=${value}; path=/`;
}
