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
document.addEventListener('DOMContentLoaded', calculationDifference);