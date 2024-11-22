let returnUrl = document.getElementById("returnUrl");
function back()
{
    returnUrl.value = localStorage.getItem('currentURL');
}
back()

let Projekt = document.getElementById("Projekt");

function changeSend()
{
    const selectedRadio = document.querySelectorAll('input[name="DhlSend"]:checked');
    let gramPrice = document.getElementById("CostGram-0");
    let gramItem = document.getElementById("CostGram");
    let allPrice = document.getElementById("costedI-0");

    let priceGram = parseFloat(gramPrice.value.replace("€", "").replace(",", ".").trim());

    allPrice.value = parseFloat(allPrice.value.replace("€", "").replace(",", ".").trim());

    if (selectedRadio)
    {
        if (selectedRadio[0].value === "DHL")
        {
            allPrice.value = parseFloat(allPrice.value) + parseFloat(priceGram);
            allPrice.value = parseFloat(allPrice.value).toLocaleString('de-DE', {
                style: 'currency',
                currency: 'EUR'
            });
        }

        if (selectedRadio[0].value === "0,00 €")
        {
            allPrice.value = parseFloat(allPrice.value) - parseFloat(priceGram);
            allPrice.value = parseFloat(allPrice.value).toLocaleString('de-DE', {
                style: 'currency',
                currency: 'EUR'
            });
        }
    }

}

function calculateDhlCost(sendDhlChecked, sumGram)
{
    if (sendDhlChecked)
    {
        let gramValue = parseFloat(sumGram);

        if (gramValue <= 2) {
            return "15,00 €";
        } else if (gramValue > 2 && gramValue <= 5) {
            return "17,00 €";
        } else if (gramValue > 5 && gramValue <= 10) {
            return "22,00 €";
        } else if (gramValue > 10 && gramValue <= 20) {
            return "35,00 €";
        } else if (gramValue > 20 && gramValue <= 31.5) {
            return "49,00 €";
        }
    }
    return "0 €";
}

function SendRehnung(value)
{
    let ordersInfo = sessionStorage.getItem('OrdersData');
    ordersInfo = JSON.parse(ordersInfo); 

    let schlüsselBrutto = document.querySelectorAll("#PreisProductBruttoSchlussel");
    let schlüsselNetto = document.querySelectorAll("#PreisProductNettoSchlussel");
    let schlüsselPrice = document.querySelectorAll("#PreisProduct");
    let sendDhl = document.getElementById("dhl");
    let gramItem = document.getElementById("CostGram");
    let procent = document.getElementById("proc");

    let gramValue = parseFloat(ordersInfo.sumGram);

    let gramPrice = document.getElementById("CostGram-0");

   
    switch (value) {

        case "AT":
            {
                procent.value = "20% MwSt";
              
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }

        case "BE":
            {                
                procent.value = "21% MwSt";
                
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }

        case "BG":
            {
                procent.value = "20% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }

        case "CY":
            {
                procent.value = "19% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "CZ":
            {
                procent.value = "21% MwSt"; // Устанавливаем текстовое значение

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "DE":
            {
                procent.value = "19% MwSt";

                if (sendDhl.checked)
                {
                    let gramValue = parseFloat(ordersInfo.sumGram);
                    if (gramValue <= 2) {
                        gramItem.value = "6,00 €";
                    } else if (gramValue > 2 && gramValue <= 5) {
                        gramItem.value = "7,00 €";
                    } else if (gramValue > 5 && gramValue <= 10) {
                        gramItem.value = "12,00 €";
                    } else if (gramValue > 10 && gramValue <= 20) {
                        gramItem.value = "15,00 €";
                    } else if (gramValue > 20 && gramValue <= 31.5) {
                        gramItem.value = "20,00 €";
                    }
                    gramItem = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());
                } else {
                    gramItem.value = "0 €";
                }

                break;
            }
        case "DK":
            {
                procent.value = "25% MwSt"; 

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());


                break;
            }

        case "EE":
            {        
                procent.value = "20% MwSt"; 

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }

        case "ES":
            {
                procent.value = "21% MwSt"; 

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }

        case "FI":
            {
                procent.value = "24% MwSt";
                
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "FR":
            {
                procent.value = "20% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "GR":
            {
                procent.value = "24% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "HR":
            {
                procent.value = "25% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "HU":
            {
                procent.value = "27% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());
                break;
            }
        case "IE":
            {
                procent.value = "23% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());
                break;

            }
        case "IT":
            {
                procent.value = "22% MwSt";

               gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "LT":
            {
                procent.value = "21% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;

            }
        case "LU":
            {
                procent.value = "17% MwSt";
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;

            }
        case "LV":
            {
                procent.value = "21% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;

            }
        case "MT":
            {
                procent.value = "18% MwSt";
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;

            }
        case "NL":
            {               
                procent.value = "21% MwSt";
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;

            }
        case "PL":
            {                
                procent.value = "23% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;

            }
        case "PT":
            {
                procent.value = "23% MwSt";
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "RO":
            {
                procent.value = "19% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "SE":
            {

                procent.value = "25% MwSt";

                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "SI":
            {
                procent.value = "22% MwSt";
                gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
        case "SK":
            {
                procent.value = "20% MwSt";

               gramItem.value = calculateDhlCost(sendDhl.checked, ordersInfo.sumGram);

                const gramItemValue = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());

                break;
            }
            default:
            {
                procent.value = "Steuer";
                
                if (sendDhl.checked == true)
                {
                    if (value == "US") {

                        if (gramValue <= 2)
                        {
                            gramItem.value = "30,00 €";
                        }
                        else if (gramValue > 2 && gramValue <= 5)
                        {
                            gramItem.value = "40,00 €";
                        }
                        else if (gramValue > 5 && gramValue <= 10)
                        {
                            gramItem.value = "50,00 €";
                        }
                        else if (gramValue > 10 && gramValue <= 20)
                        {
                            gramItem.value = "60,00 €";
                        }
                        else if (gramValue > 20 && gramValue <= 31.5)
                        {
                            gramItem.value = "70,00 €";
                        }
                        gramItem = parseFloat(gramItem.value.replace("€", "").replace(",", ".").trim());
                    }

                    else if (value == "RU" || value == "UA" || value == "BR" || value == "KZ")
                    {
                        if (gramValue <= 5)
                        {
                            gramItem.value = "35,00 €";
                        }
                        else if (gramValue > 5 && gramValue <= 10)
                        {
                            gramItem.value = "39,00 €";
                        }
                        else if (gramValue > 10 && gramValue <= 20)
                        {
                            gramItem.value = "59,00 €";
                        }
                    }

                    else if (value == "CHE" || value == "GBR")
                    {

                        if (gramValue <= 2)
                        {
                            gramItem.value = "17,00 €";
                        }
                        else if (gramValue > 2 && gramValue <= 5)
                        {
                            gramItem.value = "29,00 €";
                        }
                        else if (gramValue > 5 && gramValue <= 10)
                        {
                            gramItem.value = "38,00 €";
                        }
                        else if (gramValue > 10 && gramValue <= 20)
                        {
                            gramItem.value = "52,00 €";
                        }
                        else if (gramValue > 21 && gramValue <= 31.5)
                        {
                            gramItem.value = "59,00 €";
                        }
                       
                    }
                    else {
                        
                    }
                }

                break;
            }

    }
    gramPrice.value = gramItem.value;
}

document.addEventListener("DOMContentLoaded", function ()
{
    let userkey = sessionStorage.getItem('UserKey');
    let ordersInfo = sessionStorage.getItem('OrdersData');

    ordersInfo = JSON.parse(ordersInfo); 

    let cost = document.getElementById("costedI-0");

    const price = parseFloat(ordersInfo.summe.replace("€", "").replace(",", ".").trim());

    let percent = price * 0.19;

    const mvs = percent; 

    let steur = document.getElementById("Procent");
    steur.value = mvs.toLocaleString('de-DE', {
        style: 'currency',
        currency: 'EUR'
    });

    cost.value = price - percent ;

    let summBrutto = document.getElementById("costedР-0");

    summBrutto.value = price;

    let invoice = document.getElementById("invoiceNumber-0");

    
    axios.get(`/api/Guest/GetOrders?UserKey=${userkey}`, {
        headers: {
            'Content-Type': 'application/json'
        }
    })
        .then(function (response)
        {
            console.log('Успешно:', response.data);

            const ProjektName = document.createElement('div');

            ProjektName.innerHTML = `<h3><strong>ProjektName:</strong> ${ordersInfo.projektName || 'Не указано'}</h3>`;

            Projekt.appendChild(ProjektName);

            response.data.isOpenKey.forEach(order => {

                const card = document.createElement('div');
                card.classList.add('card');

                let orderkeyPric = ordersInfo.keyPrice * 0.19;

                let SumKeyPrice = (ordersInfo.keyPrice - orderkeyPric) * order.countKey;

                let nettoKeyPrice = SumKeyPrice.toLocaleString('de-DE', {
                    style: 'currency',
                    currency: 'EUR'
                });

                card.innerHTML = `

                 <div class="card shadow-lg mb-4 bg-body-tertiary rounded">
                   <div class="row g-0">
                       <div class="col-md-12">
                           <div class="card-body">
                               <div class="card-text" id="schlüssel">
                                   <h4>Schlüssel:${order.nameKey}</h4>
                                   <h4>Anzahl: ${order.countKey}</h4>
                                   <h4 id="PreisProduct" class="danger">Preis: ${nettoKeyPrice} </h4>
                                   <input type="hidden" id="preis_schluessel" name="PreisKey" />
                                   <input type="hidden" id="PreisProductBruttoSchlussel" value="${ordersInfo.keyPrice * order.countKey}" />
                                   <input type="hidden" id="PreisProductNettoSchlussel" value="${SumKeyPrice}" />
                               </div>
                           </div>
                       </div>
                   </div>
               </div>
                
                `;

                Projekt.appendChild(card);
            });

            response.data.orders.forEach(order => {

                let cylinderType = '';

                invoice.value = `SK${order.id}`;

                if (order.zylinderId === 1)
                {
                    cylinderType = ordersInfo.doppelName;

                    let orderkeyPric = ordersInfo.doppel_listPrice * 0.19;

                    let SumKeyPrice = (ordersInfo.doppel_listPrice - orderkeyPric) * order.count;

                    let nettoKeyPrice = SumKeyPrice.toLocaleString('de-DE', {
                        style: 'currency',
                        currency: 'EUR'
                    });
                    const card = document.createElement('div');
                    card.classList.add('card');

                    card.innerHTML = `

                    <div class="card shadow-lg mb-4 bg-body-tertiary rounded">
                                                                <div class="row g-0">                                                             
                                                                    <div class="col-md-12">
                                                                        <div class="card-body">
                                                                            <h3 class="card-title">${cylinderType}</h3>
                                                                            <div class="card-text" id="cardProductPrice-0">
                                                                                  
                                                                            <br />
                                                                               <h4>aussen: ${order.aussen || ''} || innen: ${order.innen || ''}</h4>
                                                                                <h4>Option - ${order.options || ''}.Option</h4>
                                                                                <h4>Anzahl: ${order.count}</h4>
                                                                                <h4 id="PreisProduct-0" class="danger">Preis: ${nettoKeyPrice}</h4>
                                                                                <input type="hidden" id="preis_product"   name="PreisProduct" />
                                                                                <input type="hidden" id="PreisProductBrutto" value="${ordersInfo.doppel_listPrice * order.count}" />
                                                                                <input type="hidden" id="PreisProductNetto" value="${SumKeyPrice}" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                `;

                    // Добавляем карточку в контейнер
                    Projekt.appendChild(card);
                }
                else if (order.zylinderId === 2)
                {
                    cylinderType = ordersInfo.hablName;

                    let orderkeyPric = ordersInfo.halb_listPrice * 0.19;

                    let SumKeyPrice = (ordersInfo.halb_listPrice - orderkeyPric) * order.count;

                    let nettoKeyPrice = SumKeyPrice.toLocaleString('de-DE', {
                        style: 'currency',
                        currency: 'EUR'
                    });
                    const card = document.createElement('div');
                    card.classList.add('card');

                    card.innerHTML = `

                    <div class="card shadow-lg mb-4 bg-body-tertiary rounded">
                                                                <div class="row g-0">                                                             
                                                                    <div class="col-md-12">
                                                                        <div class="card-body">
                                                                            <h3 class="card-title">${cylinderType}</h3>
                                                                            <div class="card-text" id="cardProductPrice-0">
                                                                                  
                                                                            <br />
                                                                               <h4>aussen: ${order.aussen || ''} || innen: ${order.innen || ''}</h4>
                                                                                <h4>Option - ${order.options || ''}.Option</h4>
                                                                                <h4>Anzahl: ${order.count}</h4>
                                                                                <h4 id="PreisProduct-0" class="danger">Preis: ${nettoKeyPrice}</h4>
                                                                                <input type="hidden" id="preis_product-0"   name="PreisProduct" />
                                                                                <input type="hidden" id="PreisProductBrutto-0" value="${ordersInfo.halb_listPrice * order.count}" />
                                                                                <input type="hidden" id="PreisProductNetto-0" value="${SumKeyPrice}" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                `;

                    // Добавляем карточку в контейнер
                    Projekt.appendChild(card);
                } 
                else if (order.zylinderId === 3)
                {
                    cylinderType = ordersInfo.knayfName;
                    let orderkeyPric = ordersInfo.knayf_listPrice * 0.19;

                    let SumKeyPrice = (ordersInfo.knayf_listPrice - orderkeyPric) * order.count;

                    let nettoKeyPrice = SumKeyPrice.toLocaleString('de-DE', {
                        style: 'currency',
                        currency: 'EUR'
                    });
                    const card = document.createElement('div');
                    card.classList.add('card');

                    card.innerHTML = `

                    <div class="card shadow-lg mb-4 bg-body-tertiary rounded">
                                                                <div class="row g-0">                                                             
                                                                    <div class="col-md-12">
                                                                        <div class="card-body">
                                                                            <h3 class="card-title">${cylinderType}</h3>
                                                                            <div class="card-text" id="cardProductPrice-0">
                                                                                  
                                                                            <br />
                                                                               <h4>aussen: ${order.aussen || ''} || innen: ${order.innen || ''}</h4>
                                                                                <h4>Option - ${order.options || ''}.Option</h4>
                                                                                <h4>Anzahl: ${order.count}</h4>
                                                                                <h4 id="PreisProduct-0" class="danger">Preis: ${nettoKeyPrice}</h4>
                                                                                <input type="hidden" id="preis_product-0"   name="PreisProduct" />
                                                                                <input type="hidden" id="PreisProductBrutto-0" value="${ordersInfo.knayf_listPrice * order.count}" />
                                                                                <input type="hidden" id="PreisProductNetto-0" value="${SumKeyPrice}" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                `;

                    // Добавляем карточку в контейнер
                    Projekt.appendChild(card);
                }
                else if (order.zylinderId === 4)
                {
                    cylinderType = ordersInfo.hebelZylinder;
                    let orderkeyPric = ordersInfo.hebel_listPrice * 0.19;

                    let SumKeyPrice = (ordersInfo.hebel_listPrice - orderkeyPric) * order.count;

                    let nettoKeyPrice = SumKeyPrice.toLocaleString('de-DE', {
                        style: 'currency',
                        currency: 'EUR'
                    });

                    const card = document.createElement('div');
                    card.classList.add('card');

                    card.innerHTML = `

                    <div class="card shadow-lg mb-4 bg-body-tertiary rounded">
                                                                <div class="row g-0">                                                             
                                                                    <div class="col-md-12">
                                                                        <div class="card-body">
                                                                            <h3 class="card-title">${cylinderType}</h3>
                                                                            <div class="card-text" id="cardProductPrice-0">
                                                                                  
                                                                            <br />
                                                                               <h4>aussen: ${order.aussen || ''} || innen: ${order.innen || ''}</h4>
                                                                                <h4>Option - ${order.options || ''}.Option</h4>
                                                                                <h4>Anzahl: ${order.count}</h4>
                                                                                <h4 id="PreisProduct-0" class="danger">Preis: ${nettoKeyPrice}</h4>
                                                                                <input type="hidden" id="preis_product-0"   name="PreisProduct" />
                                                                                <input type="hidden" id="PreisProductBrutto-0" value="${ordersInfo.hebel_listPrice * order.count}" />
                                                                                <input type="hidden" id="PreisProductNetto-0" value="${SumKeyPrice}" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                `;

                    // Добавляем карточку в контейнер
                    Projekt.appendChild(card);

                }
                else if (order.zylinderId === 5)
                {
                    cylinderType = ordersInfo.vorhangZylinder;
                    let orderkeyPric = ordersInfo.vorhang_listPrice * 0.19;

                    let SumKeyPrice = (ordersInfo.vorhang_listPrice - orderkeyPric) * order.count;

                    let nettoKeyPrice = SumKeyPrice.toLocaleString('de-DE', {
                        style: 'currency',
                        currency: 'EUR'
                    });

                    const card = document.createElement('div');
                    card.classList.add('card');

                    card.innerHTML = `

                    <div class="card shadow-lg mb-4 bg-body-tertiary rounded">
                                                                <div class="row g-0">                                                             
                                                                    <div class="col-md-12">
                                                                        <div class="card-body">
                                                                            <h3 class="card-title">${cylinderType}</h3>
                                                                            <div class="card-text" id="cardProductPrice-0">
                                                                                  
                                                                            <br />
                                                                               <h4>aussen: ${order.aussen || ''} || innen: ${order.innen || ''}</h4>
                                                                                <h4>Option - ${order.options || ''}.Option</h4>
                                                                                <h4>Anzahl: ${order.count}</h4>
                                                                                <h4 id="PreisProduct-0" class="danger">Preis: ${nettoKeyPrice}</h4>
                                                                                <input type="hidden" id="preis_product-0"   name="PreisProduct" />
                                                                                <input type="hidden" id="PreisProductBrutto-0" value="${ordersInfo.vorhang_listPrice * order.count}" />
                                                                                <input type="hidden" id="PreisProductNetto-0" value="${SumKeyPrice}" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                `;

                    // Добавляем карточку в контейнер
                    Projekt.appendChild(card);

                }
                else if (order.zylinderId === 6)
                {
                    cylinderType = ordersInfo.aussenZylinder;
                    let orderkeyPric = ordersInfo.aussenZylinder_listPrice * 0.19;

                    let SumKeyPrice = (ordersInfo.aussenZylinder_listPrice - orderkeyPric) * order.count;

                    let nettoKeyPrice = SumKeyPrice.toLocaleString('de-DE', {
                        style: 'currency',
                        currency: 'EUR'
                    });

                    const card = document.createElement('div');
                    card.classList.add('card');

                    card.innerHTML = `

                    <div class="card shadow-lg mb-4 bg-body-tertiary rounded">
                                                                <div class="row g-0">                                                             
                                                                    <div class="col-md-12">
                                                                        <div class="card-body">
                                                                            <h3 class="card-title">${cylinderType}</h3>
                                                                            <div class="card-text" id="cardProductPrice-0">
                                                                                  
                                                                            <br />
                                                                               <h4>aussen: ${order.aussen || ''} || innen: ${order.innen || ''}</h4>
                                                                                <h4>Option - ${order.options || ''}.Option</h4>
                                                                                <h4>Anzahl: ${order.count}</h4>
                                                                                <h4 id="PreisProduct-0" class="danger">Preis: ${nettoKeyPrice}</h4>
                                                                                <input type="hidden" id="preis_product-0"   name="PreisProduct" />
                                                                                <input type="hidden" id="PreisProductBrutto-0" value="${ordersInfo.aussenZylinder_listPrice * order.count}" />
                                                                                <input type="hidden" id="PreisProductNetto-0" value="${SumKeyPrice}" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                `;

                    // Добавляем карточку в контейнер
                    Projekt.appendChild(card);

                } 

                
            });
        })
        .catch(function (error) {
            console.error('Ошибка:', error);
            alert('Произошла ошибка при отправке данных. Попробуйте снова.');
        });
});
let currentStep = 1;

function updateSteps() {
    const steps = document.querySelectorAll('.step');
    const progressBar = document.getElementById('progress-bar');
    const progressValue = (currentStep - 1) * 25;

    steps.forEach((step, index) => {
        if (index < currentStep - 1) {
            step.classList.add('step-completed');
            step.classList.remove('step-active');
        } else if (index === currentStep - 1) {
            step.classList.add('step-active');
            step.classList.remove('step-completed');
        } else {
            step.classList.remove('step-active', 'step-completed');
        }
    });

    progressBar.style.width = `${progressValue}%`;
    progressBar.setAttribute('aria-valuenow', progressValue);
    progressBar.innerHTML = `Schritt ${currentStep}`;
}

// Обработчик для кнопки Next
document.getElementById('nextStepBtn').addEventListener('click', () => {
    if (currentStep < 5)
    {
        currentStep++;
        updateSteps();
    }
});

// Обработчик для кнопки Previous
document.getElementById('prevStepBtn').addEventListener('click', () => {
    if (currentStep > 1) {
        currentStep--;
        updateSteps();
    }
});

// Инициализируем шаги
updateSteps();

let FormStep = 1;

function showStep(step) {
    // Скрыть все шаги
    let steps = document.querySelectorAll('.step-form');
    steps.forEach(function (stepForm) {
        stepForm.classList.remove('active');
    });

    // Показать текущий шаг
    document.getElementById('step' + step).classList.add('active');
}

function nextStep() {
    // Увеличиваем шаг, если это не последний шаг
    if (FormStep < 5) {
        FormStep++;
        showStep(FormStep);
        toggleNavigationButtons();
    }
}

// Функция для перехода на предыдущий шаг
function previousStep() {
    // Уменьшаем шаг, если это не первый шаг
    if (FormStep > 1) {
        FormStep--;
        showStep(FormStep);
        toggleNavigationButtons();
    }
}

// Функция для скрытия/отображения кнопок навигации в зависимости от шага
function toggleNavigationButtons() {
    let nextButton = document.getElementById('nextStepBtn');
    let prevButton = document.getElementById('prevStepBtn');

    // Если это последний шаг, скрываем кнопку "Далее"
    if (FormStep === 5) {
        nextButton.style.display = 'none';
    } else {
        nextButton.style.display = 'inline-block';
    }

    // Если это первый шаг, скрываем кнопку "Назад"
    if (FormStep === 1) {
        prevButton.style.display = 'none';
    } else {
        prevButton.style.display = 'inline-block';
    }
}

// Изначально показываем шаг 1
showStep(FormStep);

// Привязываем обработчики событий к кнопкам
document.getElementById('nextStepBtn').addEventListener('click', nextStep);
document.getElementById('prevStepBtn').addEventListener('click', previousStep);