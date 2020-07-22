

var xLabels = [];
var yRates = [];
const months = ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"];
var label = 'kurs ' + pro.currentRate.currency;
var myChart;
chartIt(pro.rangeCurrencyRate);

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
        //beforeSend: function (xhr) {
        //    xhr.setRequestHeader("XSRF-TOKEN",
        //        $('input:hidden[name="__RequestVerificationToken"]').val());
        //},
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
    $.ajax({
        type: "Get",
        url: 'GenerateExcelFile',
        contentType: "application/json; charset=utf-8",
        data: { labels: myChart.data.labels, rate: myChart.data.datasets[0].data },
        traditional: true,
        //beforeSend: function (xhr) {
        //    xhr.setRequestHeader("XSRF-TOKEN",
        //        $('input:hidden[name="__RequestVerificationToken"]').val());
        //},
        success: function (Rdata) {
            debugger;
            var bytes = new Uint8Array(Rdata.FileContents);
            var blob = new Blob([bytes], { type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" });
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = "myFileName.xlsx";
            link.click();
        },
        error: function (msg) {
            console.log(msg);
        },
    });
}
