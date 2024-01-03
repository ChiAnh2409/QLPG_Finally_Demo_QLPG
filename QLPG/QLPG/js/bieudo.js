// ---------- CHARTS ----------

// BAR CHART
const barChartOptions = {
    series: [
        {
            data: [],
            name: 'Doanh thu',
        },
    ],
    chart: {
        type: 'bar',
        background: 'transparent',
        height: 350,
        toolbar: {
            show: false,
        },
    },
    colors: ['#2962ff'],
    plotOptions: {
        bar: {
            distributed: true,
            borderRadius: 4,
            horizontal: false,
            columnWidth: '40%',
        },
    },
    dataLabels: {
        enabled: false,
    },
    fill: {
        opacity: 1,
    },
    grid: {
        borderColor: '#55596e',
        yaxis: {
            lines: {
                show: true,
            },
        },
        xaxis: {
            lines: {
                show: true,
            },
        },
    },
    legend: {
        labels: {
            colors: '#f5f7ff',
        },
        show: true,
        position: 'top',
    },
    stroke: {
        colors: ['transparent'],
        show: true,
        width: 2,
    },
    tooltip: {
        shared: true,
        intersect: false,
        theme: 'dark',
    },
    xaxis: {
        categories: ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
        title: {
            style: {
                color: '#f5f7ff',
            },
        },
        axisBorder: {
            show: false,
            color: '#55596e',
        },
        axisTicks: {
            show: false,
            color: '#55596e',
        },
        labels: {
            style: {
                colors: '#f5f7ff',
            },
        },
    },
    yaxis: {
        title: {
            text: 'Doanh thu',
            style: {
                color: '#f5f7ff',
            },
        },
        axisBorder: {
            color: '#55596e',
            show: true,
        },
        axisTicks: {
            color: '#55596e',
            show: true,
        },
        labels: {
            style: {
                colors: '#f5f7ff',
            },
        },
    },
};

const barChart = new ApexCharts(
    document.querySelector('#bar-chart'),
    barChartOptions
);
barChart.render();



// Update chart based on selected year
function updateChart(selectedYear) {
    $.ajax({
        url: '/Admin/LoadBarChart',
        type: 'GET',
        data: { selectedYear: selectedYear },
        dataType: 'json',
        success: function (data) {
            console.log('Received data:', data);

            // Create an array to store revenue data for each month
            const chartData = Array.from({ length: 12 }, (_, index) => {
                const entry = data.find(item => item.Month === (index + 1));
                return entry ? entry.TotalRevenue : 0.000;
            });

            barChartOptions.series = [{
                data: chartData,
                name: 'Doanh thu',
            }];

            // Call barChart.updateOptions(barChartOptions) to update the chart
            barChart.updateOptions(barChartOptions);
        },
        error: function (error) {
            console.error('Error loading data:', error);
        }
    });
}

//// Sử dụng một mảng cố định của 12 tháng
//const months = [
//    'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
//    'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
//];

//function processData(data) {
//    // Tạo mảng mới với dữ liệu của 12 tháng, nếu không có dữ liệu, gán giá trị là 0
//    const processedData = months.map((month, index) => {
//        const entry = data.find(item => item.Month === (index + 1));
//        return {
//            Month: month,
//            TotalRevenue: entry ? parseFloat(entry.TotalRevenue.toFixed(3)) : 0.000
//        };
//    });

//    return processedData;
//}

$(document).ready(function () {
    // Get the current year
    const currentYear = new Date().getFullYear();

    // Set the default text for the current year
    const currentYearText = "Năm nay";

    // Assuming you have a variable 'availableYears' with the available years
    const availableYears = [currentYearText, 2024, 2023, 2022, 2021, 2020, 2019, 2018];

    // Populate the dropdown with years
    const yearDropdown = $('#selected-year');
    availableYears.forEach(year => {
        yearDropdown.append($('<option>', {
            value: year === currentYearText ? currentYear : year,
            text: year,
        }));
    });

    // Set the default selected year to "Năm nay"
    yearDropdown.val(currentYear);

    // Attach an event listener to update the chart when the year changes
    yearDropdown.change(function () {
        const selectedYear = $(this).val();
        updateChart(selectedYear === currentYearText ? currentYear : selectedYear);
    });

    // Trigger the initial chart update with the default selected year
    updateChart(currentYear);
});