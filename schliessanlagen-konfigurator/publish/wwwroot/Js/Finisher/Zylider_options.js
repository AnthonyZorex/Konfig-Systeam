function OptionsSelect(idBlock, TypeSylinder, id, optionsValue, elemNum, OptionsName, N, count)
{
    let costItems = document.getElementById("costItems-" + idBlock);

    let countSchluss = document.getElementById("countSchlusse-" + count);

    let BlockTur = document.getElementById("ExelItem-" + idBlock);

    let QueryNameOptions = BlockTur.querySelectorAll("#OptionName");

    let Selector = document.getElementById(id);

    let TotalOptions = document.getElementById("OptionAll-" + idBlock);

    let AllSelectro = BlockTur.querySelectorAll("select");

    let selectedOption = Selector.options[Selector.selectedIndex].id;

    QueryNameOptions.forEach((item) => {
        if (item.defaultValue === OptionsName) {

            if (optionsValue != "Nein") {
                item.value = '\u2713' + OptionsName;
                item.style.color = "green";

                if (item.value === '\u2713' + "NGF = Not- und Gefahrenfunktion") {
                    hasNotUndGefahrenfunktion = true;

                    if (item.value === '\u2713' + "NGF = Not- und Gefahrenfunktion" && hasFreilauffunktion === true && hasNotUndGefahrenfunktion === true) {
                        for (let i = 0; i < QueryNameOptions.length; i++) {
                            if (QueryNameOptions[i].value === '\u2713' + "F = Freilauffunktion") {
                                let OptionOldCheker = document.getElementById(`d-${idBlock} + ${i}`);

                                costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                                AllPrice.value = (parseFloat(AllPrice.value) - (OptionOldCheker.value * Number(countSchluss.value)));
                                QueryNameOptions[i].value = QueryNameOptions[i].defaultValue;

                                let regex = QueryNameOptions[i].value;

                                TotalOptions.value = TotalOptions.value.replace(regex + ':  Ja, ', '');

                                QueryNameOptions[i].style.color = "black";
                                var chekedSelect = document.getElementById(idBlock + "-Options-" + i);
                                chekedSelect.value = "Nein";
                                hasNotUndGefahrenfunktion == false;
                            }
                        }

                    }
                }
                if (item.value === '\u2713' + "F = Freilauffunktion") {
                    hasFreilauffunktion = true;

                    if (item.value === '\u2713' + "F = Freilauffunktion" && hasFreilauffunktion === true && hasNotUndGefahrenfunktion === true) {
                        for (let i = 0; i < QueryNameOptions.length; i++) {
                            if (QueryNameOptions[i].value === '\u2713' + "NGF = Not- und Gefahrenfunktion") {
                                let OptionOldCheker = document.getElementById(`d-${idBlock} + ${i}`);

                                costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                                AllPrice.value = (parseFloat(AllPrice.value) - (OptionOldCheker.value * Number(countSchluss.value)));
                                QueryNameOptions[i].value = QueryNameOptions[i].defaultValue;
                                QueryNameOptions[i].style.color = "black";

                                let regex = QueryNameOptions[i].value;

                                TotalOptions.value = TotalOptions.value.replace(regex + ':  Ja, ', '');

                                var chekedSelect = document.getElementById(idBlock + "-Options-" + i);
                                chekedSelect.value = "Nein";
                                hasFreilauffunktion == false;
                            }

                        }
                    }
                }

                if (TypeSylinder === "Doppel") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    TotalOptions.value += `${item.defaultValue}:  ${optionsValue}` + ", ";

                    costItems.value = parseFloat(costItems.value) + (DoppelCostValue[selectedOption]);

                    AllPrice.value = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) + (DoppelCostValue[selectedOption] * Number(countSchluss.value));

                    OptionOldCheker.value = DoppelCostValue[selectedOption];

                }
                if (TypeSylinder === "Knayf") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    TotalOptions.value += `${item.defaultValue}:  ${optionsValue}` + ", ";

                    costItems.value = parseFloat(costItems.value) + (KnayfOptionPrice[selectedOption]);

                    AllPrice.value = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) + (KnayfOptionPrice[selectedOption] * Number(countSchluss.value));
                    OptionOldCheker.value = KnayfOptionPrice[selectedOption];
                }
                if (TypeSylinder === "Halb") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    TotalOptions.value += `${item.defaultValue}:  ${optionsValue}` + ", ";

                    costItems.value = parseFloat(costItems.value) + (HalbOptionsValueCost[selectedOption]);
                    AllPrice.value = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) + (HalbOptionsValueCost[selectedOption] * Number(countSchluss.value));

                    OptionOldCheker.value = HalbOptionsValueCost[selectedOption];
                }
                if (TypeSylinder === "Helb") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    TotalOptions.value += `${item.defaultValue}:  ${optionsValue}` + ", ";

                    costItems.value = parseFloat(costItems.value) + (HebelOptionsCost[selectedOption]);

                    AllPrice.value = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) + (HebelOptionsCost[selectedOption] * Number(countSchluss.value));
                    OptionOldCheker.value = HebelOptionsCost[selectedOption];
                }
                if (TypeSylinder === "Vorhan") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    TotalOptions.value += `${item.defaultValue}:  ${optionsValue}` + ", ";

                    costItems.value = parseFloat(costItems.value) + (VorhanSchlossValueCost[selectedOption]);

                    AllPrice.value = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) + (VorhanSchlossValueCost[selectedOption] * Number(countSchluss.value));
                    OptionOldCheker.value = VorhanSchlossValueCost[selectedOption];
                }
                if (TypeSylinder === "Aussen") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    TotalOptions.value += `${item.defaultValue}:  ${optionsValue}` + ", ";

                    costItems.value = parseFloat(costItems.value) + (AussenRundOptionsCostValue[selectedOption]);

                    AllPrice.value = parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) + (AussenRundOptionsCostValue[selectedOption] * Number(countSchluss.value));
                    OptionOldCheker.value = AussenRundOptionsCostValue[selectedOption];
                }
            }
            else {
                if (TypeSylinder === "Doppel") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    let regex = item.defaultValue;

                    let info = TotalOptions.value;

                    let str = info.replace(`${regex}:  Ja, `, ' ');

                    TotalOptions.value = str;

                    let OptionDoppel = document.getElementById(`Option-${idBlock} + ${N}`);

                    OptionDoppel.value = ``;

                    costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                    AllPrice.value = (parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) - (OptionOldCheker.value * Number(countSchluss.value)));
                    item.value = OptionsName;

                    item.style.color = "black";
                }
                if (TypeSylinder === "Knayf") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    let regex = item.defaultValue;
                    TotalOptions.value = TotalOptions.value.replace(regex + ':  Ja, ', '');

                    let OptionKnayf = document.getElementById(`Option-${idBlock} + ${N}`);
                    OptionKnayf.value = ``;

                    costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                    AllPrice.value = (parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) - (OptionOldCheker.value * Number(countSchluss.value)));

                    item.value = OptionsName;
                    item.style.color = "black";
                }
                if (TypeSylinder === "Halb") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    let regex = item.defaultValue;
                    TotalOptions.value = TotalOptions.value.replace(regex + ':  Ja, ', '');

                    let OptionHalb = document.getElementById(`Option-${idBlock} + ${N}`);
                    OptionHalb.value = ``;

                    costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                    AllPrice.value = (parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) - (OptionOldCheker.value * Number(countSchluss.value)));

                    item.value = OptionsName;
                    item.style.color = "black";
                }
                if (TypeSylinder === "Helb") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    let regex = item.defaultValue;
                    TotalOptions.value = TotalOptions.value.replace(regex + ':  Ja, ', '');

                    let OptionHelb = document.getElementById(`Option-${idBlock} + ${N}`);
                    OptionHelb.value = ``;

                    costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                    AllPrice.value = (parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) - (OptionOldCheker.value * Number(countSchluss.value)));

                    item.value = OptionsName;
                    item.style.color = "black";
                }
                if (TypeSylinder === "Vorhan") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    let regex = item.defaultValue;
                    TotalOptions.value = TotalOptions.value.replace(regex + ':  Ja, ', '');

                    let OptionVorhan = document.getElementById(`Option-${idBlock} + ${N}`);
                    OptionVorhan.value = ``;

                    costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                    AllPrice.value = (parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) - (OptionOldCheker.value * Number(countSchluss.value)));

                    item.value = OptionsName;
                    item.style.color = "black";
                }
                if (TypeSylinder === "Aussen") {
                    let OptionOldCheker = document.getElementById(`d-${idBlock} + ${N}`);

                    let regex = item.defaultValue;
                    TotalOptions.value = TotalOptions.value.replace(regex + ':  Ja, ', '');

                    let OptionAussen = document.getElementById(`Option-${idBlock} + ${N}`);
                    OptionAussen.value = ``;

                    costItems.value = parseFloat(costItems.value) - (OptionOldCheker.value);

                    AllPrice.value = (parseFloat(AllPrice.value.replace("€", "").replace(",", ".").trim()) - (OptionOldCheker.value * Number(countSchluss.value)));

                    item.value = OptionsName;
                    item.style.color = "black";
                }
            }
        }
    });
    procent();
}