function CylinderPlus(n)
{
    let counter = document.getElementById("countSchlusse-" + n);

    let costItems = document.getElementById("costItems-" + n);

    let value = Number(counter.value);

    value++;

    counter.value = value;

    AllPrice.value = parseFloat(AllPrice.value) + parseFloat(costItems.value);

    procent();
}
function CylinderMinus(n) {
    let counter = document.getElementById("countSchlusse-" + n);
    let CountValue = Number(counter.value);
    let costItems = document.getElementById("costItems-" + n);

    if (CountValue > 1) {
        CountValue--;
        counter.value = CountValue;

        AllPrice.value = parseFloat(AllPrice.value) - parseFloat(costItems.value);
    }
    procent();
}