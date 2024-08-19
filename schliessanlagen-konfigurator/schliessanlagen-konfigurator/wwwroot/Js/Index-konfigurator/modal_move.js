
function next()
{
    let modalcontent = document.getElementsByClassName("modal-content")[0];
    let modalArrow = document.getElementsByClassName("arrow-down")[0];

    if (circlestep < 6) {
        circle2[circlestep].style.background = "white";
        circlestep++;
        circle2[circlestep].style.background = "red";

        switch (circlestep) {
            case 0:
                {
                    hilfeText.innerText = "Hier den Namen der Türen eingeben.";
                    hilfeText.innerHTML = "Hier den Namen der Türen eingeben.";
                    modalcontent.style.marginLeft = "300px";
                    modalArrow.style.marginLeft = "400px";
                    modalcontent.style.marginTop = "380px";
                    break;
                }
            case 1:
                {
                    hilfeText.innerText = "Wählen Sie den Zylindertyp aus.";
                    hilfeText.innerHTML = "Wählen Sie den Zylindertyp aus.";
                    modalcontent.style.marginTop = "380px";
                    modalcontent.style.marginLeft = "600px";
                    modalArrow.style.marginLeft = "700px";
                    break;
                }
            case 2:
                {
                    hilfeText.innerText = "Wählen Sie die Außenlänge des Zylinders aus.";
                    hilfeText.innerHTML = "Wählen Sie die Außenlänge des Zylinders aus.";
                    modalcontent.style.marginTop = "340px";
                    modalcontent.style.marginLeft = "700px";
                    modalArrow.style.marginLeft = "850px";
                    break;
                }
            case 3:
                {
                    hilfeText.innerText = "Wählen Sie die Innenlänge des Zylinders aus.";
                    hilfeText.innerHTML = "Wählen Sie die Innenlänge des Zylinders aus.";
                    modalcontent.style.marginTop = "340px";
                    modalcontent.style.marginLeft = "700px";
                    modalArrow.style.marginLeft = "890px";
                    break;
                }
            case 4:
                {
                    hilfeText.innerText = "Geben Sie die Anzahl der Schlösser ein.";
                    hilfeText.innerHTML = "Geben Sie die Anzahl der Schlösser ein.";
                    modalcontent.style.marginTop = "380px";
                    modalcontent.style.marginLeft = "800px";
                    modalArrow.style.marginLeft = "1010px";
                    break;
                }
            case 5:
                {
                    hilfeText.innerText = "Kreuzen Sie an, welche Schlösser der Schlüssel schließen soll.";
                    hilfeText.innerHTML = "Kreuzen Sie an, welche Schlösser der Schlüssel schließen soll.";
                    modalcontent.style.marginTop = "350px";
                    modalcontent.style.marginLeft = "950px";
                    modalArrow.style.marginLeft = "1130px";
                    break;
                }
            case 6:
                {
                    hilfeText.innerText = "Geben Sie die Anzahl der Schlüssel ein.";
                    hilfeText.innerHTML = "Geben Sie die Anzahl der Schlüssel ein.";
                    modalcontent.style.marginTop = "230px";
                    modalcontent.style.marginLeft = "950px";
                    modalArrow.style.marginLeft = "1130px";
                    break;
                }


        }
    }
}
function back()
{
    let modalcontent = document.getElementsByClassName("modal-content")[0];
    let modalArrow = document.getElementsByClassName("arrow-down")[0];
    if (circlestep > 0) {
        circle2[circlestep].style.background = "white";
        circlestep--;
        circle2[circlestep].style.background = "red";

        switch (circlestep) {
            case 0:
                {
                    hilfeText.innerText = "Hier den Namen der Türen eingeben.";
                    hilfeText.innerHTML = "Hier den Namen der Türen eingeben.";
                    modalcontent.style.marginLeft = "300px";
                    modalArrow.style.marginLeft = "400px";
                    modalcontent.style.marginTop = "380px";
                    break;
                }
            case 1:
                {
                    hilfeText.innerText = "Wählen Sie den Zylindertyp aus.";
                    hilfeText.innerHTML = "Wählen Sie den Zylindertyp aus.";
                    modalcontent.style.marginTop = "380px";
                    modalcontent.style.marginLeft = "600px";
                    modalArrow.style.marginLeft = "700px";
                    break;
                }
            case 2:
                {
                    hilfeText.innerText = "Wählen Sie die Außenlänge des Zylinders aus.";
                    hilfeText.innerHTML = "Wählen Sie die Außenlänge des Zylinders aus.";
                    modalcontent.style.marginTop = "340px";
                    modalcontent.style.marginLeft = "700px";
                    modalArrow.style.marginLeft = "850px";
                    break;
                }
            case 3:
                {
                    hilfeText.innerText = "Wählen Sie die Innenlänge des Zylinders aus.";
                    hilfeText.innerHTML = "Wählen Sie die Innenlänge des Zylinders aus.";
                    modalcontent.style.marginTop = "340px";
                    modalcontent.style.marginLeft = "700px";
                    modalArrow.style.marginLeft = "890px";
                    break;
                }
            case 4:
                {
                    hilfeText.innerText = "Geben Sie die Anzahl der Schlösser ein.";
                    hilfeText.innerHTML = "Geben Sie die Anzahl der Schlösser ein.";
                    modalcontent.style.marginTop = "380px";
                    modalcontent.style.marginLeft = "800px";
                    modalArrow.style.marginLeft = "1010px";
                    break;
                }
            case 5:
                {
                    hilfeText.innerText = "Kreuzen Sie an, welche Schlösser der Schlüssel schließen soll.";
                    hilfeText.innerHTML = "Kreuzen Sie an, welche Schlösser der Schlüssel schließen soll.";
                    modalcontent.style.marginTop = "350px";
                    modalcontent.style.marginLeft = "950px";
                    modalArrow.style.marginLeft = "1130px";
                    break;
                }
            case 6:
                {
                    hilfeText.innerText = "Geben Sie die Anzahl der Schlüssel ein.";
                    hilfeText.innerHTML = "Geben Sie die Anzahl der Schlüssel ein.";
                    modalcontent.style.marginTop = "230px";
                    modalcontent.style.marginLeft = "950px";
                    modalArrow.style.marginLeft = "1130px";
                    break;
                }
        }
    }
}