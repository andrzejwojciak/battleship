<template>
  <div v-if="player1 != null && player2 != null" class="game-view-container">
    <div>
      <div class="battleship-logo">
        <div>BATTLESHIP</div>
      </div>
    </div>
    <div class="game-boards">
      <div>
        <div class="player-name">
          {{ player1.playerName }}
        </div>
        <div>
          <BoardComponent
            :player-name="player1.playerName"
            :ships="player1.ships"
            :key="gameId + player1.playerName"
          />
        </div>
      </div>
      <div>
        <div class="player-name">
          {{ player2.playerName }}
        </div>
        <div>
          <BoardComponent
            :player-name="player2.playerName"
            :ships="player2.ships"
            :key="gameId + player2.playerName"
          />
        </div>
      </div>
    </div>
    <div class="control-panel">
      <div>
        <div>LOGS</div>
      </div>
      <div>
        <div>
          <ButtonsComponent :game-id="gameId" @reset-game="resetGame()" />
        </div>
      </div>
    </div>
  </div>
  <div v-else>
    <LoadingCircle />
  </div>
</template>

<script>
/* eslint-disable prefer-destructuring */
/* eslint-disable no-param-reassign */
import BoardComponent from '../components/BoardComponent.vue';
import ButtonsComponent from '../components/ButtonsComponent.vue';
import LoadingCircle from '../components/LoadingCircle.vue';
import gameService from '../services/gameService';

export default {
  name: 'GameView',
  components: { ButtonsComponent, BoardComponent, LoadingCircle },
  data() {
    return {
      gameId: null,
      player1: null,
      player2: null,
    };
  },
  beforeRouteEnter(routeTo, routeFrom, next) {
    gameService
      .getGameStateById(routeTo.params.gameId)
      .then((res) => {
        next((comp) => {
          comp.gameId = res.data.gameId;
          comp.player1 = res.data.boards[0];
          comp.player2 = res.data.boards[1];
        });
      })
      .catch((err) => {
        console.log(err);
        next({ name: 'not-found' });
      });
  },
  methods: {
    resetGame() {
      gameService
        .createGame('player1', 'player2')
        .then((res) => {
          this.gameId = res.data.gameId;
          this.player1 = res.data.boards[0];
          this.player2 = res.data.boards[1];
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },
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
