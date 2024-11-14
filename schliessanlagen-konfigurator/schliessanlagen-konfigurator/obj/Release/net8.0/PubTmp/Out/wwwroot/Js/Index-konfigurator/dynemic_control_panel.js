
function TurUp(Id) {
    let alltur = document.querySelectorAll(".block");
    let allKey = document.querySelectorAll(".horizontal");

    let dor = document.getElementById("BlockTur-" + Id);
    let Key_down = document.getElementById(Id + "horizontal");

    let parent = dor.parentNode;
    let parent2 = Key_down.parentNode;

    let allturArray = Array.from(alltur);
    let allKeyArray = Array.from(allKey);

    let index = allturArray.findIndex(element => element === dor);
    let indexKey = allKeyArray.findIndex(element => element === Key_down);

    if (index != 0) {
        let nextElement = allturArray[index - 1];
        parent.insertBefore(dor, nextElement);

        let nextElementKey = allKeyArray[index - 1];
        parent2.insertBefore(Key_down, nextElementKey);
    }
    else {
        console.log("Элемент не найден или он уже последний.");
    }
}

function TurKoppy(Id, count_Coppy) {
    let alltur = document.querySelectorAll(".block");
    let dor = document.getElementById("BlockTur-" + Id);

    let allturArray = Array.from(alltur);
    let index = allturArray.findIndex(element => element === dor);
    const targetElement = alltur[index];

    let tür_konfig = document.getElementById("zylinderMenu-" + Id);
    tür_konfig.style.display = "none";

    let arrayTur = [];

    for (let d = 1; d <= count_Coppy; d++) {
        let clone = dor.cloneNode(true);

        let originalSelects = dor.querySelectorAll('select');
        let clonedSelects = clone.querySelectorAll('select');

        for (let i = 0; i < originalSelects.length; i++) {
            clonedSelects[i].value = originalSelects[i].value;
        }

        clone.childNodes[1].childNodes[1].childNodes[1].childNodes[1].childNodes[2].textContent = `${Id}.` + d;
        clone.childNodes[1].childNodes[1].childNodes[3].childNodes[1].value = `Tür ${Id}.` + d;

        let elementControlOpen = clone.childNodes[1].childNodes[1].childNodes[1].childNodes[1].childNodes[1];
        elementControlOpen.setAttribute("onclick", `controlPannel(1${Id}${allturArray.length + d})`);

        clone.childNodes[3].id = `zylinderMenu-1${Id}` + (allturArray.length + d);

        let element = clone.childNodes[1].childNodes[3].childNodes[1];
        element.setAttribute("onchange", `selectParam(event.target.value,1${Id}${allturArray.length + d})`);

        let elementCountMinus = clone.childNodes[1].childNodes[9].childNodes[1];
        elementCountMinus.setAttribute("onclick", `Minus(1${Id}${allturArray.length + d})`);

        let elementCountPlus = clone.childNodes[1].childNodes[9].childNodes[5];
        elementCountPlus.setAttribute("onclick", `Plus(1${Id}${allturArray.length + d})`);

        let elemetnControl = clone.childNodes[3];
        elemetnControl.setAttribute("onmouseleave", `closeControlPannel(1${Id}${allturArray.length + d})`);

        let elemetnControlUp = clone.childNodes[3].childNodes[1];
        elemetnControlUp.setAttribute("onclick", `TurUp(1${Id}${allturArray.length + d})`);

        let elemetnControlDown = clone.childNodes[3].childNodes[3];
        elemetnControlDown.setAttribute("onclick", `TurDown(1${Id}${allturArray.length + d})`);

        let elemetnControlDelete = clone.childNodes[3].childNodes[7];
        elemetnControlDelete.setAttribute("onclick", `TurDelete(1${Id}${allturArray.length + d})`);


        clone.id = `BlockTur-1${Id}` + (allturArray.length + d);

        arrayTur.push(`1${Id}` + (allturArray.length + d));

        allturArray.splice(index + d, 0, clone);
    }

    document.getElementById("BlockTur-0").innerHTML = "";

    for (let i = 0; i < allturArray.length; i++) {
        document.getElementById("BlockTur-0").append(allturArray[i]);
    }

    let allKey = document.querySelectorAll(".horizontal");
    let Key_down = document.getElementById(Id + "horizontal");
    let allKeyArray = Array.from(allKey);
    let indexKey = allKeyArray.findIndex(element => element === Key_down);

    let chekbox_open_array = [];

    const targetElementKey = allKey[indexKey];

    for (let f = 1; f <= count_Coppy; f++) {
        let clone2 = Key_down.cloneNode(true);


        clone2.id = `1${Id}` + (allKeyArray.length + f) + "horizontal";

        allKeyArray.splice(indexKey + f, 0, clone2);

        for (let s = 0; s < clone2.childNodes.length; s++) {
            clone2.childNodes[s].id = `${arrayTur[f - 1]}` + ".checkboxContainer" + (s + 1);
            clone2.childNodes[s].childNodes[1].id = `${arrayTur[f - 1]}` + "checkbox" + (s + 1);

            let chekbox = clone2.childNodes[s].childNodes[1];
            chekbox.setAttribute("onmouseover", `drawLines(${arrayTur[f - 1]},${s + 1})`);
            chekbox.setAttribute("onmouseout", `hideLines(${arrayTur[f - 1]},${s + 1})`);


            chekbox_open_array.push(`${arrayTur[f - 1]}` + "checkbox" + (s + 1));
            clone2.childNodes[s].childNodes[3].id = `I${arrayTur[f - 1]}` + "checkbox" + (s + 1);
        }
    }

    document.getElementById("InfoValue").innerHTML = "";

    for (let i = 0; i < allKeyArray.length; i++) {
        document.getElementById("InfoValue").append(allKeyArray[i]);
    }

    chekbox_open_array.forEach((item) => {
        createCustomCheckbox(item);
    });

}

function TurDelete(Id) {
    let dor = document.getElementById("BlockTur-" + Id);
    dor.remove();

    let Key_down = document.getElementById(Id + "horizontal");
    Key_down.remove();
}

function TurDown(Id) {
    let alltur = document.querySelectorAll(".block");
    let allKey = document.querySelectorAll(".horizontal");

    let dor = document.getElementById("BlockTur-" + Id);
    let Key_down = document.getElementById(Id + "horizontal");

    let parent = dor.parentNode;
    let parent2 = Key_down.parentNode;

    let allturArray = Array.from(alltur);
    let allKeyArray = Array.from(allKey);

    let index = allturArray.findIndex(element => element === dor);
    let indexKey = allKeyArray.findIndex(element => element === Key_down);

    if (index !== -1 && index < allturArray.length - 1) {
        let nextElement = allturArray[index + 1];
        parent.insertBefore(nextElement, dor);

        let nextElementKey = allKeyArray[index + 1];
        parent2.insertBefore(nextElementKey, Key_down);
    }
   
}

function controlPannel(Id) {
    let dor = document.getElementById("BlockTur-" + Id);
    let controlPannel = document.getElementById("zylinderMenu-" + Id);

    controlPannel.style.display = "block";
}

function closeControlPannel(id) {
    let controlPannel = document.getElementById("zylinderMenu-" + id);

    controlPannel.style.display = "none";
}
const scroll1 = document.getElementById('itemKeyBlock');
const scroll2 = document.getElementById('InfoValue');

function syncScroll(source, target) {
    target.scrollLeft = source.scrollLeft;
}

scroll1.addEventListener('scroll', () => syncScroll(scroll1, scroll2));
scroll2.addEventListener('scroll', () => syncScroll(scroll2, scroll1));

let countTurSelect = document.getElementById("countTurSelect");

function drawLines(id, row)
{
    const elemX = document.getElementById('BlockTur-' + id);
    const elemY = document.getElementById(id + 'checkbox' + row);

    const rectX = elemX.getBoundingClientRect();
    const rectY = elemY.getBoundingClientRect();

    const intersectionY = rectY.top < rectX.bottom && rectY.bottom > rectX.top;

    const chekboxSelect = document.getElementById(id + ".checkboxContainer" + row);

    const elemxItem = elemX.querySelectorAll(".TexCountTur")[0];

    if (intersectionY) {
        elemxItem.style.background = "#8CC4E7";
        elemY.style.background = "#8CC4E7";

        const horizont = document.getElementById(`${id}horizontal`);
        horizont.style.background = "#8CC4E7";

        const allHorizont = document.querySelectorAll(".horizontal");
        let serchelement = false;

        for (let i = 0; i < allHorizont.length; i++) {
            let chekboxrow = allHorizont[i].childNodes;

            if (chekboxrow[row - 1].id == chekboxSelect.id) {
                serchelement = true;
            }
            else {
                if (serchelement != true) {
                    chekboxrow[row - 1].style.background = "red";
                }

            }

        }


    }
}

function hideLines(id, row) {
    const verticalLine = document.getElementById('BlockTur-' + id);
    const elemxItem = verticalLine.querySelectorAll(".TexCountTur")[0];


    const horizontalLine = document.getElementById(id + 'checkbox' + row);
    elemxItem.style.background = '#ededed';
    horizontalLine.style.background = '#ededed';

    const horizont = document.getElementById(`${id}horizontal`);
    horizont.style.background = "#ededed";


    const allHorizont = document.querySelectorAll(".horizontal");
    let serchelement = false;

    for (let i = 0; i < allHorizont.length; i++) {
        let chekboxrow = allHorizont[i].childNodes;
        chekboxrow[row - 1].style.background = "none";
    }


}