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
    let proc = document.getElementById("aldProcent-" + BlockItem);

    let procent = document.getElementById("procent-" + BlockItem);

    let mvstr = cost[0].innerHTML.replace("€", "");
    let Gram = document.getElementById("gramWert-" + BlockItem);

    let sendDhl = document.getElementById("dhl-" + BlockItem);

    let Plase = document.getElementById("Plase-" + BlockItem);

    let pr = proc.value.replace(".", ",");

    let Sum = parseFloat(mvstr.replace("&nbsp;", "").trim()) - parseFloat(pr.replace("€", ""));

    let costProcent = 0;

    let CostGram = document.querySelectorAll("#CostGram-" + BlockItem);

    Sum = Sum - parseFloat(CostGram[0].value.replace("€", "").replace(".", ",").trim());

    switch (value.target.value) {
        case "AT":
            {
                costProcent = (Sum.toFixed(2) * 0.20);

                procent.value = "20% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }
                break;
            }
        case "BE":
            {
                costProcent = (Sum.toFixed(2) * 0.21);
                procent.value = "21% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "BG":
            {
                costProcent = (Sum.toFixed(2) * 0.20);
                procent.value = "20% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }
                break;
            }
        case "CY":
            {
                costProcent = (Sum.toFixed(2) * 0.19);
                procent.value = "19% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }
                break;
            }
        case "CZ":
            {
                costProcent = (Sum.toFixed(2) * 0.21);
                procent.value = "21% MwSt";
                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }
                break;
            }
        case "DE":
            {
                costProcent = (Sum.toFixed(2) * 0.19);
                procent.value = "19% MwSt";
                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("6");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("7");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("12");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("15");
                    }
                    else if (parseFloat(Gram.value) > 21 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("20");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }
                break;
            }
        case "DK":
            {
                costProcent = (Sum.toFixed(2) * 0.25);
                procent.value = "25% MwSt";
                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "EE":
            {

                costProcent = (Sum.toFixed(2) * 0.20);
                procent.value = "20% MwSt";
                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "ES":
            {
                costProcent = (Sum.toFixed(2) * 0.21);
                procent.value = "21% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "FI":
            {
                costProcent = (Sum.toFixed(2) * 0.24);
                procent.value = "24% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "FR":
            {
                costProcent = (Sum.toFixed(2) * 0.20);
                procent.value = "20% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "GR":
            {
                costProcent = (Sum.toFixed(2) * 0.24);
                procent.value = "24% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "HR":
            {
                costProcent = (Sum.toFixed(2) * 0.25);
                procent.value = "25% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "HU":
            {
                costProcent = (Sum.toFixed(2) * 0.27);
                procent.value = "27% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "IE":
            {
                costProcent = (Sum.toFixed(2) * 0.23);
                procent.value = "23% MwSt";


                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "IT":
            {
                costProcent = (Sum.toFixed(2) * 0.22);
                procent.value = "22% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "LT":
            {
                costProcent = (Sum.toFixed(2) * 0.21);
                procent.value = "21% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "LU":
            {
                costProcent = (Sum.toFixed(2) * 0.17);
                procent.value = "17% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "LV":
            {
                costProcent = (Sum.toFixed(2) * 0.21);
                procent.value = "21% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "MT":
            {
                costProcent = (Sum.toFixed(2) * 0.18);
                procent.value = "18% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "NL":
            {
                costProcent = (Sum.toFixed(2) * 0.21);
                procent.value = "21% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "PL":
            {
                costProcent = (Sum.toFixed(2) * 0.23);
                procent.value = "23% MwSt";

                if (sendDhl.checked == true) {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }
                break;
            }
        case "PT":
            {
                costProcent = (Sum.toFixed(2) * 0.23);
                procent.value = "23% MwSt";

                if (sendDhl.checked == true)
                {
                    if (parseFloat(Gram.value) <= 2)
                    {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "RO":
            {
                costProcent = (Sum.toFixed(2) * 0.19);
                procent.value = "19% MwSt";

                if (sendDhl.checked == true)
                {
                    if (parseFloat(Gram.value) <= 2)
                    {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }
                break;
            }
        case "SE":
            {
                costProcent = (Sum.toFixed(2) * 0.25);
                procent.value = "25% MwSt";

                if (sendDhl.checked == true)
                {
                    if (parseFloat(Gram.value) <= 2)
                    {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "SI":
            {
                costProcent = (Sum.toFixed(2) * 0.22);
                procent.value = "22% MwSt";

                if (sendDhl.checked == true)
                {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        case "SK":
            {
                costProcent = (Sum.toFixed(2) * 0.20);
                procent.value = "20% MwSt";

                if (sendDhl.checked == true)
                {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("15,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("22,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("49,00");
                    }
                }
                else {
                    CostGram[0].value = parseFloat("0,00");
                }

                break;
            }
        default: {

            if (sendDhl.checked == true)
            {
                if (value.target.value == "US") {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("30,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("40,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("50,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("60,00");
                    }
                    else if (parseFloat(Gram.value) > 20 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("70,00");
                    }
                }
                if (value.target.value == "RU" || value.target.value == "UA" || value.target.value == "BR" || value.target.value == "KZ") {
                    if (parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("35,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("39,50");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("59,00");
                    }
                }

                if (value.target.value == "CHE" || value.target.value == "GBR") {
                    if (parseFloat(Gram.value) <= 2) {
                        CostGram[0].value = parseFloat("17,00");
                    }
                    else if (parseFloat(Gram.value) > 2 && parseFloat(Gram.value) <= 5) {
                        CostGram[0].value = parseFloat("29,00");
                    }
                    else if (parseFloat(Gram.value) > 5 && parseFloat(Gram.value) <= 10) {
                        CostGram[0].value = parseFloat("38,00");
                    }
                    else if (parseFloat(Gram.value) > 10 && parseFloat(Gram.value) <= 20) {
                        CostGram[0].value = parseFloat("52,00");
                    }
                    else if (parseFloat(Gram.value) > 21 && parseFloat(Gram.value) <= 31, 5) {
                        CostGram[0].value = parseFloat("59,00");
                    }

                }
            }
            else {
                CostGram[0].value = parseFloat("0,00");
            }



            break;
        }
    }
    proc.value = costProcent.toLocaleString('de-DE', {
        style: 'currency',
        currency: 'EUR'
    });;

    let ALLSUM = costProcent + Sum + parseFloat(CostGram[0].value);

    CostGram.forEach((item) => {
        item.value = parseFloat(CostGram[0].value).toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });
    });

    for (let i = 0; i < cost.length; i++)
    {
        cost[i].innerHTML = ALLSUM.toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });

        cost[i].textContent = ALLSUM.toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });

        cost[i].value = ALLSUM.toLocaleString('de-DE', {
            style: 'currency',
            currency: 'EUR'
        });
    }

    changePrice(ALLSUM);
}

function changePrice(newPrice) {
    currentPrice = newPrice;
}