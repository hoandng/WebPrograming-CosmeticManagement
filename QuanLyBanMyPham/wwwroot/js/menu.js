// menu.js

// Khởi tạo biểu đồ tròn
const ctx = document.getElementById('orderStatusChart').getContext('2d');
const orderStatusChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: ['Hoàn thành', 'Chờ xử lý'],
        datasets: [{
            data: [completedOrders, pendingOrders],
            backgroundColor: ['#28a745', '#ffc107'],
            hoverOffset: 4
        }]
    },
    options: {
        responsive: true,
        plugins: {
            legend: {
                position: 'bottom'
            }
        }
    }
});
