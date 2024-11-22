function VorhanSchlus(idBlock, selectId) {
    let BlockTur = document.getElementById("Size-" + idBlock);

    let SelectItemId = selectId - 1;

    let costItems = document.getElementById("costItems-" + idBlock);
    let countSchluss = BlockTur.querySelector("#countSchlusse-" + idBlock);

    let oldAussen = BlockTur.querySelector("#Aussen-" + idBlock);

    if (oldAussen.value === "") {
        oldAussen.value = 0;
    }

    let currentAllPriceValue = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim());

    // Получаем текущее значение costItems
    let currentCostItemValue = parseFloat(costItems.value.replace(",", ".").trim());

    currentAllPriceValue -= (VorhanSizeCost[oldAussen.value] * Number(countSchluss.value));
    currentCostItemValue -= VorhanSizeCost[oldAussen.value];

    // Обновляем значение oldAussen
    oldAussen.value = SelectItemId;

    // Добавляем стоимость для нового SelectItemId
    currentCostItemValue += VorhanSizeCost[SelectItemId];
    currentAllPriceValue += (VorhanSizeCost[SelectItemId] * Number(countSchluss.value));

    // Округляем до двух знаков после запятой и форматируем
    AllPrice.value = currentAllPriceValue.toFixed(2).replace(".", ",") + " €";
    costItems.value = currentCostItemValue.toFixed(2).replace(".", ",");

    procent();
}