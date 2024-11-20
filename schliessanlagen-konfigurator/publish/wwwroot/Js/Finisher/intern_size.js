
function selectParamIntern(idBlock, type, selectId, value, id)
{
    let ProductBlock = document.getElementById("ExelItem-" + idBlock);
    let BlockTur = document.getElementById("Size-" + idBlock);
    let countSchluss = BlockTur.querySelector("#countSchlusse-" + idBlock);

    let SelectItemId = selectId;

    let costItems = document.getElementById("costItems-" + idBlock);

    let Aussen = BlockTur.querySelector("#Aussen");
    let company = BlockTur.querySelector("#compName");
    let Innen = BlockTur.querySelector("#Intern");
    let Aitems = Innen.querySelectorAll("#OptionI");
    let oldIntern = BlockTur.querySelector("#Innen-" + idBlock);
    let oldAussen = BlockTur.querySelector("#Aussen-" + idBlock);

    if (Aussen != null) {
        var AussenItem = Aussen.querySelectorAll("#OptionA");
    }

    if (oldIntern.value === "") {
        oldIntern.value = 0;
    }

    if (type === "Doppelzylinder")
    {
        for (let i = 0; i <= oldIntern.value; i++)
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
   
            axios.post(`/Konfigurator/RewriteInnen?id=${id}&Grose=${value}&Type=1`, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(function (response) {

                   let  normalDopelASize = response.data.dopelInnen;
                   priceDoppelAussenCost = response.data.dopelprice;

                    let selectedValue = Aussen.value;

                    if (AussenItem.length > normalDopelASize.length) {
                        for (let i = 0; i < AussenItem.length; i++) {
                            if (normalDopelASize.length > i) {
                                AussenItem[i].value = normalDopelASize[i];
                                AussenItem[i].textContent = normalDopelASize[i];
                                AussenItem[i].style.display = "block";
                            }
                            else {
                                AussenItem[i].style.display = "none";
                            }
                        }
                    }
                    else {
                        for (let i = 0; i < normalDopelASize.length; i++) {
                            if (AussenItem.length <= i) {
                                let createOptions = document.createElement("option");
                                createOptions.id = "OptionI";
                                createOptions.value = normalDopelASize[i];
                                createOptions.textContent = normalDopelASize[i];
                                createOptions.style.display = "block";
                                Innen.appendChild(createOptions);
                            }
                            else {
                                AussenItem[i].value = normalDopelASize[i];
                                AussenItem[i].textContent = normalDopelASize[i];
                                AussenItem[i].style.display = "block";
                            }
                        }
                    }

                    for (let f = 0; f < normalDopelASize.length; f++)
                    {
                        if (normalDopelASize[f] == Number(selectedValue))
                        {
                            Aussen.selectedIndex = f;
                            oldAussen.value = f;
                        }
                    }
                })
                .catch(function (error) {
                    console.error(error);  // Обработка ошибки
                });

            is_GrossSize = false;


        for (let s = 0; s <= SelectItemId; s++)
        {

            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
            let currentPriceDoppelInternCost = parseFloat(priceDoppelInternCost[s]);

            costItems.value = (currentCostItemsValue + currentPriceDoppelInternCost);
            AllPrice.value = (currentAllPriceValue + (currentPriceDoppelInternCost * Number(countSchluss.value)))
                .toFixed(2)
                .replace(".", ",") + " €";

        }
    }
    if (type === "Knaufzylinder")
    {
        for (let i = 0; i <= oldIntern.value; i++)
        {          
            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
            let currentPriceKnayfIntern = parseFloat(priceKnayfInternCost[i]);
            costItems.value = (currentCostItemsValue - currentPriceKnayfIntern).toFixed(2);

            // Обновляем AllPrice
            let newAllPrice = currentAllPriceValue - (currentPriceKnayfIntern * Number(countSchluss.value));

            // Форматируем AllPrice
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";           
        }

        axios.post(`/Konfigurator/RewriteInnen?id=${id}&Grose=${value}&Type=3`, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
            .then(function (response) {

                let normalDopelASize = response.data.knayfInnen;
                priceKnayfAussenCost = response.data.knayfprice;
                let selectedValue = Aussen.value;

                if (AussenItem.length > normalDopelASize.length) {
                    for (let i = 0; i < AussenItem.length; i++) {
                        if (normalDopelASize.length > i) {
                            AussenItem[i].value = normalDopelASize[i];
                            AussenItem[i].textContent = normalDopelASize[i];
                            AussenItem[i].style.display = "block";
                        }
                        else {
                            AussenItem[i].style.display = "none";
                        }
                    }
                }
                else {
                    for (let i = 0; i < normalDopelASize.length; i++) {
                        if (AussenItem.length <= i) {
                            let createOptions = document.createElement("option");
                            createOptions.id = "OptionI";
                            createOptions.value = normalDopelASize[i];
                            createOptions.textContent = normalDopelASize[i];
                            createOptions.style.display = "block";
                            Innen.appendChild(createOptions);
                        }
                        else {
                            AussenItem[i].value = normalDopelASize[i];
                            AussenItem[i].textContent = normalDopelASize[i];
                            AussenItem[i].style.display = "block";
                        }
                    }
                }

                for (let f = 0; f < normalDopelASize.length; f++) {
                    if (normalDopelASize[f] == Number(selectedValue)) {
                        Aussen.selectedIndex = f;
                        oldAussen.value = f;
                    }
                }

            })
            .catch(function (error) {
                console.error(error);  // Обработка ошибки
            });

        is_GrossSize = false;

        for (let s = 0; s <= SelectItemId; s++) {
            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
            let currentKnayfInternCostValue = parseFloat(priceKnayfInternCost[s]);

            costItems.value = (currentCostItemsValue + currentKnayfInternCostValue);
            AllPrice.value = (currentAllPriceValue + (currentKnayfInternCostValue * Number(countSchluss.value)))
                .toFixed(2)
                .replace(".", ",") + " €";

        }
        //oldAussen.value = 0;
        //Aussen.selectedIndex = 0;
    }
   
    oldIntern.value = SelectItemId;

    liferung();
    procent();
}