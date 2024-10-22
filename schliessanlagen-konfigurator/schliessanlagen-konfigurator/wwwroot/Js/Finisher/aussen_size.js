function selectParamAussen(idBlock, type, selectId) {

    let SelectItemId = selectId-1;
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

    if (type === "Doppelzylinder")
    {
        for (let i = 0; i <= oldAussen.value; i++) {


            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

            // Убедимся, что priceDoppelAussenCost[i] - это число
            let currentPriceDoppelAussen = parseFloat(priceDoppelAussenCost[i].toString().replace(",", ".").trim());

            // Умножаем на количество и вычитаем из текущей цены
            let newAllPrice = currentAllPriceValue - (currentPriceDoppelAussen * Number(countSchluss.value));

            // Форматируем результат
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";

        }
    }
    if (type === "Halbzylinder")
    {
        for (let i = 0; i <= oldAussen.value; i++)
        {
            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

            // Получаем текущее значение AllPrice и очищаем от лишних символов
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

            // Получаем цену из halbAussenCost и очищаем её
            let currentHalbAussenCost = parseFloat(halbAussenCost[i].toString().replace(",", ".").trim());

            // Вычитаем цену из costItems
            let newCostItems = currentCostItemsValue - currentHalbAussenCost;

            // Обновляем AllPrice, вычитая цену, умноженную на количество
            let newAllPrice = currentAllPriceValue - (currentHalbAussenCost * Number(countSchluss.value));

            // Форматируем значения для отображения
            costItems.value = newCostItems; 
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
        }
    }
    if (type === "Knaufzylinder") {
        for (let i = 0; i <= oldAussen.value; i++) {

            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

            // Получаем текущее значение AllPrice и очищаем его от лишних символов
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

            // Получаем цену из priceKnayfAussenCost и очищаем её
            let currentPriceKnayfAussenCost = parseFloat(priceKnayfAussenCost[i].toString().replace(",", ".").trim());

            // Вычитаем цену из costItems
            let newCostItems = currentCostItemsValue - currentPriceKnayfAussenCost;

            // Обновляем AllPrice, вычитая цену, умноженную на количество
            let newAllPrice = currentAllPriceValue - (currentPriceKnayfAussenCost * Number(countSchluss.value));

            // Форматируем значения для отображения
            costItems.value = newCostItems; // Оставляем два знака после запятой и добавляем символ €
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €"; 

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
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

                            // Получаем текущее значение AllPrice и очищаем его от лишних символов
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                            // Получаем цену из kleinPrice и очищаем её
                            let currentKleinPrice = parseFloat(kleinPrice[i].toString().replace(",", ".").trim());

                            // Вычитаем цену из costItems
                            let newCostItems = currentCostItemsValue - currentKleinPrice;

                            // Обновляем AllPrice, вычитая цену, умноженную на количество
                            let newAllPrice = currentAllPriceValue - (currentKleinPrice * Number(countSchluss.value));

                            // Форматируем значения для отображения
                            costItems.value = newCostItems; // Оставляем два знака после запятой и добавляем символ €
                            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
                        }
                        else {
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

                            // Получаем текущее значение AllPrice и очищаем его от лишних символов
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                            // Получаем цену из priceDoppelInternCost[i]
                            let currentPriceDoppelIntern = parseFloat(priceDoppelInternCost[i].toString().replace(",", ".").trim());

                            // Вычитаем цену из costItems
                            let newCostItems = currentCostItemsValue - currentPriceDoppelIntern;

                            // Обновляем AllPrice, вычитая цену, умноженную на количество
                            let newAllPrice = currentAllPriceValue - (currentPriceDoppelIntern * Number(countSchluss.value));

                            // Форматируем значения для отображения
                            costItems.value = newCostItems; // Оставляем два знака после запятой и добавляем символ €
                            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
                        }
                    }
                    else {
                        let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                        let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                        // Получаем цену из priceDoppelInternCost[i] и очищаем от лишних символов
                        let currentPriceDoppelIntern = parseFloat(priceDoppelInternCost[i].toString().replace(",", ".").trim());

                        // Обновляем значения с вычитанием
                        let newCostItems = currentCostItemsValue - currentPriceDoppelIntern;
                        let newAllPrice = currentAllPriceValue - (currentPriceDoppelIntern * Number(countSchluss.value));

                        // Устанавливаем новое значение для costItems и AllPrice
                        costItems.value = newCostItems; // Форматируем и добавляем символ €
                        AllPrice.value = (newAllPrice).toFixed(2).replace(".", ",") + " €";
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
                    if (kleinSize.length > 0)
                    {
                        if (SelectItemId > kleinCountItem.length || oldIntern.value > kleinPrice.length)
                        {
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

                            // Получаем текущее значение AllPrice и очищаем от лишних символов
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                            // Получаем цену из priceDoppelInternCost и очищаем её
                            let currentPriceDoppelIntern = parseFloat(priceDoppelInternCost[i].toString().replace(",", ".").trim());

                            // Вычитаем цену из costItems
                            let newCostItems = currentCostItemsValue - currentPriceDoppelIntern;

                            // Обновляем AllPrice, вычитая цену, умноженную на количество
                            let newAllPrice = currentAllPriceValue - (currentPriceDoppelIntern * Number(countSchluss.value));

                            // Форматируем значения для отображения
                            costItems.value = newCostItems; // Оставляем два знака после запятой и добавляем символ €
                            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €"; 
                        }
                        else
                        {
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

                            // Получаем текущее значение AllPrice и очищаем от лишних символов
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                            // Убедимся, что kleinPrice[i] - это число
                            let currentKleinPrice = parseFloat(kleinPrice[i].toString().replace(",", ".").trim());

                            // Вычитаем цену из kleinPrice из costItems
                            let newCostItems = currentCostItemsValue - currentKleinPrice;

                            // Обновляем AllPrice, вычитая цену, умноженную на количество
                            let newAllPrice = currentAllPriceValue - (currentKleinPrice * Number(countSchluss.value));

                            // Форматируем значения для отображения
                            costItems.value = newCostItems; // Оставляем два знака после запятой и добавляем символ €
                            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €"; // 
                        }
                    }
                    else
                    {
                        let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

                        // Получаем текущее значение AllPrice и очищаем от лишних символов
                        let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                        // Убедимся, что priceDoppelInternCost[i] - это число
                        let currentPriceDoppelIntern = parseFloat(priceDoppelInternCost[i].toString().replace(",", ".").trim());

                        // Вычитаем цену из priceDoppelInternCost из costItems
                        let newCostItems = currentCostItemsValue - currentPriceDoppelIntern;

                        // Обновляем AllPrice, вычитая цену умноженную на количество
                        let newAllPrice = currentAllPriceValue - (currentPriceDoppelIntern * Number(countSchluss.value));

                        // Форматируем значения для отображения
                        costItems.value = newCostItems; // Оставляем два знака после запятой и добавляем символ €
                        AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
                    }
                }

                Innen.value = InnenItem[0].value;
                is_GrossSize = false;
                oldIntern.value = 0;

            }
        }


        for (let s = 0; s <= SelectItemId; s++)
        {

            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

            // Получаем текущее значение AllPrice и очищаем от лишних символов
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

            // Убедимся, что priceDoppelAussenCost[s] - это число
            let currentPriceDoppelAussen = parseFloat(priceDoppelAussenCost[s].toString().replace(",", ".").trim());

            // Добавляем цену из priceDoppelAussenCost к costItems
            let newCostItems = currentCostItemsValue + currentPriceDoppelAussen;

            // Обновляем AllPrice, добавляя цену умноженную на количество
            let newAllPrice = currentAllPriceValue + (currentPriceDoppelAussen * Number(countSchluss.value));

            // Форматируем значения для отображения
            costItems.value = newCostItems; // Оставляем два знака после запятой и добавляем символ €
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";

          
        }
    }
    if (type === "Halbzylinder") {
        for (let s = 0; s <= SelectItemId; s++)
        {
            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
            let currentHalbAussenCost = parseFloat(halbAussenCost[s].toString().replace(",", ".").trim());

            costItems.value = (currentCostItemsValue + currentHalbAussenCost);

            // Учитываем количество и выполняем расчеты для AllPrice
            let newAllPrice = currentAllPriceValue + (currentHalbAussenCost * Number(countSchluss.value));

            // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
        }
    }
    if (type === "Knaufzylinder")
    {
        if (kleinKnayf.length > 0)
        {
            if (SelectItemId >= kleinKnayfCountItem.length && is_GrossSize == false)
            {
                for (let i = 0; i < KnayfZiseNormal.length; i++)
                {
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
                for (let i = 0; i <= oldIntern.value; i++)
                {
                    if (kleinKnayf.length > 0)
                    {
                        if (kleinKnayf.includes(Number(Aussen.value)))
                        {
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                            // Получаем цену из kleinKnayfPrice[i] и очищаем от лишних символов
                            let currentKleinKnayfPrice = parseFloat(kleinKnayfPrice[i].toString().replace(",", ".").trim());
                            let newCostItems = currentCostItemsValue - currentKleinKnayfPrice;
                            let newAllPrice = currentAllPriceValue - (currentKleinKnayfPrice * Number(countSchluss.value));

                            // Устанавливаем новое значение для costItems и AllPrice с форматированием
                            costItems.value = newCostItems; // Форматируем и добавляем символ €
                            AllPrice.value = (newAllPrice).toFixed(2).replace(".", ",") + " €"; 
                        }
                        else {
                            
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                            // Получаем цену из priceKnayfInternCost[i] и очищаем от лишних символов
                            let currentPriceKnayfInternCost = parseFloat(priceKnayfInternCost[i].toString().replace(",", ".").trim());

                            let newCostItems = currentCostItemsValue - currentPriceKnayfInternCost;
                            let newAllPrice = currentAllPriceValue - (currentPriceKnayfInternCost * Number(countSchluss.value));

                            // Устанавливаем новое значение для costItems и AllPrice с форматированием
                            costItems.value = newCostItems; // Форматируем и добавляем символ €
                            AllPrice.value = (newAllPrice).toFixed(2).replace(".", ",") + " €"; 
                        }
                    }
                    else {
                        let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

                        // Получаем текущее значение AllPrice и очищаем его
                        let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                        // Получаем текущую цену из priceKnayfInternCost[i]
                        let currentPriceKnayfInternCost = parseFloat(priceKnayfInternCost[i].toString().replace(",", ".").trim());
                        let newCostItems = currentCostItemsValue - currentPriceKnayfInternCost;
                        let newAllPrice = currentAllPriceValue - (currentPriceKnayfInternCost * Number(countSchluss.value));

                        // Устанавливаем новое значение для costItems и AllPrice с форматированием
                        costItems.value = newCostItems; // Форматируем и добавляем символ €
                        AllPrice.value = (newAllPrice).toFixed(2).replace(".", ",") + " €";
                    }
                }
                is_GrossSize = true;
                Innen.value = InnenItem[0].value;
                oldIntern.value = 0;
            }
            else {

                for (let i = 0; i < InnenItem.length; i++) {
                    if (i >= kleinKnayfCountItem[SelectItemId]) {
                        InnenItem[i].style.display = "none";
                    }
                    else {
                        InnenItem[i].value = kleinKnayf[i];
                        InnenItem[i].textContent = kleinKnayf[i];
                        InnenItem[i].style.display = "block";
                    }

                }

                for (let i = 0; i <= oldIntern.value; i++)
                {
                    if (kleinKnayf.length > 0)
                    {
                        if (SelectItemId > kleinKnayfCountItem.length || oldIntern.value > kleinKnayfPrice.length)
                        {
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());

                            // Преобразуем значение AllPrice, очищая его от символов и меняя формат
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

                            // Получаем текущую цену из priceKnayfInternCost[i], меняя формат
                            let currentPriceKnayfInternCost = parseFloat(priceKnayfInternCost[i].toString().replace(",", ".").trim());
                            
                            let newCostItems = currentCostItemsValue - currentPriceKnayfInternCost;
                            let newAllPrice = currentAllPriceValue - (currentPriceKnayfInternCost * Number(countSchluss.value));

                            // Форматируем и устанавливаем новое значение для costItems
                            costItems.value = (newCostItems).toFixed(2); // Форматируем и добавляем символ €

                            // Форматируем и устанавливаем новое значение для AllPrice
                            AllPrice.value = (newAllPrice).toFixed(2).replace(".", ",") + " €";
                        }
                        else {
                            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                            let currentKleinKnayfPrice = parseFloat(kleinKnayfPrice[i].toString().replace(",", ".").trim());

                            let newCostItems = currentCostItemsValue - currentKleinKnayfPrice;
                            let newAllPrice = currentAllPriceValue - (currentKleinKnayfPrice * Number(countSchluss.value));

                            // Форматируем значения, добавляем символ € и заменяем точку на запятую
                            costItems.value = newCostItems; // Форматируем для costItems
                            AllPrice.value = (newAllPrice).toFixed(2).replace(".", ",") + " €";
                        }
                    }
                    else {
                        let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
                        let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
                        let currentPriceKnayfInternCost = parseFloat(priceKnayfInternCost[i].toString().replace(",", ".").trim());

                        costItems.value = (currentCostItemsValue - currentPriceKnayfInternCost).toFixed(2);

                        // Учитываем количество и выполняем расчеты для AllPrice
                        let newAllPrice = currentAllPriceValue - (currentPriceKnayfInternCost * Number(countSchluss.value));

                        // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
                        AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
                    }
                }

                Innen.value = InnenItem[0].value;
                is_GrossSize = false;
                oldIntern.value = 0;

            }
        }

        for (let s = 0; s <= SelectItemId; s++) {

            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
            let currentPriceKnayfAussenCost = parseFloat(priceKnayfAussenCost[s].toString().replace(",", ".").trim());
            costItems.value = (currentCostItemsValue + currentPriceKnayfAussenCost).toFixed(2);

            // Учитываем количество и выполняем расчеты для AllPrice
            let newAllPrice = currentAllPriceValue + (currentPriceKnayfAussenCost * Number(countSchluss.value));

            // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
        }
    }

    oldAussen.value = SelectItemId;

    procent();
    liferung();
}