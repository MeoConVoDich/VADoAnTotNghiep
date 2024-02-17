/// <reference path="pdf.worker.js" />
let pdfDocument;
let PAGE_HEIGHT;

let pageNum = 1,
    pageIsRendering = false,
    pageNumIsPending = null;
const DEFAULT_SCALE = 1;
let pageNumberElement;
var defaultUrl = '../pdf/default.pdf';
//PDFJS.workerSrc = path;

async function LoadPdfFile(url, dotnetHelper, nameFunc) {
    if (url == null) {
        url = defaultUrl;
    }
    else {
        url = "../" + url;
    }
    PDFJS.getDocument(url).promise.then(pdf => {
        pdfDocument = pdf;
        let viewer = document.getElementById('viewer');
        viewer.innerHTML = "";
        for (let i = 0; i < pdf.pdfInfo.numPages; i++) {
            let page = createEmptyPage(i + 1);
            viewer.appendChild(page);
            loadPage(i + 1);
        }

        for (let i = 2; i <= pdf.pdfInfo.numPages; i++) {
            let visiblePage = document.querySelector(`.page[data-page-number="${i}"][data-loaded="false"]`);
            visiblePage.style.display = "none";
        }
        var pages = []; while (pages.length < pdf.pdfInfo.numPages) pages.push(pages.length + 1);
        let thumnail = document.getElementById("thumbnail");
        thumnail.innerHTML = "";
        pageNum = 1;
        return Promise.all(pages.map(function (num) {
            // create a div for each page and build a small canvas for it
            var div = document.createElement("div");
            div.style.border = "1px solid #ccc";
            div.style.marginBottom = "5px";
            thumnail.appendChild(div);
            return pdf.getPage(num).then(makeThumb)
                .then(function (canvas) {
                    canvas.addEventListener("click", handleThumbnailClick);
                    canvas.setAttribute("id", `page-${num}`);
                    canvas.setAttribute("class", `thumnails`);
                    div.appendChild(canvas);
                });
        }));


    })
        .then(() => {
            document.getElementById('prev-page').addEventListener('click', showPrevPage);
            document.getElementById('next-page').addEventListener('click', showNextPage);
            document.getElementById('prev-page').innerText = "<";
            document.getElementById('next-page').innerText = ">";
            pageNumberElement = document.getElementById('page-number');
            pageNumberElement.innerHTML = `${pageNum} / ${pdfDocument.numPages}`;
            document.getElementById('refresh-btn').addEventListener('click', handleRefresh)
        }).then(function () {
            dotnetHelper.invokeMethodAsync(nameFunc, true);
        });
}


function renderPage(pageIndexLoad) {

    let viewer = document.getElementById('viewer');
    viewer.innerHTML = "";

    let page = createEmptyPage(pageIndexLoad);
    viewer.appendChild(page);

    loadPage(pageIndexLoad);

}

//window.addEventListener('scroll', handleWindowScroll);

function makeThumb(page) {
    var vp = page.getViewport(1);
    var canvas = document.createElement("canvas");
    canvas.width = 120;
    canvas.height = 150;
    var scale = Math.min(canvas.width / vp.width, canvas.height / vp.height);
    scale = 0.2;
    return page.render({ canvasContext: canvas.getContext("2d"), viewport: page.getViewport(scale) }).promise.then(function () {
        return canvas;
    });
}

function handleThumbnailClick(e) {
    var pageCliked = parseInt(e.target.getAttribute("id").split("-")[1]);
    if (pageCliked == pageNum) return;
    let nowPage = document.querySelector(`.page[data-page-number="${pageNum}"]`);
    let visiblePage = document.querySelector(`.page[data-page-number="${pageCliked}"]`);
    if (visiblePage != null) {
        visiblePage.style.display = "block";
    }
    nowPage.style.display = "none";
    visiblePage.style.display = "block";
    pageNum = pageCliked;
    pageNumberElement.innerHTML = `${pageNum} / ${pdfDocument.numPages}`;
}


function createEmptyPage(num) {
    let page = document.createElement('div');
    let canvas = document.createElement('canvas');
    let wrapper = document.createElement('div');
    let textLayer = document.createElement('div');

    page.className = 'page';
    wrapper.className = 'canvasWrapper';
    textLayer.className = 'textLayer';

    page.setAttribute('id', `pageContainer${num}`);
    page.setAttribute('data-loaded', 'false');
    page.setAttribute('data-page-number', num);

    canvas.setAttribute('id', `page${num}`);

    page.appendChild(wrapper);
    page.appendChild(textLayer);
    wrapper.appendChild(canvas);

    return page;
}

function loadPage(pageIndexLoad) {
    return pdfDocument.getPage(pageIndexLoad).then(pdfPage => {
        let page = document.getElementById(`pageContainer${pageIndexLoad}`);
        let canvas = page.querySelector('canvas');
        let wrapper = page.querySelector('.canvasWrapper');
        let container = page.querySelector('.textLayer');
        let canvasContext = canvas.getContext('2d');
        let viewport = pdfPage.getViewport(DEFAULT_SCALE);

        canvas.width = viewport.width * 2;
        canvas.height = viewport.height * 2;
        page.style.width = `${viewport.width}px`;
        page.style.height = `${viewport.height}px`;
        wrapper.style.width = `${viewport.width}px`;
        wrapper.style.height = `${viewport.height}px`;
        container.style.width = `${viewport.width}px`;
        container.style.height = `${viewport.height}px`;

        pdfPage.render({
            canvasContext,
            viewport
        });

        pdfPage.getTextContent().then(textContent => {
            PDFJS.renderTextLayer({
                textContent,
                container,
                viewport,
                textDivs: []
            });
        });

        page.setAttribute('data-loaded', 'true');

        return pdfPage;
    });
}


function handleWindowScroll() {
    let visiblePageNum = Math.round(window.scrollY / PAGE_HEIGHT) + 1;
    let visiblePage = document.querySelector(`.page[data-page-number="${visiblePageNum}"][data-loaded="false"]`);
    if (visiblePage) {
        setTimeout(function () {
            loadPage(visiblePageNum);
            pageNum = visiblePageNum;
        });
    }
}


// Show Prev Page
const showPrevPage = () => {
    if (pageNum <= 1) {
        return;
    }

    let nowPage = document.querySelector(`.page[data-page-number="${pageNum}"]`);
    let visiblePage = document.querySelector(`.page[data-page-number="${pageNum - 1}"]`);
    if (visiblePage != null) {
        visiblePage.style.display = "block";
    }
    nowPage.style.display = "none";
    visiblePage.style.display = "block";
    pageNum--;
    pageNumberElement.innerHTML = `${pageNum} / ${pdfDocument.numPages}`;
};

// Show Next Page
const showNextPage = () => {
    if (pageNum >= pdfDocument.numPages) {
        return;
    }
    let nowPage = document.querySelector(`.page[data-page-number="${pageNum}"]`);
    let visiblePage = document.querySelector(`.page[data-page-number="${pageNum + 1}"][data-loaded="true"]`);
    if (visiblePage != null) {
        visiblePage.style.display = "block";
    }
    nowPage.style.display = "none";
    pageNum++;
    pageNumberElement.innerHTML = `${pageNum} / ${pdfDocument.numPages}`;
};


function handleRefresh() {
    const signerItems = document.querySelectorAll('.close-sign');
    //console.log(signerItems);
    for (let i = 0; i < signerItems.length; i++) {
        signerItems[i].click();
    }
}

function refreshPosition() {
    const signerItems = document.querySelectorAll('.btnSignDocument');
    for (let i = 0; i < signerItems.length; i++) {
        signerItems[i].remove();

    }
}

function convertToSlug(Text) {
    return Text.toLowerCase()
        .replace(/ /g, '-')
        .replace(/[^\w-]+/g, '');
}


function LoadPositionExist(name, className, page, xAxis, yAxis) {

    let currentPage = document.querySelector(`.page[data-page-number="${page}"][data-loaded="true"]`);
    if (currentPage != undefined) {
        const dataIcon = '<i class="fa fa-times close-sign" aria-hidden="true"></i>';
        const absolute = 'absolute';
        var btnSign = $('<span draggable="false"></span>').addClass(className + ' btnSignDocument').css({
            position: absolute,
            left: xAxis,
            bottom: yAxis - 60,
            zIndex: 100,
        }).html(name + dataIcon);
        btnSign.appendTo(currentPage);
    }
}


window.pdfViewerFunction = {
    initDrag: function (dotnetHelper, nameFunc) {

        document.getElementById("viewerContainer").addEventListener("dragover", function (event) {
            event.preventDefault();
            return false;
        });

        document.addEventListener("dragstart", function (event) {
            event.dataTransfer.setData("sign", event.target.className);
            event.dataTransfer.setData("id", event.target.id);
        });

        $('body').on('click', '.close-sign', function () {
            const btnSignRemove = $(this).parent();
            btnSignRemove.remove();
            var signerName = btnSignRemove[0].className.split(" ")[0];
            let signer = $('.btnSignMain.' + signerName)[0];
            let signerId = signer.getAttribute('id');
            signer.style.border = "1px solid #18A558";
            signer.style.backgroundColor = "#18A558";
            signer.style.opacity = "1";
            signer.style.color = "white";
            signer.draggable = true;
            dotnetHelper.invokeMethodAsync(nameFunc, signerId);
        });
    },

    dropSignButton: function (dotnetHelper, nameFunc) {

        var stringResult = null;
        document.getElementById("viewerContainer").addEventListener("drop", function (e) {
            var rect = e.target.getBoundingClientRect();
            console.log(rect);
            var x = e.clientX - rect.left - 75;
            var y = e.clientY - rect.top - 15;
            const absolute = 'absolute';
            const dataClass = event.dataTransfer.getData("sign").split(" ");
            const dataId = event.dataTransfer.getData("id");
            const dataText = $('.btnSignMain.' + dataClass[0]).text();
            const dataTextId = $('#' + dataId).text();
            const dataIcon = '<i class="fa fa-times close-sign" aria-hidden="true"></i>';
            var btnSign = $('<span draggable="false"></span>').addClass(dataClass[0] + ' btnSignDocument').css({
                position: absolute,
                left: x,
                top: y,
                zIndex: 100,
            }).html(dataTextId + dataIcon);
            btnSign.appendTo(e.target.parentElement);
            var signer = $('.btnSignMain.' + dataClass[0])[0];
            signer.style.border = "1px solid #ccc";
            signer.style.backgroundColor = "white";
            signer.style.opacity = "0.6";
            signer.style.color = "gray";
            signer.draggable = false;

            var result = $('<label></label>').html('PageNumber: ' + e.target.parentElement.getAttribute('data-page-number') + ' | ' + 'Top: ' + x + ' | Left: ' + y + ' | ID: ' + makeid(10) + ' | Type: ' + dataClass[0]);
            result.appendTo($('.result'));

            stringResult = e.target.parentElement.getAttribute('data-page-number') + '`' + x + '`' + ($('#viewer').height() - y) + '`' + dataId + '`' + dataClass[0];
            dotnetHelper.invokeMethodAsync(nameFunc, stringResult);
        });

        function makeid(length) {
            let result = '';
            const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
            const charactersLength = characters.length;
            let counter = 0;
            while (counter < length) {
                result += characters.charAt(Math.floor(Math.random() * charactersLength));
                counter += 1;
            }
            return result;
        }

    },


}
