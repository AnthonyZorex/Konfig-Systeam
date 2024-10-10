function addOptionsValue(id)
{
    let optionsBlock = document.getElementById("block-" + id);

    let value = document.createElement("input");
    value.setAttribute('name', 'valueNGF');
    value.setAttribute('placeholder', 'Wert');
    value.setAttribute('class', 'form-control');
    value.setAttribute('type', 'text');
    value.required = true;

    let Price = document.createElement("input");
    Price.setAttribute('name', 'costNGF');
    Price.setAttribute('placeholder', 'Price');
    Price.setAttribute('class', 'form-control');
    Price.setAttribute('type', 'text');
    Price.required = true;

    let valueBlock = optionsBlock.querySelector("#valueNGF");
    let priceBlock = optionsBlock.querySelector("#costNGF");

    valueBlock.appendChild(value);
    priceBlock.appendChild(Price);

    var counter = optionsBlock.querySelector('#counter');
    let x = valueBlock.querySelectorAll("input").length;
    counter.value = x;
}
function minusOptionsValue(id) {
    let optionsBlock = document.getElementById("block-" + id);
    let inputProductValue = optionsBlock.querySelectorAll("#valueNGF  input");
    let inputProductPrice = optionsBlock.querySelectorAll("#costNGF  input");

    let valueBlock = optionsBlock.querySelector("#valueNGF");
    let priceBlock = optionsBlock.querySelector("#costNGF");

    let value = valueBlock.childNodes;
    let price = priceBlock.childNodes;

    let lastValueChild = valueBlock.lastChild; 
    lastValueChild.remove();

    let lastPriceChild = priceBlock.lastChild;
    lastPriceChild.remove();

    var counter = optionsBlock.querySelector('#counter');
    let x = valueBlock.querySelectorAll("input").length;
    counter.value = x;
}

let countKleinBlock = 1;

function newKlein() {

    let Aussen_innen_klein = document.getElementById("Aussen_innen_klein");

    countKleinBlock++;

    let block = ` <div id="kleinGrose" class="kleinZise-${countKleinBlock}">
                                        <div>
                                            <div>
                                                <h5>Aussen</h5>
                                            <input type="number" class="form-control" required value="" name="ausKlein" />
                                            </div>
                                            <div>
                                                <h5>Preis</h5>
                                            <input type="number" class="form-control" required step="0,00" value="" name="ausKleinPreis" />
                                            </div>
                                        </div>
                                        <div class="arrow-right">
                                        </div>
                                        <div>
                                            <table class="table">
                                                <thead>
                                                    <tr>
                                                        <th>Wert</th>
                                                        <th>Preis</th>
                                                             <th><button type="button" onclick="PlusKleinIntert(${countKleinBlock})" class="btn btn-success">+</button><button type="button" class="btn danger" onclick="MinusKleinIntern(${countKleinBlock})">-</button></th>
                                                    </tr>
                                                </thead>
                                                <tbody id="kleiIntern">


                                                            <tr>
                                                            <th><input name="internDoppelKlein" required class="form-control" value="" type="number" step="0,01" /></th>
                                                            <td><input name="priesDoppelKlein" required class="form-control" type="number" value="" step="0,01" /></td>
                                                            </tr>         
                                                </tbody>
                                            </table>
                                                        <input type="hidden" name="KleinZiseCount"  />
                                        </div>
                                    </div>`;

    Aussen_innen_klein.insertAdjacentHTML('beforeend', block) ;

    let blockItem = document.querySelector(`.kleinZise-${countKleinBlock}`);

    let tableSearch = blockItem.childNodes[5].childNodes[1].childNodes[3];

    let count = tableSearch.querySelectorAll('input[type="number"]').length;

    let countValue = blockItem.childNodes[5].childNodes[3];

    countValue.value = count / 2;
}

function goBack() {
    if (document.referrer !== "") {
        window.location.href = document.referrer;  // Возвращаемся на предыдущую страницу
    } else {
        window.history.back();  // Если реферера нет, используем history.back()
    }
}

function showAlert(message) {
    const alertBox = document.getElementById('alertBox');
    const alertMessage = document.getElementById('alertMessage');

    alertMessage.innerText = message;  // Установка сообщения
    alertBox.classList.remove('d-none');  // Удалить класс d-none
    alertBox.classList.add('show'); // Добавить класс show для отображения
}


function closeAlert() {
    const alertBox = document.getElementById('alertBox');
    alertBox.classList.remove('show');  // Удалить класс show
    alertBox.classList.add('d-none');  // Добавить класс d-none для скрытия
}

document.getElementById('submitForm').addEventListener('click', function (event) {

    event.preventDefault();
    tinymce.triggerSave();

    let isValid = true;
    $('input[required], select[required], textarea[required]').each(function () {
        if ($(this).val() === '') {
            isValid = false;
            $(this).addClass('error'); // добавляем класс для визуального отображения ошибки
        } else {
            $(this).removeClass('error');
        }
    });

    var form = document.getElementById('systemInfoForm');

    var formData = new FormData(form);

    if (isValid) {

        if (TypeZylinder == 1) {
            axios.post('/Home/SaveDoppelZylinder', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(function (response) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-danger');
                    alertBox.classList.add('alert-success');
                    showAlert(response.data.message);

                })
                .catch(function (error) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-success');
                    alertBox.classList.add('alert-danger');
                    showAlert(error);
                });
        }
        else if (TypeZylinder == 2) {
            axios.post('/Home/SaveHalbzylinder', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(function (response) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-danger');
                    alertBox.classList.add('alert-success');
                    showAlert(response.data.message);

                })
                .catch(function (error) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-success');
                    alertBox.classList.add('alert-danger');
                    showAlert(error);
                });
        }
        else if (TypeZylinder == 3) {
            axios.post('/Home/SaveProfil_Knaufzylinder', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(function (response) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-danger');
                    alertBox.classList.add('alert-success');
                    showAlert(response.data.message);

                })
                .catch(function (error) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-success');
                    alertBox.classList.add('alert-danger');
                    showAlert(error);
                });
        }
        else if (TypeZylinder == 4) {

            axios.post('/Home/SaveHebelzylinder', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(function (response) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-danger');
                    alertBox.classList.add('alert-success');
                    showAlert(response.data.message);

                })
                .catch(function (error) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-success');
                    alertBox.classList.add('alert-danger');
                    showAlert(error);
                });
        }
        else if (TypeZylinder == 5) {
            axios.post('/Home/SaveVorhangschloss', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(function (response) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-danger');
                    alertBox.classList.add('alert-success');
                    showAlert(response.data.message);

                })
                .catch(function (error) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-success');
                    alertBox.classList.add('alert-danger');
                    showAlert(error);
                });
        }

        else if (TypeZylinder == 6) {
            axios.post('/Home/SaveAussenzylinder_Rundzylinder', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data'
                }
            })
                .then(function (response) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-danger');
                    alertBox.classList.add('alert-success');
                    showAlert(response.data.message);

                })
                .catch(function (error) {

                    const alertBox = document.getElementById('alertBox');
                    alertBox.classList.remove('alert-success');
                    alertBox.classList.add('alert-danger');
                    showAlert(error);
                });
        }
    }
    else {

        const alertBox = document.getElementById('alertBox');
        alertBox.classList.remove('alert-success');
        alertBox.classList.add('alert-danger');
        showAlert("Заполнены не все поля!");
    }
});


function PlusKleinIntert(blockNumber) {

    let block = document.querySelector(`.kleinZise-${blockNumber}`);
    let tableSearch = block.childNodes[5].childNodes[1].childNodes[3];

    let row = `   <tr>
                                        <th><input name="internDoppelKlein" required class="form-control" type="number" step="0,01" /></th>
                                           <td><input class="form-control" required name="priesDoppelKlein" step="0,01" type="number" /></td>
                                </tr> `;

    tableSearch.insertAdjacentHTML('beforeend', row);

    let count = tableSearch.querySelectorAll('input[type="number"]').length;

    let countValue = block.childNodes[5].childNodes[3];

    countValue.value = count / 2;
}

function MinusKleinIntern(blockNumber) {
    let block = document.querySelector(`.kleinZise-${blockNumber}`);

    let tableSearch = block.childNodes[5].childNodes[1].childNodes[3];

    let countelem = tableSearch.querySelectorAll("tr");

    if (countelem.length == 0) {
        block.remove();
    }
    else {
        countelem[countelem.length - 1].remove();
    }


    let count = tableSearch.querySelectorAll('input[type="number"]').length;

    let countValue = block.childNodes[5].childNodes[3];

    countValue.value = count / 2;
}

let Galery = document.getElementById("Galerry");

function AddImage(imageBlock) {
    let addBlock = `<div>
                                        <label for="Images">Bilder auswählen:</label>
                                            <input type="file" name="UploadGalleryImages" multiple class="form-control" />
                                    </div>`;

    document.getElementById('Galerry').innerHTML += addBlock;
}

function deleteImage(imageBlock) {
    let imgBlock = document.getElementById("galeryItem-" + imageBlock);

    if (Galery.childElementCount == 3) {
        imgBlock.remove();
        Galery.remove();
    }
    else {
        imgBlock.remove();
    }
}

function deleteOption(id) {
    let optionBlock = document.getElementById("block-" + id);

    optionBlock.remove();
}

tinymce.init({
    selector: '#discriptions',
    plugins: [
        'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
        'searchreplace', 'wordcount', 'visualblocks', 'visualchars', 'code', 'fullscreen', 'insertdatetime',
        'media', 'table', 'emoticons', 'help'
    ],
    toolbar: 'undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | ' +
        'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
        'forecolor backcolor emoticons | help',
    menu: {
        favs: { title: 'Discriptions', items: 'code visualaid | searchreplace | emoticons' }
    },
    menubar: 'favs file edit view insert format tools table help',
    content_css: 'css/content.css'
});

tinymce.init({
    selector: '#exampleFormControlTextarea1',
    plugins: [
        'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
        'searchreplace', 'wordcount', 'visualblocks', 'visualchars', 'code', 'fullscreen', 'insertdatetime',
        'media', 'table', 'emoticons', 'help'
    ],
    toolbar: 'undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | ' +
        'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
        'forecolor backcolor emoticons | help',
    menu: {
        favs: { title: 'Optionen', items: 'code visualaid | searchreplace | emoticons' }
    },
    menubar: 'favs file edit view insert format tools table help',
    content_css: 'css/content.css'
});

tinymce.init({
    selector: '#exampleFormControlTextarea2',
    plugins: [
        'advlist', 'autolink', 'link', 'image', 'lists', 'charmap', 'preview', 'anchor', 'pagebreak',
        'searchreplace', 'wordcount', 'visualblocks', 'visualchars', 'code', 'fullscreen', 'insertdatetime',
        'media', 'table', 'emoticons', 'help'
    ],
    toolbar: 'undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | ' +
        'bullist numlist outdent indent | link image | print preview media fullscreen | ' +
        'forecolor backcolor emoticons | help',
    menu: {
        favs: { title: 'Lieferzeit', items: 'code visualaid | searchreplace | emoticons' }
    },
    menubar: 'favs file edit view insert format tools table help',
    content_css: 'css/content.css'
});

let plusAussen_Innen = document.getElementById('addAussen_Innen');
let removeAussen_Innen = document.getElementById('addAussen_Innen_remove');

let docInnen = document.getElementById('InnenProfilDopel');
let docAussen = document.getElementById('AussenProfilDopel');

let selectedInputs = [];

let allInnen = document.querySelectorAll("#inter");
let allAussen = document.querySelectorAll("#aus");

let aussenCost = document.querySelectorAll("#ausCost");
let InternCost = document.querySelectorAll("#interCost");

allAussen.forEach((item) => {

    item.addEventListener("keydown", (event) => {
        if (event.key === 'Control') {
            selectedInputs.push(item);
        }
    });


    item.addEventListener('change', (event) => {
        selectedInputs.forEach((x) => {
            x.value = event.target.value;
        });
        selectedInputs = [];
    });

});


aussenCost.forEach((item) => {

    item.addEventListener("keydown", (event) => {
        item.addEventListener("click", () => {
            selectedInputs.push(item);
        })
    });


    item.addEventListener('change', (event) => {

        selectedInputs.forEach((x) => {
            x.value = event.target.value;
        });
        selectedInputs = [];
    });

});


allInnen.forEach((item) => {

    item.addEventListener("keydown", (event) => {
        if (event.key === 'Control') {
            selectedInputs.push(item);
        }
    });


    item.addEventListener('change', (event) => {
        selectedInputs.forEach((x) => {
            x.value = event.target.value;
        });
        selectedInputs = [];
    });

});


InternCost.forEach((item) => {

    item.addEventListener("keydown", (event) => {
        item.addEventListener("click", () => {
            selectedInputs.push(item);
        })
    });


    item.addEventListener('change', (event) => {

        selectedInputs.forEach((x) => {
            x.value = event.target.value;
        });
        selectedInputs = [];
    });

});



window.addEventListener('load', function () {
    const blocks = document.querySelectorAll('.input-block');
    for (let i = 0; i < blocks.length; i++) {
        let counterBlock = blocks[i].querySelectorAll("#valueNGF  input");

        let counter = blocks[i].querySelectorAll("#counter");

        counter[0].value = counterBlock.length;

    }
});


let costNGF = document.getElementById('costNGF');
let valueNGF = document.getElementById('valueNGF');

let productOptions = document.getElementById('productOptions');
let addOptions = document.getElementById('addOptions');

if (plusAussen_Innen != null)
{
    plusAussen_Innen.addEventListener('click', () => {

        if (docAussen != null)
        {
            let d = document.createElement('input');
            d.setAttribute('name', 'SizeAus');
            d.setAttribute('placeholder', 'Aussen');
            d.setAttribute('class', 'form-control');
            d.id = 'aus';
            d.required = true;

            let costAussen = document.createElement('input');
            costAussen.setAttribute('name', 'costSizeAussen');
            costAussen.setAttribute('placeholder', 'costAussen');
            costAussen.setAttribute('class', 'form-control');
            costAussen.setAttribute('step', '0.01');
            costAussen.setAttribute('type', 'number');
            costAussen.id = 'ausCost';
            costAussen.required = true;
            docAussen.append(d, costAussen);
        }


        if (docInnen != null)
        {
            let x = document.createElement('input');
            x.setAttribute('name', 'SizeInen');
            x.setAttribute('placeholder', 'Innen');
            x.id = 'inter';
            x.setAttribute('class', 'form-control');
            x.required = true;

            let costInter = document.createElement('input');
            costInter.setAttribute('name', 'costSizeIntern');
            costInter.setAttribute('placeholder', 'costInnen');
            costInter.id = 'interCost';
            costInter.setAttribute('class', 'form-control');
            costInter.setAttribute('step', '0.01');
            costInter.setAttribute('type', 'number');
            costInter.required = true;

            docInnen.append(x, costInter);
        }


    });
}


if (removeAussen_Innen != null)
{
    removeAussen_Innen.addEventListener('click', () => {

        let d = document.querySelectorAll('#aus');

        let costAussen = document.querySelectorAll('#ausCost');

        let x = document.querySelectorAll('#inter');
        let costInter = document.querySelectorAll('#interCost');

        docAussen.removeChild(costAussen[costAussen.length - 1]);
        docAussen.removeChild(d[d.length - 1]);

        docInnen.removeChild(x[x.length - 1]);
        docInnen.removeChild(costInter[costInter.length - 1]);
    });
}
document.addEventListener("DOMContentLoaded", function () {
    const container = document.getElementById('productOptions');
    const createBlockBtn = document.getElementById('createBlockBtn');
    const addInputBtn = document.getElementById('addInputBtn');
    const RemoveInputBtn = document.getElementById('RemoveInputBtn');

    let blockCounter = document.querySelectorAll('.input-block').length > 0 ? document.querySelectorAll('.input-block').length : 1;

    createBlockBtn.addEventListener('click', function () {
        const block = createBlock();
        container.appendChild(block);
    });

    addInputBtn.addEventListener('click', function () {
        const blocks = document.querySelectorAll('.input-block');
        const lastBlock = blocks[blocks.length - 1];

        if (lastBlock) {
            addInputs(lastBlock);
        } else {
            alert('Сначала создайте блок');
        }
    });

    RemoveInputBtn.addEventListener('click', function () {
        const blocks = document.querySelectorAll('.input-block');
        const lastBlock = blocks[blocks.length - 1];

        let inputProductValue = lastBlock.querySelectorAll("#valueNGF  input");
        let inputProductPrice = lastBlock.querySelectorAll("#costNGF  input");

        if (lastBlock) {
            if (inputProductValue.length < 2) {
                productOptions.removeChild(lastBlock);
            }
            else {
                inputProductValue[inputProductValue.length - 1].parentNode.removeChild(inputProductValue[inputProductValue.length - 1]);
                inputProductPrice[inputProductPrice.length - 1].parentNode.removeChild(inputProductPrice[inputProductPrice.length - 1]);
            }
        }
        else {
            alert('Сначала создайте блок');
        }
    });

    function createBlock() {
        const block = document.createElement('div');
        block.classList.add(`input-block`);
        block.id = `block-${blockCounter}`;
        block.innerHTML = `<div>
                                                                                                <h1>Options</h1>
                                                                                                <label>Name</label>
                                                                                                <input class="form-control" required type="text" name="Options" />
                                                                                                <div>
                                                                                                    <p></p>
                                                                                                    <input name="postedFile" required type="file" class="btn btn-success" />
                                                                                                    <p></p>
                                                                                                </div>
                                                                                                <label>Descriptions</label>
                                                                                                <input class="form-control" type="text" required name="Descriptions" />
                                                                                                <h1>Value & Cost </h1>
                                                                                                <div id="Aussen_Innen_ProfilDopel">
                                                                                                    <div id="valueNGF">
                                                                                                                    <input type="text" class="form-control" required placeholder="valueNGF" name="valueNGF" />

                                                                                                    </div>
                                                                                                    <div id="costNGF">
                                                                                                         <input type="number" class="form-control" required placeholder="costNGF" step="0.01" name="costNGF" />
                                                                                                    </div>
                                                                                                       <input type="hidden" id="counter" name="inputCounter" />
                                                                                                </div>
                                                                                            </div>
                                                                                                         <hr/>
                                                                                                          <br />
                                                                                                                       <button class="btn btn-danger" onclick="deleteOption(${blockCounter})" 	type="button">Löschen</button>
                                                                                                          <hr />
                                                                                                          <br />

                                                                                            `;

        blockCounter++;

        return block;
    }

    function addInputs(block) {

        let d = document.createElement('input');
        d.setAttribute('name', 'costNGF');
        d.setAttribute('placeholder', 'costNGF');
        d.setAttribute('class', 'form-control');

        let x = document.createElement('input');
        x.setAttribute('name', 'valueNGF');
        x.setAttribute('placeholder', 'valueNGF');
        x.setAttribute('class', 'form-control');
        const div1 = block.querySelector('#valueNGF');
        const div2 = block.querySelector('#costNGF');

        div1.appendChild(x);

        div2.appendChild(d);

        var counter = document.querySelectorAll('#counter');
        counter[counter.length - 1].value = block.querySelectorAll("#valueNGF  input").length;
    };

})
