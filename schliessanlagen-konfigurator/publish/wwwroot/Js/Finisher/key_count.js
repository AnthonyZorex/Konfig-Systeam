function plus(n) {
    let counter = document.getElementById("countKey-" + n);
    let value = Number(counter.value);

    // Увеличиваем значение счётчика
    value++;
    counter.value = value;

    // Получаем текущее значение AllPrice и приводим его к числу
    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
    let currentKeyPriceValue = parseFloat(keyPrice);
    AllPrice.value = (currentAllPriceValue + currentKeyPriceValue).toFixed(2).replace(".", ",") + " €";
    procent();
}
function minus(n) {
    let counter = document.getElementById("countKey-" + n);
    let value = Number(counter.value);

    // Уменьшаем значение счетчика
    value--;

    // Проверяем, что значение счётчика не меньше 0
    if (value >= 0) {
        counter.value = value;

        // Получаем текущее значение AllPrice, удаляем символ €, запятую заменяем на точку
        let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());
        let currentKeyPriceValue = parseFloat(keyPrice);
      
        // Уменьшаем общую цену и форматируем до двух десятичных знаков
        AllPrice.value = (currentAllPriceValue - currentKeyPriceValue).toFixed(2).replace(".", ",") + " €";
    }
    procent();
}