let num = 0;
let id = 0;
let count = 0;
let amount = 800;
let arr = null;

function initBars() {
    arr = new Array();
    for(let i = 0; i < amount; i++) {
        arr[i] = (Math.random() * 29) + 1;
    }

    const container = document.getElementsByClassName("container").item(0);
    container.innerHTML = '';
    for(let i = 0; i < amount; i++) {
        let bar = document.createElement("div");
        let x = i + 1;
        bar.className = "bar";
        bar.id = "bar" + x;
        bar.style.backgroundColor = "aquamarine";
        container.appendChild(bar);
    }

    paintBars();
}

function paintBars() {
    const container = document.getElementsByClassName("container").item(0);

    for(let i = 0; i < amount; i++) {
        let x = i + 1;
        bar = document.getElementById("bar" + x);
        bar.style.height = "" + arr[i] + "vh";
        let sub = 30 - arr[i];
        bar.style.marginTop = "" + sub + "vh";
    }
}

function mark(x, b) {
    let id1 = x + 1;
    bar1 = document.getElementById("bar" + id1);
    if(b) bar1.style.backgroundColor = "red";
    else bar1.style.backgroundColor = "aquamarine";
}

function sortBars(index) {
    if(arr == null) return;
    if(index == amount) {
        paintBars();
        return;
    }

    for(let i = 0; i < amount; i++) mark(i, false);

    mark(index, true);
    let b = 0;
    while(b < index) {
        if(arr[index] < arr[b]) {
            mark(b, true);
            let s = index;
            let temp = arr[index];
            while(s > b) {
                arr[s] = arr[s - 1];
                s--;
            }
            arr[b] = temp;
            break;
        }
        b++;
    }
    setTimeout(sortBars(index + 1), 20);
}

function changeSize() {
    if(count == 100) {
        count = 0;
        return;
    }

    id++;
    const bar = document.getElementById("bar" + id);
    if(num == 0 || num == 2) {
        bool = false;
        bar.style.backgroundColor = "red";
        bar.style.marginTop = "20vh";
        bar.style.height = "10vh";
    } else {
        bool = true;
        bar.style.backgroundColor = "blue";
        bar.style.marginTop = "10vh";
        bar.style.height = "20vh";
    }

    if(id == 29) id = 0;
    if(num == 2) num = 0;
    else num++;
    count++;
    setTimeout(changeSize, 20);
}