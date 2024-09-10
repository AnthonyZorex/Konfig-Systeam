    function Plus(turNumber) {
    let BlockTur = document.querySelector(`#BlockTur-${turNumber}`);
    let countTur = BlockTur.querySelector("#infoButten");
    let count = countTur.value;
    count++;
    countTur.value = count;
}
function Minus(turNumber) {
    let BlockTur = document.querySelector(`#BlockTur-${turNumber}`);
    let countTur = BlockTur.querySelector("#infoButten");
    let count = countTur.value;

    if (countTur.value > 1) {
        count--;
    }

    countTur.value = count;
}

function selectParam(value, turNumber) {
    let BlockTur = document.querySelector(`#BlockTur-${turNumber}`);

    let Aussen = BlockTur.querySelector(".aussen");
    let Intern = BlockTur.querySelector(".Innen");
    let Option = BlockTur.querySelector(".Option");

    let TypeSylinder = BlockTur.querySelector("#typeSylinder");

    let Aitems = Aussen.querySelectorAll("#OptionA");
    let Iitems = Intern.querySelectorAll("#OptionI");

    Aitems.forEach(function (items) {
        items.parentNode.removeChild(items);
    });

    Iitems.forEach(function (items) {
        items.parentNode.removeChild(items);
    });

    if (value == "Doppelzylinder") {
        for (let i = 0; i < DoppelAussenSize.length; i++) {
            let option = document.createElement("option");
            option.id = "OptionA";
            option.innerHTML = DoppelAussenSize[i];
            option.innerText = DoppelAussenSize[i];
            Aussen.appendChild(option);
        }
        for (let i = 0; i < DoppelInternSize.length; i++) {
            let option = document.createElement("option");
            option.id = "OptionI";
            option.innerHTML = DoppelInternSize[i];
            option.innerText = DoppelInternSize[i];
            Intern.appendChild(option);
        }

        TypeSylinder.src = "/compression/doppelzylinder.webp";
    }
    if (value == "Halbzylinder") {
        for (let i = 0; i < HalbSize.length; i++) {
            let option = document.createElement("option");
            option.id = "OptionA";
            option.innerHTML = HalbSize[i];
            option.innerText = HalbSize[i];
            Aussen.appendChild(option);
        }

        TypeSylinder.src = "/compression/halbzylinder.webp";
    }
    if (value == "Knaufzylinder") {
        for (let i = 0; i < KnayfAussenSize.length; i++) {
            let option = document.createElement("option");
            option.id = "OptionA";
            option.innerHTML = KnayfAussenSize[i];
            option.innerText = KnayfAussenSize[i];
            Aussen.appendChild(option);
        }
        for (let i = 0; i < KnayfInternSize.length; i++) {
            let option = document.createElement("option");
            option.id = "OptionI";
            option.innerHTML = KnayfInternSize[i];
            option.innerText = KnayfInternSize[i];
            Intern.appendChild(option);
        }

        TypeSylinder.src = "/compression/knaufzylinder.webp";

    }
    if (value == "Hebelzylinder") {

        TypeSylinder.src = "/compression/briefkastenzylinder.webp";
    } 
    if (value == "Vorhangschloss") {
        TypeSylinder.src = "/compression/vorhangschloss.webp";
    }
    if (value == "Aussenzylinder") {

        TypeSylinder.src = "/compression/aussenzylinder.webp";
    }

}

function removeBlockTur() {
    let countKeySelect = document.getElementById("countTurSelect");
    let x = Number(countKeySelect.value);
    x--;
    countKeySelect.value = x;

    let blockToRemove = document.getElementById('BlockTur-' + blockTur);

    if (blockToRemove) {
        blockToRemove.remove();

        let Horizontal = document.getElementById(`${blockTur}horizontal`);

        Horizontal.remove();

        blockTur--;
    }
}

function addBlockTur()
{
    let countKeySelect = document.getElementById("countTurSelect");
    let x = Number(countKeySelect.value);
    x++;
    countKeySelect.value = x;
    blockTur++;
    let newBlock = document.createElement('div');
    newBlock.id = 'BlockTur-' + blockTur;
    newBlock.className = "block";
    newBlock.innerHTML = `  <div class="TexCountTur">
                                            <div id="ItemTur">
                                                <div id="position">
                                                                   <h6 id="counterTur">
                                                         <svg onclick="controlPannel(${blockTur})  style="margin-right: 10px;" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-dots-circle-horizontal"><path stroke="none" d="M0 0h24v24H0z" fill="none" /><path d="M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0" /><path d="M8 12l0 .01" /><path d="M12 12l0 .01" /><path d="M16 12l0 .01" /></svg>
                                                                   ${blockTur}</h6>
                                                </div>

                                                <div>
                                                                      <input id="inputTur" required  onchange="chekedNameArientiren(event.target.value,${blockTur})" name="Turname" value="Tür ${blockTur}" placeholder="Name der Tür" />
                                                </div>

                                                    <div>

                                                         <img  id="typeSylinder" src="/compression/doppelzylinder.webp" height="30" width="30"  />

                                                    </div>
                                            </div>

                                            <div id="SelectTurItem">
                                                    <select id="TypeSelectTurType"  onchange="selectParam(event.target.value,${blockTur})" name="ZylinderId">
                                                    ${zylinderOptions}
                                                </select>
                                            </div>

                                            <div id="SelectTurItem">
                                                    <select id="TypeSelectTurItem"  class="aussen" name="aussen">
                                                    ${doppelAussenOptions}
                                                </select>
                                            </div>
                                            <div id="SelectTurItem">
                                                        <select id="TypeSelectTurItem" class="Innen" name="innen">
                                                    ${doppelInternOptions}
                                                </select>
                                            </div>
                                                       <div id="chekerTur">
                                                        <button type="button" onclick="Minus(${blockTur})">-</button>
                                                    <input id="infoButten"  value="1" type="text" name="CountTur" />
                                                       <button type="button" onclick="Plus(${blockTur})">+</button>
                                                </div>
                                        </div>

                                            </div>

                                                             <div id="zylinderMenu-${blockTur}" onmouseleave="closeControlPannel(${blockTur})" class="tür_konfig">
                                                               <div style="display:flex;gap:10px" onclick="TurUp(${blockTur})">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-up"><line x1="12" y1="19" x2="12" y2="5"></line><polyline points="5 12 12 5 19 12"></polyline></svg>
                                                          <h5 id="infoControlPanelTur" class="Up">nach oben verschieben</h5>
                                                        </div>
                                                            <div style="display:flex;gap:10px" onclick="TurDown(${blockTur})">
                                                         <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-down"><line x1="12" y1="5" x2="12" y2="19"></line><polyline points="19 12 12 19 5 12"></polyline></svg>
                                                          <h5 id="infoControlPanelTur" class="Down">nach unten verschieben</h5>
                                                        </div>
                                                                 <div style="display:flex;gap:10px">

                                                                      <div class="input-group mb-3">
                                                                      <span class="input-group-text" id="basic-addon1">
                                                                            <h5>
                                                                               <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-table-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"></path><path d="M12.5 21h-7.5a2 2 0 0 1 -2 -2v-14a2 2 0 0 1 2 -2h14a2 2 0 0 1 2 2v7.5"></path><path d="M3 10h18"></path><path d="M10 3v18"></path><path d="M16 19h6"></path><path d="M19 16v6"></path></svg>
                                                                            kopieren</h5>
                                                                      </span>
                                                                             <select type="text" class="form-select" onchange="TurKoppy(${blockTur},event.target.value)">
                                                                                   ${Array.from({ length: 10 }, (_, i) => `<option value="${i + 1}">${i + 1}</option>`).join('')}
                                                                           <select/>
                                                                    </div>

                                                            </div>
                                                                <div style="display:flex;gap:10px" onclick="TurDelete(${blockTur})">
                                                                 <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-file-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"></path><path d="M14 3v4a1 1 0 0 0 1 1h4"></path><path d="M17 21h-10a2 2 0 0 1 -2 -2v-14a2 2 0 0 1 2 -2h7l5 5v11a2 2 0 0 1 -2 2z"></path><path d="M10 12l4 4m0 -4l-4 4"></path></svg>
                                                              <h5 id="infoControlPanelTur" class="deleted"> löschen</h5>
                                                            </div>
                                                        </div>

                            `;

    document.getElementById('BlockTur-0').appendChild(newBlock);

    let horizont = document.createElement('div');
    horizont.id = `${blockTur}horizontal`;
    horizont.classList.add('horizontal');

    for (let i = 1; i <= blockCount; i++) {

        let newBlockTur = document.createElement('div');

        newBlockTur.innerHTML = ` <input type="checkbox" class="konfiguratorSelect" onmouseover="drawLines(${blockTur},${i})"  onmouseout="hideLines(${blockTur},${i})" id=${blockTur}checkbox${i}>
                            <input type="hidden" value="false" id="I${blockTur}checkbox${i}">`;

        newBlockTur.id = `${blockTur}.checkboxContainer${i}`;

        newBlockTur.classList.add('checkboxContainer');

        horizont.appendChild(newBlockTur);

        document.getElementById('InfoValue').appendChild(horizont);

        createCustomCheckbox(`${blockTur}checkbox${i}`);
    }
}
function selectTur(value)
{
    localStorage.clear();

    if (value > blockTur) {
        for (let i = blockTur; i < value; i++) {
            blockTur++;

            let newBlock = document.createElement('div');
            newBlock.className = "block";
            newBlock.id = 'BlockTur-' + blockTur;
            newBlock.innerHTML = `  <div class="TexCountTur">
                                                    <div id="ItemTur">
                                                        <div id="position">
                                                              <h6 id="counterTur">
                                                                    <svg onclick="controlPannel(${blockTur})" style="margin-right: 10px;" xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-dots-circle-horizontal"><path stroke="none" d="M0 0h24v24H0z" fill="none" /><path d="M12 12m-9 0a9 9 0 1 0 18 0a9 9 0 1 0 -18 0" /><path d="M8 12l0 .01" /><path d="M12 12l0 .01" /><path d="M16 12l0 .01" /></svg>
                                                              ${blockTur}</h6>
                                                        </div>

                                                        <div>
                                                                              <input id="inputTur" required  onchange="chekedNameArientiren(event.target.value,${blockTur})" value="Tür ${blockTur}" name="Turname" placeholder="Name der Tür" />
                                                        </div>

                                                            <div>
                                                                 <img  id="typeSylinder" src="/compression/doppelzylinder.webp" height="30" width="30"  />
                                                            </div>
                                                    </div>

                                                    <div id="SelectTurItem">
                                                            <select id="TypeSelectTurType"  onchange="selectParam(event.target.value,${blockTur})" name="ZylinderId">
                                                         ${zylinderOptions}
                                                        </select>
                                                    </div>

                                                    <div id="SelectTurItem">
                                                            <select id="TypeSelectTurItem"  class="aussen" name="aussen">
                                                               ${doppelAussenOptions}  
                                                        </select>
                                                    </div>
                                                    <div id="SelectTurItem">
                                                                <select id="TypeSelectTurItem" class="Innen" name="innen">
                                                                 ${doppelInternOptions}
                                                        </select>
                                                    </div>
                                                               <div id="chekerTur">
                                                                <button type="button" onclick="Minus(${blockTur})">-</button>
                                                            <input id="infoButten"  value="1" type="text" name="CountTur" />
                                                               <button type="button" onclick="Plus(${blockTur})">+</button>
                                                        </div>
                                                </div>

                                                           <div id="zylinderMenu-${blockTur}" onmouseleave="closeControlPannel(${blockTur})" class="tür_konfig">

                                                        <div style="display:flex;gap:10px"  onclick="TurUp(${blockTur})">
                                                            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-up"><line x1="12" y1="19" x2="12" y2="5"></line><polyline points="5 12 12 5 19 12"></polyline></svg>
                                                          <h5 id="infoControlPanelTur" class="Up">nach oben verschieben</h5>
                                                        </div>
                                                            <div style="display:flex;gap:10px" onclick="TurDown(${blockTur})">
                                                         <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-down"><line x1="12" y1="5" x2="12" y2="19"></line><polyline points="19 12 12 19 5 12"></polyline></svg>
                                                          <h5 id="infoControlPanelTur" class="Down">nach unten verschieben</h5>
                                                        </div>
                                                            <div style="display:flex;gap:10px">
                                                                 <div class="input-group mb-3">
                                                                          <span class="input-group-text" id="basic-addon1">
                                                                                   <h5>
                                                                                   <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-table-plus"><path stroke="none" d="M0 0h24v24H0z" fill="none"></path><path d="M12.5 21h-7.5a2 2 0 0 1 -2 -2v-14a2 2 0 0 1 2 -2h14a2 2 0 0 1 2 2v7.5"></path><path d="M3 10h18"></path><path d="M10 3v18"></path><path d="M16 19h6"></path><path d="M19 16v6"></path></svg>
                                                                                kopieren</h5>
                                                                          </span>
                                                                                  <select type="text" class="form-select" onchange="TurKoppy(${blockTur},event.target.value)">
                                                                                     ${Array.from({ length: 10 }, (_, i) => `<option value="${i + 1}">${i + 1}</option>`).join('')}
                                                                                <select/>
                                                                        </div>
                                                        </div>
                                                            <div style="display:flex;gap:10px" onclick="TurDelete(${blockTur})">
                                                             <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="icon icon-tabler icons-tabler-outline icon-tabler-file-x"><path stroke="none" d="M0 0h24v24H0z" fill="none"></path><path d="M14 3v4a1 1 0 0 0 1 1h4"></path><path d="M17 21h-10a2 2 0 0 1 -2 -2v-14a2 2 0 0 1 2 -2h7l5 5v11a2 2 0 0 1 -2 2z"></path><path d="M10 12l4 4m0 -4l-4 4"></path></svg>
                                                          <h5 id="infoControlPanelTur" class="deleted">löschen</h5>
                                                        </div>
                                                        </div>

                                    `;


            document.getElementById('BlockTur-0').appendChild(newBlock);

            let horizont = document.createElement('div');
            horizont.id = `${blockTur}horizontal`;
            horizont.classList.add('horizontal');

            for (let i = 1; i <= blockCount; i++) {

                let newBlockTur = document.createElement('div');

                newBlockTur.innerHTML = ` <input type="checkbox" onmouseover="drawLines(${blockTur},${i})"  onmouseout="hideLines(${blockTur},${i})" id=${blockTur}checkbox${i}>
                            <input type="hidden" value="false" id="I${blockTur}checkbox${i}">`;

                newBlockTur.id = `${blockTur}.checkboxContainer${i}`;

                newBlockTur.classList.add('checkboxContainer');

                horizont.appendChild(newBlockTur);

                document.getElementById('InfoValue').appendChild(horizont)

                createCustomCheckbox(`${blockTur}checkbox${i}`);
            }
           
        }

    }
    else {
        for (let i = blockTur; i > value; i--) {
            blockTur--;

            let blocks = container.querySelectorAll('.block');

            blocks[blocks.length - 1].remove();

            let checkboxContainer = document.querySelectorAll(".horizontal");
            checkboxContainer[checkboxContainer.length - 1].remove();

        }
    }
}
document.getElementById('removeButtonTur').addEventListener('click', removeBlockTur);
document.getElementById('addButtonTur').addEventListener('click', addBlockTur);