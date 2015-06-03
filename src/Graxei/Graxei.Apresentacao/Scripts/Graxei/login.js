$('#Modal2').modal('show');

function loadPage(result) {
    if (result.url) {
        // if the server returned a JSON object containing an url
        // property we redirect the browser to that url
        window.location.href = result.url;
    }
}

function login() {
    setTimeout(function () { $("#campoLogin").focus(); }, 5);
}
