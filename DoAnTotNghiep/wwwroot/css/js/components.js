window.app = {
    connection: "",
    id: "",
    isConnect: false,
    init: function () {
        connection = new signalR.HubConnectionBuilder()
            .withUrl("/appHub", { transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling })
            .configureLogging(signalR.LogLevel.Information)
            .build();

        connection.on("SignOut", (url) => {
            url = window.location.protocol + "//" + window.location.host + url;
            $.ajax({
                url: window.location.protocol + "//" + window.location.host + '/dang-xuat?sub=' + Date.now(),
                method: "GET",
                data: {
                    '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value
                },
            });
            expired(url);
        });
        connection.start().then(function () {
            console.log("connected");
            isConnect = true;
            if (typeof id !== 'undefined') {
                connection.invoke("SignIn", id).catch(err => console.error(err.toString()));
            }
        });
    },
    close: function () {
        connection.connection.stop();
    },
    setId: function (id) {
        this.id = id;
        if (isConnect) {
            console.log(id);
            connection.invoke("SignIn", id).catch(err => console.error(err.toString()));
        }
    },
    refreshSession: function () {
        $.ajax({
            url: window.location.protocol + "//" + window.location.host + '/lam-moi-phien',
            method: "POST",
            data: {
                '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value
            },
            success: function (result) {
                if (result == true) {
                    $.ajax({
                        url: window.location.protocol + "//" + window.location.host + '/lam-moi-token',
                        method: "POST",
                        data: {
                            '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value
                        },
                        success: function (data) {
                            document.getElementsByName("__RequestVerificationToken")[0].value = data;
                        }
                    });
                }
            }
        });

    }
}


window.select2Component = {
    init: function (Id, mutilple) {
        //if (mutilple === true) {
        //    $('#' + Id).select2({
        //        //multiple: true,
        //        //tags: true
        //    }).one('select2-focus', sl2focus).on('select2-blur', function () {
        //        $(this).one('select2-focus', sl2focus);
        //        console.log("blur");
        //    });
        //}
        //else {
        //    //Initialize Select2 Elements
        //    $('#' + Id).select2().one('select2-focus', sl2focus).on('select2-blur', function () {
        //        $(this).one('select2-focus', sl2focus);
        //        console.log("blur");
        //    });
        //}

        //Initialize Select2 Elements
        $('#' + Id).select2().one('select2-focus', sl2focus).on('select2-blur', function () {
            $(this).one('select2-focus', sl2focus);
            console.log("blur");
        });

        function sl2focus() {
            console.log("1");
            var sl2 = $(this).data('select2');
            setTimeout(function () {
                if (!select2.opened()) {
                    sl2.open();
                }
            }, 0);
        }
    },
    onChange: function (id, dotnetHelper, nameFunc) {
        $('#' + id).on('select2:select', function (e) {
            dotnetHelper.invokeMethodAsync(nameFunc, $('#' + id).val());
        });
    },
    onRemove: function (id, dotnetHelper, nameFunc) {
        $('#' + id).on('select2:unselect', function (e) {
            dotnetHelper.invokeMethodAsync(nameFunc, $('#' + id).val());
        });
    },
    setData: function (id, data) {
        $('#' + id).val(data).trigger("change");
    }
};

window.fieldInputWithMask = {
    init: function (id, dotnetHelper, nameFunc, mask) {
        if (mask === 'int-input-mask') {

            $('#' + id).inputmask("decimal", {
                rightAlignNumerics: true,
                groupSeparator: '.',
                radixPoint: ',',
                autoGroup: true,
                allowMinus: false,
                allowPlus: false,
                digits: 0,
                integerDigits: 10
            });
        }
        else if (mask === 'int-mask') {
            $(".int-mask").inputmask("decimal", {
                rightAlignNumerics: true,
                groupSeparator: '.',
                radixPoint: ',',
                autoGroup: true,
                digits: 0,
                integerDigits: 15,
                allowMinus: false,
                allowPlus: false
            });
        }
        else if (mask === 'decimal-mask') {
            var integerDigits = "";
            var digits = "";
            var max1 = "";
            var minus = false;
            integerDigits = $('#' + id).attr('integer-digits');
            digits = $('#' + id).attr('digits');
            max1 = $('#' + id).attr('max');
            if ($('#' + id).attr('minus') != undefined) {
                minus = true;
            }

            if (integerDigits != undefined && digits != undefined) {
                $('#' + id).inputmask("decimal", {
                    rightAlignNumerics: false,
                    groupSeparator: '.',
                    radixPoint: ',',
                    autoGroup: true,
                    digits: digits,
                    integerDigits: integerDigits,
                    allowPlus: false,
                    allowMinus: minus,
                    max: max1
                });
            }
            else {
                $('#' + id).inputmask("decimal", {
                    rightAlignNumerics: false,
                    groupSeparator: '.',
                    radixPoint: ',',
                    autoGroup: true,
                    digits: 0,
                    integerDigits: 15,
                    allowPlus: false,
                    allowMinus: false,
                });
            }
        }
        else if (mask === 'number-input-mask') {
            var integerDigits = "";
            var digits = "";
            integerDigits = $('#' + id).attr('integer-digits');
            digits = $('#' + id).attr('digits');

            if (integerDigits != undefined && digits != undefined) {

                $('#' + id).inputmask("9999999999", {
                    rightAlignNumerics: false,
                    autoGroup: true,
                });
            }
            else {
                $('#' + id).inputmask("decimal", {
                    rightAlignNumerics: false,
                    groupSeparator: '.',
                    radixPoint: ',',
                    autoGroup: true,
                    digits: 0,
                    integerDigits: 15,
                    allowPlus: false,
                    allowMinus: false,
                });
            }
        }
        else if (mask === "BHXHcode") {
            $('#' + id).inputmask("9999999999");
        }
        else if (mask === "datetime-mask") {
            $('#' + id).inputmask("datetime", {
                inputFormat: "dd/mm/yyyy",
                displayFormat: "dd/mm/yyyy",
                clearMaskOnLostFocus: true,
                clearIncomplete: true,
                min: "01/01/2000 00:00:00",
                max: "31/12/2100 00:00:00",
                placeholder: ""
            });
        }



        $('#' + id).on('change', function () {
            dotnetHelper.invokeMethodAsync(nameFunc, $('#' + id).val());
        });

    },
};


window.processComponent = {
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

window.fixedTableComponent = {
    resizeTable: function () {
        setTimeout(function () {


            var width_fixed_column = 0;

            $('.table-blazor').find('.DTFC_LeftHeadWrapper table th').each(function () {
                width_fixed_column += $(this).width();
            });

            var width_wrap = $('.table-blazor').find('.table-blazor-wrap').width();
            var width_scroll = width_wrap - width_fixed_column;

            $('.table-blazor').find('.dataTables_scrollBody').css({ 'margin-left': width_fixed_column, 'width': width_scroll });
            $('.table-blazor').find('.dataTables_scrollHead').css({ 'margin-left': width_fixed_column, 'width': width_scroll });

        }, 300);
    },

    render: function (id) {
        if ($('#' + id).find('.DTFC_LeftHeadWrapper').length > 0 && $('#' + id).find('.DTFC_LeftBodyLiner').length > 0) {
            this.cloneTable(id);
        }

        $('#' + id).find('.dataTables_scrollBody').on('scroll', function () {
            var pr = $(this).parent().parent().parent();
            var positionLeft = $(this).scrollLeft();
            var positionTop = $(this).scrollTop();


            $(pr).find('.dataTables_scrollHeadInner').css('transform', 'translateX(-' + positionLeft + 'px)');
        });
    },

    cloneTable: function (idTable) {
        var width_fixed_column = 0;
        var total_column = 0;

        $('#' + idTable).find('.DTFC_LeftHeadWrapper table th').each(function () {
            total_column += 1;
            width_fixed_column += $(this).width();
        });

        $('#' + idTable).find('.DTFC_LeftWrapper').css('width', width_fixed_column);

        if (parseInt($('#' + idTable).find('.DTFC_LeftHeadWrapper table').width() + 1) + "-" + parseInt(width_fixed_column + total_column * 2)) {

        } else {
            $('#' + idTable).find('.DTFC_LeftWrapper').css('width', parseInt(width_fixed_column + total_column * 2));
        }

        $('#' + idTable).find('.dataTables_scrollBody table tr').each(function (i) {
            var height_tr = $(this).height();
            $('#' + idTable).find('.DTFC_LeftBodyLiner table tr:nth-child(' + (i + 1) + ')').css('height', height_tr);
        })

        var width_wrap = $('#' + idTable).find('.table-blazor-wrap').width();
        var width_scroll = width_wrap - width_fixed_column;

        $('#' + idTable).find('.dataTables_scrollBody').css({ 'margin-left': width_fixed_column, 'width': width_scroll });
        $('#' + idTable).find('.dataTables_scrollHead').css({ 'margin-left': width_fixed_column, 'width': width_scroll });

        var stylePosition = $('#' + idTable).find('.DTFC_LeftBodyLiner').attr('style');
        if (stylePosition) {
            var re = /\d+/i;
            var positionScroll = stylePosition.match(re);

            $('#' + idTable).find('.dataTables_scrollBody').scrollTop(positionScroll[0]);
        }
    },
    scroller: function (id, id1, id2) {
        var s1 = $('.' + id1);
        var s2 = $('.' + id2);

        scroller1();
        scroller2();
        function scroller1() {
            $(s1).on('scroll', function () {
                $(s2).off('scroll');
                s2[0].scrollTop = $(this)[0].scrollTop;
                $(s2).on('scroll', scroller2);


                $('#' + id).find('.dataTables_scrollBody').on('scroll', function () {
                    var pr = $(this).parent().parent().parent();
                    var positionLeft = $(this).scrollLeft();

                    $(pr).find('.dataTables_scrollHeadInner').css('transform', 'translateX(-' + positionLeft + 'px)');
                });

            });
        }

        function scroller2() {
            $(s2).on('scroll', function () {
                $(s1).off('scroll');
                s1[0].scrollTop = $(this)[0].scrollTop;
                $(s1).on('scroll', scroller1);

            });
        }



    }
};

window.account = {
    logout: function () {
        window.location.href = ('/dang-xuat');
    },

    changePasswordNoti: function () {
        $('#changePassword').modal('hide');
        var modalConfirmChangePassword = $('<div></div>').addClass('modalConfirmChangePassword').appendTo('body');
        var boxMessage = $('<div></div>').addClass('boxMessage').appendTo(modalConfirmChangePassword);
        $('<p></p>').html('Đổi mật khẩu thành công!').appendTo(boxMessage);
        $('<a></a>').attr('href', '/dang-xuat').addClass('btn btn-sm btn-primary').html('<i class="fa fa-sign-in" aria-hidden="true"></i> Đăng nhập lại').appendTo(boxMessage);


        $.ajax({
            url: window.location.protocol + "//" + window.location.host + '/dang-xuat?sub=' + Date.now(),
            method: "GET",
            data: {
                '__RequestVerificationToken': document.getElementsByName("__RequestVerificationToken")[0].value
            },
        });

    }

}
window.tippyTooltip = {
    init: function (Id, IdTemplate) {
        const template = document.getElementById(IdTemplate);

        var aaa = tippy('#' + Id, {
            content: template.innerHTML,
            allowHTML: true,
            interactive: true,
            theme: 'tomato',
            popperOptions: { strategy: 'fixed' }
        });


    },

};

window.loadingServiceComponent = {
    showLarge: function () {
        var wrapLoading = $('<div></div>').addClass('wrapLoading').appendTo('body');
        $('<img>').attr('src', '../image/loading.svg').addClass('loading-process').css({ 'width': '60px' }).appendTo(wrapLoading);
    },

    hideLarge: function () {
        setTimeout(function () {
            $('.wrapLoading').remove();

        }, 200);
    },

};

