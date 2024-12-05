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
      
        if (selectedRadio[0].value === "Vorkasse") {
            submitRechnung.style.display = "block";
            Inputrechnung.value = true;
        }
    }
}

let preis_schluessel;
let preis_product;

function ProcentMwst(value, BlockItem,country)
{
    let cost = document.querySelectorAll("#costedI-" + BlockItem);
    let modalItem = document.getElementById("myModal-" + BlockItem);
   
    let procent = document.getElementById("procent-" + BlockItem);
    let mvstr = cost[0].innerHTML.replace("€", "");
    let Gram = document.getElementById("gramWert-" + BlockItem);

    let sendDhl = document.getElementById("dhl-" + BlockItem);

    let Plase = document.getElementById("Plase-" + BlockItem);

    let bruttoCost = document.getElementById("costedР-" + BlockItem).value;
    let bruttoCostValue = new Big(bruttoCost.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));

    let proc = document.querySelectorAll("#aldProcent-" + BlockItem);
    let pr = new Big(proc[1].value.replace("€", "").replace("&nbsp;","").replace(",", ".").trim());

    let costProcent;
    let Prodct = document.querySelectorAll("#cardProductPrice-" + BlockItem);
    let cardProductPrice = Array.from(Prodct).map(product => product.querySelector("#PreisProduct-" + BlockItem));
    let PreisProductBrutto = Array.from(Prodct).map(product => product.querySelector("#PreisProductBrutto-" + BlockItem));
    let PreisProductNetto = Array.from(Prodct).map(product => product.querySelector("#PreisProductNetto-" + BlockItem));
    let ProductProcent = [];

    let ProductEprice = Array.from(Prodct).map(product => product.querySelector("#E-priceProduct-" + BlockItem));
    let countProductEprice = Array.from(Prodct).map(product => product.querySelector("#E-priceProductCount-" + BlockItem));


    preis_schluessel = document.getElementById("preis_schluessel-" + BlockItem);

    preis_product = document.querySelectorAll("#preis_product-" + BlockItem);

    let schlüssel = document.querySelectorAll("#schlüssel-" + BlockItem);
    let ePrice = Array.from(schlüssel).map(product => product.querySelector("#E-Preis-" + BlockItem));
    let countschlüssel = Array.from(schlüssel).map(product => product.querySelector("#counterKey-" + BlockItem));

    let schlüsselPrice = Array.from(schlüssel).map(product => product.querySelector("#PreisProduct-" + BlockItem));
    let priceBrutto = Array.from(schlüssel).map(product => product.querySelector("#PreisProductBruttoSchlussel-" + BlockItem));
    let SchlusselProcent = [];
    let PreisProductNettoSchlussel = Array.from(schlüssel).map(product => product.querySelector("#PreisProductNettoSchlussel-" + BlockItem));

    let Sum = new Big(mvstr.replace("&nbsp;", "").trim().replace(",", "."));
    let price = new Big(pr.toString()); 

    Sum = Sum.minus(price);

    let CostGram = document.querySelectorAll("#CostGram-" + BlockItem);
    let costGramValue = new Big(CostGram[BlockItem].value != undefined ? CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim() : CostGram[BlockItem].innerHTML.replace("€", "").replace(",", ".").trim()  );

    Sum = Sum.minus(costGramValue);

    Sum = Sum.toFixed(2)

    if (country == "" || country == undefined)
    {
        switch (value) {

            case "AT":
                {
                    costProcent = bruttoCostValue.times(0.20).toFixed(2); // 20% for Austria
                    procent.innerHTML = "20% MwSt";
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }

            case "BE":
                {
                    costProcent = bruttoCostValue.times(0.21).toFixed(2); // 21% for Belgium
                    procent.innerHTML = "21% MwSt";
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }

            case "BG":
                {
                    costProcent = bruttoCostValue.times(0.20).toFixed(2); // 20% for Bulgaria
                    procent.innerHTML = "20% MwSt";
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }

            case "CY":
                {
                    costProcent = bruttoCostValue.times(0.19).toFixed(2); // 19% for Cyprus
                    procent.innerHTML = "19% MwSt";
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
                    }
                    else {
                        CostGram[BlockItem].value = "0 €";
                    }

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }
            case "CZ":
                {

                    costProcent = bruttoCostValue.times(0.21).toFixed(2);

                    procent.innerHTML = "21% MwSt"; // Устанавливаем текстовое значение
                    Gram.value = parseFloat(Gram.value.trim().replace(",", ".")); // Преобразуем граммы

                    // Обработка PreisProductBrutto
                    PreisProductBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big
                        let x = valueBig.times(0.21).toFixed(2); // Рассчитываем налог
                        ProductProcent.push(x); // Добавляем результат в массив
                    });
                
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
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

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.19).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });

                    procent.innerHTML = "19% MwSt";

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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }
            case "DK":
                {
                    costProcent = bruttoCostValue.times(0.25).toFixed(2);
                    procent.innerHTML = "25% MwSt"; // Устанавливаем текстовое значение

                    priceOptionBrutto.forEach((item) => {
                        let value = parseFloat(item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''));
                        let valueBig = new Big(value);
                        let x = valueBig.times(0.25).toFixed(2);
                        OptionProcent.push(x);
                    });
                  
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }

            case "EE":
                {
                    costProcent = bruttoCostValue.times(0.20).toFixed(2);
                    procent.innerHTML = "20% MwSt"; // Устанавливаем текстовое значение

                    // Обработка PreisProductBrutto
                    PreisProductBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                        let valueBig = new Big(value); // Создаем объект Big
                        let x = valueBig.times(0.20).toFixed(2); // Рассчитываем налог
                        ProductProcent.push(x); // Добавляем результат в массив
                    });                  

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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }

            case "ES":
                {
                    // Налоговая ставка для Испании
                    costProcent = bruttoCostValue.times(0.21).toFixed(2);
                    procent.innerHTML = "21% MwSt"; // Устанавливаем текстовое значение
                   
                    PreisProductBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                        let valueBig = new Big(value); // Создаем объект Big
                        let x = valueBig.times(0.21).toFixed(2); // Рассчитываем налог
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);

                    break;
                }

            case "FI":
                {
                    costProcent = bruttoCostValue.times(0.24).toFixed(2);
                    procent.innerHTML = "24% MwSt"; // Устанавливаем текстовое значение
                 
                    PreisProductBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                        let valueBig = new Big(value); // Создаем объект Big
                        let x = valueBig.times(0.24).toFixed(2); // Рассчитываем налог
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }
            case "FR":
                {

                    procent.innerHTML = "20% MwSt";
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    costProcent = bruttoCostValue.times(0.20).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.20).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }
            case "GR":
                {
                    procent.innerHTML = "24% MwSt";
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    costProcent = bruttoCostValue.times(0.24).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.24).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });
                  
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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }
            case "HR":
                {
                    procent.innerHTML = "25% MwSt";
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    costProcent = bruttoCostValue.times(0.25).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.25).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });
                    
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);

                    break;
                }
            case "HU":
                {

                    procent.innerHTML = "27% MwSt";
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    costProcent = bruttoCostValue.times(0.27).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.27).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }
            case "IE":
                {
                    procent.innerHTML = "23% MwSt";

                    costProcent = bruttoCostValue.times(0.23).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.23).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
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

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.22).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });

                    procent.innerHTML = "22% MwSt";
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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;

                }
            case "LT":
                {
                    procent.innerHTML = "21% MwSt";

                    costProcent = bruttoCostValue.times(0.21).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.21).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
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

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.17).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });

                    procent.innerHTML = "17% MwSt";

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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
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

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.21).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });
                   
                    procent.innerHTML = "21% MwSt";
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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
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

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.18).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });

                    procent.innerHTML = "18% MwSt";
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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
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

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.21).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });

                    procent.innerHTML = "21% MwSt";
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
                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
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

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.23).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });

                    procent.innerHTML = "23% MwSt";
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);

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
                 
                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.23).toFixed(2);
                        SchlusselProcent.push(x); // Добавляем результат в массив
                    });

                    procent.innerHTML = "23% MwSt";
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);

                    break;
                }
            case "RO":
                {

                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    procent.innerHTML = "19% MwSt";

                    costProcent = bruttoCostValue.times(0.19).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.19).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);

                    break;
                }
            case "SE":
                {
                    procent.innerHTML = "25% MwSt";

                    costProcent = bruttoCostValue.times(0.25).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                        // Преобразуем строку в число, заменяя запятую на точку
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); // Создаем объект Big

                        let x = valueBig.times(0.25).toFixed(2);
                        ProductProcent.push(x); // Добавляем результат в массив
                    });

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); 
                        let valueBig = new Big(value); 

                        let x = valueBig.times(0.25).toFixed(2);
                        SchlusselProcent.push(x); 
                    });

                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    if (sendDhl.checked) {
                        if (Gram.value <= 2) {
                            CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €"; 
                        } else if (Gram.value > 2 && Gram.value <= 5) {
                            CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €"; 
                        } else if (Gram.value > 5 && Gram.value <= 10) {
                            CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €"; 
                        } else if (Gram.value > 10 && Gram.value <= 20) {
                            CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; 
                        } else if (Gram.value > 20 && Gram.value <= 31.5) {
                            CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; 
                        }
                        costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                    } else {
                        CostGram[BlockItem].value = "0 €"; 
                    }

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);

                    break;
                }
            case "SI":
                {
                    procent.innerHTML = "22% MwSt";
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    costProcent = bruttoCostValue.times(0.22).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                     
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                        let valueBig = new Big(value); 

                        let x = valueBig.times(0.22).toFixed(2);
                        ProductProcent.push(x); 
                    });

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); 
                        let valueBig = new Big(value); 

                        let x = valueBig.times(0.22).toFixed(2);
                        SchlusselProcent.push(x); 
                    });

                    if (sendDhl.checked) {
                        if (Gram.value <= 2) {
                            CostGram[BlockItem].value = new Big(15).toFixed(2).replace(".", ",") + " €";
                        } else if (Gram.value > 2 && Gram.value <= 5) {
                            CostGram[BlockItem].value = new Big(17).toFixed(2).replace(".", ",") + " €";
                        } else if (Gram.value > 5 && Gram.value <= 10) {
                            CostGram[BlockItem].value = new Big(22).toFixed(2).replace(".", ",") + " €";
                        } else if (Gram.value > 10 && Gram.value <= 20) {
                            CostGram[BlockItem].value = new Big(35).toFixed(2).replace(".", ",") + " €"; 
                        } else if (Gram.value > 20 && Gram.value <= 31.5) {
                            CostGram[BlockItem].value = new Big(49).toFixed(2).replace(".", ",") + " €"; 
                        }
                        costGramValue = new Big(CostGram[BlockItem].value.replace("€", "").replace(",", ".").trim());
                    } else {
                        CostGram[BlockItem].value = "0 €";
                    }

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);

                    break;
                }
            case "SK":
                {                   
                    procent.innerHTML = "20% MwSt";
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                    costProcent = bruttoCostValue.times(0.20).toFixed(2);

                    PreisProductBrutto.forEach((item) => {
                       
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, '');
                        let valueBig = new Big(value);

                        let x = valueBig.times(0.20).toFixed(2);
                        ProductProcent.push(x);
                    });

                    priceBrutto.forEach((item) => {
                        let value = item.value.trim().replace(",", ".").replace("€", "").replace(/\s/g, ''); // Удаляем лишние символы
                        let valueBig = new Big(value); 

                        let x = valueBig.times(0.20).toFixed(2);
                        SchlusselProcent.push(x); 
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

                    mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice);
                    break;
                }
            default:
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);
                    if (sendDhl.checked == true) {
                        Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                        if (value == "US") {

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
                        else if (value == "RU" || value == "UA" || value == "BR" || value == "KZ") {
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

                        else if (value == "CHE" || value == "GBR") {

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
    }
    else {
        switch (value) {

            case "AT":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

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

                    break;
                }

            case "BE":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

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

                    break;
                }

            case "BG":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

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

                    break;
                }

            case "CY":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

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
                    }
                    else {
                        CostGram[BlockItem].value = "0 €";
                    }

                    break;
                }
            case "CZ":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

                    Gram.value = parseFloat(Gram.value.trim().replace(",", ".")); // Преобразуем граммы

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

                    procent.innerHTML = "19% MwSt";

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
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }

            case "EE":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }

            case "ES":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }

            case "FI":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }
            case "FR":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }
            case "GR":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

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

                    break;
                }
            case "HR":
                {
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }
            case "HU":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

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
                 
                    break;
                }
            case "IE":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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
                 
                    break;

                }
            case "IT":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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
                    
                    break;

                }
            case "LT":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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
                   
                    break;

                }
            case "LU":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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
                    
                    break;

                }
            case "LV":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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
                    
                    break;

                }
            case "MT":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;

                }
            case "NL":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);


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

                    break;

                }
            case "PL":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);


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

                    break;

                }
            case "PT":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);


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

                    break;
                }
            case "RO":
                {
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }
            case "SE":
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }
            case "SI":
                {

                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }
            case "SK":
                {
                    Gram.value = parseFloat(Gram.value.trim().replace(",", "."));
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);

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

                    break;
                }
            default:
                {
                    procent.innerHTML = "Steuer";
                    costProcent = bruttoCostValue.times(0.0).toFixed(2);
                    if (sendDhl.checked == true) {
                        Gram.value = parseFloat(Gram.value.trim().replace(",", "."));

                        if (value == "US") {

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
                        else if (value == "RU" || value == "UA" || value == "BR" || value == "KZ") {
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

                        else if (value == "CHE" || value == "GBR") {

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
    }

    proc.forEach((item) => {
        item.innerHTML = parseFloat(costProcent).toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });
    });
    

    let ALLSUM = new Big(Sum).plus(costProcent).plus(costGramValue);

    ALLSUM = ALLSUM.toFixed(2);

    CostGram.forEach((item) => {
        item.innerHTML = parseFloat(CostGram[BlockItem].value).toLocaleString('de-DE', {
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

function mvc(cardProductPrice, schlüsselPrice, ProductProcent, PreisProductNetto, PreisProductNettoSchlussel, SchlusselProcent, countschlüssel, ePrice, ProductEprice, countProductEprice)
{
   

    for (let i = 0; i < cardProductPrice.length; i++) {
        // Получаем текущее значение цены, очищая ненужные символы и создаем объект Big
        let currentPrice = new Big(PreisProductNetto[i].value.replace(",", ".").trim());

        // Получаем значение из ProductProcent и создаем объект Big
        let additionalPrice = new Big(ProductProcent[i]);

        // Вычисляем новую цену
        let newPrice = currentPrice.plus(additionalPrice); // Используем метод plus для сложения

        // Обновляем innerHTML с отформатированной новой ценой
        cardProductPrice[i].innerHTML = newPrice.toFixed(2).replace(".", ",") + " €";
        preis_product[i].value = newPrice.toFixed(2);


        if (Number(countProductEprice[i].innerText) > 1)
        {
            let einSchlüssel = newPrice.toFixed(2) / Number(countProductEprice[i].innerText);

            ProductEprice[i].textContent = parseFloat(einSchlüssel.toFixed(2)).toLocaleString('de-DE', {
                style: 'currency',
                currency: 'EUR'
            });
        }
        else {

            ProductEprice[i].textContent = parseFloat(newPrice.toFixed(2)).toLocaleString('de-DE', {
                style: 'currency',
                currency: 'EUR'
            });
        }

    }

    for (let i = 0; i < schlüsselPrice.length; i++) {

        let currentPrice = new Big(PreisProductNettoSchlussel[i].value.replace(",", ".").trim());

        let additionalPrice = new Big(SchlusselProcent[i]);

        // Вычисляем новую цену
        let newPrice = currentPrice.plus(additionalPrice); // Используем метод plus для сложения

        // Обновляем innerHTML с отформатированной новой ценой
        schlüsselPrice[i].innerHTML = newPrice.toFixed(2).replace(".", ",") + " €"; 
       
        preis_schluessel.value = newPrice.toFixed(2);

        if (Number(countschlüssel[0].innerText) > 1)
        {
            let einSchlüssel = newPrice.toFixed(2) / Number(countschlüssel[0].innerText);

            ePrice[0].innerHTML = parseFloat(einSchlüssel.toFixed(2)).toLocaleString('de-DE', {
                style: 'currency',
                currency: 'EUR'
            });
        }
        else {

            ePrice[0].innerHTML = parseFloat(newPrice.toFixed(2)).toLocaleString('de-DE', {
                style: 'currency',
                currency: 'EUR'
            }); 
        }

    }
   
}
function changePrice(newPrice) {
    currentPrice = newPrice;
}