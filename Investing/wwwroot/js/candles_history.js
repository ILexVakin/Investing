async function getGlassOrders(event) {
    const preloader = document.getElementById('chart-preloader');
    const chartDiagram = document.getElementById('chart-instruments');

    const todayDate = new Date();
    const startDate = new Date();
    const buttonId = event.target.id;

    preloader.classList.remove('hidden');
    chartDiagram.style.display = 'none';

    const loadChartData = async () => {
        chartDiagram.width = 800;
        chartDiagram.height = 300;
        chartDiagram.style.width = '800px';
        chartDiagram.style.height = '300px';

        preloader.style.width = '800px';
        preloader.style.height = '300px';

        // Обработка дат
        switch (buttonId) {
            case 'btn_diagram_1w':
                startDate.setDate(startDate.getDate() - 7);
                break;
            case 'btn_diagram_1m':
                startDate.setDate(startDate.getDate() - 30);
                break;
            case 'btn_diagram_3m':
                startDate.setDate(startDate.getDate() - 90);
                break;
            case 'btn_diagram_6m':
                startDate.setDate(startDate.getDate() - 180);
                break;
            case 'btn_diagram_1y':
                startDate.setDate(startDate.getDate() - 365);
                break;
        }

        const data ={
            secId: allInstruments.security.secid,
            dateStart: startDate.toISOString().split('T')[0],
            dateOver: todayDate.toISOString().split('T')[0]
        };

        try {
            const response = await fetch('/Candles/GetHistoryCandlesMoreDays', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                throw new Error(`Ошибка с подгрузкой данных`);
            }

            const dataCandles = await response.json();

            preloader.classList.add('hidden');

            if (dataCandles && dataCandles.length > 0) {
                createGraphicksCandles(dataCandles);
                chartDiagram.style.display = 'block';
            } else {
                const newElement = document.createElement('p');
                newElement.textContent = 'Торгов за текущую дату не было';
                preloader.append(newElement);
            }
        } catch (error) {
            console.error('Ошибка загрузки данных:', error);
            preloader.classList.add('hidden');
        }
    };

    setTimeout(loadChartData, 50);
}

async function getGlassOrdersToday() {
    const preloader = document.getElementById('chart-preloader');
    const chartDiagram = document.getElementById('chart-instruments');

    const todayDate = new Date();
    const startDate = new Date();

    preloader.classList.remove('hidden');
    chartDiagram.style.display = 'none';

    const loadChartData = async () => {

        startDate.setDate(startDate.getDate() - 1);

        const data = {
            secId: allInstruments.security.secid,
            dateStart: startDate.toISOString().split('T')[0],
            dateOver: todayDate.toISOString().split('T')[0]
        };

        try {
            const response = await fetch('/Candles/GetHistoryCandlesByDay', {
                method: 'Post',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                throw new Error(`Ошибка с подгрузкой данных`);
            }

            const dataCandles = await response.json();

            preloader.classList.add('hidden');

            if (dataCandles && dataCandles.length > 0) {
                createGraphicksCandles(dataCandles);
                chartDiagram.style.display = 'block';
            } else {
                const newElement = document.createElement('p');
                newElement.textContent = 'Торгов за текущую дату не было';
                preloader.append(newElement);
            }
        } catch (error) {
            console.error('Ошибка загрузки данных:', error);
            preloader.classList.add('hidden');
        }
    };

    setTimeout(loadChartData, 50);
}

async function createGraphicksCandles(dataCandles) {

    var elementCanvas = document.getElementById("chart-instruments");
    var secId = document.getElementById("secid-name-p");
    if (elementCanvas.chart) {
        elementCanvas.chart.destroy();
    }

    const timeElement = [];
    const priceElement = [];
    for (let i = 0; i < dataCandles.length; i++) {
        timeElement.push(dataCandles[i].end);
        priceElement.push(dataCandles[i].close)
    }

    var instrumentData = {
        labels: timeElement,
        datasets: [{
            label: secId.innerText,
            data: priceElement,
            lineTension: 0,
            fill: false,
            borderColor: 'black',
            backgroundColor: 'transparent',
            borderDash: [5, 5],
            pointBorderColor: 'orange',
            pointRadius: 5,
            pointHoverRadius: 10,
            pointHitRadius: 30,
            pointBorderWidth: 2,
            pointStyle: 'rectRounded'
        }]
    };

    var chartOptions = {
        legend: {
            display: true,
            position: 'top',
            labels: {
                boxWidth: 80,
                fontColor: 'black'
            }
        }
    };

    var lineChart = new Chart(elementCanvas, {
        type: 'line',
        data: instrumentData,
        options: chartOptions
    });
}

document.addEventListener('DOMContentLoaded', getGlassOrdersToday);