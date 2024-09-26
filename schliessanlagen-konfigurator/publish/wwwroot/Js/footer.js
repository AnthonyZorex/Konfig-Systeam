let title = document.getElementById("footer_item_title");
let discriptions = document.getElementById("FooterItemRenderDescriptions");
let modalInfo = document.querySelector("#FooterItemRender");
function system_name(name,id) {
    render(name,id);   
}

function render(name, id)
{
    fetch('/api/System/GetData', {  
        method: 'POST',                  
        headers: {
            'Content-Type': 'application/json'  
        },
        body: JSON.stringify({ id: Number(id) })        
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Ошибка сети');
            }
            return response.json();
        })
        .then(data => {
            title.textContent = data.nameSysteam;
            discriptions.innerHTML = data.desctiptionsSysteam;
        })
        
}