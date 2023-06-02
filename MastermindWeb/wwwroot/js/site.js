
//DRAG DROP CODES
var items = document.querySelectorAll(".peg");
var peg_boxes = document.querySelectorAll(".peg-box");
var textbox = document.getElementById("textpeg");
var aval_colors_block = document.getElementById("aval-colors");
var dragged; //dragged item

items.forEach(item => {
    item.addEventListener("dragstart", function (e) {
        dragged = e.target;
    }, false);
});

peg_boxes.forEach(box => {
    box.addEventListener("drop", function(e) {
        e.preventDefault();
        if (box.colorid != undefined) { //pri nahradzovani
            box.classList.remove(getClassColorName(box));
        }
        const className = getClassColorName(dragged);
        if (dragged.colorid != undefined) { //je zo zoznamu vybratych
            dragged.classList.add("peg-empty");
            dragged.classList.remove(className);
            box.colorid = dragged.colorid;
            delete dragged.colorid;
        } else { //pridany do zoznamu vybranych
            box.colorid = dragged.id;
            box.draggable = true;
        }
        
        box.classList.add(className);
        box.classList.remove("peg-empty");
        calculateText();
    });

    box.addEventListener("dragover", function(e) {
        e.preventDefault();
    });

    box.addEventListener("dragstart", function (e) {
        dragged = e.target;
    }, false);
});

aval_colors_block.addEventListener("drop", function(e) {
    if (dragged.classList.contains("peg-box")) {
        e.preventDefault();
        delete dragged.colorid;
        dragged.classList.add("peg-empty");
        dragged.classList.remove(getClassColorName(dragged));
        dragged.draggable = false;
        calculateText();
    }
}, false);

aval_colors_block.addEventListener("dragover", function (e) {
    e.preventDefault();
}, false);

function calculateText() {
    var text = "";
    for (let i = 0; i < peg_boxes.length; i++) {
        text += peg_boxes[i].colorid != undefined ? peg_boxes[i].colorid : "";
    }
    textbox.value = text;
}

function getClassColorName(element) {
    const forSearch = ["peg-Red", "peg-Green", "peg-Blue", "peg-Orange", "peg-Yellow", "peg-Cyan", "peg-White", "peg-Violet"];
    for (let i = 0; i < element.classList.length; i++) {
        if (forSearch.indexOf(element.classList[i]) !== -1) {
            return element.classList[i];
        }
    }
    return undefined;
}

//STOPWATCH FEATURE

const timer = document.getElementById("stopwatch");

var hr = 0;
var min = 0;
var sec = 0;
var stoptime = true;

function startTimer() {
    if (stoptime === true) {
        stoptime = false;
        timerCycle();
    }
}

function setTimer(hr, min, sec) {
    this.hr = hr;
    this.min = min;
    this.sec = sec;
    stoptime = true;
}

function stopTimer() {
    if (stoptime === false) {
        stoptime = true;
    }
}

function resetTimer() {
    timer.innerHTML = "00:00:00";
}

function timerCycle() {
    if (stoptime === false) {
        sec = parseInt(sec);
        min = parseInt(min);
        hr = parseInt(hr);

        sec = sec + 1;

        if (sec === 60) {
            min = min + 1;
            sec = 0;
        }
        if (min === 60) {
            hr = hr + 1;
            min = 0;
            sec = 0;
        }

        if (sec < 10 || sec === 0) {
            sec = `0${sec}`;
        }
        if (min < 10 || min === 0) {
            min = `0${min}`;
        }
        if (hr < 10 || hr === 0) {
            hr = `0${hr}`;
        }

        timer.innerHTML = `${hr}:${min}:${sec}`;

        setTimeout("timerCycle()", 1000);
    }
}

function scrollToComments() {
    const element = document.getElementById("comment-section");
    element.scrollIntoView();
}

//STARS
var stars = [5];
stars[0] = document.getElementById("star_1");
stars[1] = document.getElementById("star_2");
stars[2] = document.getElementById("star_3");
stars[3] = document.getElementById("star_4");
stars[4] = document.getElementById("star_5");

function starCheck_1() {
    stars[0].style.color = "orange";
}

function starUncheck_1()
{
    stars[0].style.color = "black";
}

function starCheck_2() {
    stars[0].style.color = "orange";
    stars[1].style.color = "orange";
}

function starUncheck_2()
{
    stars[0].style.color = "black";
    stars[1].style.color = "black";
}

function starCheck_3() {
    stars[0].style.color = "orange";
    stars[1].style.color = "orange";
    stars[2].style.color = "orange";
}

function starUncheck_3()
{
    stars[0].style.color = "black";
    stars[1].style.color = "black";
    stars[2].style.color = "black";
}

function starCheck_4() {
    stars[0].style.color = "orange";
    stars[1].style.color = "orange";
    stars[2].style.color = "orange";
    stars[3].style.color = "orange";
}

function starUncheck_4()
{
    stars[0].style.color = "black";
    stars[1].style.color = "black";
    stars[2].style.color = "black";
    stars[3].style.color = "black";
}

function starCheck_5() {
    stars[0].style.color = "orange";
    stars[1].style.color = "orange";
    stars[2].style.color = "orange";
    stars[3].style.color = "orange";
    stars[4].style.color = "orange";
}

function starUncheck_5()
{
    stars[0].style.color = "black";
    stars[1].style.color = "black";
    stars[2].style.color = "black";
    stars[3].style.color = "black";
    stars[4].style.color = "black";
}