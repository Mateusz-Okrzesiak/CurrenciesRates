
const api_url = 'https://api.nbp.pl/api/exchangerates/rates/a/EUR/2020-01-01/2020-02-01/?format=json';
const xLabels = [];
const yRates = [];
chartIt();

async function chartIt() {
    await getData();
    const ctx = document.getElementById('chart').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: xLabels,
            datasets: [{
                label: 'Kurs Euro',
                data: yRates,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)'
                ],
                borderWidth: 1,
                lineTension: 0
            }]
        },
    });
}

async function getData() {

    const response = await fetch(api_url)
    const data = await response.json();
    data.rates.forEach(x => {
        yRates.push(x.mid);
        xLabels.push(x.effectiveDate.slice(5, x.lenght).replace('-','.'));
    });
    console.log(data);
}

