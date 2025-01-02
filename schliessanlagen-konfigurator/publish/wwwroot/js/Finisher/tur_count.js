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

    costItems.value = currentCostItemValue * value;

    procent();
}
function CylinderMinus(n) {
    let counter = document.getElementById("countSchlusse-" + n);
    let CountValue = Number(counter.value);
    let costItems = document.getElementById("costItems-" + n);

    if (CountValue > 1) {
       
        let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim() );

        // Получаем значение стоимости текущего товара (costItems), заменяем запятую на точку
        let currentCostItemValue = parseFloat(costItems.value.replace(",", ".").trim() / CountValue);

        costItems.value = currentAllPriceValue;

        let updatedPrice = currentAllPriceValue - currentCostItemValue;

        costItems.value = currentCostItemValue;

        CountValue--;
        counter.value = CountValue;


        AllPrice.value = updatedPrice.toFixed(2).replace(".", ",") + " €";
     
    }
    procent();
}