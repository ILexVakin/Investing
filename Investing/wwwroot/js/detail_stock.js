async function calculationDifference() {
    const yesterdayPrice = parseFloat(allInstruments.security.prevlegalcloseprice);
    const todayPrice = parseFloat(allInstruments.marketdata.offer);


    let differenceTodayNum = todayPrice - yesterdayPrice;
    let differenceTodayPersent = todayPrice / yesterdayPrice;

    let elementNum = document.getElementById('change-price-num');
    let elementPersent = document.getElementById('change-price-persent');

    if (differenceTodayNum > 0.001) {
        elementNum.style.color = 'green';
        elementPersent.style.color = 'green';
    }
    else if (differenceTodayNum < -0.001) {
        elementNum.style.color = 'red';
        elementPersent.style.color = 'red';
    } else {
        elementNum.style.color = 'black';
        elementPersent.style.color = 'black';
    }
    elementNum.textContent = differenceTodayNum.toFixed(2) + " Р";
    elementPersent.textContent = differenceTodayPersent.toFixed(2) + " %";

}

function openTab(evt, tabName) {
    // Скрыть все вкладки
    var tabcontent = document.getElementsByClassName("tabcontent");
    for (var i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Убрать активный класс у всех кнопок
    var tablinks = document.getElementsByClassName("tablinks");
    for (var i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Показать текущую вкладку и добавить активный класс кнопке
    document.getElementById(tabName).style.display = "block";
    evt.currentTarget.className += " active";
}

document.addEventListener('DOMContentLoaded', calculationDifference);