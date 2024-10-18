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
                costProcent = bruttoCostValue.times(0.20).toFixed(2); // 20% for Austria
                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.20).toFixed(2);
                    ProductProcent.push(x);
                });

                priceBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.20).toFixed(2);
                    SchlusselProcent.push(x);
                });

                // Cost by weight
                if (sendDhl.checked) {
                    let gramValue = new Big(Gram.value.trim().replace(",", "."));
                    if (gramValue.lte(2))
                    {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(2) && gramValue.lte(5)) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(5) && gramValue.lte(10)) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(10) && gramValue.lte(20)) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(20) && gramValue.lte(31.5)) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €";
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €";
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }

        case "BE":
            {
                costProcent = bruttoCostValue.times(0.21).toFixed(2); // 21% for Belgium
                procent.value = "21% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.21).toFixed(2);
                    ProductProcent.push(x);
                });

                priceBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.21).toFixed(2);
                    SchlusselProcent.push(x);
                });

                // Cost by weight
                if (sendDhl.checked) {
                    let gramValue = new Big(Gram.value.trim().replace(",", "."));
                    if (gramValue.lte(2)) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(2) && gramValue.lte(5)) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(5) && gramValue.lte(10)) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(10) && gramValue.lte(20)) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(20) && gramValue.lte(31.5)) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €";
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €";
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }

        case "BG":
            {
                costProcent = bruttoCostValue.times(0.20).toFixed(2); // 20% for Bulgaria
                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.20).toFixed(2);
                    ProductProcent.push(x);
                });

                priceBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.20).toFixed(2);
                    SchlusselProcent.push(x);
                });

                // Cost by weight
                if (sendDhl.checked) {
                    let gramValue = new Big(Gram.value.trim().replace(",", "."));
                    if (gramValue.lte(2)) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(2) && gramValue.lte(5)) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(5) && gramValue.lte(10)) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(10) && gramValue.lte(20)) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(20) && gramValue.lte(31.5)) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €";
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €";
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }

        case "CY":
            {
                costProcent = bruttoCostValue.times(0.19).toFixed(2); // 19% for Cyprus
                procent.value = "19% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                PreisProductBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.19).toFixed(2);
                    ProductProcent.push(x);
                });

                priceBrutto.forEach((item) => {
                    let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                    let valueBig = new Big(value);
                    let x = valueBig.times(0.19).toFixed(2);
                    SchlusselProcent.push(x);
                });

                // Cost by weight
                if (sendDhl.checked)
                {
                    let gramValue = new Big(Gram.value.trim().replace(",", "."));
                    if (gramValue.lte(2)) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(2) && gramValue.lte(5)) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(5) && gramValue.lte(10)) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(10) && gramValue.lte(20)) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €";
                    } else if (gramValue.gt(20) && gramValue.lte(31.5)) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €";
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                }
                else {
                    CostGram[BlockItem].value = "0 €";
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
                }
        case "CZ":
            {

                costProcent = bruttoCostValue.times(0.21).toFixed(2);

                procent.value = "21% MwSt"; // Устанавливаем текстовое значение
                Gram.value = parseFloat(Gram.value.trim().replace(",", ".")); // Преобразуем граммы

                // Обработка PreisProductBrutto
                PreisProductBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.21).toFixed(2); // Рассчитываем налог
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.21).toFixed(2); // Рассчитываем налог
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                // Обработка стоимости в зависимости от веса
                if (sendDhl.checked) {
                    let gramValue = new Big(Gram.value.trim().replace(",", "."));

                    if (gramValue.lte(2)) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (gramValue.gt(2) && gramValue.lte(5)) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (gramValue.gt(5) && gramValue.lte(10)) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (gramValue.gt(10) && gramValue.lte(20)) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (gramValue.gt(20) && gramValue.lte(31.5)) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
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

                    if (gramValue.lte(2))
                    {
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
                costProcent = bruttoCostValue.times(0.25).toFixed(2);
                procent.value = "25% MwSt"; // Устанавливаем текстовое значение

                // Обработка PreisProductBrutto
                PreisProductBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.25).toFixed(2); // Рассчитываем налог
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.25).toFixed(2); // Рассчитываем налог
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                // Обработка стоимости в зависимости от веса
                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }

        case "EE":
            {
                costProcent = bruttoCostValue.times(0.20).toFixed(2);
                procent.value = "20% MwSt"; // Устанавливаем текстовое значение

                // Обработка PreisProductBrutto
                PreisProductBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.20).toFixed(2); // Рассчитываем налог
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.20).toFixed(2); // Рассчитываем налог
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                // Обработка стоимости в зависимости от веса
                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }

        case "ES":
            {
                // Налоговая ставка для Испании
                costProcent = bruttoCostValue.times(0.21).toFixed(2);
                procent.value = "21% MwSt"; // Устанавливаем текстовое значение

                // Обработка PreisProductBrutto
                PreisProductBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.21).toFixed(2); // Рассчитываем налог
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.21).toFixed(2); // Рассчитываем налог
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                // Обработка стоимости в зависимости от веса
                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }

        case "FI":
            {
                costProcent = bruttoCostValue.times(0.24).toFixed(2);
                procent.value = "24% MwSt"; // Устанавливаем текстовое значение

                // Обработка PreisProductBrutto
                PreisProductBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.24).toFixed(2); // Рассчитываем налог
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                    let valueBig = new Big(value); // Создаем объект Big
                    let x = valueBig.times(0.24).toFixed(2); // Рассчитываем налог
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                // Обработка стоимости в зависимости от веса
                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "FR":
            {
                
                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                costProcent = bruttoCostValue.times(0.20).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.20).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.20).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "GR":
            {
 
                procent.value = "24% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                costProcent = bruttoCostValue.times(0.24).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.24).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.24).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "HR":
            {
                procent.value = "25% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                costProcent = bruttoCostValue.times(0.25).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.25).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.25).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "HU":
            {

                procent.value = "27% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                costProcent = bruttoCostValue.times(0.27).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.27).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.27).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        case "IE":
            {
                procent.value = "23% MwSt";

                costProcent = bruttoCostValue.times(0.23).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.23).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.23).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "IT":
            {
                costProcent = bruttoCostValue.times(0.22).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.22).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.22).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                procent.value = "22% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "LT":
            {
                procent.value = "21% MwSt";

                costProcent = bruttoCostValue.times(0.21).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.21).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.21).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "LU":
            {
                costProcent = bruttoCostValue.times(0.17).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.17).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.17).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                procent.value = "17% MwSt";

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "LV":
            {
                costProcent = bruttoCostValue.times(0.21).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.21).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.21).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                procent.value = "21% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "MT":
            {

                costProcent = bruttoCostValue.times(0.18).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.18).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.18).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                procent.value = "18% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));


                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
                  
            }
        case "NL":
            {
                costProcent = bruttoCostValue.times(0.21).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.21).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.21).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });


                procent.value = "21% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                
                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }
                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;

            }
        case "PL":
            {
                costProcent = bruttoCostValue.times(0.23).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.23).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.23).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                procent.value = "23% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;

            }
        case "PT":
            {
                costProcent = bruttoCostValue.times(0.23).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.23).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.23).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });


                procent.value = "23% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "RO":
            {

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
               
                procent.value = "19% MwSt";

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


                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "SE":
            {
               
                procent.value = "25% MwSt";

                costProcent = bruttoCostValue.times(0.25).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.25).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.25).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "SI":
            {
                
                procent.value = "22% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                costProcent = bruttoCostValue.times(0.22).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.22).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.22).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);

                break;
            }
        case "SK":
            {

                procent.value = "20% MwSt";
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                costProcent = bruttoCostValue.times(0.20).toFixed(2);

                PreisProductBrutto.forEach((item) => {
                    // Преобразуем строку в число, заменяя запятую на точку
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.20).toFixed(2);
                    ProductProcent.push(x); // Добавляем результат в массив
                });

                // Обработка priceBrutto
                priceBrutto.forEach((item) => {
                    let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                    let valueBig = new Big(value); // Создаем объект Big

                    let x = valueBig.times(0.20).toFixed(2);
                    SchlusselProcent.push(x); // Добавляем результат в массив
                });

                if (sendDhl.checked) {
                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 20 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                } else {
                    CostGram[BlockItem].value = "0 €"; // Форматируем нулевое значение
                }

                mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent);
                break;
            }
        default:
        {
                procent.value = "Steuer";
                costProcent = bruttoCostValue.times(0.0).toFixed(2);
            if (sendDhl.checked == true)
            {
                Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                if (value == "US")
                {

                        if (Gram.value <= 2) {
                            CostGram[BlockItem].value = new Big(30).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                        } else if (Gram.value > 2 && Gram.value <= 5) {
                            CostGram[BlockItem].value = new Big(40).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                        } else if (Gram.value > 5 && Gram.value <= 10) {
                            CostGram[BlockItem].value = new Big(50).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                        } else if (Gram.value > 10 && Gram.value <= 20) {
                            CostGram[BlockItem].value = new Big(60).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                        } else if (Gram.value > 20 && Gram.value <= 31.5) {
                            CostGram[BlockItem].value = new Big(70).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                        }
                        costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                    

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
                            CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                        } else if (Gram.value > 5 && Gram.value <= 10) {
                            CostGram[BlockItem].value = new Big(39).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                        } else if (Gram.value > 10 && Gram.value <= 20) {
                            CostGram[BlockItem].value = new Big(59).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                        }

                        costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                   

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

                    if (Gram.value <= 2) {
                        CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; // 15,00 €
                    } else if (Gram.value > 2 && Gram.value <= 5) {
                        CostGram[BlockItem].value = new Big(29).toFixed(2).replace(".", ",") + " €"; // 17,00 €
                    } else if (Gram.value > 5 && Gram.value <= 10) {
                        CostGram[BlockItem].value = new Big(38).toFixed(2).replace(".", ",") + " €"; // 22,00 €
                    } else if (Gram.value > 10 && Gram.value <= 20) {
                        CostGram[BlockItem].value = new Big(52).toFixed(2).replace(".", ",") + " €"; // 35,00 €
                    } else if (Gram.value > 21 && Gram.value <= 31.5) {
                        CostGram[BlockItem].value = new Big(59).toFixed(2).replace(".", ",") + " €"; // 49,00 €
                    }
                    costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());

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
    proc.value = parseFloat(costProcent).toLocaleString('de-DE', {
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
        cost[i].innerHTML = parseFloat(ALLSUM).toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });

        cost[i].textContent = parseFloat(ALLSUM).toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });

        cost[i].value = parseFloat(ALLSUM).toLocaleString('de-DE', {
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