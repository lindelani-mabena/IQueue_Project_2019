function startTime(x) {
    if ((x.ss % 3) === 0 && (x.ss % 5) === 0) {
        document.getElementById('waitingTime').innerHTML = "&copy; iQueue 2019";
    }
    else {
        document.getElementById('waitingTime').innerHTML = check(x.hh) + ":" + check(x.mm) + ":" + check(x.ss);
    }
    if (x.ss > 0) {
        x.ss--;
    } else {
        if (x.mm > 0) {
            x.mm--;
            x.ss = 60;
        } else {
            x.mm = 60;
            if (x.hh > 0) {
                x.hh--;
                x.mm = 60;
            } else {
                x.hh = 23;
            }
        }
    }
}
function check(i) {
    return i < 10 ? `0${i}` : i;
}

