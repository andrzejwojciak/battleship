<template>
  <div class="container">
    <button class="start-button" @click="gotoGame()">START GAME</button>
  </div>
</template>

<script>
export default {
  name: 'HomeView',
  data() {
    return {};
  },
  components: {},
  methods: {
    gotoGame() {
      this.$router.replace({ name: 'game' });
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
