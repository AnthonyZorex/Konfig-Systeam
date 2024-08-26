function translatePage() {
    var textToTranslate = document.getElementById("textToTranslate").value;
    var targetLanguage = "French";

    fetch("/Schop/Translate", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ textToTranslate: textToTranslate, targetLanguage: targetLanguage })
    })
        .then(response => response.json())
        .then(data => {
            document.getElementById("translatedText").value = data.TranslatedText;
        })
        .catch(error => console.error('Error:', error));
}

function select_zylinder(herschteller_name)
{
    let bis = document.getElementById("bis");
    let von = document.getElementById("von");
    let search_value = document.getElementById("suchen_Item").value;

    if (search_value != null)
    {
        if (bis.value == "" && von.value == "")
        {
            fetch("/Schop/Index",
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ page: 1, herschteller: herschteller_name, priceBis: null, priceVon: null, Sort_string: search_value  })
                })
                .then(response => {
                    if (response.redirected) {

                        window.location.href = response.url;
                    }
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                });
        }
        else {

            fetch("/Schop/Index",
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ page: 1, herschteller: herschteller_name, priceBis: bis.value, priceVon: von.value, Sort_string: search_value })
                })
                .then(response => {
                    if (response.redirected) {

                        window.location.href = response.url;
                    }
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                });
        }
    }
    else
    {
        if (bis.value == "" && von.value == "") {
            fetch("/Schop/Index",
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ page: 1, herschteller: herschteller_name, priceBis: null, priceVon: null, Sort_string:null })
                })
                .then(response => {
                    if (response.redirected) {

                        window.location.href = response.url;
                    }
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                });
        }
        else {

            fetch("/Schop/Index",
                {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ page: 1, herschteller: herschteller_name, priceBis: bis.value, priceVon: von.value, Sort_string: null })
                })
                .then(response => {
                    if (response.redirected) {
                        window.location.href = response.url;
                    }
                })
                .catch(error => {
                    console.error('Fetch error:', error);
                });
        }
    }
    
    
    
}
function cleanSort()
{
    fetch("/Schop/Index",
    {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ page: 1, herschteller: null, priceBis: null, priceVon: null, Sort_string: null })
    })
        .then(response => {
            if (response.redirected) {

                window.location.href = response.url;
            }
        })
        .catch(error => {
            console.error('Fetch error:', error);
        });
}
