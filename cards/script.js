const main = document.getElementsByClassName("main").item(0);
let deck = [];
let empty = 0;

function genDeck() {
    if(empty == 0) {
        let rank = 1;
        let el;
        for(let i = 0; i < 52; i++) {
            el = "";
            switch(rank) {
                case 1: 
                    el += "A";
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    el += rank;
                    break;
                case 11:
                    el += "J";
                    break;
                case 12:
                    el += "Q";
                    break;
                case 13:
                    el += "K";
                    break;
            }

            if(i < 13) el += " Diamonds";
            else if(i < 26) el += " Spades";
            else if(i < 39) el += " Clubs";
            else el += " Hearts";

            if(rank == 13) rank = 1;
            else rank++;
            deck[i] = el;
        }
        shuffle();
        empty = 52;
    }
}

function shuffle() {
    let index = 0;
    let rand;
    let temp = [];
    while(index < 52) {
        rand = Math.trunc(((Math.random() * 52) + 1));
        let loop = 0;
        while(deck[rand] == null && loop < 52) {
            rand = Math.trunc(((Math.random() * 52) + 1));
            loop++;
        }
        temp[index++] = deck[rand];
        deck[rand] = null;
    }
    deck = temp;
}

function drawCard() {
    genDeck();
    let n = document.createElement("div");
    let t = document.createElement("h1");
    n.className = "cardSize";
    n.id = "card";
    t.innerHTML = deck[--empty];
    n.appendChild(t);
    if(main.getElementsByClassName("cardSize").length > 1) setTimeout(removeCard(), 1);
    setTimeout(function(){add(n)}, 500);
}

function removeCard() {
    while(main.getElementsByClassName("cardSize").length > 1) main.getElementsByClassName("cardSize").item(main.getElementsByClassName("cardSize").length - 1).remove();
}

function add(n) {
    main.appendChild(n);
}