function CylinderPlus(n)
{
    let counter = document.getElementById("countSchlusse-" + n);
    let costItems = document.getElementById("costItems-" + n);

    // Преобразуем значение счетчика в число
    let value = Number(counter.value);

    // Увеличиваем значение
    value++;

    // Присваиваем новое значение счетчику
    counter.value = value;

    // Получаем текущее значение AllPrice, удаляем символ €, запятую заменяем на точку
    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

    // Получаем значение стоимости текущего товара (costItems)
    let currentCostItemValue = parseFloat(costItems.value.replace(",", ".").trim());

    AllPrice.value = (currentAllPriceValue + currentCostItemValue).toFixed(2).replace(".", ",") + " €";

    procent();
}
function CylinderMinus(n) {
    let counter = document.getElementById("countSchlusse-" + n);
    let CountValue = Number(counter.value);
    let costItems = document.getElementById("costItems-" + n);

    if (CountValue > 1) {
        CountValue--;
        counter.value = CountValue;

        let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

        // Получаем значение стоимости текущего товара (costItems), заменяем запятую на точку
        let currentCostItemValue = parseFloat(costItems.value.replace(",", ".").trim());

        let updatedPrice = currentAllPriceValue - currentCostItemValue;

        AllPrice.value = updatedPrice.toFixed(2).replace(".", ",") + " €";
     
    }
    procent();
}