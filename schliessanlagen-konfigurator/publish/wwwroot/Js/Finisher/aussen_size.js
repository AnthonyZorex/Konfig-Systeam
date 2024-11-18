function selectParamAussen(idBlock, type, selectId,value,id) {

    let SelectItemId = selectId;
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
        for (let i = 0; i <= oldAussen.value; i++)
        {

            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

            // Убедимся, что priceDoppelAussenCost[i] - это число
            let currentPriceDoppelAussen = parseFloat(priceDoppelAussenCost[i].toString().replace(",", ".").trim());

            // Умножаем на количество и вычитаем из текущей цены
            let newAllPrice = currentAllPriceValue - (currentPriceDoppelAussen * Number(countSchluss.value));

            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";
        }
    }
    if (type === "Halbzylinder")
    {
        for (let i = 0; i < oldAussen.value; i++)
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
    if (type === "Knaufzylinder")
    {
        for (let i = 0; i <= oldAussen.value; i++)
        {

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
                for (let i = 0; i <= oldIntern.value; i++)
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

                axios.post(`/Konfigurator/RewriteInnen?id=${id}&Grose=${value}&Type=1`, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                })
                    .then(function (response) {

                        normalDopelSize = response.data.dopelInnen;
                        priceDoppelInternCost = response.data.dopelprice;

                        console.log(response.data.dopelInnen);

                        console.log(response.data.dopelprice);


                        if (InnenItem.length > normalDopelSize.length)
                        {
                            for (let i = 0; i < InnenItem.length; i++)
                            {
                                if (normalDopelSize.length > i)
                                {
                                    InnenItem[i].value = normalDopelSize[i];
                                    InnenItem[i].textContent = normalDopelSize[i];
                                    InnenItem[i].style.display = "block";
                                }
                                else {
                                    InnenItem[i].style.display = "none";
                                }
                            }
                        }
                        else {
                            for (let i = 0; i < normalDopelSize.length; i++)
                            {
                                if (InnenItem.length <= i) {
                                    let createOptions = document.createElement("option");
                                    createOptions.id = "OptionI";
                                    createOptions.value = normalDopelSize[i];
                                    createOptions.textContent = normalDopelSize[i];
                                    createOptions.style.display = "block";
                                    Innen.appendChild(createOptions);
                                }
                                else {
                                    InnenItem[i].value = normalDopelSize[i];
                                    InnenItem[i].textContent = normalDopelSize[i];
                                    InnenItem[i].style.display = "block";
                                }
                            }
                        }

                      
                    })
                    .catch(function (error) {
                        console.error(error);  // Обработка ошибки
                    });

                is_GrossSize = false;
                oldIntern.value = 0;   
                Innen.selectedIndex = 0;

                for (let s = 0; s <= SelectItemId; s++) {

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


       

    if (type === "Halbzylinder")
    {
        for (let s = 0; s < SelectItemId; s++)
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
        for (let i = 0; i <= oldIntern.value; i++) {

            let currentCostItemsValue = parseFloat(costItems.value.replace("€", "").replace(",", ".").trim());
            let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
            let currentPriceKnayfInternCost = parseFloat(priceKnayfInternCost[i].toString().replace(",", ".").trim());

            costItems.value = (currentCostItemsValue - currentPriceKnayfInternCost).toFixed(2);

            // Учитываем количество и выполняем расчеты для AllPrice
            let newAllPrice = currentAllPriceValue - (currentPriceKnayfInternCost * Number(countSchluss.value));

            // Форматируем AllPrice, заменяем точку на запятую и добавляем символ €
            AllPrice.value = newAllPrice.toFixed(2).replace(".", ",") + " €";

        }

        axios.post(`/Konfigurator/RewriteInnen?id=${id}&Grose=${value}&Type=3`, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
            .then(function (response) {

                KnayfZiseNormal = response.data.knayfInnen;
                priceKnayfInternCost = response.data.knayfprice;

                console.log(response.data.knayfInnen);

                console.log(response.data.knayfprice);


                if (InnenItem.length > KnayfZiseNormal.length) {
                    for (let i = 0; i < InnenItem.length; i++) {
                        if (KnayfZiseNormal.length > i) {
                            InnenItem[i].value = KnayfZiseNormal[i];
                            InnenItem[i].textContent = KnayfZiseNormal[i];
                            InnenItem[i].style.display = "block";
                        }
                        else {
                            InnenItem[i].style.display = "none";
                        }
                    }
                }
                else {
                    for (let i = 0; i < KnayfZiseNormal.length; i++) {
                        if (InnenItem.length <= i) {
                            let createOptions = document.createElement("option");
                            createOptions.id = "OptionI";
                            createOptions.value = KnayfZiseNormal[i];
                            createOptions.textContent = KnayfZiseNormal[i];
                            createOptions.style.display = "block";
                            Innen.appendChild(createOptions);
                        }
                        else {
                            InnenItem[i].value = KnayfZiseNormal[i];
                            InnenItem[i].textContent = KnayfZiseNormal[i];
                            InnenItem[i].style.display = "block";
                        }
                    }
                }


            })
            .catch(function (error) {
                console.error(error);  // Обработка ошибки
            });

        is_GrossSize = false;
        oldIntern.value = 0;
        Innen.selectedIndex = 0;


        for (let s = 0; s <= SelectItemId; s++)
        {

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

   
    console.log(SelectItemId);
    oldAussen.value = SelectItemId;

    procent();
    liferung();
}