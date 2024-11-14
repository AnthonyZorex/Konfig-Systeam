
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
                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentKleinPrice = parseFloat(kleinPrice[i].toString().replace(",", ".").trim());

                    costItems.value = (currentCostItemsValue - currentKleinPrice).toFixed(2);

                    // Учитываем количество и выполняем расчеты для AllPrice
                    let newAllPrice = currentAllPriceValue - (currentKleinPrice * Number(countSchluss.value));

                    // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
                    AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
                }
                else
                {
                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentPriceDoppelInternCost = parseFloat(priceDoppelInternCost[i]);

                    costItems.value = (currentCostItemsValue - currentPriceDoppelInternCost).toFixed(2);

                    // Учитываем количество и обновляем AllPrice
                    let newAllPrice = currentAllPriceValue - (currentPriceDoppelInternCost * Number(countSchluss.value));

                    // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
                    AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
                }
            }
            else
            {
                let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                let currentPriceDoppelInternCost = parseFloat(priceDoppelInternCost[i]);
                costItems.value = (currentCostItemsValue - currentPriceDoppelInternCost).toFixed(2);

                // Учитываем количество и обновляем AllPrice
                let newAllPrice = currentAllPriceValue - (currentPriceDoppelInternCost * Number(countSchluss.value));

                // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
                AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
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
                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentKleinKnayfPrice = parseFloat(kleinKnayfPrice[i]);

                    costItems.value = (currentCostItemsValue - currentKleinKnayfPrice).toFixed(2).replace(".", ",") + " €";

                    // Учитываем количество и обновляем AllPrice
                    let newAllPrice = currentAllPriceValue - (currentKleinKnayfPrice * Number(countSchluss.value));

                    // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
                    AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
                }
                else
                {
                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentPriceKnayfIntern = parseFloat(priceKnayfInternCost[i]);


                    costItems.value = (currentCostItemsValue - currentPriceKnayfIntern).replace(".", ",") + " €";

                    // Обновляем AllPrice
                    let newAllPrice = currentAllPriceValue - (currentPriceKnayfIntern * Number(countSchluss.value));

                    // Форматируем AllPrice
                    AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";

                }
            }
            else
            {
                let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                let currentPriceKnayfIntern = parseFloat(priceKnayfInternCost[i]);


                costItems.value = (currentCostItemsValue - currentPriceKnayfIntern);
                AllPrice.value = ((currentAllPriceValue - (currentPriceKnayfIntern * Number(countSchluss.value)))
                    .toFixed(2)
                    .replace(".", ",")) + " €";
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
                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentKleinPrice = parseFloat(kleinPrice[s]);

                    costItems.value = (currentCostItemsValue + currentKleinPrice);
                    AllPrice.value = (currentAllPriceValue + (currentKleinPrice * Number(countSchluss.value)))
                        .toFixed(2)
                        .replace(".", ",") + " €";
                    
                }
                else {
                   
                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentPriceDoppelInternCost = parseFloat(priceDoppelInternCost[s]);


                    costItems.value = (currentCostItemsValue + currentPriceDoppelInternCost);
                    AllPrice.value = (currentAllPriceValue + (currentPriceDoppelInternCost * Number(countSchluss.value)))
                        .toFixed(2)
                        .replace(".", ",") + " €";
                }
            }
            else {
                let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                let currentPriceDoppelInternCost = parseFloat(priceDoppelInternCost[s]);

                costItems.value = (currentCostItemsValue + currentPriceDoppelInternCost);
                AllPrice.value = (currentAllPriceValue + (currentPriceDoppelInternCost * Number(countSchluss.value)))
                    .toFixed(2)
                    .replace(".", ",") + " €";
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

                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentKleinPriceValue = parseFloat(kleinPrice[s]);
                    costItems.value = (currentCostItemsValue + currentKleinPriceValue);
                    AllPrice.value = (currentAllPriceValue + (currentKleinPriceValue * Number(countSchluss.value)))
                        .toFixed(2)
                        .replace(".", ",") + " €";
                }
                else
                {
                    let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                    let currentKnayfInternCostValue = parseFloat(priceKnayfInternCost[s]);

                    costItems.value = (currentCostItemsValue + currentKnayfInternCostValue);
                    AllPrice.value = (currentAllPriceValue + (currentKnayfInternCostValue * Number(countSchluss.value)))
                        .toFixed(2)
                        .replace(".", ",") + " €";

                }
            }
            else {
                let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                let currentKnayfInternCostValue = parseFloat(priceKnayfInternCost[s]);

                costItems.value = (currentCostItemsValue + currentKnayfInternCostValue);
                AllPrice.value = (currentAllPriceValue + (currentKnayfInternCostValue * Number(countSchluss.value)))
                    .toFixed(2)
                    .replace(".", ",") + " €";
            }


        }

    }

    oldAussen.value = SelectItemId;

    liferung();
    procent();
}