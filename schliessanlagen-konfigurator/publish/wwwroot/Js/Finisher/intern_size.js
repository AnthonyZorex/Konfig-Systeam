
function selectParamIntern(idBlock, type, selectId)
{
    let ProductBlock = document.getElementById("ExelItem-" + idBlock);
    let BlockTur = document.getElementById("Size-" + idBlock);
    let countSchluss = BlockTur.querySelector("#countSchlusse-" + idBlock);

    let SelectItemId = selectId-1;

    let costItems = document.getElementById("costItems-" + idBlock);

    let Aussen = BlockTur.querySelector("#Aussen");
    let company = BlockTur.querySelector("#compName");
    let Innen = BlockTur.querySelector("#Intern");
    let Aitems = Innen.querySelectorAll("#OptionI");
    let oldAussen = BlockTur.querySelector("#Innen-" + idBlock);

    if (oldAussen.value === "") {
        oldAussen.value = 0;
    }

    if (type === "Doppelzylinder")
    {
        for (let i = 0; i <= oldAussen.value; i++)
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
                else
                {
                    costItems.value = parseFloat(costItems.value) - (priceDoppelInternCost[i]);

                    AllPrice.value = parseFloat(AllPrice.value) - (priceDoppelInternCost[i] * Number(countSchluss.value));

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
    }

    if (type === "Knaufzylinder")
    {
        for (let i = 0; i <= oldAussen.value; i++)
        {
            if (kleinKnayf.length > 0)
            {
                if (kleinKnayf.includes(Number(Aussen.value)))
                {
                    costItems.value = parseFloat(costItems.value) - (kleinKnayfPrice[i]);

                    AllPrice.value = parseFloat(AllPrice.value) - (kleinKnayfPrice[i] * Number(countSchluss.value));

                    let Costen = AllPrice.value.replace(/,/g, '.');

                    AllPrice.value = Costen;
                }
                else
                {
                    costItems.value = parseFloat(costItems.value) - (priceKnayfInternCost[i]);

                    AllPrice.value = parseFloat(AllPrice.value) - (priceKnayfInternCost[i] * Number(countSchluss.value));

                    let Costen = AllPrice.value.replace(/,/g, '.');

                    AllPrice.value = Costen;

                }
            }
            else
            {
                costItems.value = parseFloat(costItems.value) - (priceKnayfInternCost[i]);

                AllPrice.value = parseFloat(AllPrice.value) - (priceKnayfInternCost[i] * Number(countSchluss.value));

                let Costen = AllPrice.value.replace(/,/g, '.');

                AllPrice.value = Costen;
            }
        }

    }


    if (type === "Doppelzylinder")
    {
        for (let s = 0; s <= SelectItemId; s++)
        {
            if (kleinSize.length > 0)
            {
                if (kleinSize.includes(Number(Aussen.value)))
                {
                    costItems.value = parseFloat(costItems.value) + (kleinPrice[s]);

                    AllPrice.value = parseFloat(AllPrice.value) + (kleinPrice[s] * Number(countSchluss.value));

                    let Costen = AllPrice.value.replace(/,/g, '.');

                    AllPrice.value = Costen;
                    
                }
                else {
                   
                    costItems.value = parseFloat(costItems.value) + (priceDoppelInternCost[s]);

                    AllPrice.value = parseFloat(AllPrice.value) + (priceDoppelInternCost[s] * Number(countSchluss.value));

                    let Costen = AllPrice.value.replace(/,/g, '.');

                    AllPrice.value = Costen;
                }
            }
            else {
                costItems.value = parseFloat(costItems.value) + (priceDoppelInternCost[s]);

                AllPrice.value = parseFloat(AllPrice.value) + (priceDoppelInternCost[s] * Number(countSchluss.value));

                let Costen = AllPrice.value.replace(/,/g, '.');

                AllPrice.value = Costen;
            }


        }

    }

    if (type === "Knaufzylinder")
    {
        for (let s = 0; s <= SelectItemId; s++)
        {
            if (kleinKnayf.length > 0)
            {
                if (kleinKnayf.includes(Number(Aussen.value))) {

                    costItems.value = parseFloat(costItems.value) + (kleinPrice[s]);

                    AllPrice.value = parseFloat(AllPrice.value) + (kleinPrice[s] * Number(countSchluss.value));

                    let Costen = AllPrice.value.replace(/,/g, '.');

                    AllPrice.value = Costen;
                }
                else
                {
                    costItems.value = parseFloat(costItems.value) + (priceKnayfInternCost[s]);

                    AllPrice.value = parseFloat(AllPrice.value) + (priceKnayfInternCost[s] * Number(countSchluss.value));

                    let Costen = AllPrice.value.replace(/,/g, '.');

                    AllPrice.value = Costen;
                   

                }
            }
            else {
                costItems.value = parseFloat(costItems.value) + (priceKnayfInternCost[s]);

                AllPrice.value = parseFloat(AllPrice.value) + (priceKnayfInternCost[s] * Number(countSchluss.value));

                let Costen = AllPrice.value.replace(/,/g, '.');

                AllPrice.value = Costen;
            }


        }

    }

    oldAussen.value = SelectItemId;

    liferung();
    procent();
}