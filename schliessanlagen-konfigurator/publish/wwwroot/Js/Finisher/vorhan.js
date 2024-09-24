function VorhanSchlus(idBlock, selectId) {
    let BlockTur = document.getElementById("Size-" + idBlock);

    let SelectItemId = selectId - 1;

    let costItems = document.getElementById("costItems-" + idBlock);
    let countSchluss = BlockTur.querySelector("#countSchlusse-" + idBlock);

    let oldAussen = BlockTur.querySelector("#Aussen-" + idBlock);

    if (oldAussen.value === "") {
        oldAussen.value = 0;
    }

    AllPrice.value = parseFloat(AllPrice.value) - (VorhanSizeCost[oldAussen.value] * Number(countSchluss.value));
    costItems.value = parseFloat(costItems.value) - (VorhanSizeCost[oldAussen.value]);

    oldAussen.value = SelectItemId;


    costItems.value = parseFloat(costItems.value) + (VorhanSizeCost[SelectItemId]);
    AllPrice.value = parseFloat(AllPrice.value) + (VorhanSizeCost[SelectItemId] * Number(countSchluss.value));

    let Costen = AllPrice.value.replace(/,/g, '.');

    AllPrice.value = Costen;

    procent();
}