let returnUrl = document.getElementById("returnUrl");
function back() {
    returnUrl.value = localStorage.getItem('currentURL');
}
back()


let Projekt = document.getElementById("Projekt");

document.addEventListener("DOMContentLoaded", function () {

    let userkey = sessionStorage.getItem('UserKey');
    let ordersInfo = sessionStorage.getItem('OrdersData');

    axios.get(`/api/Guest/GetOrders?UserKey=${userkey}`, {
        headers: {
            'Content-Type': 'application/json'
        }
    })


        .then(function (response)
        {
            console.log('Успешно:', response.data);

            const ProjektName = document.createElement('div');

            ProjektName.innerHTML = `<p><strong>ProjektName:</strong> ${response.data.orders.projektName || 'Не указано'}</p>`;

            Projekt.appendChild(ProjektName);


            response.data.isOpenKey.forEach(order => {
                const card = document.createElement('div');
                card.classList.add('card');

                card.innerHTML = `
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">${order.nameKey}</h5>
                            <div class="info">
                                <p><strong>Anzahl:</strong> ${order.countKey}</p>
                            </div>
                        </div>
                    </div>
                `;

                Projekt.appendChild(card);
            });

            response.data.orders.forEach(order => {

                let cylinderType = '';

                if (order.zylinderId === '1')
                {
                    cylinderType = ordersInfo.doppelName;
                }
                else if (order.zylinderId === '2')
                {
                    cylinderType = ordersInfo.hablName;
                } 
                else if (order.zylinderId === '3')
                {
                    cylinderType = ordersInfo.knayfName;
                }
                else if (order.zylinderId === '4')
                {
                    cylinderType = ordersInfo.hebelZylinder;
                }
                else if (order.zylinderId === '5')
                {
                    cylinderType = ordersInfo.vorhangZylinder;
                }
                else if (order.zylinderId === '6')
                {
                    cylinderType = ordersInfo.aussenZylinder;
                } 

                const card = document.createElement('div');
                card.classList.add('card');

                card.innerHTML = `
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">${order.dorName}</h5>
                            <p><strong>Цилиндр:</strong> ${cylinderType}</p>
                            <p><strong>aussen:</strong> ${order.aussen || ''} || innen:${order.innen || ''} cm</p>
                            <p><strong>aussen:</strong> ${order.aussen || ''} || innen:${order.innen || ''} cm</p>
                              <p><strong>Option -</strong> ${order.options || ''}</p>
                            <div class="info">
                                <p><strong>Anzahl:</strong> ${order.count}</p>
                            </div>
                        </div>
                    </div>
                `;

                // Добавляем карточку в контейнер
                Projekt.appendChild(card);
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

// Функция для перехода к следующему шагу
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