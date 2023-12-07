// Associate Overview

// Vertical bar graph  ---region wise status Split
const bar_ver = document.getElementById('bar_vertical').getContext('2d');
const bar_vertical = new Chart(bar_ver, {
    type: 'bar',
    data: {
        labels: ['North', 'West', 'South', 'East', 'Center'],
        datasets: [{
                data: [994, 738, 657, 574, 193],
                backgroundColor: [
                    'rgba(42, 105, 178, 1)',
                    'rgba(42, 105, 178, 0.4)',
                    'rgba(42, 105, 178, 0.4)',
                    'rgba(42, 105, 178, 0.4)',
                    'rgba(42, 105, 178, 0.4)'
                ],
                borderColor: [
                    'rgba(42, 105, 178, 1)',
                    'rgba(42, 105, 178, 0.4)',
                    'rgba(42, 105, 178, 0.4)',
                    'rgba(42, 105, 178, 0.4)',
                    'rgba(42, 105, 178, 0.4)'
                ],
                borderWidth: 1
            },



        ]
    },
    options: {
        plugins: {
            legend: {
                position: "top",
                align: "start",
            }
        },
        responsive: true,
        scales: {
            y: {

                beginAtZero: true,
                ticks: {
                    maxTickLimit: 5,
                    stepSize: 250,
                    callback: function(value, ) {
                        var ranges = [
                            { divider: 1e6, suffix: 'M' },
                            { divider: 1e3, suffix: 'k' }
                        ];

                        function formatNumber(n) {
                            for (var i = 0; i < ranges.length; i++) {
                                if (n >= ranges[i].divider) {
                                    return (n / ranges[i].divider).toString() + ranges[i].suffix;
                                }
                            }
                            return n;
                        }
                        return formatNumber(value);

                    }
                },
                grid: {
                    display: true,
                    drawBorder: false,
                },
            },
            x: {

                grid: {
                    display: false,
                    drawBorder: false,
                },
            }
        },

        plugins: {
            legend: {
                display: false,
            },
            datalabels: {
                anchor: 'center',
                align: 'center',
                color: '#fff',
            },


        }

    },
   //plugins: [ChartDataLabels]

});
// Horziontal bar graph  ---region wise  Split
const bar_hor = document.getElementById('bar_horizontal').getContext('2d');
const bar_horizontal = new Chart(bar_hor, {
    type: 'bar',
    data: {
        labels: ['North', 'West', 'South', 'East', 'Center'],
        datasets: [{
            label: 'Live',
            data: [880, 700, 592, 515, 382],
            backgroundColor: [
                'rgba(42, 105, 178, 1)',
            ],
            borderColor: [
                'rgba(42, 105, 178, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'region',
            data: [114, 200, 364, 258, 200],
            backgroundColor: [
                'rgba(239, 124, 24, 1)',
            ],
            borderColor: [
                'rgba(239, 124, 24, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'Absconded',
            data: [113, 105, 288, 210, 120],
            backgroundColor: [
                'rgba(241, 0, 150, 1)',
            ],
            borderColor: [
                'rgba(241, 0, 150, 1)',
            ],
            borderWidth: 1
        }]
    },
    options: {
        legend: {
            display: true,
        },
        responsive: true,
        indexAxis: 'y',
        scales: {
            y: {
                stacked: true,
                beginAtZero: true,
                grid: {
                    display: true,
                    drawBorder: false,
                },
            },
            x: {
                stacked: true,
                grid: {
                    display: false,
                    drawBorder: false,
                },
            }
        },

        plugins: {
            datalabels: {
                anchor: 'center',
                align: 'left',
                color: '#fff',
            },


        }

    },
   // plugins: [ChartDataLabels]

});
// Stacked bar graph  ---Designation   Split as per region
const stack_bar = document.getElementById('stacked_bar').getContext('2d');
const stacked_bar = new Chart(stack_bar, {
    type: 'bar',
    data: {
        labels: ['North', 'West', 'South', 'East', 'Center'],
        datasets: [{
            label: 'SSM',
            data: [325, 0, 233, 247, 0],
            backgroundColor: [
                'rgba(3, 169,244 , 1)',
            ],
            borderColor: [
                'rgba(3, 169,244 , 1)',
            ],
            borderWidth: 1
        }, {
            label: 'FOS',
            data: [187, 214, 149, 60, 124],
            backgroundColor: [
                
                'rgba(94, 53, 177, 1)',
            ],
            borderColor: [
                'rgba(94, 53, 177, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'SSR',
            data: [99, 45, 55, 83, 10],
            backgroundColor: [
                'rgba(241, 0, 150, 1)',
            ],
            borderColor: [
                'rgba(241, 0, 150, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'PSR',
            data: [109, 62, 3, 60, 7],
            backgroundColor: [
                'rgba(246, 109, 0, 1)',
            ],
            borderColor: [
                'rgba(246, 109, 0, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'DSR',
            data: [0, 158, 1, 1, 0],
            backgroundColor: [
                'rgba(255, 168, 0, 1)',
            ],
            borderColor: [
                'rgba(246, 168, 0, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'Beauty Advisor',
            data: [63, 27, 42, 42, 4],
            backgroundColor: [
                'rgba(255, 143, 0, 1)',
            ],
            borderColor: [
                'rgba(255, 143, 0, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'Sales Represent',
            data: [33, 34, 12, 12, 0],
            backgroundColor: [
                'rgba(255, 143, 0, 1)',
            ],
            borderColor: [
                'rgba(255, 143, 0, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'SSM Foods',
            data: [70, 0, 0, 16, 0],
            backgroundColor: [
                'rgba(255,112,67, 1)',
            ],
            borderColor: [
                'rgba(255,112,67, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'ISR',
            data: [0, 31, 23, 2, 7],
            backgroundColor: [
                'rgba(236, 64, 122, 1)',
            ],
            borderColor: [
                'rgba(236, 64, 122, 1)',
            ],
            borderWidth: 1
        }, {
            label: 'ISP',
            data: [10, 43, 4, 0, 20],
            backgroundColor: [
                'rgba(255,112,67, 1)',
            ],
            borderColor: [
                'rgba(255,112,67,1)',
            ],
            borderWidth: 1
        }]
    },
    options: {
        legend: {
            display: true,
        },
        responsive: true,
        scales: {
            y: {
                stacked: true,
                beginAtZero: true,
                ticks: {
                    maxTickLimit: 5,
                    stepSize: 250,
                    callback: function(value, ) {
                        var ranges = [
                            { divider: 1e6, suffix: 'M' },
                            { divider: 1e3, suffix: 'k' }
                        ];

                        function formatNumber(n) {
                            for (var i = 0; i < ranges.length; i++) {
                                if (n >= ranges[i].divider) {
                                    return (n / ranges[i].divider).toString() + ranges[i].suffix;
                                }
                            }
                            return n;
                        }
                        return formatNumber(value);
                    }
                },
                grid: {
                    display: true,
                    drawBorder: false,
                },
            },
            x: {
                stacked: true,
                grid: {
                    display: false,
                    drawBorder: false,
                },
            }
        },

        plugins: {
            datalabels: {

                // anchor: 'center',
                // align: 'right',
                color: '#fff',
            },


        }

    },
   // plugins: [ChartDataLabels]

});
// doughnut graph -- hire details
var data = [{
    data: [88.00, 11.00],
    backgroundColor: [
        'rgba(7, 93, 191, 1)',
        'rgba(239, 124, 24, 1)',
    ],
    borderColor: [
        'rgba(7, 93, 191, 1)',
        'rgba(239, 124, 24, 1)',
    ],
    borderWidth: 1
}];
var options = {
    responsive: true,
    scales: {
        y: {
            display: false,
            beginAtZero: true,
            grid: {
                display: false,
                drawBorder: false,
            },
        },
        x: {
            display: false,
            grid: {
                display: false,
                drawBorder: false,
            },
        }
    },

    plugins: {
        legend: {
            position: 'right',

            display: true,
            labels: {
                usePointStyle: true,
                pointStyle: 'circle',
                boxWidth: 10,
            },
        },
        datalabels: {
            formatter: (value, ctx) => {

                let datasets = ctx.chart.data.datasets;

                if (datasets.indexOf(ctx.dataset) === datasets.length - 1) {
                    let sum = datasets[0].data.reduce((a, b) => a + b, 0);
                    let percentage = Math.round((value / sum) * 100) + '%';
                    return percentage;
                } else {
                    return percentage;
                }
            },
            anchor: 'center',
            align: 'center',
            color: '#fff',
        },



    }

};
const dou_nut = document.getElementById('doughnut').getContext('2d');
const doughnut = new Chart(dou_nut, {
    type: 'doughnut',
    data: {
        labels: ['Transfer', 'Source'],
        datasets: data
    },
    options: options,
    //plugins: [ChartDataLabels]

});