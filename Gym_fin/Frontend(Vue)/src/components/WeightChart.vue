<script setup lang="ts">
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  LineElement,
  PointElement,
  CategoryScale,
  LinearScale, type ChartOptions
} from 'chart.js'
import { Line } from 'vue-chartjs';
import { computed } from 'vue';

ChartJS.register(Title, Tooltip, Legend, LineElement, PointElement, CategoryScale, LinearScale);

interface Props {
  data: {
    date: string;
    weightKg: number;
  }[]
}

const props = defineProps<Props>();

// Prepare data for the chart
const chartData = computed(() => {
  const sorted = [...props.data].sort((a, b) => new Date(a.date).getTime() - new Date(b.date).getTime());

  return {
    labels: sorted.map(item => new Date(item.date).toLocaleDateString()),
    datasets: [
      {
        label: 'Weight (kg)',
        data: sorted.map(item => item.weightKg),
        fill: false,
        borderColor: 'rgb(75, 192, 192)',
        tension: 0.1
      }
    ]
  };
});

const chartOptions: ChartOptions<'line'> = {
  responsive: true,
  plugins: {
    legend: {
      position: 'top' as const  // 👈 explicitly typed
    },
    title: {
      display: true,
      text: 'Weight Over Time'
    }
  },
  scales: {
    y: {
      beginAtZero: true
    }
  }
};
</script>

<template>
  <Line :data="chartData" :options="chartOptions" />
</template>
