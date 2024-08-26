function plus(n) {
    let counter = document.getElementById("countKey-" + n);
    let value = Number(counter.value);
    value++;
    counter.value = value;
    AllPrice.value = parseFloat(AllPrice.value) + keyPrice;
    procent();
}
function minus(n) {
    let counter = document.getElementById("countKey-" + n);
    let value = Number(counter.value);
    value--;

    if (value >= 0) {
        counter.value = value;
        AllPrice.value = parseFloat(AllPrice.value) - keyPrice;
    }
    procent();
}