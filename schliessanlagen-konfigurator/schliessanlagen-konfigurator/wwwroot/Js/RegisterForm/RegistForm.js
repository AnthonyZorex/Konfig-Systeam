let returnUrl = document.getElementById("returnUrl");
function back() {
    returnUrl.value = localStorage.getItem('currentURL');
}
back()

let Projekt = document.getElementById("Projekt");


document.addEventListener("DOMContentLoaded", function ()
{
    let userkey = sessionStorage.getItem('UserKey');
    let ordersInfo = sessionStorage.getItem('OrdersData');

    ordersInfo = JSON.parse(ordersInfo); 

    let cost = document.getElementById("costedI-0");

    const price = parseFloat(ordersInfo.summe.replace("€", "").replace(",", ".").trim());

    let percent = price * 0.19;

    const mvs = percent;

    cost.value = price - percent ;
    cost.innerHTML = price - percent;

    let steur = document.getElementById("aldProcent-0");

    let gramItem = document.getElementById("CostGram-0");

    gramItem.value = ordersInfo.sumGram;

    let summBrutto = document.getElementById("costedР-0");

    summBrutto.value = price;

    let invoice = document.getElementById("invoiceNumber-0");

    steur.value = parseFloat(mvs).toLocaleString('de-DE', {
        style: 'currency',
        currency: 'EUR'
    });

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
                               <div class="card-text" id="schlüssel-0">
                                   <h4>Schlüssel:${order.nameKey}</h4>
                                   <h4>Anzahl: ${order.countKey}</h4>
                                   <h4 id="PreisProduct-0" class="danger">Preis: ${nettoKeyPrice} </h4>
                                   <input type="hidden" id="preis_schluessel-0" name="PreisKey" />
                                   <input type="hidden" id="PreisProductBruttoSchlussel-0" value="${ordersInfo.keyPrice * order.countKey}" />
                                   <input type="hidden" id="PreisProductNettoSchlussel-0" value="${SumKeyPrice}" />
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
                                                                                <input type="hidden" id="preis_product-0"   name="PreisProduct" />
                                                                                <input type="hidden" id="PreisProductBrutto-0" value="${ordersInfo.doppel_listPrice * order.count}" />
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