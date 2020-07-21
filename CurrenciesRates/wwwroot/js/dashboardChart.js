

const xLabels = [];
const yRates = [];
const label = 'kurs ' + pro.currentRate.currency;
chartIt(pro.rangeCurrencyRate);

async function chartIt(pro) {
    await getData(pro);
    const ctx = document.getElementById('chart').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: xLabels,
            datasets: [{
                label: label,
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

async function getData(data) {

    data.forEach(x => {
        yRates.push(x.mid);
        xLabels.push(x.effectiveDate.slice(5, x.lenght).replace('-','.'));
    });
    console.log(data);
}

//document.getElementById('currentMonth').addEventListener('click', function () {
//    $.ajax({
//        type: "POST",
//        url: ''
//        contentType: "application/json; charset=utf-8",

//        beforeSend: function (xhr) {
//            xhr.setRequestHeader("XSRF-TOKEN",
//                $('input:hidden[name="__RequestVerificationToken"]').val());
//        },

//        dataType: "json"
//    }).done(function (data) {
//        console.log(data.result);
//    })

//    const response = await fetch(api_url)
//    const data = await response.json();
//    data.rates.forEach(x => {
//        yRates.push(x.mid);
//        xLabels.push(x.effectiveDate.slice(5, x.lenght).replace('-', '.'));
//    });
//    console.log(data);

//    window.myLine.update();
//});


