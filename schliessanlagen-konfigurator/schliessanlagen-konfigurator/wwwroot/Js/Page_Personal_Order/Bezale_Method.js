function BezaleMethod(id) {

    const div = document.getElementById("bezalenMethod-" + id);
    const selectedRadio = div.querySelectorAll('input[name="Pay"]:checked');

    const submitRechnung = document.getElementById("SendRechnung-" + id);

    const Inputrechnung = document.getElementById("Aufrechnung-" + id);

    if (selectedRadio) {
        if (selectedRadio[0].value === "PayPall") {
            submitRechnung.style.display = "none";
            Inputrechnung.value = false;
        }
        //if (selectedRadio[0].value === "Google Pay") {
        //    submitRechnung.style.display = "none";
        //    Inputrechnung.value = false;
        //}
        //if (selectedRadio[0].value === "Apple Pay") {
        //    submitRechnung.style.display = "none";
        //    Inputrechnung.value = false;
        //}
        if (selectedRadio[0].value === "auf Rechnung") {
            submitRechnung.style.display = "block";
            Inputrechnung.value = true;
        }
    }
}



function ProcentMwst(value, BlockItem)
{
    let cost = document.querySelectorAll("#costedI-" + BlockItem);
    let modalItem = document.getElementById("myModal-" + BlockItem);
   
    let procent = document.getElementById("procent-" + BlockItem);
    let mvstr = cost[0].innerHTML.replace("€", "");
    let Gram = document.getElementById("gramWert-" + BlockItem);

    let sendDhl = document.getElementById("dhl-" + BlockItem);

    let Plase = document.getElementById("Plase-" + BlockItem);

    let bruttoCost = document.getElementById("costedР-" + BlockItem).value; // Получаем значение из DOM
    let bruttoCostValue = new Big(bruttoCost.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));

    let proc = document.getElementById("aldProcent-" + BlockItem);
    let pr = new Big(proc.value.replace("€", "").replace(",", ".").trim());

    let costProcent;
    let Prodct = document.querySelectorAll("#cardProductPrice-" + BlockItem);
    let cardProductPrice = Array.from(Prodct).map(product => product.querySelector("#PreisProduct-" + BlockItem));
    let PreisProductBrutto = Array.from(Prodct).map(product => product.querySelector("#PreisProductBrutto-" + BlockItem));
    let PreisProductNetto = Array.from(Prodct).map(product => product.querySelector("#PreisProductNetto-" + BlockItem));
    let ProductProcent = [];

    let schlüssel = document.querySelectorAll("#schlüssel-" + BlockItem);
    let schlüsselPrice = Array.from(schlüssel).map(product => product.querySelector("#PreisProduct-" + BlockItem));
    let priceBrutto = Array.from(schlüssel).map(product => product.querySelector("#PreisProductBruttoSchlussel-" + BlockItem));
    let SchlusselProcent = [];
    let PreisProductNettoSchlussel = Array.from(schlüssel).map(product => product.querySelector("#PreisProductNettoSchlussel-" + BlockItem));

    // Вычисляем сумму
    let Sum = new Big(mvstr.replace("&nbsp;", "").trim().replace(",", "."));
    let price = new Big(pr.toString()); // Используйте new Big(pr) вместо new Bis(pr), так как это ошибка

    Sum = Sum.minus(price);

    // Обновление стоимости грамма
    let CostGram = document.querySelectorAll("#CostGram-" + BlockItem);
    let costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());

    Sum = Sum.minus(costGramValue);

    // Окончательное значение
    Sum = Sum.toFixed(2)


    switch (value) {
        case "AT":
            {
                costProcent = bruttoCost.times(0.21).toFixed(2);

                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "BE":
            {
                costProcent = bruttoCost.value * 0.21;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "21% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "BG":
            {
                costProcent = bruttoCost.value * 0.20;
                costProcent = Math.round(costProcent * 100) / 100; 

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else
                {
                    CostGram[0].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "CY":
            {
                costProcent = bruttoCost.value * 0.19;
                costProcent = Math.round(costProcent * 100) / 100; 

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.19) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.19) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "19% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15,00;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "CZ":
            {
                costProcent = bruttoCost.value * 0.21;
                costProcent = Math.round(costProcent * 100) / 100; 

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "21% MwSt";

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && pGram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "DE":
            {
                costProcent = bruttoCostValue.times(0.19).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.19).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.19).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                procent.value = "19% MwSt";

                // Обработка стоимости в зависимости от веса
                if (sendDhl.checked) {
                    let gramValue = new Big(Gram.value.trim().replace(",", "."));

                    if (gramValue.lte(2)) {
                        CostGram[BlockItem].value = new Big(6).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (gramValue.gt(2) && gramValue.lte(5)) {
                        CostGram[BlockItem].value = new Big(7).toFixed(2).replace(".", ",") + " €"; // 7,00 €
                    } else if (gramValue.gt(5) && gramValue.lte(10)) {
                        CostGram[BlockItem].value = new Big(12).toFixed(2).replace(".", ",") + " €"; // 12,00 €
                    } else if (gramValue.gt(10) && gramValue.lte(20)) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (gramValue.gt(20) && gramValue.lte(31.5)) {
                        CostGram[BlockItem].value = new Big(20).toFixed(2).replace(".", ",") + " €"; // 20,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                    costGramValue = new Big(0);
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "DK":
            {
                costProcent = bruttoCost.value * 0.25;
                costProcent = Math.round(costProcent * 100) / 100; 

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.25) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.25) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "25% MwSt";

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "EE":
            {

                costProcent = bruttoCost.value * 0.20;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "20% MwSt";

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value  > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "ES":
            {
                costProcent = bruttoCost.value * 0.21;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "21% MwSt";

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "FI":
            {
                costProcent = bruttoCost.value * 0.24;
                costProcent = Math.round(costProcent * 100) / 100; 

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.24) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.24) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "24% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "FR":
            {
                costProcent = bruttoCost.value * 0.20;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "GR":
            {
                costProcent = bruttoCost.value * 0.24;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "24% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.24) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.24) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "HR":
            {
                costProcent = bruttoCost.value * 0.25;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "25% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.25) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.25) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "HU":
            {
                costProcent = bruttoCost.value * 0.27;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "27% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.27) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.27) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "IE":
            {
                costProcent = bruttoCost.value * 0.23;
                costProcent = Math.round(costProcent * 100) / 100; 
                procent.value = "23% MwSt";

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.23) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.23) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "IT":
            {
                costProcent = bruttoCost.value * 0.22;
                costProcent = Math.round(costProcent * 100) / 100;

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.22) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.22) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "22% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (parseFloat(Gram.value) > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "LT":
            {
                costProcent = bruttoCost.value * 0.21;
                costProcent = Math.round(costProcent * 100) / 100;

                procent.value = "21% MwSt";

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = parseFloat("0,00");
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "LU":
            {

                costProcent = bruttoCost.value * 0.17;
                costProcent = Math.round(costProcent * 100) / 100;

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.17) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.17) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "17% MwSt";

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "LV":
            {
                costProcent = bruttoCost.value * 0.21;
                costProcent = Math.round(costProcent * 100) / 100;

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "21% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "MT":
            {

                costProcent = bruttoCost.times(0.18).toFixed(2);;

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let valueBig = new Big(value);

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 19% от value с точным округлением
                        let x = valueBig.times(0.18).toFixed(2);

                        // Добавляем результат в массив ProductProcent
                        ProductProcent.push(x);
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {

                    let valueBig = new Big(value);

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 19% от value с точным округлением
                        let x = valueBig.times(0.18).toFixed(2);

                        // Добавляем результат в массив ProductProcent
                        ProductProcent.push(x);
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "18% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));


                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
                  
            }
        case "NL":
            {
                costProcent = bruttoCost.value * 0.21;
                costProcent = Math.round(costProcent * 100) / 100;

                let costProcent = bruttoCostValue.times(0.21).toFixed(2);
                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.21) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });


                procent.value = "21% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                
                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10)
                    {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20)
                    {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5)
                    {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "PL":
            {
                costProcent = bruttoCost.value * 0.23;
                costProcent = Math.round(costProcent * 100) / 100;

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.23) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.23) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                procent.value = "23% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;

            }
        case "PT":
            {
                costProcent = bruttoCost.value * 0.23;
                costProcent = Math.round(costProcent * 100) / 100;

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.23) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.23) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });


                procent.value = "23% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "RO":
            {

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                costProcent = bruttoCost.value * 0.19;
                costProcent = Math.round(costProcent * 100) / 100;
                procent.value = "19% MwSt";

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.19) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.19) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });


                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "SE":
            {
                costProcent = bruttoCost.value * 0.25;
                costProcent = Math.round(costProcent * 100) / 100;
                procent.value = "25% MwSt";

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.25) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.25) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "SI":
            {
                costProcent = bruttoCost.value * 0.22;
                costProcent = Math.round(costProcent * 100) / 100;
                procent.value = "22% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.22) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.22) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "SK":
            {
                costProcent = bruttoCost.value  * 0.20;
                costProcent = Math.round(costProcent * 100) / 100;

                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        ProductProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в PreisProductBrutto: ${item.value}`);
                        ProductProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                // Для priceBrutto
                priceBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменив запятую на точку
                    let value = parseFloat(item.value.trim().replace(",", "."));

                    // Проверяем, является ли число корректным
                    if (!isNaN(value)) {
                        // Вычисляем 20% с точным округлением до двух знаков
                        let x = Math.round((value * 0.20) * 100) / 100;
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    } else {
                        console.warn(`Некорректное значение в priceBrutto: ${item.value}`);
                        SchlusselProcent.push(0); // Добавляем 0 для некорректных значений
                    }
                });

                if (sendDhl.checked == true)
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 15;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 22;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 49;
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;
                   
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        default:
        {
                procent.value = "Steuer";

            if (sendDhl.checked == true)
            {
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (value == "US")
                {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = 30;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = 40;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 50;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 60;
                    }
                    else if (Gram.value > 20 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 70;
                    }

                    for (let i = 0; i < cardProductPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNetto[i].value.replace(",", ".").trim());

                        cardProductPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }

                    for (let i = 0; i < schlüsselPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNettoSchlussel[i].value.replace(",", ".").trim());

                        schlüsselPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }
                }
                else if (value == "RU" || value == "UA" || value == "BR" || value == "KZ")
                {
                    if (Gram.value <= 5) {
                        CostGram[BlockItem].value = 35;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 39;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 59;
                    }
                    for (let i = 0; i < cardProductPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNetto[i].value.replace(",", ".").trim());

                        cardProductPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }

                    for (let i = 0; i < schlüsselPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNettoSchlussel[i].value.replace(",", ".").trim());

                        schlüsselPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }
                }

                else if (value == "CHE" || value == "GBR")
                {
                    if (Gram.value <= 2)
                    {
                        CostGram[BlockItem].value = 17;
                    }
                    else if (Gram.value > 2 && Gram.value <= 5)
                    {
                        CostGram[BlockItem].value = 29;
                    }
                    else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = 38;
                    }
                    else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = 52;
                    }
                    else if (Gram.value > 21 && Gram.value <= 31, 5) {
                        CostGram[BlockItem].value = 59;
                    }
                    for (let i = 0; i < cardProductPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNetto[i].value.replace(",", ".").trim());

                        cardProductPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }

                    for (let i = 0; i < schlüsselPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNettoSchlussel[i].value.replace(",", ".").trim());

                        schlüsselPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }
                }
                else {
                    CostGram[BlockItem].value = 0;  
                    
                    for (let i = 0; i < cardProductPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNetto[i].value.replace(",", ".").trim());

                        cardProductPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }

                    for (let i = 0; i < schlüsselPrice.length; i++) {
                        // Получаем текущее значение цены, очищая ненужные символы
                        let currentPrice = parseFloat(PreisProductNettoSchlussel[i].value.replace(",", ".").trim());

                        schlüsselPrice[i].innerHTML = currentPrice.toLocaleString('de-DE', {
                            style: 'currency',
                            currency: 'EUR'
                        });
                    }
                }
            }
           
            break;
        }
    }
    proc.value = costProcent.toLocaleString('de-DE', {
        style: 'currency',
        currency: 'EUR'
    });;

    let ALLSUM = new Big(Sum).plus(costProcent).plus(costGramValue);

    ALLSUM = ALLSUM.toFixed(2);

    CostGram.forEach((item) => {
        item.value = parseFloat(CostGram[BlockItem].value).toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });
    });

    for (let i = 0; i < cost.length; i++)
    {
        cost[i].innerHTML = ALLSUM.toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });

        cost[i].textContent = ALLSUM.toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });

        cost[i].value = ALLSUM.toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });
    }

    changePrice(ALLSUM);
}

function mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent)
{
    for (let i = 0; i < cardProductPrice.length; i++) {
        // Получаем текущее значение цены, очищая ненужные символы и создаем объект Big
        let currentPrice = new Big(PreisProductNetto[i].value.replace(",", ".").trim());

        // Получаем значение из ProductProcent и создаем объект Big
        let additionalPrice = new Big(ProductProcent[i]);

        // Вычисляем новую цену
        let newPrice = currentPrice.plus(additionalPrice); // Используем метод plus для сложения

        // Обновляем innerHTML с отформатированной новой ценой
        cardProductPrice[i].innerHTML = newPrice.toFixed(2).replace(".", ",") + " €"; // Форматируем цену
    }

    for (let i = 0; i < schlüsselPrice.length; i++) {
        // Получаем текущее значение цены, очищая ненужные символы и создаем объект Big
        let currentPrice = new Big(PreisProductNettoSchlussel[i].value.replace(",", ".").trim());

        // Получаем значение из SchlusselProcent и создаем объект Big
        let additionalPrice = new Big(SchlusselProcent[i]);

        // Вычисляем новую цену
        let newPrice = currentPrice.plus(additionalPrice); // Используем метод plus для сложения

        // Обновляем innerHTML с отформатированной новой ценой
        schlüsselPrice[i].innerHTML = newPrice.toFixed(2).replace(".", ",") + " €"; // Форматируем цену
    }

}
function changePrice(newPrice) {
    currentPrice = newPrice;
}