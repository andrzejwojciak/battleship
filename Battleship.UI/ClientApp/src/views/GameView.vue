<template>
  <div class="game-view-container">
    <div>
      <div class="battleship-logo">
        <div>BATTLESHIP</div>
      </div>
    </div>
    <div class="game-boards">
      <div>
        <div class="player-name">
          {{ player1 }}
        </div>
        <div>
          <BoardComponent :player-name="player1" />
        </div>
      </div>
      <div>
        <div class="player-name">
          {{ player2 }}
        </div>
        <div>
          <BoardComponent :player-name="player2" />
        </div>
      </div>
    </div>
    <div class="control-panel">
      <div>
        <div>LOGS</div>
      </div>
      <div>
        <div>
          <ButtonsComponent />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BoardComponent from '../components/BoardComponent.vue';
import ButtonsComponent from '../components/ButtonsComponent.vue';

export default {
  name: 'GameView',
  data() {
    return { player1: 'Player1', player2: 'Player2' };
  },
  components: { BoardComponent, ButtonsComponent },
  methods: {},
  computed: {},
};
</script>
<style scoped>
.game-view-container {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: space-evenly;
  flex-direction: column;
}

.battleship-logo > div {
  font-family: 'Ultra', Helvetica, Arial;
  font-size: 4vh;
  color: white;
  background: #c83349;
  border: solid 1px rgb(0, 0, 0);
  padding: 0 10px 0 10px;
  text-shadow: -1px 0 black, 0 1px black, 1px 0 black, 0 -1px black;
  box-shadow: 2px 2px 0 #000000;
  letter-spacing: 0.1em;
}

.player-name {
  text-align: center;
  font-size: 2vh;
}

.game-boards {
  display: flex;
}

.control-panel {
  width: 100%;
  display: flex;
}

.control-panel > div {
  justify-content: center;
  width: 100%;
  display: flex;
  justify-content: center;
}
</style>
