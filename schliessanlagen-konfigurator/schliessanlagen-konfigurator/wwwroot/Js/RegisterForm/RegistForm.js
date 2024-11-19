let returnUrl = document.getElementById("returnUrl");
function back() {
    returnUrl.value = localStorage.getItem('currentURL');
}
back()

let currentStep = 1;

// Функция обновления шагов
function updateSteps() {
    const steps = document.querySelectorAll('.step');
    const progressBar = document.getElementById('progress-bar');
    const progressValue = (currentStep - 1) * 25;

    // Обновляем шаги
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

    // Обновляем прогресс-бар
    progressBar.style.width = `${progressValue}%`;
    progressBar.setAttribute('aria-valuenow', progressValue);
    progressBar.innerHTML = `Schritt ${currentStep}`;
}

// Обработчик для кнопки Next
document.getElementById('nextStepBtn').addEventListener('click', () => {
    if (currentStep < 5) {
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