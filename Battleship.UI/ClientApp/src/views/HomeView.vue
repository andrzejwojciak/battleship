<template>
  <div class="container">
    <div v-if="!isLoading">
      <button class="start-button" @click="gotoGame()">START GAME</button>
    </div>
    <div v-else>
      <LoadingCircle :is-active="isLoading" />
    </div>
  </div>
</template>

<script>
import gameService from '../services/gameService';
import LoadingCircle from '../components/LoadingCircle.vue';

export default {
  name: 'HomeView',
  data() {
    return { isLoading: false };
  },
  components: { LoadingCircle },
  methods: {
    gotoGame() {
      this.isLoading = true;

      gameService
        .createGame('player1', 'player2')
        .then((res) => {
          this.$router.push({ name: 'game', params: { gameId: res.data.gameId } });
        })
        .catch((err) => {
          console.log(err);
        })
        .finally(() => {
          this.isLoading = false;
        });
    },
  },
  computed: {},
};
</script>
<style scoped>
.container {
  height: 100%;
  width: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}

.start-button {
  font-size: 5vh;
  font-weight: 550;
  background: linear-gradient(180deg, rgb(226, 0, 0) 13%, rgb(182, 0, 0) 59%);
  color: white;
  border-radius: 60px;
  border: 1vh solid #f50707;
  padding: 30px;
}
</style>
