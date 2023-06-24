<template>
    <div style="margin: 100px 20px; display: flex; justify-content: center; flex-direction: column;">
        <template v-if="data">
            <h1>{{ data.emriAnalizes }}</h1>
            <NuxtLink to="/dashboard/profile" @click="goBack" class="btn btn-success" style="width: 100px;">Go Back
            </NuxtLink>
            <table class="table">
                <thead>
                    <tr>
                        <th>Emri Llojit Te Analizes</th>
                        <th>Rezultati</th>
                        <th>Vlerat Referente</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="rezultati in data.listaRezultateve" :key="rezultati.terapiaAnalizaRezultatiId">
                        <td>{{ rezultati.emriLlojitTeAnalizes }}</td>
                        <td>{{ rezultati.rezultati }}</td>
                        <td>{{ rezultati.vleratReferente }}</td>
                    </tr>
                </tbody>
            </table>
        </template>
        <template v-else>
            <p>Loading...</p>
        </template>
    </div>
</template>
  
<script>
import axios from 'axios';

export default {
    data() {
        return {
            data: null
        };
    },
    methods: {
        goBack() {
            // Implement your logic here
            // For example, you can use Vue Router to navigate back
          
        }
    },
    async mounted() {
        try {
            const analizaData = JSON.parse(localStorage.getItem('analizaData'));
            const { analizaId, terapiaId } = analizaData;
            console.log(analizaId, terapiaId);
            const response = await axios.get(`https://localhost:7292/api/TerapiaAPI/GetAnalizenETerapise/${analizaId}/${terapiaId}`);

            this.data = response.data;
            console.log(this.data);
        } catch (error) {
            console.error(error);
        }
    }
};
</script>
  