window.loadingComponent = {
    show: function (id) {
        $('<img>').attr('src', '../image/loading.gif').addClass('loading-process').css({ 'width': '18px' }).insertAfter($('#' + id));
    },

    hide: function (id) {
        var parent = $('#' + id).parent();
        parent.find('img.loading-process').remove();
    },

    showLarge: function () {
        var wrapLoading = $('<div></div>').addClass('wrapLoading').appendTo('body');
        $('<img>').attr('src', '../image/loading.gif').addClass('loading-process').css({ 'width': '60px' }).appendTo(wrapLoading);
    },

    hideLarge: function () {
        setTimeout(function () {
            $('.wrapLoading').remove();

        }, 200);
    },

};


function signContract(signInfo, dotnetHelper, nameFunc, callback) {
    window.hwcrypto.selectCertSerial({
        lang: "en"
    }).then(function (response) {
        var certificate = response.value.cert;
        var jsondata = { base64Cert: certificate, signInfos: signInfo, '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
        $.ajax({
            type: "POST",
            url: "/sign-hash-contract",
            data: jsondata,
            success: function (result) {
                if (result.success === false) {
                    dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                    return;
                }
                var signData = {
                    "keys": []
                };
                var hashData = [];
                for (var i = 0; i < result.data.length; i++) {
                    hashData.push(result.data[i].hash);
                }
                window.hwcrypto.signHashData(certificate, {
                    type: "hashcontract",
                    hex: hashData
                }, {
                    lang: "en"
                }).then(function (response) {
                    var signedData = response.value.signature;
                    if (signedData === null) {
                        dotnetHelper.invokeMethodAsync(nameFunc, response.message);
                        return;
                    }
                    signData.signeds = signedData;
                    saveRecordSign(signData, function (e) {
                        dotnetHelper.invokeMethodAsync(nameFunc, e);
                    });
                }, function (err) {
                    dotnetHelper.invokeMethodAsync(nameFunc, "Không lấy được khóa của CTS, vui lòng kiểm tra lại CTS đã lựa chọn!");

                });
            }
        });
    }, function (err) {
        if (err.message === "no_implementation" || err.message === undefined) {
            dotnetHelper.invokeMethodAsync(nameFunc, "Bạn chưa tool ký số, vui lòng kiểm tra lại");
        }
        else if (err.result === "user_cancelled") {
            dotnetHelper.invokeMethodAsync(nameFunc, "Cancel_Sign");
        }
        else {
            dotnetHelper.invokeMethodAsync(nameFunc, err.result);
        }

    });
}

function signContractAppendix(signInfo, dotnetHelper, nameFunc, callback) {
    window.hwcrypto.selectCertSerial({
        lang: "en"
    }).then(function (response) {
        var certificate = response.value.cert;
        var jsondata = { base64Cert: certificate, signInfos: signInfo, '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
        $.ajax({
            type: "POST",
            url: "/sign-hash-contract-appendix",
            data: jsondata,
            success: function (result) {
                if (result.success === false) {
                    dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                    return;
                }
                var signData = {
                    "keys": []
                };
                var hashData = [];
                for (var i = 0; i < result.data.length; i++) {
                    hashData.push(result.data[i].hash);
                }
                window.hwcrypto.signHashData(certificate, {
                    type: "hashcontract",
                    hex: hashData
                }, {
                    lang: "en"
                }).then(function (response) {
                    var signedData = response.value.signature;
                    if (signedData === null) {
                        dotnetHelper.invokeMethodAsync(nameFunc, response.message);
                        return;
                    }
                    signData.signeds = signedData;
                    saveRecordAppendixSign(signData, function (e) {
                        dotnetHelper.invokeMethodAsync(nameFunc, e);
                    });
                }, function (err) {
                    dotnetHelper.invokeMethodAsync(nameFunc, "Không lấy được khóa của CTS, vui lòng kiểm tra lại CTS đã lựa chọn!");

                });
            }
        });
    }, function (err) {
        if (err.message === "no_implementation" || err.message === undefined) {
            dotnetHelper.invokeMethodAsync(nameFunc, "Bạn chưa tool ký số, vui lòng kiểm tra lại");
        }
        else if (err.result === "user_cancelled") {
            dotnetHelper.invokeMethodAsync(nameFunc, "Cancel_Sign");
        }
        else {
            dotnetHelper.invokeMethodAsync(nameFunc, err.result);
        }

    });
}
function signContractLiquidation(signInfo, dotnetHelper, nameFunc, callback) {
    window.hwcrypto.selectCertSerial({
        lang: "en"
    }).then(function (response) {
        var certificate = response.value.cert;
        var jsondata = { base64Cert: certificate, signInfos: signInfo, '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
        $.ajax({
            type: "POST",
            url: "/sign-hash-contract-liquidation",
            data: jsondata,
            success: function (result) {
                if (result.success === false) {
                    dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                    return;
                }
                var signData = {
                    "keys": []
                };
                var hashData = [];
                for (var i = 0; i < result.data.length; i++) {
                    hashData.push(result.data[i].hash);
                }
                window.hwcrypto.signHashData(certificate, {
                    type: "hashcontract",
                    hex: hashData
                }, {
                    lang: "en"
                }).then(function (response) {
                    var signedData = response.value.signature;
                    if (signedData === null) {
                        dotnetHelper.invokeMethodAsync(nameFunc, response.message);
                        return;
                    }
                    signData.signeds = signedData;
                    saveRecordAppendixSign(signData, function (e) {
                        dotnetHelper.invokeMethodAsync(nameFunc, e);
                    });
                }, function (err) {
                    dotnetHelper.invokeMethodAsync(nameFunc, "Không lấy được khóa của CTS, vui lòng kiểm tra lại CTS đã lựa chọn!");

                });
            }
        });
    }, function (err) {
        if (err.message === "no_implementation" || err.message === undefined) {
            dotnetHelper.invokeMethodAsync(nameFunc, "Bạn chưa tool ký số, vui lòng kiểm tra lại");
        }
        else if (err.result === "user_cancelled") {
            dotnetHelper.invokeMethodAsync(nameFunc, "Cancel_Sign");
        }
        else {
            dotnetHelper.invokeMethodAsync(nameFunc, err.result);
        }

    });
}

function saveRecordSign(signData, callback) {
    var contractjson = { signData: JSON.stringify(signData), '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
    $.ajax({
        type: "POST",
        url: "/save-contract-sign",
        data: contractjson,
        dataType: "json",
        success: function (data) {
            if (data.success === true) {
                callback(true);
            } else {
                callback(data.message);
            }
        }, error: function () {
            callback("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại");
        }
    });

}
function saveRecordAppendixSign(signData, callback) {
    var contractjson = { signData: JSON.stringify(signData), '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
    $.ajax({
        type: "POST",
        url: "/save-contract-sign-appendix",
        data: contractjson,
        dataType: "json",
        success: function (data) {
            if (data.success === true) {
                callback(true);
            } else {
                callback(data.message);
            }
        }, error: function () {
            callback("Có lỗi trong quá trình xử lý, vui lòng thực hiện lại");
        }
    });

}

function signContractServer(signInfo, dotnetHelper, nameFunc, callback) {
    var jsondata = { signInfos: signInfo, '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
    $.ajax({
        type: "POST",
        url: "/sign-hash-contract-server",
        data: jsondata,
        success: function (result) {
            if (result.success === false) {
                dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                return;
            }
            else {
                dotnetHelper.invokeMethodAsync(nameFunc, true);
            }
        }, error: function () {
            dotnetHelper.invokeMethodAsync(nameFunc, "Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
        }
    });
}

function signContractAppendixServer(signInfo, dotnetHelper, nameFunc, callback) {
    var jsondata = { signInfos: signInfo, '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
    $.ajax({
        type: "POST",
        url: "/sign-hash-contract-appendix-server",
        data: jsondata,
        success: function (result) {
            if (result.success === false) {
                dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                return;
            }
            else {
                dotnetHelper.invokeMethodAsync(nameFunc, true);
            }
        }, error: function () {
            dotnetHelper.invokeMethodAsync(nameFunc, "Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
        }
    });
}

function signContractLiquidationServer(signInfo, dotnetHelper, nameFunc, callback) {
    var jsondata = { signInfos: signInfo, '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value };
    $.ajax({
        type: "POST",
        url: "/sign-hash-contract-liquidation-server",
        data: jsondata,
        success: function (result) {
            if (result.success === false) {
                dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                return;
            }
            else {
                dotnetHelper.invokeMethodAsync(nameFunc, true);
            }
        }, error: function () {
            dotnetHelper.invokeMethodAsync(nameFunc, "Có lỗi trong quá trình xử lý, vui lòng thực hiện lại.");
        }
    });
}
window.getCertValueFunction = {
    getCert: function (dotnetHelper, nameFunc) {
        window.hwcrypto.selectCertSerial({
            lang: "en"
        }).then(function (response) {
            var certificate = response.value.cert;
            dotnetHelper.invokeMethodAsync(nameFunc, certificate);
        }, function (err) {
            if (err.message === "no_implementation" || err.message === undefined) {
                dotnetHelper.invokeMethodAsync(nameFunc, "no_implementation");
            }
        });
    }
}

function signContractWithOutSelectCert(cert, signInfo, dotnetHelper, nameFunc, callback) {
    var certificate = cert;

    var jsondata = {
        base64Cert: certificate,
        signInfos: signInfo,
        '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value
    };

    $.ajax({
        type: "POST",
        url: "/sign-hash-contract",
        data: jsondata,
        success: function (result) {
            if (result.success === false) {
                dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                return;
            }

            var signData = {
                "keys": []
            };

            var hashData = [];
            for (var i = 0; i < result.data.length; i++) {
                hashData.push(result.data[i].hash);
            }

            window.hwcrypto.signHashData(certificate, {
                type: "hashcontract",
                hex: hashData
            }, {
                lang: "en"
            }).then(function (response) {
                var signedData = response.value.signature;

                if (signedData === null) {
                    dotnetHelper.invokeMethodAsync(nameFunc, response.message);
                    return;
                }

                signData.signeds = signedData;

                saveRecordSign(signData, function (e) {
                    dotnetHelper.invokeMethodAsync(nameFunc, e);
                });
            }, function (err) {
                dotnetHelper.invokeMethodAsync(nameFunc, "Không lấy được khóa của CTS, vui lòng kiểm tra lại CTS đã lựa chọn!");
            });
        }
    });
}

function signAppendixWithOutSelectCert(cert, signInfo, dotnetHelper, nameFunc, callback) {
    var certificate = cert;

    var jsondata = {
        base64Cert: certificate,
        signInfos: signInfo,
        '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value
    };

    $.ajax({
        type: "POST",
        url: "/sign-hash-contract-appendix",
        data: jsondata,
        success: function (result) {
            if (result.success === false) {
                dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                return;
            }

            var signData = {
                "keys": []
            };

            var hashData = [];
            for (var i = 0; i < result.data.length; i++) {
                hashData.push(result.data[i].hash);
            }

            window.hwcrypto.signHashData(certificate, {
                type: "hashcontract",
                hex: hashData
            }, {
                lang: "en"
            }).then(function (response) {
                var signedData = response.value.signature;

                if (signedData === null) {
                    dotnetHelper.invokeMethodAsync(nameFunc, response.message);
                    return;
                }

                signData.signeds = signedData;

                saveRecordSign(signData, function (e) {
                    dotnetHelper.invokeMethodAsync(nameFunc, e);
                });
            }, function (err) {
                dotnetHelper.invokeMethodAsync(nameFunc, "Không lấy được khóa của CTS, vui lòng kiểm tra lại CTS đã lựa chọn!");
            });
        }
    });
}
function signLiquidWithOutSelectCert(cert, signInfo, dotnetHelper, nameFunc, callback) {
    var certificate = cert;

    var jsondata = {
        base64Cert: certificate,
        signInfos: signInfo,
        '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value
    };

    $.ajax({
        type: "POST",
        url: "/sign-hash-contract-liquidation",
        data: jsondata,
        success: function (result) {
            if (result.success === false) {
                dotnetHelper.invokeMethodAsync(nameFunc, result.message);
                return;
            }

            var signData = {
                "keys": []
            };

            var hashData = [];
            for (var i = 0; i < result.data.length; i++) {
                hashData.push(result.data[i].hash);
            }

            window.hwcrypto.signHashData(certificate, {
                type: "hashcontract",
                hex: hashData
            }, {
                lang: "en"
            }).then(function (response) {
                var signedData = response.value.signature;

                if (signedData === null) {
                    dotnetHelper.invokeMethodAsync(nameFunc, response.message);
                    return;
                }

                signData.signeds = signedData;

                saveRecordSign(signData, function (e) {
                    dotnetHelper.invokeMethodAsync(nameFunc, e);
                });
            }, function (err) {
                dotnetHelper.invokeMethodAsync(nameFunc, "Không lấy được khóa của CTS, vui lòng kiểm tra lại CTS đã lựa chọn!");
            });
        }
    });
}





