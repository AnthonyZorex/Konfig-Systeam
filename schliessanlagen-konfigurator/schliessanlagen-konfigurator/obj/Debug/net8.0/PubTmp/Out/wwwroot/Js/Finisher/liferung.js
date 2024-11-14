function liferung() {

    let WaitTime = document.querySelector("#lifer");
    let Aussen = document.querySelectorAll("#Aussen");
    let Innen = document.querySelectorAll("#Intern");

    if (Aussen != null) {
        let AussenCheked = Array.from(Aussen)
            .map(element => parseFloat(element.value))
            .some(value => value >= 55);


        if (AussenCheked == true) {
            WaitTime.textContent = liferungGroz.map(text => text.replace(/<[^>]*>/g, '').trim());
        }
        else {
            WaitTime.textContent = liferun.map(text => text.replace(/<[^>]*>/g, '').trim());
        }
        if (Innen != null && !AussenCheked) {
            let InternCheked = Array.from(Innen)
                .map(element => parseFloat(element.value))
                .some(value => value >= 55);

            if (InternCheked == true) {
                WaitTime.textContent = liferungGroz.map(text => text.replace(/<[^>]*>/g, '').trim());
            }
            else {
                WaitTime.textContent = liferun.map(text => text.replace(/<[^>]*>/g, '').trim());
            }
        }
    }
}