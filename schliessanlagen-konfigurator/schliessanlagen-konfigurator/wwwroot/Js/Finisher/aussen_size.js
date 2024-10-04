function selectParamAussen(idBlock, type, selectId) {

    let SelectItemId = selectId - 1;
    let is_GrossSize = false;
    let BlockTur = document.getElementById("Size-" + idBlock);
    let countSchluss = BlockTur.querySelector("#countSchlusse-" + idBlock);

    let company = BlockTur.querySelector("#compName");

    let ProductBlock = document.getElementById("ExelItem-" + idBlock);
    let Innen = BlockTur.querySelector("#Intern");

    if (Innen != null) {
        var InnenItem = Innen.querySelectorAll("#OptionI");
    }

    let Aussen = BlockTur.querySelector("#Aussen");
    let Aitems = Aussen.querySelectorAll("#OptionA");
    let oldAussen = BlockTur.querySelector("#Aussen-" + idBlock);
    let oldIntern = BlockTur.querySelector("#Innen-" + idBlock);

    let oldValueStart = Aitems;

    let costItems = document.getElementById("costItems-" + idBlock);

    if (oldAussen.value === "") {
        oldAussen.value = 0;
    }

    if (type === "Doppelzylinder") {
        for (let i = 0; i <= oldAussen.value; i++) {

            costItems.value = parseFloat(costItems.value) - (priceDoppelAussenCost[i]);

            AllPrice.value = parseFloat(AllPrice.value) - (priceDoppelAussenCost[i] * Number(countSchluss.value));

            let Costen = AllPrice.value.replace(/,/g, '.');

            AllPrice.value = Costen;

        }
    }
    if (type === "Halbzylinder") {
        for (let i = 0; i <= oldAussen.value; i++) {
            costItems.value = parseFloat(costItems.value) - (halbAussenCost[i]);

            AllPrice.value = parseFloat(AllPrice.value) - (halbAussenCost[i] * Number(countSchluss.value));

            let Costen = AllPrice.value.replace(/,/g, '.');

            AllPrice.value = Costen;
        }
    }
    if (type === "Knaufzylinder") {
        for (let i = 0; i <= oldAussen.value; i++) {

            costItems.value = parseFloat(costItems.value) - (priceKnayfAussenCost[i]);

            AllPrice.value = parseFloat(AllPrice.value) - (priceKnayfAussenCost[i] * Number(countSchluss.value));

            let Costen = AllPrice.value.replace(/,/g, '.');

            AllPrice.value = Costen;

        }
    }


    if (type === "Doppelzylinder")
    {
        if (kleinSize.length > 0)
        {
            if (SelectItemId >= kleinCountItem.length && is_GrossSize == false)
            {
                for (let i = 0; i < normalDopelSize.length; i++)
                {
                    if (i >= InnenItem.length) {
                        let options = document.createElement("option");
                        options.id = "OptionI";
                        options.value = normalDopelSize[i];
                        options.textContent = normalDopelSize[i];
                        Innen.appendChild(options);
                    }
                    else {
                        InnenItem[i].value = normalDopelSize[i];
                        InnenItem[i].textContent = normalDopelSize[i];
                        InnenItem[i].style.display = "block";
                    }

                }
                for (let i = 0; i <= oldIntern.value; i++)
                {
                    if (kleinSize.length > 0)
                    {
                        if (kleinSize.includes(Number(Aussen.value)))
                        {
                            costItems.value = parseFloat(costItems.value) - (kleinPrice[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (kleinPrice[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;
                        }
                        else {
                            costItems.value = parseFloat(costItems.value) - (priceDoppelInternCost[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (priceDoppelInternCost[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;
                           

                        }
                    }
                    else {
                        costItems.value = parseFloat(costItems.value) - (priceDoppelInternCost[i]);

                        AllPrice.value = parseFloat(AllPrice.value) - (priceDoppelInternCost[i] * Number(countSchluss.value));

                        let Costen = AllPrice.value.replace(/,/g, '.');

                        AllPrice.value = Costen;
                    }
                }
                is_GrossSize = true;
                Innen.value = InnenItem[0].value;
                oldIntern.value = 0;
            }
            else {

                    for (let i = 0; i < InnenItem.length; i++)
                    {
                        if (i >= kleinCountItem[SelectItemId])
                        {
                            InnenItem[i].style.display = "none";
                        }
                        else {
                            InnenItem[i].value = kleinSize[i];
                            InnenItem[i].textContent = kleinSize[i];
                            InnenItem[i].style.display = "block";
                        }

                    }
               
                for (let i = 0; i <= oldIntern.value; i++)
                {
                    if (kleinSize.length > 0 && oldIntern.value > 0)
                    {
                        if (SelectItemId > kleinCountItem.length || oldIntern.value > kleinPrice.length)
                        {
                            costItems.value = parseFloat(costItems.value) - (priceDoppelInternCost[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (priceDoppelInternCost[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;
                        }
                        else
                        {
                            costItems.value = parseFloat(costItems.value) - (kleinPrice[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (kleinPrice[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;
                        }
                    }
                    else
                    {
                        costItems.value = parseFloat(costItems.value) - (priceDoppelInternCost[i]);

                        AllPrice.value = parseFloat(AllPrice.value) - (priceDoppelInternCost[i] * Number(countSchluss.value));

                        let Costen = AllPrice.value.replace(/,/g, '.');

                        AllPrice.value = Costen;
                    }
                }

                Innen.value = InnenItem[0].value;
                is_GrossSize = false;
                oldIntern.value = 0;

            }
        }


        for (let s = 0; s <= SelectItemId; s++) {

            costItems.value = parseFloat(costItems.value) + (priceDoppelAussenCost[s]);

            AllPrice.value = parseFloat(AllPrice.value) + (priceDoppelAussenCost[s] * Number(countSchluss.value));

            let Costen = AllPrice.value.replace(/,/g, '.');

            AllPrice.value = Costen;

        }
    }
    if (type === "Halbzylinder") {
        for (let s = 0; s <= SelectItemId; s++) {
            costItems.value = parseFloat(costItems.value) + (halbAussenCost[s]);

            AllPrice.value = parseFloat(AllPrice.value) + (halbAussenCost[s] * Number(countSchluss.value));

            let Costen = AllPrice.value.replace(/,/g, '.');

            AllPrice.value = Costen;

        }
    }
    if (type === "Knaufzylinder")
    {
        if (kleinKnayf.length > 0)
        {
            if (Aussen.value > 29 && is_GrossSize == false) {
                for (let i = 0; i < KnayfZiseNormal.length; i++) {
                    if (i >= InnenItem.length) {
                        let options = document.createElement("option");
                        options.id = "OptionI";
                        options.value = KnayfZiseNormal[i];
                        options.textContent = KnayfZiseNormal[i];
                        Innen.appendChild(options);
                    }
                    else {
                        InnenItem[i].value = KnayfZiseNormal[i];
                        InnenItem[i].textContent = KnayfZiseNormal[i];
                        InnenItem[i].style.display = "block";
                    }

                }
                for (let i = 0; i <= oldIntern.value; i++) {
                    if (kleinKnayf.length > 0) {
                        if (Aussen.value > 29) {

                            costItems.value = parseFloat(costItems.value) - (priceKnayfInternCost[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (priceKnayfInternCost[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;
                        }
                        else {
                            costItems.value = parseFloat(costItems.value) - (kleinKnayfPrice[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (kleinKnayfPrice[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;

                        }
                    }
                    else {
                        costItems.value = parseFloat(costItems.value) - (priceKnayfInternCost[i]);

                        AllPrice.value = parseFloat(AllPrice.value) - (priceKnayfInternCost[i] * Number(countSchluss.value));

                        let Costen = AllPrice.value.replace(/,/g, '.');

                        AllPrice.value = Costen;
                    }
                }
                is_GrossSize = true;
                Innen.value = InnenItem[0].value;
                oldIntern.value = 0;
            }
            else {

                for (let i = 0; i < InnenItem.length; i++) {
                    if (i >= kleinKnayf.length) {
                        InnenItem[i].style.display = "none";
                    }
                    else {
                        InnenItem[i].value = kleinKnayf[i];
                        InnenItem[i].textContent = kleinKnayf[i];
                    }

                }

                for (let i = 0; i <= oldIntern.value; i++) {
                    if (kleinKnayf.length > 0 && oldIntern.value > 0) {
                        if (Aussen.value > 29 || oldIntern.value > kleinKnayfPrice.length) {
                            costItems.value = parseFloat(costItems.value) - (priceKnayfInternCost[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (priceKnayfInternCost[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;
                        }
                        else {
                            costItems.value = parseFloat(costItems.value) - (kleinKnayfPrice[i]);

                            AllPrice.value = parseFloat(AllPrice.value) - (kleinKnayfPrice[i] * Number(countSchluss.value));

                            let Costen = AllPrice.value.replace(/,/g, '.');

                            AllPrice.value = Costen;
                        }
                    }
                    else {
                        costItems.value = parseFloat(costItems.value) - (priceKnayfInternCost[i]);

                        AllPrice.value = parseFloat(AllPrice.value) - (priceKnayfInternCost[i] * Number(countSchluss.value));

                        let Costen = AllPrice.value.replace(/,/g, '.');

                        AllPrice.value = Costen;
                    }
                }

                Innen.value = InnenItem[0].value;
                is_GrossSize = false;
                oldIntern.value = 0;

            }
        }

        for (let s = 0; s <= SelectItemId; s++) {

            costItems.value = parseFloat(costItems.value) + (priceKnayfAussenCost[s]);

            AllPrice.value = parseFloat(AllPrice.value) + (priceKnayfAussenCost[s] * Number(countSchluss.value));

            let Costen = AllPrice.value.replace(/,/g, '.');

            AllPrice.value = Costen;
        }
    }

    oldAussen.value = SelectItemId;

    procent();
    liferung();
}