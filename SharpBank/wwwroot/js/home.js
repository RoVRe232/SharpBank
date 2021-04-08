//pie chart
var ctxP = document.getElementById("pieChart").getContext('2d');
var myPieChart = new Chart(ctxP, {
    type: 'pie',
    data: {
        labels: ["Account1", "Account2", "Account3", "Account4", "Account5"],
        datasets: [{
            data: [240, 50, 120, 40, 120],
            backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
            hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
        }]
    },
    options: {
        responsive: true
    }
});

//bar chart

var ctxP = document.getElementById("barChart").getContext('2d');
var myPieChart = new Chart(ctxP, {
    type: 'bar',
    data: {
        labels: ["January", "February", "March", "April", "May"],
        datasets: [{
            data: [70, 50, 35, 78, 195],
            backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
            hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
        }]
    },
    options: {
        responsive: true
    }
});
//console.log('done!')