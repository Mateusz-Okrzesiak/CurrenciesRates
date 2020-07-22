
var xLabels = [];
var yRates = [];
const months = ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"];
var label = 'kurs ' + model.currentRate.currency;
var myChart;

chartIt(model.rangeCurrencyRate);

async function chartIt(pro) {
    await getData(pro);
    const ctx = document.getElementById('chart').getContext('2d');
    myChart = new Chart(ctx, {
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
        xLabels.push(x.effectiveDate.slice(5, 10).replace('-','.'));
    });
    console.log(data);
}
function monthsAVG(currencyCode) {
        $.ajax({
        type: "Get",
            url: 'MonthsAVG',
        contentType: "application/json; charset=utf-8",
            data: { currencyCode: currencyCode },
        dataType: "json"
        }).done(function (data) {
            myChart.data.datasets[0].data = [];
            myChart.data.labels = months.slice(0, data.length);
           
            data.forEach(x => {
                    myChart.data.datasets[0].data.push(x);
            });
            myChart.update();
    })
}

function generateExcel() {
        
    //xhttp = new XMLHttpRequest();
    //xhttp.onreadystatechange = function () {
    //    var a;
    //    if (xhttp.readyState === 4 && xhttp.status === 200) {
    //        a = document.createElement('a');
    //        a.href = window.URL.createObjectURL(xhttp.response);
    //        a.download = filename;
    //        a.style.display = 'none';
    //        document.body.appendChild(a);
    //        a.click();
    //    }
    //};
    //xhttp.open("POST", 'GenerateExcelFile');
    //xhttp.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    //xhttp.responseType = 'blob';
    //xhttp.send(
    //    'labels+'+myChart.data.labels+'&rate='+myChart.data.datasets[0].data);


    $.ajax({
        type: "Get",
        url: 'GenerateExcelFile',
        contentType: "application/json; charset=utf-8",
        data: { labels: myChart.data.labels, rate: myChart.data.datasets[0].data },
        traditional: true,
        success: function (Rdata) {
            var blob = new Blob([Rdata], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = "Kurs_walut.xlsx";
            link.click();
        },
        error: function (msg) {
            console.log(msg);
        },
    });
}
