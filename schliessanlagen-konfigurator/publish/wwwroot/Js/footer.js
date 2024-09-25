let title = document.getElementById("footer_item_title");
let discriptions = document.getElementById("FooterItemRenderDescriptions");
let modalInfo = document.querySelector("#FooterItemRender");
function system_name(name) {
    render(name);   
}

function render(name) {
    abusData.forEach((item) => {
        if (item.nameSysteam == name) {
            title.textContent = item.nameSysteam;
            discriptions.innerHTML = item.desctiptionsSysteam;
        }

    });

    cesSystem.forEach((item) => {
        if (item.nameSysteam == name) {
            title.textContent = item.nameSysteam;
            discriptions.innerHTML = item.desctiptionsSysteam;
        }

    });

    Basi.forEach((item) => {
        if (item.nameSysteam == name) {
            title.textContent = item.nameSysteam;
            discriptions.innerHTML = item.desctiptionsSysteam;
        }
    });

    EVVA.forEach((item) => {
        if (item.nameSysteam == name) {
            title.textContent = item.nameSysteam;
            discriptions.innerHTML = item.desctiptionsSysteam;
        }
    });
}