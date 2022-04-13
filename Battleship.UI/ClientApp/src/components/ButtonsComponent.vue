<template>
  <div class="buttons-container">
    <div>
      <div>
        <button
          :disabled="gameIsOver"
          class="ship-button inform"
          @click="$emit('nextMove')"
        ></button>
      </div>
      <div class="button-label">NEXT MOVE</div>
    </div>

    <div>
      <div>
        <button class="ship-button danger" @click="$emit('resetGame')"></button>
      </div>
      <div class="button-label">RESET GAME</div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['gameId', 'gameIsOver'],
  name: 'ButtonsComponent',
  methods: {},
};
</script>
<style scoped>
.buttons-container {
  display: flex;
}

.buttons-container > div {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.ship-button {
  color: white;
  border-radius: 100px;
  padding: 30px;
  border: none;
  box-shadow: 0 9px rgb(204, 204, 204);
  cursor: pointer;
}

.ship-button:active {
  box-shadow: 0 5px #666;
  transform: translateY(4px);
}

.inform {
  background: #1bbd6c;
}

.danger {
  background: #e06377;
}

.button-label {
  margin: 15px 10px 0 10px;
  padding: 10px;
}
</style>
