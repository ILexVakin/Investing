document.getElementById('btn_diagram_1d').addEventListener('click', function (event) {
    getGlassOrders(event);
});
document.getElementById('btn_diagram_1w').addEventListener('click', function (event) {
    getGlassOrders(event);
});
document.getElementById('btn_diagram_1m').addEventListener('click', function (event) {
    getGlassOrders(event);
});
document.getElementById('btn_diagram_3m').addEventListener('click', function (event) {
    getGlassOrders(event);
});
document.getElementById('btn_diagram_6m').addEventListener('click', function (event) {
    getGlassOrders(event);
});
document.getElementById('btn_diagram_1y').addEventListener('click', function (event) {
    getGlassOrders(event);
});

async function getGlassOrders(event) {
    const currentDate = new Date();

    const buttonId = event.target.id;

    nameButtonClick = "";

    let todayDate = currentDate.getTime();
    let periodDate = new Date(todayDate.getTime());

    let dateSearch = "";
    switch (nameButtonClick)
    {
        case 'btn_diagram_1d':
            dateSearch = periodDate.setDate(todayDate.getDate() - 1);
            break;
        case 'btn_diagram_1w':
            dateSearch = periodDate.setDate(todayDate.getDate() - 7);
            break;
        case 'btn_diagram_1m':
            dateSearch = periodDate.setDate(todayDate.getDate() - 7);
            break;
    }

    await fetch('/CandlesController/GetHistory', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data)
    });

    

}

async function getGlassOrdersDefault(){
    //Здесь мы должны будем получить дефолтное значение для графика - текущий день.
    //если за сегодня не было графика и это не ошибка, тогда нужно вывести "сегодня торгов не было"
}