$.connection.hub.start()
    .done(function () { console.log("It works!!"); })
    .fail(function () { alert("It didn't work"); });